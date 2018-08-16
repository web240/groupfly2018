using System.Text.RegularExpressions;
using System.Web;

namespace GroupflyGroup.FrontEnd.Service
{
	/// <summary>
	///     html格式化
	/// </summary>
	public static class HtmlFormat
	{
		/// <summary>
		///     把带标签的html字符串转换为纯文本
		/// </summary>
		/// <param name="htmlString"></param>
		/// <returns></returns>
		public static string NoHtml(string htmlString)
		{
			htmlString = htmlString.Replace("\r\n", "");
			htmlString = Regex.Replace(htmlString, "<script.*?</script>", "", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "<style.*?</style>", "", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "<.*?>", "", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "([\\r\\n])[\\s]+", "", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "&ndash;", "-", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "&ldquo;", "", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "&rdquo;", "", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "-->", "", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "<!--.*", "", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "&(nbsp|#160);", "", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "&(iexcl|#161);", "¡", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "&(cent|#162);", "¢", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "&(pound|#163);", "£", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "&(copy|#169);", "©", RegexOptions.IgnoreCase);
			htmlString = Regex.Replace(htmlString, "&#(\\d+);", "", RegexOptions.IgnoreCase);
			htmlString = htmlString.Replace("<", "");
			htmlString = htmlString.Replace(">", "");
			htmlString = htmlString.Replace("\r\n", "");
			htmlString = HttpContext.Current.Server.HtmlEncode(htmlString).Trim();
			return htmlString;
		}
	}
}
