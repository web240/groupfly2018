using GroupflyGroup.Platform.ObjectFramework;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// <summary>
	///     模板组件
	/// </summary>
	public class FeTemplateComponent : Objekt
	{
		/// <summary>
		///     组件标签名
		/// </summary>
		public string TagName
		{
			get
			{
				return GetProperty<string>("tagName");
			}
			set
			{
				SetProperty("tagName", value);
			}
		}

		/// <summary>
		///     组件路径
		/// </summary>
		public string DirectoryPath
		{
			get
			{
				return GetProperty<string>("directoryPath");
			}
			set
			{
				SetProperty("directoryPath", value);
			}
		}

		/// <summary>
		///     组件分类
		/// </summary>
		public Value Category
		{
			get
			{
				return GetProperty<Value>("category");
			}
			set
			{
				SetProperty("category", value);
			}
		}
	}
}
