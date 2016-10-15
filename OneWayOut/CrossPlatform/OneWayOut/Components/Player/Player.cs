using System;
using OneWayOut.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OneWayOut.Components.Player
{
	/// <summary>
	/// Player Main Class.
	/// Fields and Constructor
	/// </summary>
	partial class Player : GameObject
	{
		//A single sprite's width and height
		const int PLAYER_TEXTURE_SIZE = 512;

		const int PLAYER_SIZE = 90;

		private int currentFrame;

		public int blink;

		public int row;

		public int column;

		//slow down animation
		internal float timer;

		public string name { get; set; }

		public int arrowSupply { get; set; }

		public int health { get; set; }

		public Direction direction;

		private int millisecondsPerFrame = 100;

		/// <summary>
		/// Initializes a new instance of the <see cref="OneWayOut.Components.Player.Player"/> class.
		/// </summary>
		/// <param name="t">T.</param>
		/// <param name="r">The red component.</param>
		/// <param name="c">C.</param>
		public Player (Texture2D t, int r, int c) : base (new Rectangle (0, 0, PLAYER_SIZE, PLAYER_SIZE))
		{
			texture = t;
			health = 100;
			timer = 0;
			blink = 0;
			row = r;
			column = c;
		}

		/// <summary>
		/// Sets the position to center of screen.
		/// </summary>
		/// <param name="graphicDevice">Graphic device.</param>
		public new void SetPositionCenter (GraphicsDevice graphicDevice)
		{

			int screenWidth = graphicDevice.Viewport.Width;

			int screenHeight = graphicDevice.Viewport.Height;

			SetPosition ((screenWidth - PLAYER_SIZE) / 2, (screenHeight - PLAYER_SIZE) / 2);
		}

	}
}

