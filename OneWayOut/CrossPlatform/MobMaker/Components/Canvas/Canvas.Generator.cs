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

		public byte[][] GenerateBitMap ()
		{
			byte[][] shape = new byte[width] [];

			for (int i = 0; i < shape.Length; ++i) {

				shape [i] = new byte[height];

				var row = shape [i];

				for (int j = 0; j < row.Length; ++j) {
					row [j] = (byte)(int)tiles [i * height + (row.Length - j - 1)].type;
				}
			}

			return shape;
		}


		public void GenerateTiles (int heightOffset, int widthOffset)
		{
			for (int i = 0; i < width; i++) {
				for (int j = 0; j < height; j++) {
					// TODO: Create a Tile Object more specific to randomize itself.
					var rect = new Rectangle (
						           heightOffset + (TILE_SIZE + CANVAS_OFFSET) * j, 
						           widthOffset + (TILE_SIZE + CANVAS_OFFSET) * i, 
						           TILE_SIZE, 
						           TILE_SIZE);

					var tile = new Tile (rect);

					tile.Texture = tileTexture;

					tiles.Add (tile);
				}
			}
		}

		/// <summary>
		/// Generates the tiles and their position.
		/// </summary>
		/// <param name="random">Random.</param>
		public void GenerateCenterTiles (GraphicsDevice Graphics)
		{
			int screenWidth = Graphics.Viewport.Width;

			int screenHeight = Graphics.Viewport.Height;

			int widthOffset = (screenWidth - width * (TILE_SIZE + CANVAS_OFFSET)) / 2;

			int heightOffset = (screenHeight - height * (TILE_SIZE + CANVAS_OFFSET)) / 2;

			GenerateTiles (widthOffset, heightOffset);
		}

	}
}

