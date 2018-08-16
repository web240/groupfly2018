using GroupflyGroup.FrontEnd.ObjectFramework;
using GroupflyGroup.Platform.ObjectFramework;
using GroupflyGroup.Platform.ObjectFramework.Persistence;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace GroupflyGroup.FrontEnd.Service
{
	/// <summary>
	///     图片操作
	/// </summary>
	public class ImageOperator
	{
		/// <summary>
		///     按指定尺寸缩放图片
		/// </summary>
		/// <param name="img"></param>
		/// <param name="destWidth"></param>
		/// <param name="destHeight"></param>
		/// <returns></returns>
		public Bitmap Zoom(Image img, int destWidth, int destHeight)
		{
			ImageFormat thisFormat = img.RawFormat;
			int sW2 = 0;
			int sH2 = 0;
			int sWidth = img.Width;
			int sHeight = img.Height;
			if (sHeight > destHeight || sWidth > destWidth)
			{
				if (sWidth * destHeight > sHeight * destWidth)
				{
					sW2 = destWidth;
					sH2 = destWidth * sHeight / sWidth;
				}
				else
				{
					sH2 = destHeight;
					sW2 = sWidth * destHeight / sHeight;
				}
			}
			else
			{
				sW2 = sWidth;
				sH2 = sHeight;
			}
			Bitmap outBmp = new Bitmap(destWidth, destHeight);
			Graphics g = Graphics.FromImage(outBmp);
			g.Clear(Color.Transparent);
			g.CompositingQuality = CompositingQuality.HighQuality;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			g.DrawImage(img, new Rectangle((destWidth - sW2) / 2, (destHeight - sH2) / 2, sW2, sH2), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
			g.Dispose();
			EncoderParameters encoderParams = new EncoderParameters();
			EncoderParameter encoderParam = new EncoderParameter(value: new long[1]
			{
				100L
			}, encoder: Encoder.Quality);
			encoderParams.Param[0] = encoderParam;
			img.Dispose();
			return outBmp;
		}

		/// <summary>
		///     按尺寸缩放图片
		/// </summary>
		/// <param name="imgStream"></param>
		/// <param name="destWidth"></param>
		/// <param name="destHeight"></param>
		/// <returns></returns>
		public Bitmap Zoom(Stream imgStream, int destWidth, int destHeight)
		{
			Bitmap bmp = new Bitmap(imgStream);
			return Zoom(bmp, destWidth, destHeight);
		}

		/// <summary>
		///     按高度缩放图片，自动计算宽度
		/// </summary>
		/// <param name="img"></param>
		/// <param name="destHeight"></param>
		/// <returns></returns>
		public Bitmap ZoomByHeight(Bitmap img, int destHeight)
		{
			int sWidth = img.Width;
			int sHeight = img.Height;
			int destWidth2 = 0;
			destWidth2 = ((destHeight <= sHeight) ? ((int)((double)sWidth / ((double)sHeight / (double)destHeight))) : ((int)((double)destHeight / (double)sHeight * (double)sWidth)));
			return Zoom(img, destWidth2, destHeight);
		}

		/// <summary>
		///     按高度缩放图片，自动计算宽度
		/// </summary>
		/// <param name="imgStream"></param>
		/// <param name="destHeight"></param>
		/// <returns></returns>
		public Bitmap ZoomByHeight(Stream imgStream, int destHeight)
		{
			Bitmap bmp = new Bitmap(imgStream);
			return ZoomByHeight(bmp, destHeight);
		}

		/// <summary>
		///     按宽度缩放图片，自动计算高度
		/// </summary>
		/// <param name="img"></param>
		/// <param name="destWidth"></param>
		/// <returns></returns>
		public Bitmap ZoomByWidth(Bitmap img, int destWidth)
		{
			int sWidth = img.Width;
			int sHeight = img.Height;
			int destHeight2 = 0;
			destHeight2 = ((destWidth <= sWidth) ? ((int)((double)sHeight / ((double)sWidth / (double)destWidth))) : ((int)((double)destWidth / (double)sWidth * (double)sHeight)));
			return Zoom(img, destWidth, destHeight2);
		}

		/// <summary>
		///     按宽度缩放图片，自动计算高度
		/// </summary>
		/// <param name="imgStream"></param>
		/// <param name="destWidth"></param>
		/// <returns></returns>
		public Bitmap ZoomByWidth(Stream imgStream, int destWidth)
		{
			Bitmap bmp = new Bitmap(imgStream);
			return ZoomByWidth(bmp, destWidth);
		}

		/// <summary>
		///     图片剪切
		/// </summary>
		/// <param name="img"></param>
		/// <param name="startX"></param>
		/// <param name="startY"></param>
		/// <param name="destWidth"></param>
		/// <param name="destHeight"></param>
		/// <returns></returns>
		public Bitmap Cut(Bitmap img, int startX, int startY, int destWidth, int destHeight)
		{
			int w = img.Width;
			int h = img.Height;
			if (startX >= w || startY >= h)
			{
				throw new Exception("起始点大于原图尺寸");
			}
			if (startX + destWidth > w)
			{
				destWidth = w - startX;
			}
			if (startY + destHeight > h)
			{
				destHeight = h - startY;
			}
			Bitmap bmpOut = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
			Graphics g = Graphics.FromImage(bmpOut);
			g.DrawImage(img, new Rectangle(0, 0, destWidth, destHeight), new Rectangle(startX, startY, destWidth, destHeight), GraphicsUnit.Pixel);
			g.Dispose();
			return bmpOut;
		}

		/// <summary>
		///     图片剪切
		/// </summary>
		/// <param name="imgStream"></param>
		/// <param name="startX"></param>
		/// <param name="startY"></param>
		/// <param name="destWidth"></param>
		/// <param name="destHeight"></param>
		/// <returns></returns>
		public Bitmap Cut(Stream imgStream, int startX, int startY, int destWidth, int destHeight)
		{
			Bitmap bmp = new Bitmap(imgStream);
			return Cut(bmp, startX, startY, destWidth, destHeight);
		}

		/// <summary>
		/// 生成水印
		/// </summary>
		/// <param name="warterid"></param>
		/// <param name="imgStream"></param>
		/// <returns></returns>
		public Stream CreateWarterImage(string warterid, Stream imgStream)
		{
			FeWatermark watermarkType = ObjektFactory.Find<FeWatermark>(warterid);
			MemoryStream result = null;
			double inttransparency2 = 1.0;
			string tlocationValueName = watermarkType.Location.Value_;
			using (Bitmap bitmap = new Bitmap(imgStream))
			{
				if (watermarkType.Type.Id == "af8d0384224e46e7a556ef06a019b1bd@Value")
				{
					string tfontSizeName = watermarkType.FontSize.Value_;
					string tfontfamilyValueName = watermarkType.Font.Value_;
					inttransparency2 = Convert.ToDouble(watermarkType.Transparency) / 100.0 * 255.0;
					float fontsize = float.Parse(tfontSizeName);
					float textwidth = (float)watermarkType.Text.Length * fontsize;
					float rectx = 0f;
					float recty = 0f;
					float rectwidth = (float)watermarkType.Text.Length * (fontsize + 8f);
					float rectheight = fontsize + 8f;
					float imageHeight = (float)bitmap.Height;
					float imageWeight = (float)bitmap.Width;
					switch (tlocationValueName)
					{
					case "1":
						rectx = 0f;
						recty = 0f;
						break;
					case "2":
						rectx = imageWeight / 2f - rectwidth / 2f;
						recty = 0f;
						break;
					case "3":
						rectx = imageWeight - rectwidth;
						recty = 0f;
						break;
					case "4":
						rectx = 0f;
						recty = imageHeight / 2f - rectheight / 2f;
						break;
					case "5":
						rectx = imageWeight / 2f - rectwidth / 2f;
						recty = imageHeight / 2f - rectheight / 2f;
						break;
					case "6":
						rectx = imageWeight - rectwidth;
						recty = imageHeight / 2f - rectheight / 2f;
						break;
					case "7":
						rectx = 0f;
						recty = imageHeight - rectheight;
						break;
					case "8":
						rectx = imageWeight / 2f - rectwidth / 2f;
						recty = imageHeight - rectheight;
						break;
					case "9":
						rectx = imageWeight - rectwidth;
						recty = imageHeight - rectheight;
						break;
					}
					RectangleF textarea = new RectangleF(rectx, recty, rectwidth, rectheight);
					Font font = new Font(tfontfamilyValueName, fontsize);
					Color color = ColorTranslator.FromHtml(watermarkType.FontColor);
					Brush whitebrush = new SolidBrush(Color.FromArgb(Convert.ToInt32(inttransparency2), color));
					Brush blackbrush = new SolidBrush(Color.FromArgb(14492536));
					using (Graphics g = Graphics.FromImage(bitmap))
					{
						g.FillRectangle(blackbrush, rectx, recty, rectwidth, rectheight);
						StringFormat strFormat = new StringFormat();
						strFormat.Alignment = StringAlignment.Center;
						g.DrawString(watermarkType.Text, font, whitebrush, textarea, strFormat);
						result = new MemoryStream();
						bitmap.Save(result, bitmap.RawFormat);
					}
				}
				else
				{
					int phWidth = bitmap.Width;
					int phHeight = bitmap.Height;
					using (Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb))
					{
						bmPhoto.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
						using (Graphics grPhoto = Graphics.FromImage(bmPhoto))
						{
							using (Image imgWatermark = new Bitmap(watermarkType.imageFile.FileContent))
							{
								int wmWidth = imgWatermark.Width;
								int wmHeight = imgWatermark.Height;
								grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
								grPhoto.DrawImage(bitmap, new Rectangle(0, 0, phWidth, phHeight), 0, 0, phWidth, phHeight, GraphicsUnit.Pixel);
								using (Bitmap bmWatermark = new Bitmap(bmPhoto))
								{
									bmWatermark.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
									using (Graphics grWatermark = Graphics.FromImage(bmWatermark))
									{
										ImageAttributes imageAttributes = new ImageAttributes();
										ColorMap colorMap = new ColorMap();
										colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
										colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
										ColorMap[] remapTable = new ColorMap[1]
										{
											colorMap
										};
										imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
										float alpha = float.Parse(watermarkType.Transparency.ToString()) / 100f;
										float[][] colorMatrixElements = new float[5][]
										{
											new float[5]
											{
												1f,
												0f,
												0f,
												0f,
												0f
											},
											new float[5]
											{
												0f,
												1f,
												0f,
												0f,
												0f
											},
											new float[5]
											{
												0f,
												0f,
												1f,
												0f,
												0f
											},
											new float[5]
											{
												0f,
												0f,
												0f,
												alpha,
												0f
											},
											new float[5]
											{
												0f,
												0f,
												0f,
												0f,
												1f
											}
										};
										ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
										imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
										int xPosOfWm = 0;
										int yPosOfWm = 0;
										switch (tlocationValueName)
										{
										case "1":
											xPosOfWm = 0;
											yPosOfWm = 0;
											break;
										case "2":
											xPosOfWm = phWidth / 2 - wmWidth / 2;
											yPosOfWm = 0;
											break;
										case "3":
											xPosOfWm = phWidth - wmWidth;
											yPosOfWm = 0;
											break;
										case "4":
											xPosOfWm = 0;
											yPosOfWm = phHeight / 2 - wmHeight / 2;
											break;
										case "5":
											xPosOfWm = phWidth / 2 - wmWidth / 2;
											yPosOfWm = phHeight / 2 - wmHeight / 2;
											break;
										case "6":
											xPosOfWm = phWidth - wmWidth;
											yPosOfWm = phHeight / 2 - wmHeight / 2;
											break;
										case "7":
											xPosOfWm = 0;
											yPosOfWm = phHeight - wmHeight;
											break;
										case "8":
											xPosOfWm = phWidth / 2 - wmWidth / 2;
											yPosOfWm = phHeight - wmHeight;
											break;
										case "9":
											xPosOfWm = phWidth - wmWidth;
											yPosOfWm = phHeight - wmHeight;
											break;
										}
										grWatermark.DrawImage(imgWatermark, new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight), 0, 0, wmWidth, wmHeight, GraphicsUnit.Pixel, imageAttributes);
										result = new MemoryStream();
										bmWatermark.Save(result, bitmap.RawFormat);
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		/// <summary>
		/// 创建水印
		/// </summary>
		/// <param name="warterid"></param>
		/// <param name="fileid"></param>
		/// <returns></returns>
		public Stream CreateWarterImage(string warterid, string fileid)
		{
			GroupflyGroup.Platform.ObjectFramework.File waterImage = ObjektFactory.Find<GroupflyGroup.Platform.ObjectFramework.File>(fileid);
			return CreateWarterImage(warterid, waterImage.FileContent);
		}
	}
}
