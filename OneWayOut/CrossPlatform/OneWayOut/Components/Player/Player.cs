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
        public int Score { get; set; }

        public string Name { get; set; }

        public int ArrowCount { get; set; }

        public int Health { get; set; }

        //A single sprite's width and height
        const int PLAYER_TEXTURE_SIZE = 512;

        const int PLAYER_SIZE = 90;

        const int PLAYER_TEXTURE_START_R = 1;

        const int PLAYER_TEXTURE_START_C = 4;
        
        Direction direction;

        int currentFrame;

        int blink;

        int row;

        int column;

        //slow down animation
        internal float timer;

        int millisecondsPerFrame = 100;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayOut.Components.Player.Player"/> class.
        /// </summary>
        /// <param name="Texture">T.</param>
        /// <param name="r">The red component.</param>
        /// <param name="c">C.</param>
        public Player(Texture2D t): base(new Rectangle(0, 0, PLAYER_SIZE, PLAYER_SIZE))
        {
            Texture = t;

            Reset();
        }

        public void Reset()
        {
            Health = 100;

            ArrowCount = 10;   //amount of arrows play has to start with

            Score = 0;

            timer = 0;

            blink = 0;

            row = PLAYER_TEXTURE_START_R;

            column = PLAYER_TEXTURE_START_C;

            IsActive = true;            
        }

        //takes a arrow away with each use
        public void UseArrow()
        {
            if (ArrowCount > 0)
            {
                ArrowCount -= 1;
            }
        }

        //gains arrow for each slime death
        public void GainArrow()
        {
            ArrowCount += 2;
        }
    }
}

