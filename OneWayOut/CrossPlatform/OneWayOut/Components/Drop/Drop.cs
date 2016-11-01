using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OneWayOut.Components.Drop
{
    partial class Drop : GameObject
    {
        
        Texture2D health;

        Texture2D arrow;

        Rectangle local;

        Random ran = new Random();

        public int healingAmount { get; set; }

        public int random;

        //Constructor 
        public Drop(Texture2D texure, Texture2D texure2, int x, int y, int width, int height) : base(x, y, width, height)
        {
            healingAmount = 10;
            health = texure;
            arrow = texure2;
            local = new Rectangle(x, y, width, height);
        }

        public void PickDrop()
        {
            random = ran.Next(1, 10);
        }

        public void DrawDrop(SpriteBatch sb)
        {
            if (random <= 2)
            {
                sb.Draw(health, local, Color.White);
            }
            if (random >= 3)
            {
                sb.Draw(arrow, local, Color.White);
            }

        }
    }
}
