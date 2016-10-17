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
                    line = highScore.ReadToEnd();
                    highScore.Close();
                }
            }
            catch (Exception e)
            {

            }
        }

        public void DrawScore(SpriteBatch sb)
        {
            sb.DrawString(font, line, pos, Color.Red);
        }
    }
}
