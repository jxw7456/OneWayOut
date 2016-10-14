using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWayOut.Components
{
	public class GameObject
	{
		public Texture2D texture;

		public Rectangle position;

		public GameObject (Rectangle pos)
		{
			position = pos;
		}

		public GameObject (int x, int y, int width, int height)
		{
			position = new Rectangle (x, y, width, height);
		}

		public void SetPosition (int x, int y)
		{
			position.X = x;
			position.Y = y;
		}

		public void SetPositionCenter (GraphicsDevice graphicDevice)
		{

			int screenWidth = graphicDevice.Viewport.Width;

			int screenHeight = graphicDevice.Viewport.Height;

			SetPosition ((screenWidth - texture.Width) / 2, (screenHeight - texture.Height) / 2);
		}

		public void Draw (SpriteBatch spriteBatch)
		{
			spriteBatch.Draw (texture, position, Color.White);
		}

	}
}
