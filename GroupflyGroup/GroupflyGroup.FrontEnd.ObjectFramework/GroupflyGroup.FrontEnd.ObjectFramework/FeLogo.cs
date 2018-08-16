using GroupflyGroup.Platform.ObjectFramework;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// <summary>
	///     LOGO
	/// </summary>
	public class FeLogo : Objekt
	{
		/// <summary>
		///     LOGO对象的名称
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
		///     LOGO图像文件
		/// </summary>
		public File ImageFile
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

		/// <summary>
		///     LOGO中的文本标签
		/// </summary>
		public string Label
		{
			get
			{
				return GetProperty<string>("label");
			}
			set
			{
				SetProperty("label", value);
			}
		}

		/// <summary>
		///     点击LOGO图片后跳转链接的url
		/// </summary>
		public string Link
		{
			get
			{
				return GetProperty<string>("link");
			}
			set
			{
				SetProperty("link", value);
			}
		}
	}
}
