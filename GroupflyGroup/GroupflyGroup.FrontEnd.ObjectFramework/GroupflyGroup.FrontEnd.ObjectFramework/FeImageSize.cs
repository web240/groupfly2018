using GroupflyGroup.Platform.ObjectFramework;
using System;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// <summary>
	/// 图片尺寸
	/// </summary>
	[Serializable]
	public class FeImageSize : Objekt
	{
		/// <summary>
		/// 图片名称。
		/// </summary>
		public string Name
		{
			get
			{
				return GetProperty<string>("name");
			}
			set
			{
				SetProperty("name", value);
			}
		}

		/// <summary>
		/// 图片配置参数。
		/// </summary>
		public int Height
		{
			get
			{
				return GetProperty<int>("height");
			}
			set
			{
				SetProperty("height", value);
			}
		}

		/// <summary>
		/// 图片配置的宽度。
		/// </summary>
		public int Width
		{
			get
			{
				return GetProperty<int>("width");
			}
			set
			{
				SetProperty("width", value);
			}
		}
	}
}
