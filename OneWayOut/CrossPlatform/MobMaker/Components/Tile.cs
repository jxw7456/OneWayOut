using System;
using Microsoft.Xna.Framework;

namespace Components
{
	public enum TileTextureMap
	{
		NULL,
		BODY,
		EYE
	}

	public class Tile:GameObject
	{

		public TileTextureMap type;

		public Tile (Rectangle rec) : base (rec)
		{
			type = TileTextureMap.BODY;
		}
	}
}

