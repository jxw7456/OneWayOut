using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Utils;

namespace Components
{
	public partial class Canvas : GameObject
	{
		const int TILE_SIZE = 27;

		const int CANVAS_OFFSET = 10;

		// n x n
		int height;

		int width;

		public string name;

		Texture2D tileTexture;

		Vector2 namePos;

		List<Tile> tiles;

		Random random;

		Tile clickedTile;

		/// <summary>
		/// Initializes a new instance of the <see cref="OneWayOut.Components.Dungeon.Dungeon"/> class.
		/// </summary>
		/// <param name="Content">Content.</param>
		public Canvas (
			ContentManager Content, 
			GraphicsDevice Graphics, 
			Random r, 
			string n = "IDLE", 
			int h = 4, 
			int w = 4,
			int x = 0,
			int y = 0
		) : base (x, y, w * CANVAS_OFFSET, h * CANVAS_OFFSET)
		{
			height = h;

			width = w;

			name = n;

			random = r;
			
			tileTexture = new Texture2D (Graphics, 1, 1);

			tileTexture.SetData<Color> (new Color[] { Color.White });

			tiles = new List<Tile> ();

			namePos = new Vector2 (
				Position.X, 
				(float)(Position.Y - 25)
			);

			GenerateTiles (Position.X, Position.Y);
		}

	}
}

