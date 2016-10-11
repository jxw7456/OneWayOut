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
using OneWayOut.Components.Slime;

namespace OneWayOut.Manager
{
	class AssetManager
	{


		const int RANDOM_SEED = 999;

		const int SLIME_SIZE = 90;

		Random random;

		Texture2D slimeTexture;

		public Slime slime;

		public Dungeon dungeon;

		public AssetManager (ContentManager Content, GraphicsDevice Graphics)
		{
			random = new Random ();

			dungeon = new Dungeon (Content);

			dungeon.GenerateTiles (random);

			InitSlime (Graphics);
		}

		void InitSlime (GraphicsDevice Graphics)
		{
			slimeTexture = new Texture2D (Graphics, 1, 1);

			slimeTexture.SetData<Color> (new Color[] { Color.White });

			slime = new	Slime (300, 300, SLIME_SIZE, SLIME_SIZE, Graphics, random);

			slime.texture = slimeTexture;

		}

	}
}
