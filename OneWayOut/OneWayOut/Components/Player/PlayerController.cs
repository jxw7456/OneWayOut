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
    enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
        IDLE
    }

    /// <summary>
    /// Object describing a character
    /// </summary>
    partial class Player
    {
        public string name { get; set; }
        public int arrowSupply { get; set; }
        public int health { get; set; }
        public Rectangle archerlocal = new Rectangle(0, 0, 100, 100);
        public Direction direction;

        public Player()
        {

        }
        public void Move()
        {
            KeyboardState kbState = Keyboard.GetState();
            direction = Direction.IDLE;
            if (kbState.IsKeyDown(Keys.Up))
            {
                direction = Direction.UP;
                archerlocal.Y -= 3;
            }
            if (kbState.IsKeyDown(Keys.Down))
            {
                direction = Direction.DOWN;
                archerlocal.Y += 3;
            }
            if (kbState.IsKeyDown(Keys.Left))
            {
                direction = Direction.LEFT;
                archerlocal.X -= 3;
            }
            if (kbState.IsKeyDown(Keys.Right))
            {
                direction = Direction.RIGHT;
                archerlocal.X += 3;
            }
        }
    }
}
