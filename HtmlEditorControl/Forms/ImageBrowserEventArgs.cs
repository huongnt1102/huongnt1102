using System;
namespace MSDN.Html.Editor
{
	public class ImageBrowserEventArgs : EventArgs
	{
		private string _ImageUrl;
		public string ImageUrl
		{
			get
			{
				return this._ImageUrl;
			}
			set
			{
				this._ImageUrl = value;
			}
		}
	}
}
