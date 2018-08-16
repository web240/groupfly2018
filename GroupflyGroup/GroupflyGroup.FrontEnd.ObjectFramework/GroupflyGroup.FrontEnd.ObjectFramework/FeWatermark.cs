using GroupflyGroup.Platform.ObjectFramework;
using System;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// <summary>
	/// 图片尺寸
	/// </summary>
	[Serializable]
	public class FeWatermark : Objekt
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
		/// 水印类型
		/// </summary>
		public Value Type
		{
			get
			{
				return GetProperty<Value>("type");
			}
			set
			{
				SetProperty("type", value);
			}
		}

		/// <summary>
		/// 文字内容
		/// </summary>
		public string Text
		{
			get
			{
				return GetProperty<string>("text");
			}
			set
			{
				SetProperty("text", value);
			}
		}

		/// <summary>
		/// 文字字体
		/// </summary>
		public Value Font
		{
			get
			{
				return GetProperty<Value>("font");
			}
			set
			{
				SetProperty("font", value);
			}
		}

		/// <summary>
		/// 字体大小
		/// </summary>
		public Value FontSize
		{
			get
			{
				return GetProperty<Value>("fontSize");
			}
			set
			{
				SetProperty("fontSize", value);
			}
		}

		/// <summary>
		/// 字体颜色
		/// </summary>
		public string FontColor
		{
			get
			{
				return GetProperty<string>("fontColor");
			}
			set
			{
				SetProperty("fontColor", value);
			}
		}

		/// <summary>
		/// 字体透明度
		/// </summary>
		public int Transparency
		{
			get
			{
				return GetProperty<int>("transparency");
			}
			set
			{
				SetProperty("transparency", value);
			}
		}

		/// <summary>
		/// 字体显示位置
		/// </summary>
		public Value Location
		{
			get
			{
				return GetProperty<Value>("location");
			}
			set
			{
				SetProperty("location", value);
			}
		}

		/// <summary>
		/// 是否启用
		/// </summary>
		public bool Enabled
		{
			get
			{
				return GetProperty<bool>("enabled");
			}
			set
			{
				SetProperty("enabled", value);
			}
		}

		/// <summary>
		/// 水印图片
		/// </summary>
		public File imageFile
		{
			get
			{
				return GetProperty<File>("imageFile");
			}
			set
			{
				SetProperty("imageFile", value);
			}
		}
	}
}
