using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// 解压类
	public class UnZipUtil
	{
		/// 解压功能(解压压缩文件到指定目录)
		///
		/// 待解压的文件
		/// 指定解压目标目录
		public static void UnZip(string FileToUpZip, string ZipedFolder, string Password)
		{
			if (File.Exists(FileToUpZip))
			{
				if (!Directory.Exists(ZipedFolder))
				{
					Directory.CreateDirectory(ZipedFolder);
				}
				ZipInputStream s2 = null;
				ZipEntry theEntry2 = null;
				FileStream streamWriter2 = null;
				try
				{
					s2 = new ZipInputStream(File.OpenRead(FileToUpZip));
					s2.Password = Password;
					while ((theEntry2 = s2.GetNextEntry()) != null)
					{
						if (theEntry2.Name != string.Empty)
						{
							string fileName = Path.Combine(ZipedFolder, theEntry2.Name);
							if (fileName.IndexOf("/") >= 0)
							{
								fileName = fileName.Replace("/", "\\");
								if (!Directory.Exists(Path.GetDirectoryName(fileName)))
								{
									Directory.CreateDirectory(Path.GetDirectoryName(fileName));
								}
								if (theEntry2.Name.EndsWith("/") || theEntry2.Name.EndsWith("//"))
								{
									continue;
								}
							}
							using (streamWriter2 = File.Create(fileName))
							{
								int size2 = 2048;
								byte[] data = new byte[2048];
								while (true)
								{
									size2 = s2.Read(data, 0, data.Length);
									if (size2 <= 0)
									{
										break;
									}
									streamWriter2.Write(data, 0, size2);
								}
							}
							if (streamWriter2 != null)
							{
								streamWriter2.Close();
								streamWriter2.Dispose();
							}
						}
					}
				}
				finally
				{
					if (streamWriter2 != null)
					{
						streamWriter2.Close();
						streamWriter2.Dispose();
						streamWriter2 = null;
					}
					if (theEntry2 != null)
					{
						theEntry2 = null;
					}
					if (s2 != null)
					{
						s2.Close();
						s2.Dispose();
						s2 = null;
					}
					GC.Collect();
					GC.Collect(1);
				}
			}
		}
	}
}
