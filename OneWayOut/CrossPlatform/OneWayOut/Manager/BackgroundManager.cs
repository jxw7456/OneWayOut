using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace OneWayOut.Manager
{
    /// <summary>
    /// Background manager.
    /// In charge of switching background.
    /// </summary>
    public class BackgroundManager
    {
        const string START_BG = @"textures/dungeon";

        const string HELP_BG = @"textures/dungeonHelp";

        const string STORY_BG = @"textures/dungeonOptions";

        const string GAMEOVER_BG = @"textures/deadArcher";

        Texture2D startBg, optionBg, helpBg, gameoverBg;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayOut.Manager.BackgroundManager"/> class.
        /// Cache all background texture
        /// </summary>
        /// <param name="Content">Content.</param>
        public BackgroundManager(ContentManager Content)
        {
            startBg = Content.Load<Texture2D>(START_BG);

            optionBg = Content.Load<Texture2D>(STORY_BG);

            helpBg = Content.Load<Texture2D>(HELP_BG);

            gameoverBg = Content.Load<Texture2D>(GAMEOVER_BG);
        }

        /// <summary>
        /// Draws the gameover background.
        /// </summary>
        /// <param name="sb">SpriteBatch</param>
        /// <param name="graphics">Graphics.</param>
        public void DrawGameover(SpriteBatch sb, GraphicsDevice graphics)
        {
            DrawBackGround(sb, graphics, gameoverBg);
        }

        /// <summary>
        /// Draws the help background.
        /// </summary>
        /// <param name="sb">SpriteBatch</param>
        /// <param name="graphics">Graphics.</param>
        public void DrawHelp(SpriteBatch sb, GraphicsDevice graphics)
        {
            DrawBackGround(sb, graphics, helpBg);
        }

        /// <summary>
        /// Draws the option background.
        /// </summary>
        /// <param name="sb">SpriteBatch</param>
        /// <param name="graphics">Graphics.</param>
        public void DrawStory(SpriteBatch sb, GraphicsDevice graphics)
        {
            DrawBackGround(sb, graphics, optionBg);
        }

        /// <summary>
        /// Draws the start background.
        /// </summary>
        /// <param name="sb">SpriteBatch</param>
        /// <param name="graphics">Graphics.</param>
        public void DrawStart(SpriteBatch sb, GraphicsDevice graphics)
        {
            DrawBackGround(sb, graphics, startBg);
        }


        /// <summary>
        /// Helper to Draws a background.
        /// </summary>
        /// <param name="sb">SpriteBatch</param>
        /// <param name="graphics">Graphics.</param>
        /// <param name="bg">Background to be Drawn.</param>
        void DrawBackGround(SpriteBatch sb, GraphicsDevice graphics, Texture2D bg)
        {
            sb.Draw(bg, graphics.Viewport.Bounds, Color.White);
        }
    }
}

