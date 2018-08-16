using GroupflyGroup.Platform.ObjectFramework;
using GroupflyGroup.Platform.ObjectFramework.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// <summary>
	///     文章分类
	/// </summary>
	[Serializable]
	public class FeArticleCategory : Objekt
	{
		/// <summary>
		///     名称
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
		///     父分类
		/// </summary>
		public FeArticleCategory Parent
		{
			get
			{
				return GetProperty<FeArticleCategory>("parent");
			}
			set
			{
				SetProperty("parent", value);
			}
		}

		/// <summary>
		///     序号
		/// </summary>
		public decimal? SortOrder
		{
			get
			{
				return GetProperty<decimal?>("sortOrder");
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
		/// seo关键字
		/// </summary>
		public string SeoKeys
		{
			get
			{
				ROC<RelationshipObjekt> relationship = ROCC.GetROC("FeArticleCategorySeoKey");
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
		///     显示
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
		///
		/// </summary>
		public string RootPath
		{
			get
			{
				string result = "";
				FeArticleCategory item = this;
				do
				{
					if (!string.IsNullOrWhiteSpace(result))
					{
						result = "/" + result;
					}
					result = item.Name + result;
					item = item.Parent;
				}
				while (item != null);
				return result;
			}
		}

		/// <summary>
		/// 上级节点全路径。
		/// </summary>
		public string ParentNamePath
		{
			get
			{
				string result = "";
				FeArticleCategory item = Parent;
				do
				{
					if (!string.IsNullOrWhiteSpace(result))
					{
						result = "/" + result;
					}
					result = item.Name + result;
					item = item.Parent;
				}
				while (item != null);
				return result;
			}
		}

		/// <summary>
		///     添加删除业务逻辑。
		/// </summary>
		public override void BeforeDelete()
		{
			ObjektCollection<FeArticle> oc = new ObjektCollection<FeArticle>(Klass.ForId("FeArticle@Klass"), new WhereClause("\"category\" = '" + base.Id + "'"));
			if (oc.Count > 0)
			{
				throw new Exception("请先删除分类下的所有文章");
			}
			ObjektCollection<FeChannel> channelOc = new ObjektCollection<FeChannel>(Klass.ForId("FeChannel@Klass"), new WhereClause("\"category\" = '" + base.Id + "'"));
			if (channelOc.Count > 0)
			{
				throw new Exception("请先删除分类绑定的频道");
			}
			List<FeArticleCategory> children = GetChildren().ToList();
			foreach (FeArticleCategory item in children)
			{
				item.Delete();
				item.Save();
			}
			base.BeforeDelete();
		}

		/// <summary>
		/// </summary>
		public override void BeforeSave()
		{
			base.BeforeSave();
			if ((base.ObjektStatus == ObjektStatus.NewModified || base.ObjektStatus == ObjektStatus.Modified) && IsModifiedProperty("isDisplay"))
			{
				ObjektCollection<FeArticleCategory> list = GetChildren();
				foreach (FeArticleCategory item in list)
				{
					item.SetProperty("isDisplay", GetProperty("isDisplay"));
					item.Save();
				}
			}
		}

		/// <summary>
		///     获取
		/// </summary>
		/// <returns></returns>
		public List<FeArticleCategory> GetDescendants()
		{
			List<FeArticleCategory> result = new List<FeArticleCategory>();
			ObjektCollection<FeArticleCategory> children = GetChildren();
			result.AddRange(children.ToList());
			foreach (FeArticleCategory item in children)
			{
				result.AddRange(item.GetDescendants());
			}
			return result;
		}

		/// <summary>
		///     清空文章分类所有数据。
		/// </summary>
		public static void DeleteAll()
		{
			ObjektCollection<FeArticleCategory> list = Klass.ForId("FeArticleCategory@Klass").GetInstances<FeArticleCategory>();
			list.DeleteAll();
		}

		private ObjektCollection<FeArticleCategory> GetChildren()
		{
			return new ObjektCollection<FeArticleCategory>(Klass.ForId("FeArticleCategory@Klass"), new WhereClause("\"parent\" = '" + base.Id + "'"));
		}

		/// <summary>
		/// 创建新增对象排序。
		/// </summary>
		/// <returns></returns>
		public static decimal NewSortOrder()
		{
			ObjektCollection<FeArticleCategory> oc = new ObjektCollection<FeArticleCategory>(Klass.ForId("FeArticleCategory@Klass"), new WhereClause("\"sortOrder\" is not null"));
			oc.OrderByClause.Add(new OrderByCell("sortOrder", Order.Desc));
			FeArticleCategory entity = oc.FirstOrDefault();
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
