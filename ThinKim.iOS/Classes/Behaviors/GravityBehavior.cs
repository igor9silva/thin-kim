//TODO document this file
using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ThinKim
{
	public class GravityBehavior : Behavior
	{
		#region Constructor
		public GravityBehavior(int groundY, float gravity)
		{
			this.groundY = groundY;
			this.gravity = gravity;
		}
		#endregion

		#region Fields
		private float gravity;
		private int groundY;
		private List<float> spriteCurrentAcceleration = new List<float>();
		private List<List<float>> collectionCurrentAcceleration = new List<List<float>>();
		#endregion

		#region Properties
		// TODO gravity and groundY public properties
		#endregion

		#region Methods
		public override void AttachSprite(Sprite sprite)
		{
			base.AttachSprite(sprite);
			this.spriteCurrentAcceleration.Add(this.gravity);
		}

		public override void AttachSpriteCollection(SpriteCollection spriteCollection)
		{
			base.AttachSpriteCollection(spriteCollection);
			
			List<float> gravities = new List<float>(spriteCollection.Count);
			for (int i = 0; i < spriteCollection.Count; i++)
				gravities.Add(this.gravity);
			
			spriteCollection.SpriteAdded += (i) => 
			{
				this.collectionCurrentAcceleration[this.attachedSpriteCollections.IndexOf(spriteCollection)].Add(this.gravity);
			};
			
			spriteCollection.SpriteRemoved += (i) => 
			{
				this.collectionCurrentAcceleration[this.attachedSpriteCollections.IndexOf(spriteCollection)].RemoveAt(i);
			};
				
			this.collectionCurrentAcceleration.Add(gravities);
		}

		public override void DetachSprite(Sprite sprite)
		{
			this.spriteCurrentAcceleration.RemoveAt(this.attachedSprites.IndexOf(sprite));
			base.DetachSprite(sprite);
		}

		public override void DetachSpriteCollection(SpriteCollection spriteCollection)
		{
			this.collectionCurrentAcceleration.RemoveAt(this.attachedSpriteCollections.IndexOf(spriteCollection));
			base.DetachSpriteCollection(spriteCollection);
		}

		public override void Update()
		{
			base.Update(); // base MUST be called to AVOID memory leaks
			foreach (Sprite sprite in this.attachedSprites)
			{
				int index = this.attachedSprites.IndexOf(sprite);
				if (sprite.Y2 < this.groundY)
				{
					sprite.Y2 += this.spriteCurrentAcceleration[index];
					this.spriteCurrentAcceleration[index] += this.gravity;
				}
				
//				if (sprite.Y2 > this.groundY)
//					sprite.Y2 = this.groundY;
				
				if (sprite.Y2 == this.groundY)
					this.spriteCurrentAcceleration[index] = this.gravity;				
			}
			
			foreach (SpriteCollection sc in this.attachedSpriteCollections)
			{
				for (int i = 0; i < sc.Count; i++)
				{
					int index = this.attachedSpriteCollections.IndexOf(sc);
					if (sc.GetY2(i) < this.groundY)
					{
						sc.SetY2(i, sc.GetY2(i) + this.collectionCurrentAcceleration[index][i]);
						this.collectionCurrentAcceleration[index][i] += this.gravity;
					}
					
					if (sc.GetY2(i) > this.groundY)
						sc.SetY2(i, this.groundY);
					
					if (sc.GetY2(i) == this.groundY)
						this.collectionCurrentAcceleration[index][i] = this.gravity;
				}
			}
		}
		#endregion
	}
}

