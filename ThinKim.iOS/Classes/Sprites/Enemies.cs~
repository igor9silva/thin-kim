// TODO document this file
//using Microsoft.Xna.Framework.Graphics.PackedVector;
//using System.Net;

#region Using Statements
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

#endregion
namespace ThinKim
{
	public class Enemies
	{
		#region Constants
		// The X const for placing new generated enemies
		private const short kX = 912;
		// Each kFrameAmout frames, generate a new Enemie
		private const byte kFrameAmount = 25;
		// Max variation on enemies X
		private const byte kMaxFrameAmountVariation = 15;
		// Trigger to start jumping
		private const short kJumpTrigger = 400;
		readonly int[] kJumpingEnemyPoisitionVariation = new int[5] { 60, 30, 0, -30, -60 };
		private const float kIntensityAddAmount = 0.001f;
		#endregion

		#region Fields
		private float scale;
		private float kimX;
		private int groundY;
		private int score = 0;
		private Random random;
		private PullBehavior behav;
		private int frameCount = 0;
		private byte randomFrameAmount = 4;
		private byte idleEnemiesToDispose = 0;
		private byte jumpingEnemiesToDispose = 0;
		private AnimatedSpriteCollection idleEnemies;
		private AnimatedSpriteCollection jumpingEnemies;
		private List<float> JumpingEnemyPoisitionVariation = new List<float>(2);
		#endregion

		#region Constructor
		public Enemies(int idleDelay, int jumpingDelay)
		{
			this.idleEnemies = new AnimatedSpriteCollection(idleDelay);
			this.jumpingEnemies = new AnimatedSpriteCollection(jumpingDelay);
			this.random = new Random();
		}
		#endregion

		#region Properties
		public float Scale
		{
			get { return this.scale; }
			set { this.scale = value; }
		}

		public int Score
		{
			get { return this.score; }
			set { this.score = value; }
		}

		public PullBehavior Behavior
		{
			get { return this.behav; }
			set { this.behav = value; }
		}

		public AnimatedSpriteCollection IdleEnemies
		{
			get { return this.idleEnemies; }
		}

		public AnimatedSpriteCollection JumpingEnemies
		{
			get { return this.jumpingEnemies; }
		}

		public Texture2D[] TexturePackIdle
		{
			get { return this.idleEnemies.TexturePack; }
			set { this.idleEnemies.TexturePack = value; }
		}

		public Texture2D[] TexturePackJumping
		{
			get { return this.jumpingEnemies.TexturePack; }
			set { this.jumpingEnemies.TexturePack = value; }
		}
		#endregion

		#region Methods
		public void Initialize(int groundY, float kimX, float scale)
		{
			this.groundY = groundY;
			this.kimX = kimX;
			this.scale = scale;
			
			this.jumpingEnemies.TextureChanged += (i, frame) => 
			{
				if (this.jumpingEnemies.GetX1(i) < kJumpTrigger)
				{
					if (JumpingEnemyPoisitionVariation.Count > i)
						JumpingEnemyPoisitionVariation[i] = (float)kJumpingEnemyPoisitionVariation[frame] / this.jumpingEnemies.Delay;
					else
					{
						this.jumpingEnemies.SetTexture(i, 0);
						JumpingEnemyPoisitionVariation.Add((float)kJumpingEnemyPoisitionVariation[0] / this.jumpingEnemies.Delay);
					}
				}
			};
		}

		public void Update()
		{
			this.idleEnemies.Update();
			this.jumpingEnemies.Update();
			
			#region Update jumping enemies position (JUMP)
			for (byte i = 0; i < JumpingEnemyPoisitionVariation.Count; i++)
				this.jumpingEnemies.SetY1(i, this.jumpingEnemies.GetY1(i) - (float)JumpingEnemyPoisitionVariation[i]);
			
			for (byte i = 0; i < this.jumpingEnemies.Count; i++)
			{
				if (this.jumpingEnemies.GetX1(i) < kJumpTrigger * this.scale)
				{
					if (!this.jumpingEnemies.AnimationActive[i])
						this.jumpingEnemies.AnimationActive[i] = true;
				}
			}
			
			#endregion
			
			#region IDLE ENEMIES
			if (this.idleEnemies.Count > 0)
			{
				// If there are idle enemies not marked to dispose,
				// check if idleEnemie.X are minor then kim.X, 
				// if yes, score++, mark to dispose (when out of screen [x2 < 0]) and play sound;
				if (this.idleEnemies.Count > this.idleEnemiesToDispose)
				{
					if (this.idleEnemies.GetX1(idleEnemiesToDispose) < this.kimX)
					{
						score++;
						idleEnemiesToDispose++;
						this.behav.intensity += kIntensityAddAmount;
					}
				}

				// Check if the very first idleEnemie is out of screen, if yes, dispose and unmark to dispose
				if (this.idleEnemies.GetX2(0) < 0)
				{
					this.idleEnemies.RemoveSpriteAt(0);
					idleEnemiesToDispose--;
				}
			}
			#endregion

			#region JUMPING ENEMIES
			if (this.jumpingEnemies.Count > 0)
			{
				// If there are jumping enemies not marked to dispose,
				// check if jumpingEnemie.X are minor then kim.X,
				// 	if yes, score++, mark to dispose (when out of screen [x2 < 0]) and play sound;
				if (this.jumpingEnemies.Count > this.jumpingEnemiesToDispose)
				{
					if (this.jumpingEnemies.GetX1(jumpingEnemiesToDispose) < this.kimX)
					{
						score++;
						jumpingEnemiesToDispose++;
						this.behav.intensity += kIntensityAddAmount;
					}
				}
				
				// Check if the very first jumpingEnemie is out of screen, if yes, dispose and unmark to dispose
				if (this.jumpingEnemies.GetX2(0) < 0)
				{
					this.jumpingEnemies.RemoveSpriteAt(0);
					jumpingEnemiesToDispose--;
					JumpingEnemyPoisitionVariation.RemoveAt(0);
				}
			}
			#endregion
			
			#region Generate a new Sprite
			if (frameCount > kFrameAmount + randomFrameAmount)
			{
				frameCount = 0;
				randomFrameAmount = (byte)random.Next(kMaxFrameAmountVariation + 1);
				int i = random.Next(100);

				if (i < 55)
				{
					if (i > 9)
						this.idleEnemies.SetY2(this.idleEnemies.AddSprite(kX, 0, false), this.groundY);
				}
				else
					this.jumpingEnemies.SetY2(this.jumpingEnemies.AddSprite(kX, 0, false), this.groundY);
			}
			else
				frameCount++;
			#endregion
		}

		public void Reset()
		{
			this.JumpingEnemyPoisitionVariation.Clear();
			this.idleEnemies.Clear();
			this.jumpingEnemies.Clear();
			this.score = 0;
			this.frameCount = 0;
			this.idleEnemiesToDispose = 0;
			this.jumpingEnemiesToDispose = 0;

		}
		#endregion
	}
}