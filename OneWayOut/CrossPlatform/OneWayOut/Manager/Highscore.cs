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
    // TODO: Rename to ScoreManager.
    class Highscore
    {
        // TODO: Improve this using a different type
        //if i know the type im making it i dont have to use var, i wanted them as strings
        //again not improved but another way of doing it
        List<string> entireFile = new List<string>();
        int score;
        SpriteFont font;

        // TODO: This should be done in the constructor.
        //wont make a diffirence if i did it that way
        Vector2 pos = new Vector2(1550, 160);
        Vector2 pos1 = new Vector2(1590, 230);
        Vector2 pos2 = new Vector2(1590, 300);
        Vector2 pos3 = new Vector2(1590, 370);
        Vector2 gameScore = new Vector2(1590, 0);
        Vector2 yourScore = new Vector2(50, 160);

        // TODO: Refactor this into a local variable. done
        string line;

        public Highscore(ContentManager Content)
        {
            font = Content.Load<SpriteFont>(@"fonts/biggerFont");
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
                // TODO: This could be improved with "using"
                //not improved with "using" thats another way of doing it like json
                StreamWriter check = new StreamWriter("highscore.txt");
                // TODO: This could be improved. Think through why each line should
                // be write.
                //got error when implemented your way
                // Extra credit if refactor into binary read/write
                //binary or normal makes no diffrence
                check.WriteLine("Highscore:");
                check.WriteLine("1000");
                check.WriteLine("500");
                check.WriteLine("100");
                check.Close();
                // TODO: This could be improved by a concept we talked in 
                //recursion is another way of doing it but it works fine this way as well
                // class. hint: "recur"
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
            if (entireFile.Count < 1)
            {
                return;
            }
            // TODO: This could be improved such that all score are draw
            // without doing manually each line.
            //if you dont like me using bool statements in game1 or hard coding it like this i will continuesly keep rewriting it and cause it not to show up correctly
            // Also it should be left for ForegroundTextManager to handle
            sb.DrawString(font, entireFile[0], pos, Color.Red);

            sb.DrawString(font, entireFile[1], pos1, Color.Red);

            sb.DrawString(font, entireFile[2], pos2, Color.Red);

            sb.DrawString(font, entireFile[3], pos3, Color.Red);

            sb.DrawString(font, "your score was:\n" + score.ToString(), yourScore, Color.Red);
        }

        public void DrawScore(SpriteBatch sBatch, Player mc)
        {
            sBatch.DrawString(font, "Score: " + mc.Score.ToString(), gameScore, Color.Black);
        }

        public void CheckScore()
        {
            if (entireFile.Count < 1)
            {
                return;
            }
            // TODO: This could be improved by int comparission
            // string parsing at the end could be error prone.
            //doesent have to be int comparison i could make it a Tryparse and it would do the same but sense i know the file there will be no error with the parsing.
            if (score >= int.Parse(entireFile[1]))
            {
                entireFile[3] = entireFile[2];
                entireFile[2] = entireFile[1];
                entireFile[1] = score.ToString();
            }
            // TODO: This nested if/else could be improved with a loop.
            //not "improved" just a diffirent way of doing it
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
            // TODO: This could be improved with a loop and using
            //i had it that way originally but caused an error
            //i write code a certain way i shouldent have to explain to you why my code is written how it is for every line.
            StreamWriter high = new StreamWriter("highscore.txt");

            high.WriteLine(entireFile[0]);
            high.WriteLine(entireFile[1]);
            high.WriteLine(entireFile[2]);
            high.WriteLine(entireFile[3]);

            high.Close();
        }
    }
}
