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
        int timer;

        int damage;

        const int WIDTH = 80;
        const int HEIGHT = 60;

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

        public void Draw(SpriteBatch spriteBatch, Direction playerDirection)
        {
            if (!IsActive)
            {
                return;
            }

            switch (playerDirection)
            {
                case Direction.LEFT:
                    spriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                    break;
                case Direction.RIGHT:
                    spriteBatch.Draw(texture, position, Color.White);
                    break;
                case Direction.UP:
                    break;
                case Direction.DOWN:
                    break;
                case Direction.IDLE:
                    break;
                default:
                    break;
            }
        }
    }
}
