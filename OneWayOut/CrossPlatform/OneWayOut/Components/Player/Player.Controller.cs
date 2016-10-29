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
    enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
        IDLE
    }

    /// <summary>
    /// In charge of controlling the user
    /// </summary>
    partial class Player : GameObject
    {
        /// <summary>
        /// Moving Handler
        /// </summary>
        public void Move()
        {
            KeyboardState kbState = Keyboard.GetState();
            direction = Direction.IDLE;
            if (kbState.IsKeyDown(Keys.Up))
            {
                direction = Direction.UP;
                Position.Y -= 3;
            }
            if (kbState.IsKeyDown(Keys.Down))
            {
                direction = Direction.DOWN;
                Position.Y += 3;
            }
            if (kbState.IsKeyDown(Keys.Left))
            {
                direction = Direction.LEFT;
                Position.X -= 3;
            }
            if (kbState.IsKeyDown(Keys.Right))
            {
                direction = Direction.RIGHT;
                Position.X += 3;
            }
        }
    }
}
