// TODO document this file
using System;
using ThinKim;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// A class to implement a sprite collection with shared texture.
/// </summary>
using MonoTouch.CoreVideo;
using MonoTouch.AVFoundation;
using MonoTouch.AudioUnit;
using System.Xml;

namespace ThinKim
{
	public class SpriteCollection
	{
		#region Fields
		protected List<Vector2> positions = new List<Vector2>();
		protected Texture2D texture;
		#endregion

		#region Events
		public delegate void SpriteAddedHandler(int i);

		public delegate void SpriteRemovedHandler(int i);

		public event SpriteAddedHandler SpriteAdded;
		public event SpriteRemovedHandler SpriteRemoved;
		#endregion

		#region Constructor
		public SpriteCollection()
		{
			this.SpriteAdded += (i) => {};
			this.SpriteRemoved += (i) => {};
		}
		#endregion

		#region Properties
		public virtual List<Vector2> Positions
		{
			get { return this.positions; }
		}

		public virtual Texture2D Texture
		{
			get { return this.texture; }
			set { this.texture = value; }
		}

		public virtual int Width
		{
			get { return this.texture.Width; }
		}

		public virtual int Height
		{
			get { return this.texture.Height; }
		}

		public virtual int Count
		{
			get { return this.positions.Count; }
		}
		#endregion

		#region Methods
		public virtual float GetX1(int i)
		{
			return this.positions[i].X;
		}

		public virtual void SetX1(int i, float value)
		{
			this.positions[i] = new Vector2(value, this.positions[i].Y);
		}

		public virtual float GetX2(int i)
		{
			return this.positions[i].X + this.texture.Width;
		}

		public virtual void SetX2(int i, float value)
		{
			this.positions[i] = new Vector2(value - this.texture.Width, this.positions[i].Y);
		}

		public virtual float GetY1(int i)
		{
			return this.positions[i].Y;
		}

		public virtual void SetY1(int i, float value)
		{
			this.positions[i] = new Vector2(this.positions[i].X, value);
		}

		public virtual float GetY2(int i)
		{
			return this.positions[i].Y + this.texture.Height;
		}

		public virtual void SetY2(int i, float value)
		{
			this.positions[i] = new Vector2(this.positions[i].X, value - this.texture.Height);
		}

		public Vector2 GetPosition(int i)
		{
			return new Vector2(this.GetX1(i), this.GetY1(i));
		}

		public void SetPosition(int i, Vector2 pos)
		{
			this.positions[i] = pos;
		}

		public virtual int AddSprite(float x, float y)
		{
			this.positions.Add(new Vector2(x, y));
			int i = this.positions.Count - 1;
			this.SpriteAdded.Invoke(i);
			return i;
		}

		public virtual bool RemoveSprite(float x, float y)
		{
			var vect = new Vector2(x, y);
			int i = this.positions.IndexOf(vect);
			bool ret = this.positions.Remove(vect);
			if (ret)
				this.SpriteRemoved.Invoke(i);
			return ret;
		}

		public virtual void RemoveSpriteAt(int i)
		{
			this.positions.RemoveAt(i);
			this.SpriteRemoved.Invoke(i);
		}

		public virtual void Clear()
		{
			this.positions.Clear();
		}

		public virtual void Update() {}
		#endregion
	}
	// TODO AnimatedSpriteCollection WITH shared texture
	public class AnimatedSpriteCollection : SpriteCollection
	{
		#region Fields
		protected SpriteAnimatorCollection animator;
		#endregion

		#region Events
		public event SpriteAnimatorCollection.TextureChangedHandler TextureChanged;
		#endregion

		#region Constructor
		public AnimatedSpriteCollection(int delay)
		{
			this.TextureChanged += (i, frame) => {};
			this.animator = new SpriteAnimatorCollection(delay);
			this.animator.TextureChanged += (i, frame) =>
			{
				this.TextureChanged.Invoke(i, frame);
			};
		}
		#endregion

		#region Properties
		public List<bool> AnimationActive
		{
			get { return this.animator.Active; }
		}

		public int Delay
		{
			get { return this.animator.Delay; }
			set { this.animator.Delay = value; }
		}

		public override int Height
		{
			get
			{
				throw new Exception("To get the height of an AnimatedSpriteCollection instance, you should use it's GetHeight method.");
			}
		}

		public override int Width
		{
			get
			{
				throw new Exception("To get the width of an AnimatedSpriteCollection instance, you should use it's GetWidth method.");
			}
		}

		public Texture2D[] TexturePack
		{
			get { return this.animator.TexturePack; }
			set
			{ 
				this.texture = value[0];
				this.animator.TexturePack = value; 
			}
		}

		public override Texture2D Texture
		{
			get
			{	
				throw new Exception("To get the texture of an AnimatedSpriteCollection instance, you should use it's GetTexture method.");
			}
			set
			{
				throw new Exception("To change the textures of an AnimatedSpriteCollection instance, you should set it's TexturePack property.");
			}
		}
		#endregion

		#region Methods
		public override void Update()
		{
			base.Update();
			animator.Update();
		}

		public int GetHeight(int i)
		{
			return animator.TexturePack[animator.CurrentFrame[i]].Height;
		}

		public int GetWidth(int i)
		{
			return animator.TexturePack[animator.CurrentFrame[i]].Width;
		}

		public Texture2D GetTexture(int i)
		{
			return this.animator.GetTexture(i);
		}

		public void SetTexture(int i, int textIndex)
		{
			this.animator.SetTexture(i, textIndex);
		}

		public override int AddSprite(float x, float y)
		{
			throw new Exception("Use AddSprite(x, y, active), instead of this.");
		}

		public int AddSprite(float x, float y, bool active)
		{
			int i = base.AddSprite(x, y);
			this.animator.AddSprite(active);
			return i;
		}

		public override bool RemoveSprite(float x, float y)
		{
			bool ret = base.RemoveSprite(x, y);
			this.animator.RemoveSpriteAt(this.positions.IndexOf(new Vector2(x, y)));
			return ret;
		}

		public override void RemoveSpriteAt(int i)
		{
			base.RemoveSpriteAt(i);
			this.animator.RemoveSpriteAt(i);
		}

		public override void Clear()
		{
			base.Clear();
			this.animator.Clear();
		}
		#endregion
	}
}