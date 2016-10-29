using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OneWayOut;
using Microsoft.Xna.Framework.Content;
using OneWayOut.Components;

namespace OneWayOut
{
    class Highscore
    {
        List<string> entireFile = new List<string>();
        int score;
        SpriteFont font;

        Vector2 pos = new Vector2(600, 180);
        Vector2 pos1 = new Vector2(600, 210);
        Vector2 pos2 = new Vector2(600, 240);
        Vector2 pos3 = new Vector2(600, 270);
        Vector2 gameScore = new Vector2(240, 0);
        Vector2 yourScore = new Vector2(50, 180);

        public string line;

        public Highscore(ContentManager Content)
        {
            font = Content.Load<SpriteFont>(@"fonts/bold");
        }

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

        public void getScore(int giveMe)
        {
            score = giveMe;
        }

        public void DrawScore(SpriteBatch sb)
        {

            sb.DrawString(font, entireFile[0], pos, Color.Red);

            sb.DrawString(font, entireFile[1], pos1, Color.Red);

            sb.DrawString(font, entireFile[2], pos2, Color.Red);

            sb.DrawString(font, entireFile[3], pos3, Color.Red);

            sb.DrawString(font, "your score was:\n" + score.ToString(), yourScore, Color.Red);

        }

        public void DrawScore(SpriteBatch sBatch, Player mc)
        {
            sBatch.DrawString(font, mc.Score.ToString(), gameScore, Color.White);
        }

        public void CheckScore()
        {
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
