// TODO document this file
using System;
using System.Collections.Generic;

namespace ThinKim
{
	public abstract class Behavior
	{
		#region Fields
		protected List<Sprite> attachedSprites = new List<Sprite>();
		protected List<SpriteCollection> attachedSpriteCollections = new List<SpriteCollection>();
		#endregion

		#region Methods
		public virtual void Update()
		{
			foreach (Sprite sprite in this.attachedSprites)
				if (sprite == null)
					this.attachedSprites.Remove(sprite);
			
			foreach (SpriteCollection sc in this.attachedSpriteCollections)
				if (sc == null)
					this.attachedSpriteCollections.Remove(sc);
		}

		public virtual void AttachSprite(Sprite sprite)
		{
			this.attachedSprites.Add(sprite);
		}

		public virtual void DetachSprite(Sprite sprite)
		{
			this.attachedSprites.Remove(sprite);
		}

		public virtual void AttachSpriteCollection(SpriteCollection spriteCollection)
		{
			this.attachedSpriteCollections.Add(spriteCollection);
		}

		public virtual void DetachSpriteCollection(SpriteCollection spriteCollection)
		{
			this.attachedSpriteCollections.Remove(spriteCollection);
		}
		#endregion
	}
}