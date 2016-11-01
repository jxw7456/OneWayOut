using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Utils;

namespace Components
{
	public partial class Canvas
	{

		public void ChangeDimension (int w, int h, int x, int y)
		{
			width = w;

			height = h;

			Position.X = x;
			
			Position.Y = y;
			
			tiles.Clear ();

			namePos = new Vector2 (
				Position.X, 
				(float)(Position.Y - 25)
			);
			GenerateTiles (Position.X, Position.Y);
		}

		public bool Contains (Point p)
		{
			foreach (var tile in tiles) {
				if (tile.Position.Contains (p)) {
					clickedTile = tile;
					return true;
				}
			}
			return false;
		}

		public void ToggleClickedTile ()
		{
			int t = (int)clickedTile.type;

			clickedTile.type = (TileTextureMap)((t + 1) % 4);
		}

	}
}

