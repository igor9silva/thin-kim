// TODO document this file
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
//using MonoTouch.CoreText;
using System.Net.Mime;
using System.Threading;

namespace ThinKim
{
	public class STPSpriteBatch : SpriteBatch
	{
		public STPSpriteBatch(GraphicsDevice g) : base(g) { }

		public void Draw(Texture2D tex, Vector2 pos, float scale)
		{
			this.Draw(tex, 							 	// Texture
			          ScalePos(pos, tex.Height, scale),	// Position
			          null, 							// Source Rectangle
			          Color.White, 						// Color
			          0f, 								// Rotation
			          Vector2.Zero, 					// Origin
			          scale, 							// Scale
			          SpriteEffects.None, 				// Sprite Effect
			          0); 								// Layer Depth
		}

		public void Draw(Texture2D tex, Vector2 pos, float scale, Color color)
		{
			this.Draw(tex, 							 	// Texture
			          ScalePos(pos, tex.Height, scale),	// Position
			          null, 							// Source Rectangle
			          color, 						// Color
			          0f, 								// Rotation
			          Vector2.Zero, 					// Origin
			          scale, 							// Scale
			          SpriteEffects.None, 				// Sprite Effect
			          0); 								// Layer Depth
		}

		public void Draw(Texture2D tex, Rectangle rect, Color color, float scale)
		{
			Vector2 temp = ScalePos(new Vector2(rect.Location.X, rect.Location.Y), rect.Height, scale);
			rect.X = (int)temp.X;
			rect.Y = (int)temp.Y;
			rect.Width = (int)(rect.Width * scale);
			rect.Height = (int)(rect.Height * scale);
			
			this.Draw(tex, rect, color);
		}

		public void DrawString(SpriteFont font, string text, Vector2 pos, Color color, float scale)
		{
			
			this.DrawString(font,				// Font
			                text,				// Text
			                pos,				// Position
			                color,				// Color
			                0f,					// Rotation
			                Vector2.Zero,		// Origin
			                scale,				// Scale
			                SpriteEffects.None, // SpriteEffect
			                0f);				// Layer Depth
		}

		public void DrawCenteredString(SpriteFont font, string text, Vector2 pos, Color color, float scale)
		{
			this.DrawCenteredString(font, text, pos, color, new Vector2(scale, scale));
		}

		public void DrawCenteredString(SpriteFont font, string text, Vector2 pos, Color color, Vector2 scale)
		{
			Vector2 size = font.MeasureString(text) * scale;
			pos.X -= size.X / 2;
			pos.Y -= size.Y;
			//Console.WriteLine("Size.Y: {0} | Pos.Y: {1}", size.Y, pos.Y);
			
			this.DrawString(font,								// Font
			                text,								// Text
			                pos,								// Position
			                color,								// Color
			                0f,									// Rotation
			                Vector2.Zero,						// Origin
			                scale,								// Scale
			                SpriteEffects.None, 				// SpriteEffect
			                0f);								// Layer Depth
		}

		public static Vector2 ScalePos(Vector2 pos, int height, float scale)
		{
			return new Vector2(pos.X * scale, pos.Y - ((height * scale) - height));
		}
	}
}