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

using OneWayOut.Components;

namespace OneWayOut.Manager
{
	class AssetManager
	{
		const int RANDOM_SEED = 999;

		const int SLIME_SIZE = 90;

		const int SLIME_COUNT = 9;

		Random random;

		Texture2D slimeTexture;

		MarkovNameGenerator nameGen;

		public List<Slime> slimes;

		public Dungeon dungeon;

		public AssetManager (ContentManager Content, GraphicsDevice Graphics)
		{
			random = new Random ();

			dungeon = new Dungeon (Content);

			dungeon.GenerateTiles (random);

			nameGen = new MarkovNameGenerator ();

			InitSlime (Graphics);

			slimes = new List<Slime> ();

			for (int i = 0; i < SLIME_COUNT; i++) {
				
				AddNewSlime (Graphics);
			}
		}

		void InitSlime (GraphicsDevice Graphics)
		{
			slimeTexture = new Texture2D (Graphics, 1, 1);

			slimeTexture.SetData<Color> (new Color[] { Color.White });
		}

		void AddNewSlime (GraphicsDevice Graphics)
		{
			slimes.Add (MakeNewSlime (Graphics));	
		}

		Slime MakeNewSlime (GraphicsDevice Graphics)
		{
			var vp = Graphics.Viewport;
			
			var s = new	Slime (
				        random.Next (vp.Width), 
				        random.Next (vp.Height), 
				        SLIME_SIZE, 
				        SLIME_SIZE, 
				        Graphics, 
				        random, 
				        nameGen.NextName
			        );

			s.texture = slimeTexture;

			return s;
		}
	}
}
