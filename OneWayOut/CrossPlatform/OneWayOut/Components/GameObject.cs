using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWayOut.Components
{
    /// <summary>
    /// Game object is a powerful base class that
    /// describes an object with texture, position,
    /// with some helper method for collision handling.
    /// </summary>
    public class GameObject
    {
        public Texture2D Texture;

        public Rectangle Position;

        public bool IsActive
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayOut.Components.GameObject"/> class.
        /// </summary>
        /// <param name="pos">Position.</param>
        public GameObject(Rectangle pos)
        {
            Position = pos;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayOut.Components.GameObject"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public GameObject(int x, int y, int width, int height)
        {
            Position = new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Sets the position of the game object.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public void SetPosition(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }

        /// <summary>
        /// Sets the game object position to center of the screen.
        /// </summary>
        /// <param name="graphicDevice">Graphic device.</param>
        public void SetPositionCenter(GraphicsDevice graphicDevice)
        {
            int screenWidth = graphicDevice.Viewport.Width;

            int screenHeight = graphicDevice.Viewport.Height;

            int screenCenterX = (screenWidth - Texture.Width) / 2;

            int screenCenterY = (screenHeight - Texture.Height) / 2;

            SetPosition(screenCenterX, screenCenterY);
        }

        /// <summary>
        /// Draw the game object.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

    }
}
