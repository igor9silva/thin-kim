using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.Xna.Framework;
using Mono.Security;
using System.IO;

namespace ThinKim
{
	[Activity(Label = 
	          	"Thin Kim", 
	          MainLauncher = 
	          	true,
	          Icon = 
	          	"@drawable/icon",
	          Theme = 
	          	"@style/Theme.Splash",
	          AlwaysRetainTaskState = 
	          	true,
	          LaunchMode = 
	          	Android.Content.PM.LaunchMode.SingleInstance,
	          ConfigurationChanges = 
	          	Android.Content.PM.ConfigChanges.KeyboardHidden |
	          Android.Content.PM.ConfigChanges.Keyboard |
	          Android.Content.PM.ConfigChanges.Orientation,
	          ScreenOrientation =
	          	Android.Content.PM.ScreenOrientation.Landscape
	)]
	public class Activity1 : AndroidGameActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			
			var g = new ThinKim();
			var fl = new FrameLayout(this);
			
			fl.AddView((View)g.Services.GetService(typeof(View)));
			this.SetContentView(fl);
			
			g.Run();
		}

		protected override void OnStop()
		{
			base.OnStop();
			
			if (File.Exists(LeaderboardManager.AdImagePath))
				File.Delete(LeaderboardManager.AdImagePath);
		}
	}
}