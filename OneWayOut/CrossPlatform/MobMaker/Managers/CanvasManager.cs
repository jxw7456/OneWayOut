using System;
using Microsoft.Xna.Framework.Graphics;

using Components;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using MobMaker;

namespace Manager
{
	public class CanvasManager
	{

		public Canvas c;

		Random random;

		public CanvasManager (ContentManager Content, GraphicsDevice Graphics)
		{
			random = new Random ();

			c = new Canvas (Content, Graphics);

			c.GenerateTiles (Graphics, random);
		}

		public void Save (string fileName)
		{
			MobFile.Write (fileName, c.GenerateBitMap ());
		}

		public void ToggleTile (int x, int y)
		{
			var mousePosition = new Point (x, y);

			if (c.Contains (mousePosition)) {
				c.ToggleClickedTile ();
			}

		}

		public void Draw (SpriteBatch spriteBatch)
		{
			c.Draw (spriteBatch);
		}
	}
}

