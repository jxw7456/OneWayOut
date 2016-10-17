using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Components
{
	public class Canvas
	{
		const int TILE_SIZE = 27;

		const int CANVAS_OFFSET = 10;

		// n x n
		const int CANVAS_HEIGHT = 4;
		const int CANVAS_WIDTH = 5;

		Texture2D tileTexture;

		List<Tile> tiles;

		Random random;

		/// <summary>
		/// Initializes a new instance of the <see cref="OneWayOut.Components.Dungeon.Dungeon"/> class.
		/// </summary>
		/// <param name="Content">Content.</param>
		public Canvas (ContentManager Content, GraphicsDevice Graphics)
		{
			tileTexture = new Texture2D (Graphics, 1, 1);

			tileTexture.SetData<Color> (new Color[] { Color.White });

			tiles = new List<Tile> ();
		}

		Tile clickedTile;

		public byte[][] GenerateBitMap ()
		{
			byte[][] shape = new byte[CANVAS_WIDTH] [];

			for (int i = 0; i < shape.Length; ++i) {

				shape [i] = new byte[CANVAS_HEIGHT];

				var row = shape [i];

				for (int j = 0; j < row.Length; ++j) {
					row [j] = (byte)tiles [i * CANVAS_HEIGHT + j].type;
				}
			}

			return shape;
		}

		public bool Contains (Point p)
		{
			foreach (var tile in tiles) {
				if (tile.position.Contains (p)) {
					clickedTile = tile;

//					int t = (int)tile.type;
//					
//					tile.type = (TileTextureMap)((t + 1) % 3);

					return true;
				}
			}
			return false;
		}

		public void ToggleClickedTile ()
		{
			int t = (int)clickedTile.type;

			clickedTile.type = (TileTextureMap)((t + 1) % 3);
		}

		/// <summary>
		/// Generates the tiles and their position.
		/// </summary>
		/// <param name="random">Random.</param>
		public void GenerateTiles (GraphicsDevice Graphics, Random r)
		{
			random = r;

			int screenWidth = Graphics.Viewport.Width;

			int screenHeight = Graphics.Viewport.Height;

			int widthOffset = (screenWidth - CANVAS_WIDTH * (TILE_SIZE + CANVAS_OFFSET)) / 2;

			int heightOffset = (screenHeight - CANVAS_HEIGHT * (TILE_SIZE + CANVAS_OFFSET)) / 2;

			for (int i = 0; i < CANVAS_WIDTH; i++) {
				for (int j = 0; j < CANVAS_HEIGHT; j++) {
					// TODO: Create a Tile Object more specific to randomize itself.
					var rect = new Rectangle (
						           widthOffset + (TILE_SIZE + CANVAS_OFFSET) * i, 
						           heightOffset + (TILE_SIZE + CANVAS_OFFSET) * j, 
						           TILE_SIZE, 
						           TILE_SIZE);

					var tile = new Tile (rect);

					tile.texture = tileTexture;

					tiles.Add (tile);
				}
			}
		}

		/// <summary>
		/// Draw the dungeon (each and every tiles).
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		public void Draw (SpriteBatch spriteBatch)
		{
			
			foreach (var tile in tiles) {
				if (tile.type.Equals (TileTextureMap.NULL)) {
					continue;
				}
				switch (tile.type) {
				case TileTextureMap.EYE:
					// Simulate a blinking effect...
					if (random.NextDouble () > 0.81) { 
						tile.Draw (spriteBatch);
					}
					break;
				case TileTextureMap.BODY:
				default:
					tile.Draw (spriteBatch);
					break;
				}
			}
		}
	}
}

