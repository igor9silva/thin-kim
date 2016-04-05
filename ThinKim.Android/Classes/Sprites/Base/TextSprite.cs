// TODO document this file
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.InteropServices;

namespace ThinKim
{
	public class TextSprite
	{
		#region Fields
		// Local instances
		protected Vector2 position;
		protected SpriteFont font;
		protected string text;
		protected Vector2 size;
		protected Color color;
		protected float scale;
		#endregion

		#region Constructors
		public TextSprite(float x, float y, string text, Color color)
		{
			this.color = color;
			this.position = new Vector2(x, y);
			this.text = text;
		}

		public TextSprite()
		{
			this.position = Vector2.Zero;
			this.text = "";
			this.color = Color.White;
		}
		#endregion

		#region Properties
		public virtual Vector2 Position
		{
			get { return this.position; }
			set { this.position = value; }
		}

		public virtual Vector2 Size
		{
			get { return this.size; }
		}

		public virtual SpriteFont Font
		{
			get { return this.font; }
			set { this.font = value; }
		}

		public virtual Color Color
		{
			get { return this.color; }
			set { this.color = value; }
		}

		public virtual string Text
		{
			get { return this.text; }
			set
			{
				this.size = this.font.MeasureString(value) * this.scale;
				this.text = value;
			}
		}

		public virtual float Scale
		{
			get { return this.scale; }
			set { this.scale = value; }
		}
		#endregion

		#region Methods
		public virtual void Update() {}
		#endregion
	}
}