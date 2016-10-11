using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using OneWayOut.Utils;

namespace OneWayOut.Components.Slime
{
	partial class Slime
	{
		const int MOVE_SPEED = 270;

		[Flags]
		enum SlimeDirection
		{
			LEFT = 0x1,
			RIGHT = 0x2,
			UP = 0x4,
			DOWN = 0x8,
		}

		public void ProcessInput (GameTime gameTime)
		{
			var keyboardState = Keyboard.GetState ();

			var elapsed = gameTime.ElapsedGameTime;

			state = SlimeState.IDLE;


			if (keyboardState.IsKeyDown (Keys.Left)) {
				position.X -= Helper.MovingInterval (elapsed, MOVE_SPEED);
				state = SlimeState.WALK;	
				direction = SlimeDirection.LEFT;
			}

			if (keyboardState.IsKeyDown (Keys.Right)) {
				position.X += Helper.MovingInterval (elapsed, MOVE_SPEED);
				state = SlimeState.WALK;
				direction = SlimeDirection.RIGHT;
			}

			if (keyboardState.IsKeyDown (Keys.Up)) {
				position.Y -= Helper.MovingInterval (elapsed, MOVE_SPEED);
				state = SlimeState.WALK;
				direction |= SlimeDirection.UP;
			}

			if (keyboardState.IsKeyDown (Keys.Down)) {
				position.Y += Helper.MovingInterval (elapsed, MOVE_SPEED);
				state = SlimeState.WALK;
				direction |= SlimeDirection.DOWN;
			}


			if (keyboardState.IsKeyDown (Keys.D)) {
				state = SlimeState.BLOPPED;
			}
		}
	}
}

