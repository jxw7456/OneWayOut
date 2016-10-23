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

namespace OneWayOut
{
    class Highscore
    {
        List<string> entireFile = new List<string>();

        SpriteFont font;

        Vector2 pos = new Vector2(600, 180);
        Vector2 pos1 = new Vector2(600, 210);
        Vector2 pos2 = new Vector2(600, 240);
        Vector2 pos3 = new Vector2(600, 270);

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

            }
        }

        public void DrawScore(SpriteBatch sb)
        {

            sb.DrawString(font, entireFile[0], pos, Color.Red);

            sb.DrawString(font, entireFile[1], pos1, Color.Red);

            sb.DrawString(font, entireFile[2], pos2, Color.Red);

            sb.DrawString(font, entireFile[3], pos3, Color.Red);


        }

        public void CheckScore(int score)
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
