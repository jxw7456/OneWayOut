using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWayOut.Components.Dungeon
{
	/// <summary>
	/// Dungeon Generator Class.
	/// </summary>
	partial class Dungeon
	{

		const int TILE_SIZE = 45;

		// n x n
		const int DUNGEON_SIZE = 90;

		const string TILE_TEXTURE = @"textures/ground";

		//const string TILE_TEXTURE = @"textures/floor";

		Texture2D tileTexture;

		List<GameObject> tiles;

		/// <summary>
		/// Initializes a new instance of the <see cref="OneWayOut.Components.Dungeon.Dungeon"/> class.
		/// </summary>
		/// <param name="Content">Content.</param>
		public Dungeon (ContentManager Content)
		{
			tileTexture = Content.Load<Texture2D> (TILE_TEXTURE);
			tiles = new List<GameObject> ();
		}

		/// <summary>
		/// Generates the tiles and their position.
		/// </summary>
		/// <param name="random">Random.</param>
		public void GenerateTiles (Random random)
		{

			for (int i = 0; i < DUNGEON_SIZE; i++) {
				for (int j = 0; j < DUNGEON_SIZE; j++) {
					// TODO: Create a Tile Object more specific to randomize itself.
					var tile = new GameObject (TILE_SIZE * i, TILE_SIZE * j, TILE_SIZE, TILE_SIZE);
					tile.Texture = tileTexture;
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
				tile.Draw (spriteBatch);
			}
		}

	}
}
