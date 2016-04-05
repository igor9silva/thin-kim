#region Using Statements
using System;
using Org.Json;
using System.IO;
using System.Net;
using System.Text;
using Android.App;
using Android.Util;
using Android.Widget;
using Android.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.GamerServices;

#endregion
namespace ThinKim
{
	public class ThinKim : Game
	{
		#region Constants
		//

		#region Messages
		const string kRateMessage = "If you'd like to make a suggestion or tell us about an error, you should send us an email (and not to write a review), otherwise it'd make Kim sad :(";
		const string kRateTitle = "Please, take note that";
		const string kRateButton = "Rate Kim :)";
		const string kEmailUsButton = "Email us";
		const string kCancelButton = "Cancel";
		const string kNoGooglePlay = "You don't have Google Play installed :(";
		#endregion

		const byte kFPS = 60;
		const float kGravity = 1.8f;
		const byte kPullIntensity = 10;
		const byte kTimesToShowTuto = 2;
		const byte kIdleEnemiesDelay = 5;
		const byte kJumpingEnemiesDelay = 7;
		const float kTransitionSpeed = 0.04f;
		const string kRecordKey = "RECORD_KEY";
		const float kPreferredScreedWidth = 960;
		const float kPreferredScreedHeight = 640;
		const float kBackgroundMusicVolume = 0.25f;
		const string kAppPackage = "com.sinky.thin_kim";
		const string kAppUrl = "market://details?id=" + kAppPackage;
		const string kAppUrlNoMarket = "https://play.google.com/store/apps/details?id=" + kAppPackage;
		const string kAdUrl = "https://admin.appnext.com/offerWallApi.aspx?id=51a04288-e744-4df3-a463-c5df32da9248&cnt=10&type=json";
		#endregion

		#region Fields
		int width;
		int height;
		float scale;
		float scaleY;
		float state = 0;
		string AdUrl = "";
		string AdTitle = "";
		byte timesShowedTuto = 0;
		bool drawAdImage = false;
		STPSpriteBatch spriteBatch;
		System.Net.WebClient webClient;
		GraphicsDeviceManager graphics;
		ScreenType actualScreen = ScreenType.Menu;
		//
		// Behaviors
		//
		PullBehavior pullLeftBehavior;
		GravityBehavior gravityBehavior;
		//
		// Sprites
		//
		Kim kim;
		Sprite menu;
		Record record;
		Ground ground;
		Sprite AdImage;
		Sprite gameOver;
		Enemies enemies;
		Sprite tutorial;
		Sprite background;
		Sprite noRank;
		CenteredTextSprite score;
		//
		// Sounds
		//
		Song backgroundMusic;
		#endregion

		#region Initialization
		//

		#region Constructor
		public ThinKim()
		{
			// this force the Update method to be called EVERY time Draw is called
			this.IsFixedTimeStep = false;
			// this sets the FPS to kFPS
			this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / kFPS);
			
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			graphics.IsFullScreen = true;
			
			this.webClient = new System.Net.WebClient();
			webClient.DownloadFileCompleted += (sender, e) => 
			{	
				this.drawAdImage = true;
				AdImage.Texture = Texture2D.FromStream(graphics.GraphicsDevice, File.OpenRead(LeaderboardManager.AdImagePath));
			};
			
			#region Initialize Sprites
			kim = new Kim();
			ground = new Ground();
			record = new Record();
			AdImage = new Sprite();
			menu = new Sprite(0, 0);
			noRank = new Sprite(0, 0);
			background = new Sprite();
			tutorial = new Sprite(0, 0);
			gameOver = new Sprite(0, 0);
			score = new CenteredTextSprite();
			enemies = new Enemies(kIdleEnemiesDelay, kJumpingEnemiesDelay);
			//parentViewController = this.Services.GetService(typeof(UIViewController)) as UIViewController;
			#endregion
		}
		#endregion

		#region Load Content
		private void LoadAllContent()
		{
			#region Load Fonts
			score.Font = Content.Load<SpriteFont>("Fonts/miramonteB");
			record.Font = Content.Load<SpriteFont>("Fonts/miramonteB");
			#endregion
			
			#region Load SoundFX
			// KIM jump | roll
			kim.JumpSoundFX = Content.Load<SoundEffect>("SoundFX/kim_roll");
			kim.RollSoundFX = Content.Load<SoundEffect>("SoundFX/kim_jump");
			#endregion

			#region Load Songs
			backgroundMusic = Content.Load<Song>("Songs/background_music");
			#endregion
			
			#region Load UI textures
			menu.Texture = Content.Load<Texture2D>("Sprites/UI/menu");
			gameOver.Texture = Content.Load<Texture2D>("Sprites/UI/game_over");
			tutorial.Texture = Content.Load<Texture2D>("Sprites/UI/tutorial");
			noRank.Texture = Content.Load<Texture2D>("Sprites/UI/no_rank");
			#endregion
			
			#region Load Scenario textures
			background.Texture = Content.Load <Texture2D>("Sprites/Scenario/background");
			ground.Texture = Content.Load<Texture2D>("Sprites/Scenario/ground");
			#endregion
			
			#region Load ENEMIES textures
			// IDLE animation ========================================================================
			Texture2D[] textures = new Texture2D[4];
			for (byte i = 1; i < textures.Length + 1; i++)
				textures[i - 1] = Content.Load<Texture2D>("Sprites/Enemy/Idle/" + i.ToString());
			enemies.TexturePackIdle = textures;
			// =======================================================================================
			
			// JUMP animation ========================================================================
			textures = new Texture2D[5];
			for (byte i = 1; i < textures.Length + 1; i++)
				textures[i - 1] = Content.Load<Texture2D>("Sprites/Enemy/Jumping/" + i.ToString());
			enemies.TexturePackJumping = textures;
			// =======================================================================================
			#endregion			
			
			#region Load KIM textures
			// RUN animation =========================================================================
			textures = new Texture2D[6];
			for (byte i = 1; i < textures.Length + 1; i++)
				textures[i - 1] = Content.Load<Texture2D>("Sprites/Kim/Running/" + i.ToString());
			kim.TexturePackRun = textures;
			// =======================================================================================
			
			// ROLL animation ========================================================================
			textures = new Texture2D[5];
			for (byte i = 1; i < textures.Length + 1; i++)
				textures[i - 1] = Content.Load<Texture2D>("Sprites/Kim/Rolling/" + i.ToString());
			kim.TexturePackRoll = textures;
			// =======================================================================================
			#endregion
		}

		protected override void LoadContent()
		{
			spriteBatch = new STPSpriteBatch(graphics.GraphicsDevice);
			
			LoadAllContent();
		}
		#endregion

		#region Initialize
		protected override void Initialize()
		{
			base.Initialize();
			
			#region GENERAL Initialization
			graphics.SupportedOrientations = DisplayOrientation.LandscapeRight | DisplayOrientation.LandscapeLeft;
			graphics.ApplyChanges();
			
			this.width = GraphicsDevice.PresentationParameters.BackBufferWidth;
			this.height = GraphicsDevice.PresentationParameters.BackBufferHeight;
			
			this.scaleY = this.height / kPreferredScreedHeight;
			this.scale = this.width / kPreferredScreedWidth;
			#endregion
			
			#region SPRITE Initialization
			ground.Initialize(this.height);
			int groundY = (int)(ground.Position(0).Y + (ground.Height / 2));
			
			kim.Initialize(groundY);
			enemies.Initialize(groundY, kim.X1 + (kim.Width / 2), scale);
			AdImage.Position = new Vector2(this.width * 0.29375f, this.height * 0.75f);
			
			// TODO review both V
			score.Initialize(this.width, scale, this.height * 0.15f);
			record.Initialize(this.width, scale * 0.4f, this.height * 0.05f);
			#endregion
			
			#region BEHAVIOR Initialization
			this.gravityBehavior = new GravityBehavior(groundY, kGravity);
			this.gravityBehavior.AttachSprite(kim);
			
			this.pullLeftBehavior = new PullBehavior(kPullIntensity, PullBehaviorDirection.Left);
			this.pullLeftBehavior.AttachSprite(ground);
			this.pullLeftBehavior.AttachSpriteCollection(enemies.IdleEnemies);
			this.pullLeftBehavior.AttachSpriteCollection(enemies.JumpingEnemies);
			this.enemies.Behavior = this.pullLeftBehavior;
			#endregion
			
			#region Retrieve record from Shared Preferences
//			record.Value = 0;
			string val = "";
			try
			{
				val = ThinKim.Activity.GetPreferences(FileCreationMode.Private).GetString(kRecordKey, "");
				val = Encoding.UTF8.GetString(Base64.Decode(val, Base64Flags.Default));
			}
			catch
			{
				#if DEBUG
				Console.WriteLine("Error 1");
				Toast.MakeText(ThinKim.Activity, "Error 1", ToastLength.Long).Show();
				#endif
			}
			
			long i;
			if (long.TryParse(val, out i))
				record.Value = i;
			else
			{
				record.Value = 0;
				#if DEBUG
				Console.WriteLine("Erro 2");
				#endif
			}
			#endregion
			
			#region PLAY Background music
			MediaPlayer.Volume = kBackgroundMusicVolume;
			MediaPlayer.IsRepeating = true;
			MediaPlayer.Play(backgroundMusic);
			#endregion
			
			#region AUTHENTICATE on GameCenter
			// TODO a
			//LeaderboardManager.Authenticate(parentViewController);
			#endregion
		}
		#endregion

		#endregion

		#region Update
		protected override void Update(GameTime gameTime)
		{
//			if (gameTime.ElapsedGameTime > TimeSpan.FromSeconds(0.017))
//				Console.WriteLine("Running Slowly!");
			//Console.WriteLine(gameTime.ElapsedGameTime);
			
			TouchCollection tc = TouchPanel.GetState();
			foreach (TouchLocation Tl in tc)
			{
				TouchLocation prev;
				if (!Tl.TryGetPreviousLocation(out prev))
				{
					switch (actualScreen)
					{
						#region Game
						case ScreenType.Game:
							{
								// JUMP 
								if (Tl.Position.X > this.width / 2)
									kim.Jump();

								// ROLL
								else if (Tl.Position.X < this.width / 2)
									kim.Roll();
							}	
							break;
						#endregion
							
						#region Pause
//						case ScreenType.Pause:
//							if (touchCount++ > 0)
//								this.UnPause();
//							break;
						#endregion
							
						#region Menu
						case ScreenType.Menu:
							{
								if (Tl.Position.X < this.width / 3)
								{
									#region Button 3 - RANK
									//Show Leaderboard View Controller
									// TODO a
//									if (LeaderboardManager.Authenticated)
//									{
//										GKLeaderboardViewController vc = new GKLeaderboardViewController();
//										vc.Finished += (sender, e) => 
//										{
//											vc.DismissViewControllerAsync(true); 
//										};
//										parentViewController.PresentViewControllerAsync(vc, true);
//									}
//									else
//										new UIAlertView("Error", "You're not signed-in to Game Center!", null, "OK", null).Show();
									#endregion
								}
								else if (Tl.Position.X < (this.width / 3) * 2)
								{
									#region Button 2 - RATE
									AlertDialog.Builder builder = new AlertDialog.Builder(ThinKim.Activity);
									
									builder.SetTitle(kRateTitle);
									builder.SetMessage(kRateMessage);
									
									#region RATE
									builder.SetNegativeButton(kRateButton, ((sender, e) => 
									{
										try
										{
											Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(kAppUrl));
											ThinKim.Activity.StartActivity(intent);
											intent.Dispose();
										}
										catch
										{
											try
											{
												Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(kAppUrlNoMarket));
												ThinKim.Activity.StartActivity(intent);
												intent.Dispose();
											}
											catch
											{
												Toast.MakeText(ThinKim.Activity.ApplicationContext, kNoGooglePlay, ToastLength.Short).Show();
											}
										}
										
									}));
									#endregion
									
									#region EMAIL US
									builder.SetNeutralButton(kEmailUsButton, ((sender, e) => 
									{
										Intent mailIntent = new Intent(Intent.ActionSend, Android.Net.Uri.Parse("mailto:"));
										
										mailIntent.SetType("message/rfc822"); // prompts email clients only
										
										mailIntent.PutExtra(Intent.ExtraEmail, "support@sinky.com.br");
										mailIntent.PutExtra(Intent.ExtraSubject, "Contact via Thin Kim");
										//mailIntent.PutExtra(Intent.ExtraText, "");
										
										try
										{
											// the user can choose the email client
											ThinKim.Activity.StartActivity(Intent.CreateChooser(mailIntent, "Choose..."));
										}
										catch
										{
											Toast.MakeText(ThinKim.Activity, "No email client installed.", ToastLength.Short).Show();
										}

									}));
									#endregion

									builder.Create().Show();
									#endregion
								}
								else
								{
									#region Button 3 - PLAY
									actualScreen = ScreenType.TranstionMenuToGame;
									if (timesShowedTuto < kTimesToShowTuto + 1)
										timesShowedTuto++;
									this.state = 1.0f;
									
									try
									{
										HttpWebRequest req = new HttpWebRequest(new Uri(kAdUrl));
										req.BeginGetResponse((ar) => 
										{ 
											var wresp = req.EndGetResponse(ar);
											var streamReader = new StreamReader(wresp.GetResponseStream());
										
											var obj = new JSONObject(streamReader.ReadToEnd());
											var apps = obj.GetJSONArray("apps");
										
											JSONObject result = new JSONObject();
											for (byte i = 0; i < apps.Length(); i++)
											{
												result = apps.GetJSONObject(i);
												if (result.GetString("revenueType") != "cpi")
													break;
											}
										
											this.AdTitle = result.GetString("title");
											this.AdUrl = result.GetString("urlApp");
											Uri uri = new Uri(result.GetString("urlImg"));
											
											this.webClient.DownloadFileTaskAsync(uri, LeaderboardManager.AdImagePath);
										
											streamReader.Close();
											wresp.Close();
										}, null);
									}
									catch
									{
									}
									#endregion
								}
							}	
							break;
						#endregion
							
						#region GameOver
						case ScreenType.GameOver:
							{
								if (Tl.Position.Y > (this.height / 3) * 2)
								{
									if (Tl.Position.X < this.width / 4)
									{
										#region OK Button
										actualScreen = ScreenType.TransitionGameOverToMenu;
										enemies.Reset();
										kim.Reset();
										#endregion
									}
									else
									{
										#region Advertisement
										if (AdUrl.Length > 0)
										{
											try
											{
												Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(AdUrl));
												ThinKim.Activity.StartActivity(intent);
												intent.Dispose();
											}
											catch
											{
												Toast.MakeText(ThinKim.Activity.ApplicationContext, "Failed :(", ToastLength.Short).Show();
											}
										}
										#endregion
									}
								}
							}
							break;
						#endregion
							
						#region Tutorial
						case ScreenType.Tutorial:
							{
								actualScreen = ScreenType.Game;
							}
							break;
						#endregion
					}
				}

			}
			
			switch (actualScreen)
			{
				#region Game
				case ScreenType.Game:
					{
						#region Check for Collisions
						// Kim x Idle Enemies
						for (int i = 0; i < enemies.IdleEnemies.Count; i++)
						{
							var e = enemies.IdleEnemies;
							if (kim.X2 > e.GetX1(i) && kim.Y2 > e.GetY1(i) && kim.X1 < e.GetX2(i) && kim.Y1 < e.GetY2(i))
								this.GameOver();
						}
			
						// Kim x Jumping Enemies
						for (int i = 0; i < enemies.JumpingEnemies.Count; i++)
						{
							var e = enemies.JumpingEnemies;
							if (kim.X2 > e.GetX1(i) && kim.Y2 > e.GetY1(i) && kim.X1 < e.GetX2(i) && kim.Y1 < e.GetY2(i))
								this.GameOver();
						}
						#endregion

						score.Text = enemies.Score.ToString();
				
						// Behavior updates MUST be called BEFORE sprite updates.
				
						// Update Behaviors
						gravityBehavior.Update();
						pullLeftBehavior.Update();
				
						// Update Sprites
						enemies.Update();
						ground.Update();
						kim.Update();
					}
					break;
				#endregion
					
				#region Menu, Tutorial
				case ScreenType.Tutorial:
				case ScreenType.TranstionMenuToGame:
				case ScreenType.Menu:
					{
						// Update Behaviors
						pullLeftBehavior.Update();
				
						// Update Sprites
						ground.Update();
						kim.Update();
					}
					break;
				#endregion
			}
			
			#region Transitions
			if (actualScreen == ScreenType.TranstionMenuToGame)
			{
				if (this.state > 0)
					this.state -= kTransitionSpeed;
				else
				{
					if (timesShowedTuto < kTimesToShowTuto + 1)
						actualScreen = ScreenType.Tutorial;
					else
					{
						actualScreen = ScreenType.Game;
						this.state = 0;
					}
				}
			}
			else if (actualScreen == ScreenType.TransitionGameToGameOver)
			{
				if (this.state < 1)
					this.state += kTransitionSpeed;
				else
				{
					actualScreen = ScreenType.GameOver;
					this.state = 0;
				}
			}
			else if (actualScreen == ScreenType.TransitionGameOverToMenu)
			{
				if (this.state < 1)
					this.state += kTransitionSpeed;
				else
				{
					actualScreen = ScreenType.Menu;
					this.AdUrl = "";
					this.AdTitle = "";
					this.drawAdImage = false;
					if (File.Exists(LeaderboardManager.AdImagePath))
						File.Delete(LeaderboardManager.AdImagePath);
					this.state = 0;
				}
			}
			#endregion
			
			base.Update(gameTime);
		}
		#endregion

		#region Draw
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.Green);

			spriteBatch.Begin();

			// BACKGROUND
			spriteBatch.Draw(background.Texture,
			                 new Rectangle((int)background.Position.X,
			                               (int)background.Position.Y,
			                               (int)this.width,
			                               (int)this.height),
			                 Color.White);
			
			// GROUND
			for (byte i = 0; i < 2; i++)
				spriteBatch.Draw(ground.Texture, ground.Position(i), scale);

			if (actualScreen != ScreenType.GameOver &&
			    actualScreen != ScreenType.TransitionGameToGameOver &&
			    actualScreen != ScreenType.TransitionGameOverToMenu)
			{
				// IDLE ENEMIES
				for (int i = 0; i < enemies.IdleEnemies.Count; i++)
					spriteBatch.Draw(enemies.IdleEnemies.GetTexture(i),
					                 enemies.IdleEnemies.GetPosition(i),
					                 enemies.Scale,
					                 Color.Green);
			
				// JUMPING ENEMIES
				for (int i = 0; i < enemies.JumpingEnemies.Count; i++)
					spriteBatch.Draw(enemies.JumpingEnemies.GetTexture(i),
					                 enemies.JumpingEnemies.GetPosition(i),
					                 enemies.Scale,
					                 Color.Red);
			}
			
			// KIM
			spriteBatch.Draw(kim.Texture, kim.Position, scale);
			
			
			if (timesShowedTuto < kTimesToShowTuto + 1)
			{
				if (actualScreen == ScreenType.Tutorial || actualScreen == ScreenType.TranstionMenuToGame)
					spriteBatch.Draw(tutorial.Texture,
					                 new Rectangle((int)tutorial.X1, (int)tutorial.Y1, this.width, this.height),
					                 Color.White);
			}
			
			if (actualScreen == ScreenType.Game)
			{
				// RECORD
				spriteBatch.DrawString(score.Font, record.Text, record.Position, record.Color, record.Scale);
			
				// SCORE
				spriteBatch.DrawString(score.Font, score.Text, score.Position, score.Color, score.Scale);
			}
			else if (actualScreen == ScreenType.Menu || actualScreen == ScreenType.TranstionMenuToGame || actualScreen == ScreenType.TransitionGameOverToMenu)
			{
				// MENU
				spriteBatch.Draw(menu.Texture,
				                 new Rectangle((int)menu.X1, (int)menu.Y1, this.width, this.height),
				                 actualScreen == ScreenType.Menu ? Color.White : Color.White * this.state);
				
				spriteBatch.Draw(noRank.Texture,
				                 new Rectangle(0, 0, (int)(noRank.Width * this.scale), this.height),
				                 actualScreen == ScreenType.Menu ? Color.White : Color.White * this.state);
			}
			
			if (actualScreen == ScreenType.GameOver || actualScreen == ScreenType.TransitionGameToGameOver || actualScreen == ScreenType.TransitionGameOverToMenu)
			{
				float GOState = actualScreen == ScreenType.TransitionGameToGameOver ? this.state : (1 - this.state);
				//float TState = actualScreen == ScreenType.GameOver ? 1 : this.state;
				
				// GAME OVER
				spriteBatch.Draw(gameOver.Texture,
				                 new Rectangle((int)gameOver.X1, (int)gameOver.Y1, this.width, this.height),
				                 actualScreen == ScreenType.GameOver ? Color.White : Color.White * GOState);
				
				// RECORD
				spriteBatch.DrawCenteredString(record.Font,
				                               record.Value.ToString(),
				                               new Vector2(this.width * 0.85f, this.height * 0.53f),
				                               record.Color * GOState,
				                               new Vector2(this.scale, this.scaleY));
			
				// SCORE
				spriteBatch.DrawCenteredString(score.Font,
				                               score.Text,
				                               new Vector2(this.width * 0.36f, this.height * 0.53f),
				                               score.Color * GOState,
				                               new Vector2(this.scale, this.scaleY));
				
				// AD TITLE
				if (this.AdTitle.Length > 0)
				{
					// TODO dd this to iOS version
					var size = score.Font.MeasureString(this.AdTitle);
					float varScale = 490 / size.X;
					if (size.Y * varScale > 100)
						varScale = 100 / size.Y;
					spriteBatch.DrawCenteredString(score.Font,
					                               this.AdTitle,
					                               new Vector2(this.width * 0.7f, this.height * 0.89f),
					                               score.Color * GOState,
					                               this.scale * varScale);
				}
				
				// AD IMAGE
				if (this.drawAdImage)
				if (AdImage.Texture != null)
					spriteBatch.Draw(AdImage.Texture,
					                 new Rectangle((int)AdImage.Position.X,
					                               (int)AdImage.Position.Y,
					                               (int)(110 * this.scale),
					                               (int)(110 * this.scaleY)),
					                 Color.White * GOState);
			}
			
			spriteBatch.End();
			
			base.Draw(gameTime);
		}
		#endregion

		#region GameOver
		private void GameOver()
		{
			// PAUSE
			actualScreen = ScreenType.TransitionGameToGameOver;
			
//			// Check if record was broken
			if (enemies.Score > record.Value)
			{
				try
				{
					var editor = ThinKim.Activity.GetPreferences(FileCreationMode.Private).Edit();
					string encoded = Base64.EncodeToString(Encoding.UTF8.GetBytes(enemies.Score.ToString()), Base64Flags.Default).ToString();
					
					editor.PutString(kRecordKey, encoded);
					editor.Commit();
				
					record.Value = enemies.Score;
				}
				catch
				{
					#if DEBUG
					Console.WriteLine("Error 3");
					#endif
				}
			}
//			// Post on Leaderboard TODO a
//			LeaderboardManager.PostRecord(record.Value);
		}
		#endregion

		#region OnActivated
		protected override void OnActivated(object sender, EventArgs args)
		{
			base.OnActivated(sender, args);
			
			if (GraphicsDevice != null)
			{
				this.LoadAllContent();
			
				if (File.Exists(LeaderboardManager.AdImagePath))
					AdImage.Texture = Texture2D.FromStream(graphics.GraphicsDevice, File.OpenRead(LeaderboardManager.AdImagePath));			
			}
		}
		#endregion
	}

	enum ScreenType
	{
		Game,
		Menu,
		GameOver,
		Tutorial,
		TranstionMenuToGame,
		TransitionGameToGameOver,
		TransitionGameOverToMenu,
	}
}