// TODO document this file
using System;
using System.Deployment.Internal;

namespace ThinKim
{
	public class PullBehavior : Behavior
	{
		#region Constructor
		public PullBehavior(short intensity, PullBehaviorDirection direction)
		{
			this.intensity = intensity;
			this.direction = direction;
		}
		#endregion

		#region Fields
		private PullBehaviorDirection direction;
		public float intensity;
		#endregion

		#region Methods
		public override void Update()
		{
			base.Update(); // base MUST be called to AVOID memory leaks
			foreach (Sprite sprite in this.attachedSprites)
			{
				switch (this.direction)
				{
					case PullBehaviorDirection.Left:
						sprite.X1 -= this.intensity;
						break;
						
					case PullBehaviorDirection.Right:
						sprite.X1 += this.intensity;
						break;
				}
			}
			
			foreach (SpriteCollection sc in this.attachedSpriteCollections)
			{
				for (int i = 0; i < sc.Count; i++)
				{
					switch (this.direction)
					{
						case PullBehaviorDirection.Left:
							sc.SetX1(i, sc.GetX1(i) - this.intensity);
							break;
							
						case PullBehaviorDirection.Right:
							sc.SetX1(i, sc.GetX1(i) + this.intensity);
							break;
					}
				}
			}
		}
		#endregion
	}

	public enum PullBehaviorDirection
	{
		Left,
		Right
	}
}