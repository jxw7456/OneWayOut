using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OneWayOut.Components.Player
{
    /// <summary>
    /// Object describing a character
    /// </summary>
    partial class Player
    {
        public string name { get; set; }
        public int arrowSupply { get; set; }
        public int health { get; set; }
        public Rectangle archerlocal = new Rectangle(0, 0, 100, 100);

        public Player()
        {

        }
        public void move()
        {
            KeyboardState kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(Keys.Up))
            {
                archerlocal.Y -= 1;
            }
            if (kbState.IsKeyDown(Keys.Down))
            {
                archerlocal.Y += 1;
            }
            if (kbState.IsKeyDown(Keys.Left))
            {
                archerlocal.X -= 1;
            }
            if (kbState.IsKeyDown(Keys.Right))
            {
                archerlocal.X += 1;
            }

        }

    }
}
