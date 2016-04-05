// TODO review documentation
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;

namespace ThinKim
{
	/// <summary>
	/// Base class for Sprite types.
	/// </summary>
	public class Sprite
	{
		#region Fields
		// Local instances
		protected Vector2 position;
		protected Texture2D texture;
		#endregion

		#region Constructors
		public Sprite(float x, float y)
		{
			this.position = new Vector2(x, y);
		}

		public Sprite()
		{
			this.position = Vector2.Zero;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the position of the sprite.
		/// </summary>
		/// <value>The position.</value>
		public virtual Vector2 Position
		{
			get { return this.position; }
			set { this.position = value; }
		}

		/// <summary>
		/// Gets or sets the texture of the sprite.
		/// </summary>
		/// <value>The texture.</value>
		public virtual Texture2D Texture
		{
			get { return this.texture; }
			set { this.texture = value; }
		}

		/// <summary>
		/// Gets the width of the texture.
		/// </summary>
		/// <value>The width.</value>
		public virtual int Width
		{
			get { return this.texture.Width; }
		}

		/// <summary>
		/// Gets the height of the texture.
		/// </summary>
		/// <value>The height.</value>
		public virtual int Height
		{
			get { return this.texture.Height; }
		}

		/// <summary>
		/// Gets or sets the X1 position for the sprite (left-bound).
		/// </summary>
		/// <value>The X1 position.</value>
		public virtual float X1
		{
			get { return this.position.X; }
			set { this.position = new Vector2(value, this.position.Y); }
		}

		/// <summary>
		/// Gets or sets the Y1 position for the sprite (upper-bound).
		/// </summary>
		/// <value>The Y1 position.</value>
		public virtual float Y1
		{
			get { return this.position.Y; }
			set { this.position = new Vector2(this.position.X, value); }
		}

		/// <summary>
		/// Gets or sets the X2 position for the sprite (right-bound).
		/// </summary>
		/// <value>The X2 position.</value>
		public virtual float X2
		{
			get { return this.position.X + this.Width; }
			set { this.position = new Vector2(value - this.Width, this.position.Y); }
		}

		/// <summary>
		/// Gets or sets the Y2 position for the sprite (bottom-bound).
		/// </summary>
		/// <value>The Y2 position.</value>
		public virtual float Y2
		{
			get { return this.position.Y + this.Height; }
			set { this.position = new Vector2(this.position.X, value - this.Height); }
		}
		#endregion

		#region Methods
		//		public void Dispose()
		//		{
		//
		//		}
		/// <summary>
		/// Override this method to implement any update that should be performed to the Sprite.
		/// Should be called in the Update() method of the main Game class.
		/// </summary>
		public virtual void Update() { }
		//		/// <summary>
		//		/// Override this method to implement any initialization needed to be done AFTER loading the textures.
		//		/// </summary>
		//		/// <param name="data">Any data needed for initialize.</param>
		//		public virtual void Initialize(object data) {}
		#endregion
	}

	public class AnimatedSprite : Sprite
	{
		#region Fields
		protected SpriteAnimator animator;
		#endregion

		#region Events
		public event SpriteAnimator.TextureChangedHandler TextureChanged;
		#endregion

		#region Constructor
		public AnimatedSprite() : base() 
		{
			this.TextureChanged += () => {};
			this.animator = new SpriteAnimator(0);
			this.animator.TextureChanged += () =>
			{
				this.TextureChanged.Invoke();
			};
		}

		public AnimatedSprite(float x, float y) : base(x, y) 
		{
			this.TextureChanged += () => {};
			this.animator = new SpriteAnimator(0);
			this.animator.TextureChanged += () =>
			{
				this.TextureChanged.Invoke();
			};
			
			this.position = new Vector2(x, y);
		}
		#endregion

		#region Properties
		public int Delay
		{
			get { return animator.Delay; }
			set { animator.Delay = value; }
		}

		public Texture2D[] TexturePack
		{
			get { return this.animator.TexturePack; }
			set { this.animator.TexturePack = value; }
		}

		public override Texture2D Texture
		{
			set
			{
				throw new Exception("To set textures in an Animated Sprite, you should set TexturePack property.");
			}
		}
		#endregion

		#region Methods
		public override void Update()
		{
			base.Update();
			this.texture = animator.Texture;
		}
		#endregion
	}
}