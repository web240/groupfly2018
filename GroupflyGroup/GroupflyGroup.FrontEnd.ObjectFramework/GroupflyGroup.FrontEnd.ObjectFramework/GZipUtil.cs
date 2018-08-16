using System;
using System.IO;
using System.IO.Compression;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// <summary>
	/// 利用GzipStream进行压缩和解压
	/// </summary>
	public class GZipUtil
	{
		private static GZipStream gZipStream = null;

		/// <summary>
		/// 压缩
		/// </summary>
		/// <param name="srcBytes"></param>
		/// <returns></returns>
		public static byte[] Compress(byte[] srcBytes)
		{
			MemoryStream ms = new MemoryStream(srcBytes);
			gZipStream = new GZipStream(ms, CompressionMode.Compress);
			gZipStream.Write(srcBytes, 0, srcBytes.Length);
			gZipStream.Close();
			return ms.ToArray();
		}

		/// <summary>
		/// 解压
		/// </summary>
		/// <param name="srcBytes"></param>
		/// <returns></returns>
		public static byte[] Decompress(byte[] buffer)
		{
			MemoryStream ms = new MemoryStream();
			gZipStream = new GZipStream(ms, CompressionMode.Decompress, true);
			gZipStream.Write(buffer, 0, buffer.Length);
			gZipStream.Close();
			ms.Position = 0L;
			GZipStream zipStream = new GZipStream(ms, CompressionMode.Decompress);
			Console.WriteLine("Decompression");
			byte[] decompressedBuffer = new byte[buffer.Length + 100];
			int totalCount = ReadAllBytesFromStream(zipStream, decompressedBuffer);
			Console.WriteLine("Decompressed {0} bytes", totalCount);
			if (!CompareData(buffer, buffer.Length, decompressedBuffer, totalCount))
			{
				Console.WriteLine("Error. The two buffers did not compare.");
			}
			zipStream.Close();
			return decompressedBuffer;
		}

		/// <summary>
		/// 将指定的字节数组压缩,并写入到目标文件
		/// </summary>
		/// <param name="srcBuffer">指定的源字节数组</param>
		/// <param name="destFile">指定的目标文件</param>
		public static void CompressData(byte[] srcBuffer, string destFile)
		{
			FileStream destStream = null;
			GZipStream compressedStream = null;
			try
			{
				destStream = new FileStream(destFile, FileMode.OpenOrCreate, FileAccess.Write);
				compressedStream = new GZipStream(destStream, CompressionMode.Compress, true);
				compressedStream.Write(srcBuffer, 0, srcBuffer.Length);
			}
			catch (Exception ex)
			{
				throw new Exception($"压缩数据写入文件{destFile}时发生错误", ex);
			}
			finally
			{
				if (compressedStream != null)
				{
					compressedStream.Close();
					compressedStream.Dispose();
				}
				destStream?.Close();
			}
		}

		/// <summary>
		/// 将指定的文件解压,返回解压后的数据
		/// </summary>
		/// <param name="srcFile">指定的源文件</param>
		/// <returns>解压后得到的数据</returns>
		public static byte[] DecompressData(string srcFile)
		{
			if (File.Exists(srcFile))
			{
				FileStream sourceStream = null;
				GZipStream decompressedStream = null;
				byte[] quartetBuffer2 = null;
				try
				{
					sourceStream = new FileStream(srcFile, FileMode.Open, FileAccess.Read, FileShare.Read);
					decompressedStream = new GZipStream(sourceStream, CompressionMode.Decompress, true);
					quartetBuffer2 = new byte[4];
					long position = sourceStream.Position = sourceStream.Length - 4;
					sourceStream.Read(quartetBuffer2, 0, 4);
					int checkLength = BitConverter.ToInt32(quartetBuffer2, 0);
					byte[] data = (checkLength > sourceStream.Length) ? new byte[checkLength + 100] : new byte[32767];
					byte[] buffer = new byte[100];
					sourceStream.Position = 0L;
					int offset = 0;
					int total = 0;
					while (true)
					{
						int bytesRead = decompressedStream.Read(buffer, 0, 100);
						if (bytesRead == 0)
						{
							break;
						}
						buffer.CopyTo(data, offset);
						offset += bytesRead;
						total += bytesRead;
					}
					byte[] actualdata = new byte[total];
					for (int i = 0; i < total; i++)
					{
						actualdata[i] = data[i];
					}
					return actualdata;
				}
				catch (Exception ex)
				{
					throw new Exception($"从文件{srcFile}解压数据时发生错误", ex);
				}
				finally
				{
					sourceStream?.Close();
					decompressedStream?.Close();
				}
			}
			throw new FileNotFoundException($"找不到指定的文件{srcFile}");
		}

		public static int ReadAllBytesFromStream(Stream stream, byte[] buffer)
		{
			int offset = 0;
			int totalCount = 0;
			while (true)
			{
				int bytesRead = stream.Read(buffer, offset, 100);
				if (bytesRead == 0)
				{
					break;
				}
				offset += bytesRead;
				totalCount += bytesRead;
			}
			return totalCount;
		}

		public static bool CompareData(byte[] buf1, int len1, byte[] buf2, int len2)
		{
			if (len1 == len2)
			{
				for (int i = 0; i < len1; i++)
				{
					if (buf1[i] != buf2[i])
					{
						Console.WriteLine("byte {0} is different {1}|{2}", i, buf1[i], buf2[i]);
						return false;
					}
				}
				Console.WriteLine("All bytes compare.");
				return true;
			}
			Console.WriteLine("Number of bytes in two buffer are different {0}:{1}", len1, len2);
			return false;
		}
	}
}
