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
        //Check to see if rectangles intersect with a character object
        public void Collision(List<Slime> slime)
        {
            if (IsActive == true)
            {
                for (int i = 0; i < slime.Count; i++)
                {
                    if (position.Intersects(slime[i].position))
                    {
                        //Use property and kill the slime (ONE HIT from arrow KILLS)
                        slime[i].Health -= damage;

                        IsActive = false;
                    }
                }
            }
        }

        /// <summary>
        /// inhert from Move Class
        /// </summary>
        /// <param name="obj"></param>
        public void Move(GameObject obj)
        {
            Point p = new Point(obj.position.Left, obj.position.Top);
            Move(p);
        }

        //Arrow Movement        
        public void Move(Point end)
        {
            if (position.X > end.X)
            {
                //WalkLeft(elapsed);
                position.X -= 5;
                direction = Direction.LEFT;
            }

            if (position.X < end.X)
            {
                //WalkRight(elapsed);
                position.X += 5;
                direction = Direction.RIGHT;
            }

            if (position.Y > end.Y)
            {
                //WalkUp(elapsed);
                position.Y -= 5;
            }

            if (position.Y < end.Y)
            {
                //WalkDown(elapsed);
                position.Y += 5;
            }


        }


        public void Travel(List<Slime> slime)//makes the bullet move until it hits something or travels too far. uses collision method as well
        {
            if (IsActive == true)
            {
                Collision(slime); // check for collisions
                timer++;

                if (timer > 250)
                {
                    IsActive = false;
                }
            }
        }
    }
}
