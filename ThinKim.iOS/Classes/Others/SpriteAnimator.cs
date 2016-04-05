// TODO document this file
using System;
using Microsoft.Xna.Framework.Graphics;
using OpenTK.Graphics.ES11;
using System.Runtime.InteropServices;
using System.IO;

namespace ThinKim
{
	public class SpriteAnimator
	{
		#region Fields
		private int currentFrame = 0;
		private int currentDelay = 0;
		// fields for properties
		protected int frames;
		protected int delay;
		protected bool active;
		protected Texture2D[] texturePack;
		#endregion

		#region Events
		public delegate void TextureChangedHandler();

		public event TextureChangedHandler TextureChanged;
		#endregion

		#region Constructor
		public SpriteAnimator(int delay)
		{
			this.delay = delay;
			this.active = true;
		}
		#endregion

		#region Properties
		public bool Active
		{
			get { return active; }
			set { this.active = value; }
		}

		public int Delay
		{
			get { return delay; }
			set { delay = value; }
		}

		public virtual Texture2D[] TexturePack
		{
			get { return this.texturePack; }
			set
			{
				this.currentDelay = 0;
				this.currentFrame = 0;
				this.frames = value.Length;
				this.texturePack = value; 
			}
		}

		public virtual Texture2D Texture
		{
			get
			{
				int index;
				if (active)
				{
					if (currentDelay == this.delay)
					{
						this.TextureChanged.Invoke();
						currentDelay = 0;
						if (currentFrame == this.frames - 1)
						{
							index = currentFrame;
							currentFrame = 0;
						}
						else
						{
							index = currentFrame;
							currentFrame++;
						}
					}
					else
					{
						currentDelay++;
						index = currentFrame;
					}
				}
				else
					index = 0;
			
				return this.texturePack[index];
			}
		}

		public virtual void Clear()
		{
			this.currentDelay = 0;
			this.currentFrame = 0;
		}
		#endregion

		#region Methods
		public virtual void Update() {}
		#endregion
	}
}

