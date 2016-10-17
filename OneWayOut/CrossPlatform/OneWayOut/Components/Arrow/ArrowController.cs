using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OneWayOut.Components.Slime;

namespace OneWayOut.Components.Arrow
{
    /// <summary>
    /// This object monitor the arrow's position as well as their general property
    /// </summary>
    partial class Arrow : GameObject
    {
        //Arrow Rectangle
        Rectangle arrowCollision;
        int timer;

        //Constructor
        public Arrow(int health, Texture2D texture, int x, int y, int width, int height) : base(x, y, width, height)
        {
            IsActive = true;
            timer = 0;
        }

        //Check to see if rectangles intersect with a character object
        public void Collision(GameObject obj, Slime.Slime enemy)
        {
            if (IsActive == true)
            {
                //Subtract the health from the slime enemy
                if (this.position.Intersects(obj.position))
                {
                    // Use property and kill the slime
                    enemy.health = 0;
                    this.IsActive = false;
                }
            }
        }

        //Makes the arrow move until it hits something or travels too far. Uses Collision method above
        public void Travel(GameObject obj, Slime.Slime enemy)
        {
            if (IsActive == true)
            {
                Collision(obj, enemy); //check for collision
                timer++;

                if (timer > 100)
                {
                    IsActive = false;
                }
            }
        }
    }
}
