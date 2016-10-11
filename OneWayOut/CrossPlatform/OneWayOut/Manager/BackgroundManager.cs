using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace OneWayOut.Manager
{
	public class BackgroundManager
	{
		const string START_BG = @"textures/dungeon";

		const string HELP_BG = @"textures/dungeonHelp";

		const string OPTION_BG = @"textures/dungeonOptions";

		const string GAMEOVER_BG = @"textures/deadArcher";

		Texture2D startBg, optionBg, helpBg, gameoverBg;

		public BackgroundManager (ContentManager Content)
		{
			startBg = Content.Load<Texture2D> (START_BG);

			optionBg = Content.Load<Texture2D> (OPTION_BG);

			helpBg = Content.Load<Texture2D> (HELP_BG);

			gameoverBg = Content.Load<Texture2D> (GAMEOVER_BG);
		}

		public void DrawGameover (SpriteBatch sb, GraphicsDevice graphics)
		{
			DrawBackGround (sb, graphics, gameoverBg);
		}

		public void DrawHelp (SpriteBatch sb, GraphicsDevice graphics)
		{
			DrawBackGround (sb, graphics, helpBg);
		}

		public void DrawOption (SpriteBatch sb, GraphicsDevice graphics)
		{
			DrawBackGround (sb, graphics, optionBg);
		}

		public void DrawStart (SpriteBatch sb, GraphicsDevice graphics)
		{
			DrawBackGround (sb, graphics, startBg);
		}

		void DrawBackGround (SpriteBatch sb, GraphicsDevice graphics, Texture2D bg)
		{
			sb.Draw (bg, graphics.Viewport.Bounds, Color.White);                    
		}
	}
}

