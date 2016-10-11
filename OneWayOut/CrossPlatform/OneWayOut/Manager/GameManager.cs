﻿using System;
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
	enum GameState
	{
		START,
		HELP,
		GAME,
		OPTIONS,
		GAMEOVER,
		PAUSE
	}

	class GameManager
	{
		public GameState state;

		/// <summary>
		/// Initialize with timer to default value, state to menu
		/// </summary>
		public GameManager ()
		{
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
