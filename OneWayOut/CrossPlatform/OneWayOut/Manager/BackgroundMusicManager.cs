using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace OneWayOut.Manager
{
	/// <summary>
	/// Background Music manager.
	/// In charge of changing the music
	/// </summary>
	public class BgmManager
	{
		const string MENU_BGM = @"media/menu";

		const string GAME_BGM = @"media/game";

		const string GAMEOVER_BGM = @"media/gameOver";

		const string OPTION_BGM = @"media/options";

		const string HELP_BGM = @"media/help";

		float volume;

		const float REGULAR_VOLUME = 0.50f;

		Song currentSong, menuSong, helpSong, gameSong, gameOverSong, optionsSong;

		/// <summary>
		/// Initializes a new instance of the <see cref="OneWayOut.Manager.BgmManager"/> class.
		/// Cache all song object and set the media object state
		/// </summary>
		/// <param name="Content">Content.</param>
		public BgmManager (ContentManager Content)
		{
            volume = REGULAR_VOLUME;

            try
            {
                using (var settings = new StreamReader("settings.txt"))
                {
                    volume = REGULAR_VOLUME * float.Parse(settings.ReadToEnd());
                }

            }
            catch (FileNotFoundException ex)
            {
                
            }
                        
			menuSong = Content.Load<Song> (MENU_BGM);            
			gameSong = Content.Load<Song> (GAME_BGM);
			gameOverSong = Content.Load<Song> (GAMEOVER_BGM);
			optionsSong = Content.Load<Song> (OPTION_BGM);
			helpSong = Content.Load<Song> (HELP_BGM);

			currentSong = menuSong;

			MediaPlayer.Volume = volume;

			MediaPlayer.IsRepeating = true;

			MediaPlayer.Play (currentSong);
		}

		/// <summary>
		/// Helper method to switch between bgm
		/// </summary>
		/// <param name="targetBgm">Target bgm.</param>
		void SwitchBgm (Song targetBgm)
		{
			if (!MediaPlayer.Equals (currentSong, targetBgm)) {
				currentSong = targetBgm;
				MediaPlayer.Play (currentSong);
			}
		}

		/// <summary>
		/// Resume playing.
		/// </summary>
		public void Resume ()
		{
			MediaPlayer.Resume ();			
		}

		/// <summary>
		/// Pause the Media.
		/// </summary>
		public void Pause ()
		{
			MediaPlayer.Pause ();
		}

		/// <summary>
		/// Play the menu song.
		/// </summary>
		public void PlayMenu ()
		{
			SwitchBgm (menuSong);
		}

		/// <summary>
		/// Play the game song.
		/// </summary>
		public void PlayGame ()
		{
			SwitchBgm (gameSong);
		}

		/// <summary>
		/// Play the gameover song.
		/// </summary>
		public void PlayGameOver ()
		{
			SwitchBgm (gameOverSong);
		}

		/// <summary>
		/// Play the options song.
		/// </summary>
		public void PlayOptions ()
		{
			SwitchBgm (optionsSong);
		}

		/// <summary>
		/// Play the help song.
		/// </summary>
		public void PlayHelp ()
		{
			SwitchBgm (helpSong);
		}
	}
}

