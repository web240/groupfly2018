using System.Collections.Generic;

namespace GroupflyGroup.FrontEnd.ObjectFramework.Strings
{
	/// <summary>
	///     FrontEnd默认文件id
	/// </summary>
	public static class FeFileIDs
	{
		/// <summary>
		///     前端系统存储图像的目录，系统预设目录
		/// </summary>
		public const string FeImages = "6dfd40570c084bda9b7989837fe24a9f@File";

		/// <summary>
		///     前端系统存储水印图像的目录，系统预设目录
		/// </summary>
		public const string FeWatermark = "19dce76dbb5c477c90f85508de2b7334@File";

		/// <summary>
		///     前端系统存储文章图像的目录，系统预设目录
		/// </summary>
		public const string FeArticle = "df9eb048617c4083b1185aea67186528@File";

		/// <summary>
		///     前端系统存储文档的目录，系统预设目录
		/// </summary>
		public const string FeDocument = "1830e60104cf4304bf8d5ce434ce7c00@File";

		/// <summary>
		///     前端系统存储文章文档的目录
		/// </summary>
		public const string FeArticleDocument = "b2902464c1bd45eaba3625eae937fb54@File";

		/// <summary>
		///     允许上传的图片类型
		/// </summary>
		public const string ImageUploadType = "2fe09f72c04b469ba3551dacde2f6698@SystemConfiguration";

		/// <summary>
		///     允许上传的文档类型
		/// </summary>
		public const string DocumentUploadType = "f3dcc3f2db1f4317a29de5b02c24f42f@SystemConfiguration";

		/// <summary>
		///     后台图片上传大小限制(KB)
		/// </summary>
		public const string BackImageUploadSize = "0d9af334e358460f803d6f5d8c76a8e3@SystemConfiguration";

		/// <summary>
		///     后台文档上传大小限制(KB)
		/// </summary>
		public const string BackDocumentUploadSize = "c85a108e01bf409db64089df3ba6c4f7@SystemConfiguration";

		/// <summary>
		///     前台图片上传大小限制(KB)
		/// </summary>
		public const string FrontImageUploadSize = "07b501f718e64632abd96ebc36046c14@SystemConfiguration";

		/// <summary>
		///     前台文档上传大小限制(KB)
		/// </summary>
		public const string FrontDocumentUploadSize = "10f3db49938b4098a5cf054b51f5d1c4@SystemConfiguration";

		/// <summary>
		/// 模板上传文件根目录
		/// </summary>
		public const string FeTemplate = "4cb9b097717d4dbdb9c5f08944e5cebc@File";

		/// <summary>
		///     需要显示锁图标，且不允许删除的文件或目录
		/// </summary>
		public static List<string> FeLockFiles = new List<string>
		{
			"6dfd40570c084bda9b7989837fe24a9f@File",
			"19dce76dbb5c477c90f85508de2b7334@File",
			"df9eb048617c4083b1185aea67186528@File",
			"1830e60104cf4304bf8d5ce434ce7c00@File",
			"b2902464c1bd45eaba3625eae937fb54@File"
		};
	}
}
