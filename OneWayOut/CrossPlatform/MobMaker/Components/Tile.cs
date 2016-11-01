using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Components
{
	public enum TileTextureMap
	{
		NULL,
		BODY,
		EYE,
		COLORFUL
	}

	public class Tile:GameObject
	{

		public TileTextureMap type;

		public Tile (Rectangle rec) : base (rec)
		{
			type = TileTextureMap.BODY;
		}

		public void Draw (SpriteBatch spriteBatch, Color color)
		{
			spriteBatch.Draw (Texture, Position, color);
		}
	}
}

