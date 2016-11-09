using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OneWayOut;
using Microsoft.Xna.Framework.Content;
using OneWayOut.Components;

//Gets score and compares to highscores
namespace OneWayOut
{
    class Highscore
    {
        List<string> entireFile = new List<string>();
        int score;
        SpriteFont font;

        Vector2 pos = new Vector2(1550, 160);
        Vector2 pos1 = new Vector2(1590, 230);
        Vector2 pos2 = new Vector2(1590, 300);
        Vector2 pos3 = new Vector2(1590, 370);
        Vector2 gameScore = new Vector2(1590, 0);
        Vector2 yourScore = new Vector2(50, 160);

        string line;

        //Load Content
        public Highscore(ContentManager Content)
        {
            font = Content.Load<SpriteFont>(@"fonts/biggerFont");
        }

        //Reads in the score from the game
        public void readScore()
        {
            try
            {
                using (var highScore = new StreamReader("highscore.txt"))
                {
                    while ((line = highScore.ReadLine()) != null)
                    {
                        entireFile.Add(line);
                    }
                }
            }

            catch (Exception e)
            {
                StreamWriter check = new StreamWriter("highscore.txt");
                
                check.WriteLine("Highscore:");
                check.WriteLine("1000");
                check.WriteLine("500");
                check.WriteLine("100");
                check.Close();
                
                using (var highScore = new StreamReader("highscore.txt"))
                {
                    while ((line = highScore.ReadLine()) != null)
                    {
                        entireFile.Add(line);
                    }
                }
            }
        }

        //Gets the score from the game after player dies
        public void getScore(int giveMe)
        {
            score = giveMe;
        }

        //Draws score on game over screen
        public void DrawScore(SpriteBatch sb)
        {
            if (entireFile.Count < 1)
            {
                return;
            }
            
            sb.DrawString(font, entireFile[0], pos, Color.Red);

            sb.DrawString(font, entireFile[1], pos1, Color.Red);

            sb.DrawString(font, entireFile[2], pos2, Color.Red);

            sb.DrawString(font, entireFile[3], pos3, Color.Red);

            sb.DrawString(font, "your score was:\n" + score.ToString(), yourScore, Color.Red);
        }

        //Draws score on game screen
        public void DrawScore(SpriteBatch sBatch, Player mc)
        {
            sBatch.DrawString(font, "Score: " + mc.Score.ToString(), gameScore, Color.Black);
        }

        //Checks if score beats the highscore
        public void CheckScore()
        {
            if (entireFile.Count < 1)
            {
                return;
            }
            
            if (score >= int.Parse(entireFile[1]))
            {
                entireFile[3] = entireFile[2];
                entireFile[2] = entireFile[1];
                entireFile[1] = score.ToString();
            }
            
            else
            {
                if (score >= int.Parse(entireFile[2]))
                {
                    entireFile[3] = entireFile[2];
                    entireFile[2] = score.ToString();
                }
                else
                {
                    if (score >= int.Parse(entireFile[3]))
                    {
                        entireFile[3] = score.ToString();
                    }
                    else
                    {

                    }
                }

            }
            
            StreamWriter high = new StreamWriter("highscore.txt");

            high.WriteLine(entireFile[0]);
            high.WriteLine(entireFile[1]);
            high.WriteLine(entireFile[2]);
            high.WriteLine(entireFile[3]);

            high.Close();
        }
    }
}
