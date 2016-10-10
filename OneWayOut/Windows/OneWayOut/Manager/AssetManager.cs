using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework.Content;

using OneWayOut.Components.Dungeon;

namespace OneWayOut.Manager
{
    class AssetManager
    {
        Random random;

        public Dungeon dungeon;

        public AssetManager(ContentManager Content)
        {
            random = new Random();

            dungeon = new Dungeon(Content);

            dungeon.GenerateTiles(random);
        }
    }
}
