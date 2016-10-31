using System;

using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Content;

using OneWayOut.Components.Dungeon;

using OneWayOut.Components;

namespace OneWayOut.Manager
{
    /// <summary>
    /// Asset manager 
    /// Manages outstanding collections and list that 
    /// does not require much helper method.
    /// </summary>
    class AssetManager
    {
        const int RANDOM_SEED = 999;

        const int SLIME_WIDTH = 70;

        const int SLIME_COUNT = 9;

        const int SLIME_HEIGHT = 45;

        const string PLAYER_TEXTURE = @"textures/ArcherSpritesheet";

        const string ARROW_TEXTURE = @"textures/arrow";

        Random random;

        public Texture2D arrowTexture;

        Texture2D slimeTexture;

        Texture2D playerTexture;

        MarkovNameGenerator nameGen;

        public List<Slime> slimes;

        public Dungeon dungeon;

        public Player player;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayOut.Manager.AssetManager"/> class.
        /// Init the Slime List and Dungeon Tiles
        /// </summary>
        /// <param name="Content">Content.</param>
        /// <param name="Graphics">Graphics.</param>
        public AssetManager(ContentManager Content, GraphicsDevice Graphics)
        {
            random = new Random();

            dungeon = new Dungeon(Content);

            dungeon.GenerateTiles(random);

            nameGen = new MarkovNameGenerator();

            arrowTexture = Content.Load<Texture2D>(ARROW_TEXTURE);

            playerTexture = Content.Load<Texture2D>(PLAYER_TEXTURE);

            InitSlime(Graphics);

            slimes = new List<Slime>();

            player = new Player(playerTexture);

            ResetGame(Graphics);
        }

        public void SpawnSlimes(GraphicsDevice Graphics, int count)
        {
            for (int i = 0; i < SLIME_COUNT; i++)
            {
                AddNewSlime(Graphics);
            }
        }

        //Resets the game if player dies or quits
        public void ResetGame(GraphicsDevice Graphics)
        {
            slimes.Clear();

            player.Reset();

            player.SetPositionCenter(Graphics);

            SpawnSlimes(Graphics, SLIME_COUNT);
            //add new slime for the player            
        }

        /// <summary>
        /// Draws all the slimes.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        /// <param name="foreGroundText">Fore ground text.</param>
        public void DrawSlimes(SpriteBatch spriteBatch, ForegroundTextManager foreGroundText)
        {
            foreach (var slime in slimes)
            {
                slime.Draw(spriteBatch);
                foreGroundText.DrawSlimeName(spriteBatch, slime);
            }
        }

        /// <summary>
        /// Draws the dungeon.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        public void DrawDungeon(SpriteBatch spriteBatch)
        {
            dungeon.Draw(spriteBatch);
        }

        /// <summary>
        /// Inits the slime texture.
        /// </summary>
        /// <param name="Graphics">Graphics.</param>
        void InitSlime(GraphicsDevice Graphics)
        {
            slimeTexture = new Texture2D(Graphics, 1, 1);

            slimeTexture.SetData<Color>(new Color[] { Color.White });
        }

        /// <summary>
        /// Adds new slime into the slime list.
        /// </summary>
        /// <param name="Graphics">Graphics.</param>
        public void AddNewSlime(GraphicsDevice Graphics)
        {
            slimes.Add(MakeNewSlime(Graphics));
        }

        /// <summary>
        /// Generate a new random slime.
        /// </summary>
        /// <returns>A new slime.</returns>
        /// <param name="Graphics">Graphics.</param>
        public Slime MakeNewSlime(GraphicsDevice Graphics)
        {
            var vp = Graphics.Viewport;

            string name = nameGen.RandomBottomCase(nameGen.NextName, GameManager.level);

            int i = random.Next(4);

            int slimeX = 0;

            int slimeY = 0;

            switch (i)
            {
                case 0:
                    slimeX = vp.Width - SLIME_WIDTH;
                    goto case 1;
                case 1:
                    slimeY = random.Next(vp.Height);
                    break;
                case 2:
                    slimeY = vp.Height - SLIME_HEIGHT;
                    goto case 3;
                case 3:
                    slimeX = random.Next(vp.Width);
                    break;
                default:
                    break;
            }

            var s = new Slime(slimeX, slimeY, SLIME_WIDTH, SLIME_HEIGHT, Graphics, random, name);

            s.Texture = slimeTexture;

            return s;
        }
    }
}
