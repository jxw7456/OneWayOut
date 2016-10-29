using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using OneWayOut.Utils;

namespace OneWayOut.Components
{
    /// <summary>
    /// Slime Behavior Helper.
    /// </summary>
    partial class Slime
    {
        /// <summary>
        /// Compares the name to a random string.
        /// </summary>
        /// <returns><c>true</c>, if name match, <c>false</c> otherwise.</returns>
        /// <param name="str">A string.</param>
        public bool CompareName(string str)
        {
            return upperCaseName.Equals(str);
        }

        /// <summary>
        /// Attack a User
        /// </summary>
        /// <param name="player">Player.</param>
        public void Attack(Player player)
        {
            if (Position.Intersects(player.Position))
            {
                // Use property and kill the slime (ONE HIT from arrow KILLS)
                player.Health -= Damage;
            }
        }

        /// <summary>
        /// Chase the specified obj using gameTime to calculate speed.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="gameTime">Game time.</param>
        public void Chase(GameObject obj, GameTime gameTime)
        {
            ChaseZiggly(obj, gameTime);
        }

        /// <summary>
        /// Chases straight behavior.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="gameTime">Game time.</param>
        public void ChaseStraight(GameObject obj, GameTime gameTime)
        {
            var elapsed = gameTime.ElapsedGameTime;

            state = SlimeState.IDLE;

            if (Position.X > obj.Position.X)
            {
                WalkLeft(elapsed);
            }

            if (Position.X < obj.Position.X)
            {
                WalkRight(elapsed);
            }

            if (Position.Y > obj.Position.Y)
            {
                WalkUp(elapsed);
            }

            if (Position.Y < obj.Position.Y)
            {
                WalkDown(elapsed);
            }
        }

        /// <summary>
        /// Chases uniformly.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="gameTime">Game time.</param>
        public void ChaseUniformly(GameObject obj, GameTime gameTime)
        {
            var elapsed = gameTime.ElapsedGameTime;

            state = SlimeState.IDLE;

            if (Position.X > obj.Position.X)
            {
                WalkLeft(elapsed);
            }
            else if (Position.X < obj.Position.X)
            {
                WalkRight(elapsed);
            }
            else if (Position.Y > obj.Position.Y)
            {
                WalkUp(elapsed);
            }
            else if (Position.Y < obj.Position.Y)
            {
                WalkDown(elapsed);
            }
        }

        /// <summary>
        /// Chases ziggly.
        /// </summary>
        /// <param name="obj">Object.</param>
        /// <param name="gameTime">Game time.</param>
        public void ChaseZiggly(GameObject obj, GameTime gameTime)
        {
            var elapsed = gameTime.ElapsedGameTime;

            elapsedTime += (float)elapsed.TotalSeconds;

            if (elapsedTime > slimeDelay)
            {

                elapsedTime -= slimeDelay;

                int i = random.Next(4);

                state = SlimeState.IDLE;

                switch (i)
                {
                    case 0:
                        if (Position.X > obj.Position.X)
                        {
                            WalkLeft(elapsed);
                        }
                        break;
                    case 1:
                        if (Position.X < obj.Position.X)
                        {
                            WalkRight(elapsed);
                        }
                        break;
                    case 2:
                        if (Position.Y > obj.Position.Y)
                        {
                            WalkUp(elapsed);
                        }
                        break;
                    case 3:
                        if (Position.Y < obj.Position.Y)
                        {
                            WalkDown(elapsed);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Manual control if needed
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public void ProcessInput(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            var elapsed = gameTime.ElapsedGameTime;

            state = SlimeState.IDLE;

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                WalkLeft(elapsed);
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                WalkRight(elapsed);
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                WalkUp(elapsed);
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                WalkDown(elapsed);
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                state = SlimeState.BLOPPED;
            }
        }
    }
}
