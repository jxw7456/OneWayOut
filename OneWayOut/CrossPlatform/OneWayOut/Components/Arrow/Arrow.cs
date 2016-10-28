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
        public double timer;

        public int damage;

        const int WIDTH = 55;
        const int HEIGHT = 35;

        Direction direction;

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
            timer = ARROW_TIME;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;

                switch (direction)
                {
                    case Direction.RIGHT:
                        spriteBatch.Draw(texture, position, Color.White);
                        break;

                    case Direction.LEFT:
                        spriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                        break;
                }                
            }
        }
    }
}
