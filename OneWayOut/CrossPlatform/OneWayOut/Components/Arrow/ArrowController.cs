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
                    if (this.position.Intersects(slime[i].position))
                    {
                        // TODO: Use property and kill the slime (ONE HIT from arrow KILLS)
                        slime[i].Health -= damage;

                        IsActive = false;
                    }
                }
            }
        }

        //Bullet Movement
        /*
        public void Move(Direction playerDirection)
        {
            if (playerDirection == Direction.RIGHT)
            {
                position.X += 5;
            }

            if (playerDirection == Direction.LEFT)
            {
                position.X -= 5;
            }

            if(position.X >= 480 || position.X <= 0)
            {
                IsActive = false;
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
        */
    }
}
