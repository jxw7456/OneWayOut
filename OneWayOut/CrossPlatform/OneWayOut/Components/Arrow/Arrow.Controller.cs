using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OneWayOut.Components
{
    /// <summary>
    /// This object monitor the arrow's position as well as their general property
    /// </summary>
    partial class Arrow : GameObject
    {
        /// <summary>
        /// Check for collsion with the slime list.
        /// </summary>
        /// <param name="slime">Slime.</param>
        public void Collision(List<Slime> slimes)
        {
            if (!IsActive)
                return;

            for (int i = 0; i < slimes.Count; i++)
            {
                var slime = slimes[i];

                if (Position.Intersects(slime.Position))
                {
                    //Use property and kill the slime (ONE HIT from arrow KILLS)
                    slime.Health -= Damage;

                    if (i == Target)
                    {
                        timer = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Draw the arrow.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        /// <param name="gameTime">Game time.</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!IsActive)
                return;
            
            timer -= gameTime.ElapsedGameTime.TotalSeconds;

            //rotate arrow for up and down
            switch (direction)
            {
                case Direction.RIGHT:
                    spriteBatch.Draw(Texture, Position, Color.White);
                    break;

                case Direction.LEFT:
                    spriteBatch.Draw(Texture, Position, null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                    break;
            }
        }

        /// <summary>
        /// inhert from Move Class
        /// </summary>
        /// <param name="obj"></param>
        public void Move(GameObject obj)
        {
            if (!IsActive)
                return;
            
            Point p = new Point(obj.Position.Left, obj.Position.Top);

            Move(p);
        }

        //Arrow Movement
        public void Move(Point end)
        {
            if (!IsActive)
                return;
            
            if (Position.X > end.X)
            {
                //Left
                Position.X -= 5;
                direction = Direction.LEFT;
            }

            if (Position.X < end.X)
            {
                //Right
                Position.X += 5;
                direction = Direction.RIGHT;
            }

            if (Position.Y > end.Y)
            {
                //Up
                Position.Y -= 5;
            }

            if (Position.Y < end.Y)
            {
                //Down
                Position.Y += 5;
            }
        }
    }
}
