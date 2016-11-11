using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OneWayOut.Components.Drop
{
    partial class Drop : GameObject
    {
        Texture2D health;

        Texture2D arrow;

        Rectangle local;

        Random ran = new Random();

        public List<Drop> allItems;

        public int healingAmount { get; set; }

        public int random;

        public bool checkIt, dropIt, drawIt;

        //Constructor 
        public Drop(Texture2D texure, Texture2D texure2, int x, int y, int width, int height) : base(x, y, width, height)
        {
            healingAmount = 10;
            health = texure;
            arrow = texure2;
            local = new Rectangle(x, y, width, height);
            checkIt = false;
            dropIt = false;
            drawIt = false;
            allItems = new List<Drop>();
        }

        //Picks random drop between arrow or health
        public void PickDrop()
        {
            random = ran.Next(1, 10);
        }

        //Draws health or arrow depending on random
        public void DrawDrop(SpriteBatch sb)
        {
            if (random <= 2)
            {
                sb.Draw(health, local, Color.White);
            }
            if (random >= 3)
            {
                sb.Draw(arrow, local, Color.White);
            }

        }

        //Intersects with the player
        public void Intersection(bool dropIt, Player player, List<Drop> allItems, Drop item, int i)
        {
            if (dropIt && player.Position.Intersects(allItems[i].Position))
            {
                item = allItems[i];
                if (item.random >= 3)
                {
                    player.GainArrow();
                    allItems.Remove(allItems[i]);

                }

                else if (item.random < 2)
                {
                    if (player.Health == 100)
                    {
                        allItems.Remove(allItems[i]);

                    }
                    else if (player.Health >= 90)
                    {
                        allItems.Remove(allItems[i]);

                        player.Health = 100;
                    }
                    else
                    {
                        allItems.Remove(allItems[i]);

                        player.Health += 10;
                    }

                }

                else
                {
                    if (player.Health == 100)
                    {
                        allItems.Remove(allItems[i]);

                    }
                    else if (player.Health >= 90)
                    {
                        allItems.Remove(allItems[i]);

                        player.Health = 100;
                    }
                    else
                    {
                        allItems.Remove(allItems[i]);

                        player.Health += 10;
                    }
                }
            }
        }
    }
}
