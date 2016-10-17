using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
	/// <summary>
	/// Game object is a powerful base class that
	/// describes an object with texture, position,
	/// with some helper method for collision handling.
	/// </summary>
	public class GameObject
	{
		public Texture2D texture;

		public Rectangle position;

		/// <summary>
		/// Initializes a new instance of the <see cref="OneWayOut.Components.GameObject"/> class.
		/// </summary>
		/// <param name="pos">Position.</param>
		public GameObject (Rectangle pos)
		{
			position = pos;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="OneWayOut.Components.GameObject"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public GameObject (int x, int y, int width, int height)
		{
			position = new Rectangle (x, y, width, height);
		}

		/// <summary>
		/// Sets the position of the game object.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void SetPosition (int x, int y)
		{
			position.X = x;
			position.Y = y;
		}

		/// <summary>
		/// Sets the game object position to center of the screen.
		/// </summary>
		/// <param name="graphicDevice">Graphic device.</param>
		public void SetPositionCenter (GraphicsDevice graphicDevice)
		{
			int screenWidth = graphicDevice.Viewport.Width;

			int screenHeight = graphicDevice.Viewport.Height;

			SetPosition ((screenWidth - texture.Width) / 2, (screenHeight - texture.Height) / 2);
		}

		/// <summary>
		/// Draw the game object.
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		public void Draw (SpriteBatch spriteBatch)
		{
			spriteBatch.Draw (texture, position, Color.White);
		}

	}
}
