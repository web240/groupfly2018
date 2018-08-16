using GroupflyGroup.Platform.ObjectFramework;
using GroupflyGroup.Platform.ObjectFramework.Persistence;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// <summary>
	///     文章
	/// </summary>
	[Serializable]
	public class FeArticle : Objekt
	{
		/// <summary>
		///     文章的标题
		/// </summary>
		public string Title
		{
			get
			{
				return GetProperty<string>("title");
			}
			set
			{
				SetProperty("title", value);
			}
		}

		/// <summary>
		///     文章的标题
		/// </summary>
		public string TitleValue
		{
			get
			{
				return GetProperty<string>("titleValue");
			}
			set
			{
				SetProperty("titleValue", value);
			}
		}

		/// <summary>
		///     文章的所属分类
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
		///     文章在所属分类中的排序序号
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
		///     文章主题图片
		/// </summary>
		public GroupflyGroup.Platform.ObjectFramework.File Image
		{
			get
			{
				return GetProperty<GroupflyGroup.Platform.ObjectFramework.File>("image");
			}
			set
			{
				SetProperty("image", value);
			}
		}

		/// <summary>
		///     文章主题图片ID
		/// </summary>
		public string ImageFileId
		{
			get
			{
				GroupflyGroup.Platform.ObjectFramework.File file = GetProperty<GroupflyGroup.Platform.ObjectFramework.File>("image");
				if (file == null)
				{
					return "";
				}
				return file.Id;
			}
		}

		/// <summary>
		///     文章内容
		/// </summary>
		public string Content
		{
			get
			{
				Stream s = GetProperty<Stream>("content");
				if (s != null)
				{
					byte[] t = new byte[s.Length];
					s.Read(t, 0, (int)s.Length);
					s.Dispose();
					s.Close();
					return Encoding.UTF8.GetString(t, 0, t.Length);
				}
				return "";
			}
			set
			{
				SetProperty("content", new MemoryStream(Encoding.UTF8.GetBytes(value)));
			}
		}

		/// <summary>
		///     文章的属性ID
		/// </summary>
		public string CharacterId
		{
			get
			{
				ROC<RelationshipObjekt> relationship = ROCC.GetROC("FeArticleCharacter");
				StringBuilder sb = new StringBuilder();
				foreach (RelationshipObjekt item in relationship)
				{
					FeCharacter character = item.Related as FeCharacter;
					if (character != null)
					{
						if (sb.Length > 0)
						{
							sb.Append(",");
						}
						sb.Append(character.Id);
					}
				}
				return sb.ToString();
			}
		}

		/// <summary>
		///     文章的属性
		/// </summary>
		public string Character
		{
			get
			{
				ROC<RelationshipObjekt> relationship = ROCC.GetROC("FeArticleCharacter");
				StringBuilder sb = new StringBuilder();
				foreach (RelationshipObjekt item in relationship)
				{
					FeCharacter character = item.Related as FeCharacter;
					if (character != null)
					{
						if (sb.Length > 0)
						{
							sb.Append(",");
						}
						sb.Append(character.Name);
					}
				}
				return sb.ToString();
			}
		}

		/// <summary>
		///     文章的作者
		/// </summary>
		public string Author
		{
			get
			{
				return GetProperty<string>("author");
			}
			set
			{
				SetProperty("author", value);
			}
		}

		/// <summary>
		///     文章的浏览（点击）数
		/// </summary>
		public int? HitsNum
		{
			get
			{
				return GetProperty<int?>("hitsNum");
			}
			set
			{
				SetProperty("hitsNum", value);
			}
		}

		/// <summary>
		///     文章的评论数
		/// </summary>
		public int? CommentNum
		{
			get
			{
				return GetProperty<int?>("commentNum");
			}
			set
			{
				SetProperty("commentNum", value);
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
		///     seo关键字
		/// </summary>
		public string SeoKeys
		{
			get
			{
				ROC<RelationshipObjekt> relationship = ROCC.GetROC("FeArticleSeoKey");
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
		///     文章是否在前台显示
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
		///     文章审核状态
		/// </summary>
		public Value ApprovalStatus
		{
			get
			{
				return GetProperty<Value>("approvalStatus");
			}
			set
			{
				SetProperty("approvalStatus", value);
			}
		}

		/// <summary>
		///     文章审核人
		/// </summary>
		public User Approver
		{
			get
			{
				return GetProperty<User>("approver");
			}
			set
			{
				SetProperty("approver", value);
			}
		}

		/// <summary>
		///     文章审核时间
		/// </summary>
		public DateTime? ApprovedOn
		{
			get
			{
				return GetProperty<DateTime?>("approvedOn");
			}
			set
			{
				SetProperty("approvedOn", value);
			}
		}

		/// <summary>
		///     文章是否允许评论
		/// </summary>
		public bool CanComment
		{
			get
			{
				return GetProperty<bool>("canComment");
			}
			set
			{
				SetProperty("canComment", value);
			}
		}

		/// <summary>
		///     文章是否回收
		/// </summary>
		public new bool IsTrash
		{
			get
			{
				return GetProperty<bool>("isTrash");
			}
			set
			{
				SetProperty("isTrash", value);
			}
		}

		/// <summary>
		///     文章是否草稿
		/// </summary>
		public bool IsDraft
		{
			get
			{
				return GetProperty<bool>("isDraft");
			}
			set
			{
				SetProperty("isDraft", value);
			}
		}

		/// <summary>
		///     文章来源
		/// </summary>
		public string From
		{
			get
			{
				return GetProperty<string>("from");
			}
			set
			{
				SetProperty("from", value);
			}
		}

		/// <summary>
		///     文章标签
		/// </summary>
		public string Tag
		{
			get
			{
				ROC<RelationshipObjekt> relationship = ROCC.GetROC("FeArticleTag");
				StringBuilder sb = new StringBuilder();
				foreach (RelationshipObjekt item in relationship)
				{
					FeTag ft = item.Related as FeTag;
					if (ft != null)
					{
						if (sb.Length > 0)
						{
							sb.Append(",");
						}
						sb.Append(ft.Tag);
					}
				}
				return sb.ToString();
			}
		}

		/// <summary>
		///     创建新增对象排序。
		/// </summary>
		/// <returns></returns>
		public static decimal NewSortOrder()
		{
			ObjektCollection<FeArticle> oc = new ObjektCollection<FeArticle>(Klass.ForId("FeArticle@Klass"), new WhereClause("\"sortOrder\" is not null"));
			oc.OrderByClause.Add(new OrderByCell("sortOrder", Order.Desc));
			FeArticle entity = oc.FirstOrDefault();
			int sort2 = 1;
			if (entity != null && entity.IsExists() && entity.SortOrder.HasValue)
			{
				sort2 = (int)entity.SortOrder.Value;
				sort2++;
			}
			return sort2;
		}

		/// <summary>
		///
		/// </summary>
		public override void BeforeSave()
		{
			if (base.ObjektStatus == ObjektStatus.NewModified)
			{
				SystemConfiguration sysArticGlobal = ObjektFactory.Find<SystemConfiguration>("58e502ecf56144cdb67ff129b6e6e4d5@SystemConfiguration");
				if (sysArticGlobal.Value == "False")
				{
					ApprovalStatus = ObjektFactory.Find<Value>("e87630c5ca374e98a454287ff8484b68@Value");
				}
				else
				{
					ApprovalStatus = ObjektFactory.Find<Value>("fd96691ff35f46db9fb42464db910972@Value");
				}
			}
			base.BeforeSave();
		}
	}
}
