// TODO document this file
using System;
using OpenTK.Graphics.ES11;
using Microsoft.Xna.Framework;
//using MonoTouch.Foundation;
using System.Runtime.InteropServices;

namespace ThinKim
{
	public class Ground : Sprite
	{
		#region Fields
		private float y;
		private float[] x = new float[2];
		#endregion

		#region Constructor
		public Ground() : base() { }
		#endregion

		#region Properties
		public override float X1
		{
			get
			{
				return this.x[0];
			}
			set
			{
				float dif = this.x[0] - value;
				this.x[1] -= dif;
				this.x[0] = value;
			}
		}
		#endregion

		#region Methods
		public void Initialize(int screenHeight)
		{
			this.x[0] = 0;
			this.x[1] = this.Width;
			this.y = screenHeight - this.Height;
		}

		new public Vector2 Position(byte i)
		{	
			return new Vector2(x[i], y);
		}

		public override void Update()
		{
			for (byte i = 0; i < 2; i++)
			{
				if (this.x[i] + this.Width <= 0)
				{
					this.x[i == 0 ? 1 : 0] = 0;
					this.x[i] = this.Width;
				}
			}
		}
		#endregion
	}
}

