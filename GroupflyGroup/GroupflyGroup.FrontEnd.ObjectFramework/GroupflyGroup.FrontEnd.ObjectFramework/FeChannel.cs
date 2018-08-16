using GroupflyGroup.Platform.ObjectFramework;
using GroupflyGroup.Platform.ObjectFramework.Persistence;
using System;
using System.Linq;
using System.Text;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// <summary>
	///     频道
	/// </summary>
	[Serializable]
	public class FeChannel : Objekt
	{
		/// <summary>
		///     频道名称。
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
		///     频道绑定的文章分类
		/// </summary>
		public FeArticleCategory Category
		{
			get
			{
				return GetProperty<FeArticleCategory>("category");
			}
			set
			{
				SetProperty("category", value);
			}
		}

		/// <summary>
		///     频道对应的模板
		/// </summary>
		public FeTemplate Template
		{
			get
			{
				return GetProperty<FeTemplate>("template");
			}
			set
			{
				SetProperty("template", value);
			}
		}

		/// <summary>
		///     频道是否在前台显示
		/// </summary>
		public bool IsDisplay
		{
			get
			{
				return GetProperty<bool>("isDisplay");
			}
			set
			{
				SetProperty("isDisplay", value);
			}
		}

		/// <summary>
		///     频道的排序序号
		/// </summary>
		public decimal? SortOrder
		{
			get
			{
				return GetProperty<decimal>("sortOrder");
			}
			set
			{
				SetProperty("sortOrder", value);
			}
		}

		/// <summary>
		///     SEO标题
		/// </summary>
		public string SeoTitle
		{
			get
			{
				return GetProperty<string>("seoTitle");
			}
			set
			{
				SetProperty("seoTitle", value);
			}
		}

		/// <summary>
		///     SEO描述
		/// </summary>
		public string SeoDescription
		{
			get
			{
				return GetProperty<string>("seoDescription");
			}
			set
			{
				SetProperty("seoDescription", value);
			}
		}

		/// <summary>
		///     频道的访问URL，可以是子域名、独立域名、目录方式
		/// </summary>
		public Url Url
		{
			get
			{
				return GetProperty<Url>("url");
			}
			set
			{
				SetProperty("url", value);
			}
		}

		/// <summary>
		///     seo关键字
		/// </summary>
		public string SeoKeys
		{
			get
			{
				ROC<RelationshipObjekt> relationship = ROCC.GetROC("FeChannelSeoKey");
				StringBuilder sb = new StringBuilder();
				foreach (RelationshipObjekt item in relationship)
				{
					FeSeoKey seoKey = item.Related as FeSeoKey;
					if (seoKey != null)
					{
						if (sb.Length > 0)
						{
							sb.Append(",");
						}
						sb.Append(seoKey.Key);
					}
				}
				return sb.ToString();
			}
		}

		/// <summary>
		///     访问域名类型，使用目录名称、二级域名、顶级域名
		/// </summary>
		public Value DomainType
		{
			get
			{
				return GetProperty<Value>("domainType");
			}
			set
			{
				SetProperty("domainType", value);
			}
		}

		/// <summary>
		///     用户输入的域名文本内容
		/// </summary>
		public string DomainText
		{
			get
			{
				return GetProperty<string>("domainText");
			}
			set
			{
				SetProperty("domainText", value);
			}
		}

		/// <summary>
		///     LOGO图像文件
		/// </summary>
		public FeLogo Logo
		{
			get
			{
				return GetProperty<FeLogo>("logo");
			}
			set
			{
				SetProperty("logo", value);
			}
		}

		/// <summary>
		///     创建新增对象排序。
		/// </summary>
		/// <returns></returns>
		public static decimal NewSortOrder()
		{
			ObjektCollection<FeChannel> oc = new ObjektCollection<FeChannel>(Klass.ForId("FeChannel@Klass"), new WhereClause("\"sortOrder\" is not null"));
			oc.OrderByClause.Add(new OrderByCell("sortOrder", Order.Desc));
			FeChannel entity = oc.FirstOrDefault();
			int sort2 = 1;
			if (entity != null && entity.IsExists() && entity.SortOrder.HasValue)
			{
				sort2 = (int)entity.SortOrder.Value;
				sort2++;
			}
			return sort2;
		}
	}
}
