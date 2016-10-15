using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace OneWayOut.Manager
{
	public class BgmManager
	{
		const string MENU_BGM = @"media/menu";

		const string GAME_BGM = @"media/game";

		const string GAMEOVER_BGM = @"media/gameOver";

		const string OPTION_BGM = @"media/options";

		const string HELP_BGM = @"media/help";

		Song currentSong, menuSong, helpSong, gameSong, gameOverSong, optionsSong;

		public BgmManager (ContentManager Content)
		{
			menuSong = Content.Load<Song> (MENU_BGM);            
			gameSong = Content.Load<Song> (GAME_BGM);
			gameOverSong = Content.Load<Song> (GAMEOVER_BGM);
			optionsSong = Content.Load<Song> (OPTION_BGM);
			helpSong = Content.Load<Song> (HELP_BGM);

			currentSong = menuSong;

			MediaPlayer.Volume = 0.50f;

			MediaPlayer.IsRepeating = true;

			MediaPlayer.Play (currentSong);
		}

		void SwitchBgm (Song targetBgm)
		{
			if (!MediaPlayer.Equals (currentSong, targetBgm)) {
				currentSong = targetBgm;
				MediaPlayer.Play (currentSong);
			}
		}

		public void Resume ()
		{
			MediaPlayer.Resume ();			
		}

		public void Pause ()
		{
			MediaPlayer.Pause ();
		}

		public void PlayMenu ()
		{
			SwitchBgm (menuSong);
		}

		public void PlayGame ()
		{
			SwitchBgm (gameSong);
		}

		public void PlayGameOver ()
		{
			SwitchBgm (gameOverSong);
		}

		public void PlayOptions ()
		{
			SwitchBgm (optionsSong);
		}

		public void PlayHelp ()
		{
			SwitchBgm (helpSong);
		}
	}
}

