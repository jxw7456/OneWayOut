using System;
using Microsoft.Xna.Framework.Graphics;

using Components;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using MobMaker;
using System.Collections.Generic;

namespace Manager
{
	public class CanvasManager
	{

		List<Canvas> canvasList;

		string[] names = { "IDLE", "BLOP", "UP", "RIGHT" };

		const int MARGIN_LEFT = 10;

		const int MARGIN_TOP = 70;

		const string EXTENSION = ".owo";

		Random random;

		int canvasWidth = 5;

		int canvasHeight = 5;

		public CanvasManager (
			ContentManager Content, 
			GraphicsDevice Graphics
		)
		{
			random = new Random ();

			int screenWidth = Graphics.Viewport.Width;

			int screenHeight = Graphics.Viewport.Height;

			canvasList = new List<Canvas> ();

			for (int i = 0; i < names.Length; i++) {
				int canvasX = MARGIN_LEFT + (screenWidth / 2) * ((i) % 2);

				int canvasY = MARGIN_TOP + (screenHeight / 2) * ((i / 2) % 2);

				var c = new Canvas (
					        Content, 
					        Graphics,
					        random, 
					        names [i],
					        canvasWidth, canvasHeight, 
					        canvasX, canvasY
				        );

				canvasList.Add (c);
			}
		}

		public void Scale (int x, int y, GraphicsDevice Graphics)
		{
			int screenWidth = Graphics.Viewport.Width;

			int screenHeight = Graphics.Viewport.Height;

			canvasWidth += x;

			canvasHeight += y;

			for (int i = 0; i < canvasList.Count; i++) {
				var c = canvasList [i];

				int canvasX = MARGIN_LEFT + (screenWidth / 2) * ((i) % 2);

				int canvasY = MARGIN_TOP + (screenHeight / 2) * ((i / 2) % 2);

				c.ChangeDimension (
					canvasWidth, canvasHeight, 
					canvasX, canvasY
				);		
			}	
		}

		public void Load ()
		{
			
		}

		public void Save ()
		{
			for (int i = 0; i < canvasList.Count; i++) {
				var c = canvasList [i];

				MobFile.Write (
					c.name + EXTENSION, 
					c.GenerateBitMap ()
				);
			}
		}

		public void ToggleTile (int x, int y)
		{
			var mousePosition = new Point (x, y);

			for (int i = 0; i < canvasList.Count; i++) {
				var c = canvasList [i];

				if (c.Contains (mousePosition)) {
					c.ToggleClickedTile ();
				}
			}
		}

		public void Draw (SpriteBatch spriteBatch, SpriteFont spriteFont)
		{
			for (int i = 0; i < canvasList.Count; i++) {
				var c = canvasList [i];
				c.DrawName (spriteBatch, spriteFont);
				c.Draw (spriteBatch);
			}
		}
	}
}
