using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OneWayOut.Components
{
	/// <summary>
	/// This object monitor the arrow's position as well as their general property
	/// </summary>
	partial class Arrow : GameObject
	{
		//Arrow Rectangle
		Rectangle arrowCollision;
		int timer;

		//Arrow Constructor
		public Arrow (int health, Texture2D texture, int x, int y, int width, int height) : base (x, y, width, height)
		{
			IsActive = true;
			timer = 0;
		}

		//Check to see if rectangles intersect with a character object
		public void Collision (Slime slime, Manager.AssetManager enemy)
		{
			if (IsActive == true) {
				for (int i = 0; i < enemy.slimes.Count; i++) {
					if (this.position.Intersects (enemy.slimes [i].position)) {
						// Use property and kill the slime
						enemy.slimes [i].Health = 0;
						if (enemy.slimes [i].Health <= 0) {
							IsActive = false;
						}
					}
				}

				//Subtract the health from the slime enemy

			}
		}

		//Makes the arrow move until it hits something or travels too far. Uses Collision method above
		public void Distance (Slime slime, Manager.AssetManager enemy)
		{
			if (IsActive == true) {
				Collision (slime, enemy); //check for collision
				timer++;

				if (timer > 100) {
					IsActive = false;
				}
			}
		}
	}
}
