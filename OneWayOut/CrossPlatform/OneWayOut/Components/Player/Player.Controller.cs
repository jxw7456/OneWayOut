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
                Position.Y -= 5;
            }
            if (kbState.IsKeyDown(Keys.Down))
            {
                direction = Direction.DOWN;
                Position.Y += 5;
            }
            if (kbState.IsKeyDown(Keys.Left))
            {
                direction = Direction.LEFT;
                Position.X -= 5;
            }
            if (kbState.IsKeyDown(Keys.Right))
            {
                direction = Direction.RIGHT;
                Position.X += 5;
            }
        }


        /// <summary>
        /// Sets the position to center of screen.
        /// </summary>
        /// <param name="graphicsDevice">Graphic device.</param>
        public new void SetPositionCenter(GraphicsDevice graphicsDevice)
        {
            int screenWidth = graphicsDevice.Viewport.Width;

            int screenHeight = graphicsDevice.Viewport.Height;

            int screenCenterX = (screenWidth - PLAYER_SIZE) / 2;

            int screenCenterY = (screenHeight - PLAYER_SIZE) / 2;

            SetPosition(screenCenterX, screenCenterY);
        }

    }
}
