using GroupflyGroup.Platform.ObjectFramework;
using GroupflyGroup.Platform.ObjectFramework.Persistence;
using System;
using System.Collections.Generic;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// <summary>
	///     前端文件
	/// </summary>
	public class FeFile
	{
		/// <summary>
		///     获取文件对象
		/// </summary>
		public File File
		{
			get;
		}

		/// <summary>
		///     当前文件是否为图片包
		/// </summary>
		public bool IsImagePack
		{
			get
			{
				if (!File.IsDirectory || !File.Name.EndsWith(".feimgpack"))
				{
					return false;
				}
				return true;
			}
		}

		/// <summary>
		///     希望宽度，图片缩放时，希望缩放为300*300的尺寸，但为了规避不失真，实际尺寸并不是希望的尺寸。
		///     实际尺寸可查看文件中的width和height
		/// </summary>
		public int? HopeWidth
		{
			get;
			set;
		}

		/// <summary>
		///     希望高度，图片缩放时，希望缩放为300*300的尺寸，但为了规避不失真，实际尺寸并不是希望的尺寸。
		///     实际尺寸可查看文件中的width和height
		/// </summary>
		public int? HopeHeight
		{
			get;
			set;
		}

		/// <summary>
		/// </summary>
		/// <param name="fileId"></param>
		public FeFile(string fileId)
			: this(ObjektFactory.Find<File>(fileId))
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="file"></param>
		public FeFile(File file)
		{
			File = file;
		}

		/// <summary>
		///     获取图片包中原图文件
		/// </summary>
		/// <returns></returns>
		public File SourceImageFile()
		{
			if (!IsImagePack)
			{
				throw new Exception("当前文件并不是图片包");
			}
			string fileName = File.Name.Substring(0, File.Name.Length - 10);
			string sql = "\"parent\" = '" + File.Id + "' and \"name\" = '" + fileName + "'";
			ObjektCollection<File> oc = new ObjektCollection<File>(Klass.ForId("File@Klass"), new WhereClause(sql));
			return oc.GetSingleResult();
		}

		/// <summary>
		///     获取图片包中所有尺寸图片列表
		/// </summary>
		/// <returns></returns>
		public List<FeFile> ImageSizeList()
		{
			List<FeFile> list = new List<FeFile>();
			File sourceFile = SourceImageFile();
			if (sourceFile != null)
			{
				foreach (File childrenFile in File.GetChildrenFiles())
				{
					string fileName3 = childrenFile.Name;
					fileName3 = fileName3.Substring(0, fileName3.Length - childrenFile.ExtensionName.Length - 1);
					int? width = null;
					int? height = null;
					string sourceName2 = sourceFile.Name;
					sourceName2 = sourceName2.Substring(0, sourceName2.Length - sourceFile.ExtensionName.Length - 1);
					if (fileName3.StartsWith(sourceName2))
					{
						fileName3 = fileName3.Substring(sourceName2.Length);
						string[] tmp = fileName3.Split(new string[1]
						{
							"_"
						}, StringSplitOptions.RemoveEmptyEntries);
						if (tmp.Length == 2)
						{
							int size;
							if (tmp[tmp.Length - 2] == "*")
							{
								width = -1;
							}
							else if (int.TryParse(tmp[tmp.Length - 2], out size))
							{
								width = size;
							}
							if (tmp[tmp.Length - 1] == "*")
							{
								height = -1;
							}
							else if (int.TryParse(tmp[tmp.Length - 1], out size))
							{
								height = size;
							}
							if (width.HasValue && height.HasValue)
							{
								list.Add(new FeFile(childrenFile)
								{
									HopeWidth = width,
									HopeHeight = height
								});
							}
						}
					}
				}
			}
			return list;
		}
	}
}
