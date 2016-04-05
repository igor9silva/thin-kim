// TODO document this file
using System;
using Microsoft.Xna.Framework;
//using MonoTouch;

namespace ThinKim
{
	public class CenteredTextSprite : TextSprite
	{
		#region Fields
		private int screenWidth;
		#endregion

		#region Constructor
		public CenteredTextSprite() : base() {}
		#endregion

		#region Properties
		public override string Text
		{
			set
			{
				base.Text = value;
				float x = screenWidth / 2 - this.size.X / 2;
				this.position.X = x;
			}
		}
		#endregion

		#region Methods
		public void Initialize(int screenWidth, float y)
		{
			this.screenWidth = screenWidth;
			this.position.Y = y;
		}

		public void Initialize(int screenWidth, float scale, float y)
		{
			this.Initialize(screenWidth, y);
			this.scale = scale;
		}
		#endregion
	}
}