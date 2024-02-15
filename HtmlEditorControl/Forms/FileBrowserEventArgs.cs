using System;
namespace MSDN.Html.Editor
{
	public class FileBrowserEventArgs : EventArgs
	{
		private string _FileUrl;
		public string FileUrl
		{
			get
			{
				return this._FileUrl;
			}
			set
			{
				this._FileUrl = value;
			}
		}
	}
}
