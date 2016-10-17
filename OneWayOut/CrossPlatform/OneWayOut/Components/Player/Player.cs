using System;
using OneWayOut.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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

        public int score;

		//slow down animation
		internal float timer;

		public string name { get; set; }

		public Arrow.Arrow arrow { get; set; }

        public int arrowSupply;

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
            arrowSupply = 10;
            timer = 0;
			blink = 0;
			row = r;
			column = c;
            IsActive = true;
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

        public void PlayerShoot(KeyboardState kbState)
        {
            if (kbState.IsKeyDown(Keys.Space) == true)
            {
                //arrow = new Arrow.Arrow(100, @"textures/arrow" , this.position.X + 50, this.position.Y + 10, 10, 10);
                arrow.IsActive = true;
                while (arrow.IsActive)
                {
                    arrow.position = new Rectangle(arrow.position.X + 10, arrow.position.Y, arrow.position.Width, arrow.position.Height);
                    if (arrow.position.X > 300)
                    {
                        arrow.IsActive = false;
                    }
                }
            }
        }

        public int ArrowCount
        {
            get { return arrowSupply; }
            set { arrowSupply = value; }
        }

        public void UseArrow()
        {
            if (arrowSupply > 0)
            {
                arrowSupply--;
            }
        }

        public void GainArrow()
        {
            arrowSupply += 5;
        }
    }
}

