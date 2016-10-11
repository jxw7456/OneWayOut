using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OneWayOut.Components.Player
{
	enum Direction
	{
		LEFT,
		RIGHT,
		UP,
		DOWN,
		IDLE
	}

	/// <summary>
	/// Object describing a character
	/// </summary>
	partial class Player:GameObject
	{

		public void Move ()
		{
			KeyboardState kbState = Keyboard.GetState ();
			direction = Direction.IDLE;
			if (kbState.IsKeyDown (Keys.Up)) {
				direction = Direction.UP;
				position.Y -= 3;
			}
			if (kbState.IsKeyDown (Keys.Down)) {
				direction = Direction.DOWN;
				position.Y += 3;
			}
			if (kbState.IsKeyDown (Keys.Left)) {
				direction = Direction.LEFT;
				position.X -= 3;
			}
			if (kbState.IsKeyDown (Keys.Right)) {
				direction = Direction.RIGHT;
				position.X += 3;
			}
		}
	}
}
