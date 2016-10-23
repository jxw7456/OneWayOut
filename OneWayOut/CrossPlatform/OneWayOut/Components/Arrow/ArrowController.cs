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
        int timer;

        //Arrow Constructor
        public Arrow(int health, Texture2D texture, int x, int y, int width, int height) : base(x, y, width, height)
        {
            IsActive = true;
            timer = 0;
        }

        //Check to see if rectangles intersect with a character object
        public void Collision(List<Slime> slime, Player archer)
        {
            if (IsActive == true)
            {
                for (int i = 0; i < slime.Count; i++)
                {
                    if (this.position.Intersects(slime[i].position))
                    {
                        // Use property and kill the slime (ONE HIT from arrow KILLS)
                        slime[i].Health = 0;
                        if (slime[i].Health <= 0)
                        {
                            archer.GainArrow();
                            IsActive = false;
                            slime.RemoveAt(i);  //removes the slime that was hit by projectile and gives play 'x' amount of arrows               
                        }
                    }
                }
            }
        }

        //MIGHT DELETE
        //Makes the arrow move until it hits something or travels too far. Uses Collision method above
        public void Distance(List<Slime> slime, Player archer)
        {
            if (IsActive == true)
            {
                Collision(slime, archer); //check for collision
                timer++;

                if (timer > 100)
                {
                    IsActive = false;
                }
            }
        }
    }
}
