// TODO document this file
using System;

namespace ThinKim
{
	public class Record : CenteredTextSprite
	{
		#region Constants
		private const string kPreText = "Best: ";
		#endregion

		#region Properties
		public override string Text
		{
			set { base.Text = kPreText + value; }
		}

		public long Value
		{
			get { return Convert.ToInt64(this.text.Replace(kPreText, "")); }
			set { this.Text = value.ToString(); }
		}
		#endregion
	}
}