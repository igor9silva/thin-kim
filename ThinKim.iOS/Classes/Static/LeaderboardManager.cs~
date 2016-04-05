// TODO document this file
using System;
using MonoTouch.UIKit;
using MonoTouch.GameKit;
using MonoTouch.Foundation;
using System.IO;

namespace ThinKim
{
	public static class LeaderboardManager
	{
		#region Constants
		const string kLeaderboardKey = "scoreLeaderboard";
		#endregion

		#region Fields
		private static GKScore score = new GKScore(kLeaderboardKey);
		public static string AdImagePath = Path.Combine(Path.GetTempPath(), "ad.jpg");
		#endregion

		#region Properties
		public static bool Authenticated
		{
			get { return GKLocalPlayer.LocalPlayer.Authenticated; }
		}
		#endregion

		#region Methods
		public static void Authenticate(UIViewController parentVC)
		{
			GKLocalPlayer.LocalPlayer.AuthenticateHandler = (vc, error) =>
			{
				if (vc != null)
					parentVC.PresentViewControllerAsync(vc, true);
				if (error != null)
					Console.WriteLine("Erro: " + error);
			};
		}

		public static void PostRecord(long longScore)
		{
			score.Value = longScore;
			GKScore.ReportScores(new GKScore[] { score }, (e) => {});
		}
		#endregion
	}
}