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
    partial class ArrowC : GameObject
    {
        //Arrow Rectangle
        Rectangle arrowCollision;
        int timer;

        //Arrow Constructor
        public ArrowC(int health, Texture2D texture, int x, int y, int width, int height) : base(x, y, width, height)
        {
            IsActive = false;
            timer = 0;
        }

        //Check to see if rectangles intersect with a character object
        public void Collision(Slime slime, List<Slime> enemy)
        {
            if (IsActive == true)
            {
                for (int i = 0; i < enemy.Count; i++)
                {
                    if (this.position.Intersects(enemy[i].position))
                    {
                        // Use property and kill the slime
                        enemy[i].Health = 0;
                        if (enemy[i].Health <= 0)
                        {
                            enemy[i].IsActive = false;
                        }
                    }
                }
            }
        }

        //Makes the arrow move until it hits something or travels too far. Uses Collision method above
        public void Distance(Slime slime, List<Slime> enemy)
        {
            if (IsActive == true)
            {
                Collision(slime, enemy); //check for collision
                timer++;

                if (timer > 100)
                {
                    IsActive = false;
                }
            }
        }
    }
}
