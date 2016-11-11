using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWayOut.Components
{
    partial class Arrow : GameObject
    {
        const double ARROW_TIME = 2.5;

        const int WIDTH = 55;

        const int HEIGHT = 35;

        public int Target;

        public int Damage;

        double timer;

        Direction direction;

        //Arrow Constructor        
        public Arrow(int d, Texture2D t, int x, int y) : base(x, y, WIDTH, HEIGHT)
        {
            timer = ARROW_TIME;

            Damage = d;

            Texture = t;
        }
    }
}
