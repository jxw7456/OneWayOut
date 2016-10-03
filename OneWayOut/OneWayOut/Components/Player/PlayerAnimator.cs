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
    /// <summary>
    /// This is a maybe-sample class to control the animation of the player
    /// </summary>
    partial class Player
    {
        const int PLAYER_SIZE = 90;

        const int PLAYER_TEXTURE_SIZE = 512;

        public Texture2D Texture { get; set; }

        private int currentFrame;

        private int totalFrames;

        //slow down animation
        private int time = 0;

        private int millisecondsPerFrame = 100;

        public Player(Texture2D texture, int rows, int columns)
        {
            Texture = texture;

        }

        public void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.Milliseconds;
            if (time > millisecondsPerFrame)
            {
                time -= millisecondsPerFrame;

                //increment current frame
                currentFrame++;
                time = 0;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            Rectangle sourceRectangle;
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, PLAYER_SIZE, PLAYER_SIZE);

            int row = 0;
            int column = 0;


            switch (direction)
            {                
                case Direction.UP:
                    row = 2;
                    column = 2;
                    break;
                case Direction.DOWN:
                    row = 1;
                    column = 0;
                    break;
                case Direction.LEFT:
                    row = 1;
                    column = 2;
                    break;
                case Direction.RIGHT:
                    row = 1;
                    column = 1;
                    break;
                case Direction.IDLE:
                default:
                    break;
            }

            sourceRectangle = new Rectangle(row*PLAYER_TEXTURE_SIZE, column*PLAYER_TEXTURE_SIZE, PLAYER_TEXTURE_SIZE, PLAYER_TEXTURE_SIZE);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
