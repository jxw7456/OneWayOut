using System;

using Microsoft.Xna.Framework.Graphics;

using Components;

using Microsoft.Xna.Framework;

using System.Threading;

namespace Components.Slime
{
	/// <summary>
	/// Slime main class, contain constructors and fields.
	/// </summary>
	partial class Slime: GameObject
	{
		const int PIXEL_SIZE = 9;

		const int MAX_MOVE_SPEED = 500;

		const int MIN_MOVE_SPEED = 200;

		[Flags]
		enum SlimeDirection
		{
			LEFT = 0x1,
			RIGHT = 0x2,
			UP = 0x4,
			DOWN = 0x8,
		}

		SlimeState state;

		SlimeDirection direction;

		Random random;

		string name;

		int speed;

		Color color;

		//Time elapsed since the last check
		float elapsedTime = 0;

		float slimeDelay;

		/// <summary>
		/// Initializes a new instance of the <see cref="OneWayOut.Components.Slime.Slime"/> class.
		/// And assigning the properties value
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		/// <param name="gp">Graphic </param>
		/// <param name="r">Random object</param>
		/// <param name="n">Asigned name</param>
		public Slime (int x, int y, int width, int height, GraphicsDevice gp, Random r, string n)
			: base (x, y, width, height)
		{
			state = SlimeState.IDLE;

			direction = SlimeDirection.RIGHT;

			random = r;

			speed = random.Next (MIN_MOVE_SPEED, MAX_MOVE_SPEED);

			name = n;

			//name += speed;

			slimeDelay = (float)random.NextDouble () / 2;
			
			color = Color.White;
		}
	}
}

