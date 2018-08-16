using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	public class ZipUtil
	{
		/// 压缩类
		/// 递归压缩文件夹方法
		private static bool ZipFileDictory(string FolderToZip, ZipOutputStream s, string ParentFolderName)
		{
			bool res = true;
			ZipEntry entry = null;
			FileStream fs2 = null;
			Crc32 crc = new Crc32();
			try
			{
				entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip) + "/"));
				s.PutNextEntry(entry);
				s.Flush();
				string[] filenames = Directory.GetFiles(FolderToZip);
				string[] array = filenames;
				foreach (string file in array)
				{
					fs2 = File.OpenRead(file);
					byte[] buffer = new byte[fs2.Length];
					fs2.Read(buffer, 0, buffer.Length);
					entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip) + "/" + Path.GetFileName(file)));
					entry.ExternalFileAttributes = (int)File.GetAttributes(file);
					entry.DateTime = DateTime.Now;
					entry.Size = fs2.Length;
					fs2.Close();
					crc.Reset();
					crc.Update(buffer);
					entry.Crc = crc.Value;
					s.PutNextEntry(entry);
					s.Write(buffer, 0, buffer.Length);
				}
			}
			catch
			{
				res = false;
			}
			finally
			{
				if (fs2 != null)
				{
					fs2.Close();
					fs2 = null;
				}
				if (entry != null)
				{
					entry = null;
				}
				GC.Collect();
				GC.Collect(1);
			}
			string[] folders = Directory.GetDirectories(FolderToZip);
			string[] array2 = folders;
			foreach (string folder in array2)
			{
				if (!ZipFileDictory(folder, s, Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip))))
				{
					return false;
				}
			}
			return res;
		}

		/// 压缩目录
		///
		/// 待压缩的文件夹，全路径格式
		/// 压缩后的文件名，全路径格式
		private static bool ZipFileDictory(string FolderToZip, string ZipedFile, string Password)
		{
			if (Directory.Exists(FolderToZip))
			{
				ZipOutputStream s = new ZipOutputStream(File.Create(ZipedFile));
				s.SetLevel(6);
				s.Password = Password;
				bool res = ZipFileDictory(FolderToZip, s, "");
				s.Finish();
				s.Close();
				return res;
			}
			return false;
		}

		/// 压缩文件
		///
		/// 要进行压缩的文件名
		/// 压缩后生成的压缩文件名
		private static bool ZipFile(string FileToZip, string ZipedFile, string Password)
		{
			if (!File.Exists(FileToZip))
			{
				throw new FileNotFoundException("指定要压缩的文件: " + FileToZip + " 不存在!");
			}
			FileStream ZipFile2 = null;
			ZipOutputStream ZipStream = null;
			ZipEntry ZipEntry2 = null;
			bool res = true;
			try
			{
				ZipFile2 = File.OpenRead(FileToZip);
				byte[] buffer = new byte[ZipFile2.Length];
				ZipFile2.Read(buffer, 0, buffer.Length);
				ZipFile2.Close();
				ZipFile2 = File.Create(ZipedFile);
				ZipStream = new ZipOutputStream(ZipFile2);
				ZipStream.Password = Password;
				ZipEntry2 = new ZipEntry(Path.GetFileName(FileToZip));
				ZipStream.PutNextEntry(ZipEntry2);
				ZipStream.SetLevel(6);
				ZipStream.Write(buffer, 0, buffer.Length);
			}
			catch
			{
				res = false;
			}
			finally
			{
				if (ZipEntry2 != null)
				{
					ZipEntry2 = null;
				}
				if (ZipStream != null)
				{
					ZipStream.Finish();
					ZipStream.Close();
				}
				if (ZipFile2 != null)
				{
					ZipFile2.Close();
					ZipFile2 = null;
				}
				GC.Collect();
				GC.Collect(1);
			}
			return res;
		}

		/// 压缩文件 和 文件夹
		/// 待压缩的文件或文件夹，全路径格式
		/// 压缩后生成的压缩文件名，全路径格式
		public static bool Zip(string FileToZip, string ZipedFile, string Password)
		{
			if (!Directory.Exists(FileToZip))
			{
				if (!File.Exists(FileToZip))
				{
					return false;
				}
				return ZipFile(FileToZip, ZipedFile, Password);
			}
			return ZipFileDictory(FileToZip, ZipedFile, Password);
		}
	}
}
