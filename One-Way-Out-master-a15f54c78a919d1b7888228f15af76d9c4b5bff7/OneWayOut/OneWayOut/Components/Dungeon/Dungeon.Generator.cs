using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWayOut.Components.Dungeon
{
    partial class Dungeon : GameObject
    {
        
		const int TILE_SIZE = 100;

        const int DUNGEON_SIZE = 9; // n x n 

        List<GameObject> tiles;

		public void GenerateTiles(Random random)
        {
            for (int i = 0; i < DUNGEON_SIZE; i++)
            {
                for (int j = 0; j < DUNGEON_SIZE; j++)
                {
                    // TODO: Create a Tile Object more specific to randomize itself.
                    var tile = new GameObject(0, 0, 0, 0);
                }
            }
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in tiles)
            {
                tile.Draw(spriteBatch);
            }
        }

    }
}
