// TODO document this file
using System;
using System.Collections.Generic;
using System.Security.Policy;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using Mono.Security.X509;

namespace ThinKim
{
	public class SpriteAnimatorCollection : SpriteAnimator
	{
		#region Fields
		public List<int> CurrentFrame = new List<int>();
		public List<int> CurrentDelay = new List<int>();
		new public List<bool> Active = new List<bool>();
		#endregion

		#region Events
		new public delegate void TextureChangedHandler(int i,int frame);

		new public event TextureChangedHandler TextureChanged;
		#endregion

		#region Constructor
		public SpriteAnimatorCollection(int delay) : base(delay) 
		{
			this.TextureChanged += (i, frame) => {};
		}
		#endregion

		#region Properties
		public override Texture2D[] TexturePack
		{
			get { return this.texturePack; }
			set
			{
				this.frames = value.Length;
				this.texturePack = value;
			}
		}

		public override Texture2D Texture
		{
			get { throw new Exception("To get the current texture of a SpriteAnimatorCollection instance, you should use GetTexture."); }
		}
		#endregion

		#region Methods
		public override void Update()
		{
			base.Update();
		
			for (int i = 0; i < this.CurrentFrame.Count; i++)
			{
				if (this.Active[i])
				{
					if (CurrentDelay[i] == this.delay)
					{
						CurrentDelay[i] = 0;
						if (CurrentFrame[i] == this.frames - 1)
							CurrentFrame[i] = 0;
						else
							CurrentFrame[i]++;
						this.TextureChanged.Invoke(i, CurrentFrame[i]);
					}
					else
						CurrentDelay[i]++;
				}
//				else
//					textureIndex = 0;
			}
		}

		public virtual void AddSprite(bool active)
		{
			this.CurrentDelay.Add(0);
			this.CurrentFrame.Add(0);
			this.Active.Add(active);
		}

		public virtual void RemoveSpriteAt(int i)
		{
			this.CurrentDelay.RemoveAt(i);
			this.CurrentFrame.RemoveAt(i);
			this.Active.RemoveAt(i);
		}

		public virtual Texture2D GetTexture(int i)
		{
			return this.texturePack[CurrentFrame[i]];
		}

		public virtual void SetTexture(int i, int textIndex)
		{
			CurrentFrame[i] = textIndex;
			CurrentDelay[i] = 0;
		}

		public override void Clear()
		{
			base.Clear();
			this.CurrentDelay.Clear();
			this.CurrentFrame.Clear();
		}
		#endregion
	}
}