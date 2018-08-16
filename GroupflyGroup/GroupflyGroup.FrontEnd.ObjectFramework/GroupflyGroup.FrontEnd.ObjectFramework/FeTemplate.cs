using GroupflyGroup.Platform.ObjectFramework;
using System;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// <summary>
	///     模板
	/// </summary>
	[Serializable]
	public class FeTemplate : Objekt
	{
		/// <summary>
		///     模板名称
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

		public File Images
		{
			get
			{
				return GetProperty<File>("image");
			}
			set
			{
				SetProperty("image", value);
			}
		}

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

		public File Directory
		{
			get
			{
				return GetProperty<File>("directory");
			}
			set
			{
				SetProperty("directory", value);
			}
		}

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

		public bool IsDefault
		{
			get
			{
				return GetProperty<bool>("isDefault");
			}
			set
			{
				SetProperty("isDefault", value);
			}
		}

		public bool IsEnable
		{
			get
			{
				return GetProperty<bool>("isEnable");
			}
			set
			{
				SetProperty("isEnable", value);
			}
		}

		/// <summary>
		///     备份源
		/// </summary>
		public FeTemplate BackupSource
		{
			get
			{
				return GetProperty<FeTemplate>("backupSource");
			}
			set
			{
				SetProperty("backupSource", value);
			}
		}

		/// <summary>
		///     是否为系统默认模板，系统默认模板不允许删除
		/// </summary>
		public bool IsSystemTemplate
		{
			get
			{
				return GetProperty<bool>("isSystemTemplate");
			}
			set
			{
				SetProperty("isSystemTemplate", value);
			}
		}
	}
}
