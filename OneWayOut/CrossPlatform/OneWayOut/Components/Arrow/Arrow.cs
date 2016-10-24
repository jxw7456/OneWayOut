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
        public int timer;

        public int damage;

        const int WIDTH = 55;
        const int HEIGHT = 35;

        //Arrow Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i">Index of this arrow in the list</param>
        /// <param name="d"></param>
        /// <param name="t"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Arrow(int Damage, Texture2D Texture, int x, int y) : base(x, y, WIDTH, HEIGHT)
        {
            damage = Damage;
            texture = Texture;
            IsActive = true;
            timer = 0;
        }
    }
}
