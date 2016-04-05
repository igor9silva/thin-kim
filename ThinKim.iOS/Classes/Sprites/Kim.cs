// TODO document this file
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Security;
using System.Security.Cryptography;
using Microsoft.Xna.Framework.Audio;
using OpenTK.Graphics.ES11;

namespace ThinKim
{
	public class Kim : AnimatedSprite
	{
		#region Constants
		private const sbyte kJumpSpeed = 20;
		private const sbyte kRollDuration = 30;
		private const byte kRunDelay = 3;
		private const byte kRollDelay = 1;
		private const float kScoredVolume = 0.2f;
		private const byte kKimX = 130;
		#endregion

		#region Fields
		private bool isRolling = false;
		private bool isJumping = false;
		private byte rollState;
		private char query;
		private int groundY;
		//fields for properties
		private Texture2D[] texturePackRoll;
		private Texture2D[] texturePackRun;
		private SoundEffect jumpSoundFX;
		private SoundEffect rollSoundFX;
		#endregion

		#region Constructor
		public Kim() : base()
		{
			this.Delay = kRunDelay;
		}
		#endregion

		#region Properties
		public SoundEffect JumpSoundFX
		{
			get { return this.jumpSoundFX; }
			set { this.jumpSoundFX = value; }
		}

		public SoundEffect RollSoundFX
		{
			get { return this.rollSoundFX; }
			set { this.rollSoundFX = value; }
		}

		public Texture2D[] TexturePackRoll
		{
			get { return texturePackRoll; }
			set { texturePackRoll = value; }
		}

		public Texture2D[] TexturePackRun
		{
			get { return texturePackRun; }
			set
			{
				this.texture = value[0];
				animator.TexturePack = value;
				texturePackRun = value; 
			}
		}
		#endregion

		#region Methods
		public void Initialize(int groundY)
		{
			this.groundY = groundY;
			this.Y2 = this.groundY;
			this.X1 = kKimX;
		}

		public override void Update()
		{
			// Call base to update the textures
			base.Update();
			
			// check if it has a Roll QUERIED
			if (!this.isJumping && !this.isRolling)
			{
				if (this.query == 'J')
				{
					this.isJumping = true;
					this.Y1 -= kJumpSpeed;
					this.query = (char)0;
					this.jumpSoundFX.Play(kScoredVolume, 0f, 0f);
				}
				else if (this.query == 'R')
				{
					// Starting Rolling
					animator.TexturePack = texturePackRoll;
					this.Delay = kRollDelay;
					this.rollState = 1;
					this.isRolling = true;
					this.texture = texturePackRoll[0];
					this.query = (char)0;
					this.rollSoundFX.Play(kScoredVolume, 0f, 0f);
				}
			}
				
			// Check if Kim is rolling
			if (this.isRolling)
			{
				if (this.rollState++ == kRollDuration)
				{
					// STOP rolling
					animator.TexturePack = texturePackRun;
					this.Delay = kRunDelay;
					this.isRolling = false;
					this.texture = texturePackRun[0];
					this.Y2 = this.groundY;
				}
			}
				
			if (this.Y2 == this.groundY)
				this.isJumping = false;

			if (this.isJumping)
				this.Y1 -= kJumpSpeed;
			
			if (this.Y2 > this.groundY)
				this.Y2 = this.groundY;
		}

		public void Jump()
		{
			this.query = 'J';
		}

		public void Roll()
		{
			this.query = 'R';
		}

		public void Reset()
		{
			this.query = (char)0;
			this.isJumping = false;
			this.isRolling = false;
			this.Y2 = this.groundY;
			animator.TexturePack = texturePackRun;
			this.Delay = kRunDelay;
		}
		#endregion
	}
}

