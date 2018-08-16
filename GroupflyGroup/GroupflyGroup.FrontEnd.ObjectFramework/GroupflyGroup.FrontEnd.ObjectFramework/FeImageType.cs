using GroupflyGroup.Platform.ObjectFramework;
using System;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// <summary>
	/// 频道
	/// </summary>
	[Serializable]
	public class FeImageType : Objekt
	{
		/// <summary>
		/// 频道名称。
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
		/// 频道绑定的文章分类
		/// </summary>
		public string Description
		{
			get
			{
				return GetProperty<string>("description");
			}
			set
			{
				SetProperty("description", value);
			}
		}
	}
}
