using System;
using OneWayOut.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace OneWayOut.Components
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

        const int PLAYER_TEXTURE_START_R = 1;

        const int PLAYER_TEXTURE_START_C = 4;

        private int currentFrame;

        public int blink;

        public int row;

        public int column;

        public int score { get; set; }

        //slow down animation
        internal float timer;

        public string name { get; set; }

        public int arrowSupply;

        public int health { get; set; }

        public Direction direction;

        private int millisecondsPerFrame = 100;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayOut.Components.Player.Player"/> class.
        /// </summary>
        /// <param name="Texture">T.</param>
        /// <param name="r">The red component.</param>
        /// <param name="c">C.</param>
        public Player(Texture2D Texture) : base(new Rectangle(0, 0, PLAYER_SIZE, PLAYER_SIZE))
        {
            texture = Texture;
            health = 100;
            arrowSupply = 5;
            timer = 0;
            blink = 0;
            row = PLAYER_TEXTURE_START_R;
            column = PLAYER_TEXTURE_START_C;
            IsActive = true;
        }

        /// <summary>
        /// Sets the position to center of screen.
        /// </summary>
        /// <param name="graphicDevice">Graphic device.</param>
        public new void SetPositionCenter(GraphicsDevice graphicDevice)
        {
            int screenWidth = graphicDevice.Viewport.Width;

            int screenHeight = graphicDevice.Viewport.Height;

            SetPosition((screenWidth - PLAYER_SIZE) / 2, (screenHeight - PLAYER_SIZE) / 2);
        }

        public void PlayerShoot()
        {

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
                arrowSupply -= 1;
            }
        }

        public void GainArrow()
        {
            arrowSupply += 2;
        }
    }
}

