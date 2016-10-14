using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWayOut.Components.Dungeon
{
	partial class Dungeon
	{

		const int TILE_SIZE = 45;

		const int DUNGEON_SIZE = 90;
		// n x n

		const string TILE_TEXTURE = @"textures/ground";

		//const string TILE_TEXTURE = @"textures/floor";

		Texture2D tileTexture;

		List<GameObject> tiles;

		public Dungeon (ContentManager Content)
		{
			tileTexture = Content.Load<Texture2D> (TILE_TEXTURE);
			tiles = new List<GameObject> ();
		}

		public void GenerateTiles (Random random)
		{

			for (int i = 0; i < DUNGEON_SIZE; i++) {
				for (int j = 0; j < DUNGEON_SIZE; j++) {
					// TODO: Create a Tile Object more specific to randomize itself.
					var tile = new GameObject (TILE_SIZE * i, TILE_SIZE * j, TILE_SIZE, TILE_SIZE);
					tile.texture = tileTexture;
					tiles.Add (tile);
				}
			}
		}

		public void Draw (SpriteBatch spriteBatch)
		{
			foreach (var tile in tiles) {
				tile.Draw (spriteBatch);
			}
		}

	}
}
