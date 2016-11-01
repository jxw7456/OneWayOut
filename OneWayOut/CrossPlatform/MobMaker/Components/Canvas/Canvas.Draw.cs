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

		public void DrawName (
			SpriteBatch spriteBatch,
			SpriteFont spriteFont
		)
		{
			
			spriteBatch.DrawString (
				spriteFont, 
				name, 
				namePos,
				Color.White
			);
		}

		/// <summary>
		/// Draw the dungeon (each and every tiles).
		/// </summary>
		/// <param name="spriteBatch">Sprite batch.</param>
		public new void Draw (SpriteBatch spriteBatch)
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
				case TileTextureMap.COLORFUL:
					tile.Draw (spriteBatch, ColorGenerator.RandomColor (random));
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

