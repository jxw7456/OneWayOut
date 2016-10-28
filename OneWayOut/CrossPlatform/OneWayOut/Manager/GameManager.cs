using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using OneWayOut.Components;

namespace OneWayOut.Manager
{
	/// <summary>
	/// Game state enums.
	/// Assigned each state a keyboard enum
	/// </summary>
	[Flags]
	enum GameState
	{
		START = Keys.F1,
		HELP = Keys.F2,
		GAME = Keys.F3,
		OPTIONS = Keys.F4,
		GAMEOVER = Keys.F5,
		PAUSE = Keys.F6
	}

	/// <summary>
	/// Game manager.
	/// In charge of Handling Game State and Helper methods managing game
	/// </summary>
	class GameManager
	{
		public GameState state;

		public static int level;

		/// <summary>
		/// Initialize with state to start
		/// </summary>
		public GameManager ()
		{
			level = 3;
			state = GameState.START;
		}

		/// <summary>
		/// Warp an object so it stays inside the scene
		/// </summary>
		/// <param name="graphicDevice">The scene to limit the object</param>
		/// <param name="theObject">The object to wrap inside the scene</param>
		public void ScreenWrap (GraphicsDevice graphicDevice, GameObject theObject)
		{
			int screenWidth = graphicDevice.Viewport.Width;

			int screenHeight = graphicDevice.Viewport.Height;

			Rectangle objPos = theObject.position;

			if (objPos.Center.X > screenWidth) {
				theObject.SetPosition (-objPos.Width / 2, objPos.Y);
			}

			if (objPos.Center.X < 0) {
				theObject.SetPosition (screenWidth - objPos.Width / 2, objPos.Y);
			}

			if (objPos.Center.Y > screenHeight) {
				theObject.SetPosition (objPos.X, -objPos.Height / 2);
			}

			if (objPos.Center.Y < 0) {
				theObject.SetPosition (objPos.X, screenHeight - objPos.Height / 2);
			}
		}
	}
}
