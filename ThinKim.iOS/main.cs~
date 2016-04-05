// TODO document this file
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Microsoft.Xna.Framework;
using System.IO;

namespace ThinKim
{
	[Register("AppDelegate")]
	class Program : UIApplicationDelegate
	{
		ThinKim game;

		public override void FinishedLaunching(UIApplication app)
		{
			// Disable device screen 'auto-locking'
			app.IdleTimerDisabled = true;
			
			// Instantiate and run the main game class
			game = new ThinKim();
			game.Run();
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			UIApplication.Main(args, null, "AppDelegate");
		}

		public override void WillTerminate(UIApplication application)
		{
			if (File.Exists(LeaderboardManager.AdImagePath))
				File.Delete(LeaderboardManager.AdImagePath);
		}
	}
}