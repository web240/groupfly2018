using GroupflyGroup.Platform.Extension;
using GroupflyGroup.Platform.ObjectFramework;
using GroupflyGroup.Platform.ObjectFramework.Extension;
using GroupflyGroup.Platform.ObjectFramework.Persistence;
using log4net;
using System.ComponentModel.Composition;

namespace GroupflyGroup.FrontEnd.ObjectFramework.EventListener
{
	/// <summary>
	/// FrontEnd系统建模
	/// </summary>
	[Export(typeof(DoModelingEventListener))]
	public class DoFrontEndModelingEventListener : DoModelingEventListener
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(DoFrontEndModelingEventListener));

		/// <summary>
		///     第二层
		/// </summary>
		public override int Priority => 900;

		public override string Description => "前端系统建模。";

		public override void Do(Event e)
		{
			Modeling1_0_0();
			Modeling1_0_1();
		}

		private void Modeling1_0_0()
		{
			Modeling1_0_0_ApprovalStatusList();
			Modeling1_0_0_SeoKey();
			Modeling1_0_0_TAG();
			Modeling1_0_0_ArticleCategory();
			Modeling1_0_0_Character();
			Modeling1_0_0_Comment();
			Modeling1_0_0_Article();
			Modeling1_0_0_Template();
			Modeling1_0_0_Channel();
			Modeling1_0_0_Channel_DomainType();
			Modeling1_0_0_MenuItem();
			Modeling1_0_0_SystemConfiguration();
		}

		/// <summary>
		///     SEO关键字
		/// </summary>
		private void Modeling1_0_0_SeoKey()
		{
			if (!ObjektFactory.IsExists("FeSeoKey@Klass"))
			{
				Klass ecSeoKey = ObjektFactory.New<Klass>("FeSeoKey@Klass", Klass.ForId("Klass@Klass"));
				ecSeoKey.Name = "FeSeoKey";
				ecSeoKey.Label = "SEO关键字";
				ecSeoKey.Description = "SEO关键字";
				ecSeoKey.EntityClass = typeof(FeSeoKey).FullName;
				ecSeoKey.EntityClassAssembly = typeof(FeSeoKey).Assembly.GetName().Name;
				ecSeoKey.Sealed = true;
				ecSeoKey.Save();
				Property p = new Property();
				p.Source = ecSeoKey;
				p.Name = "key";
				p.SortOrder = 3000;
				p.IsKeyed = true;
				p.Label = "关键字";
				p.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p.CombinedLabelOrder = 1;
				p.Description = "SEO关键字";
				p.IsRequired = true;
				p.StoredLength = 64;
				p.Save();
			}
		}

		/// <summary>
		///     SEO关键字
		/// </summary>
		private void Modeling1_0_0_TAG()
		{
			if (!ObjektFactory.IsExists("FeTag@Klass"))
			{
				List tagSource = ObjektFactory.New<List>("a61ec58a16c44e4985f40e3ea43e5a53@List", Klass.ForId("List@Klass"));
				tagSource.Name = "FeTagSource";
				tagSource.Label = "添加来源";
				tagSource.Description = "标签的来源，分为手工添加、自动添加";
				tagSource.Save();
				Value v2 = ObjektFactory.New<Value>("75d290896b3d40ad802c9d6b04d61a9b@Value", Klass.ForId("Value@Klass"));
				v2.Source = tagSource;
				v2.SortOrder = 100;
				v2.Value_ = "auto";
				v2.Label = "自动添加";
				v2.SetProperty("description", "自动添加的标签");
				v2.Save();
				v2 = ObjektFactory.New<Value>("50507b94e2034f30b41c45d3f2181a7d@Value", Klass.ForId("Value@Klass"));
				v2.Source = tagSource;
				v2.SortOrder = 200;
				v2.Value_ = "manual";
				v2.Label = "手工添加";
				v2.SetProperty("description", "手工在标签库中维护标签所增加的标签");
				v2.Save();
				Klass feTag = ObjektFactory.New<Klass>("FeTag@Klass", Klass.ForId("Klass@Klass"));
				feTag.Name = "FeTag";
				feTag.Label = "标签";
				feTag.Description = "系统集中维护的标签";
				feTag.EntityClass = typeof(FeTag).FullName;
				feTag.EntityClassAssembly = typeof(FeTag).Assembly.GetName().Name;
				feTag.Sealed = true;
				feTag.Save();
				Property p2 = new Property();
				p2.Source = feTag;
				p2.Name = "tag";
				p2.SortOrder = 3000;
				p2.IsKeyed = true;
				p2.Label = "标签";
				p2.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p2.CombinedLabelOrder = 1;
				p2.Description = "标签值";
				p2.IsRequired = true;
				p2.StoredLength = 16;
				p2.Save();
				p2 = new Property();
				p2.Source = feTag;
				p2.Name = "from";
				p2.SortOrder = 3100;
				p2.Label = "添加来源";
				p2.DataType = ObjektFactory.Find<Value>("ab097f34fd9d4b7ca216084e6386b99e@Value");
				p2.Description = "标签添加来源";
				p2.ListDataSource = tagSource;
				p2.IsRequired = true;
				p2.Save();
			}
		}

		/// <summary>
		///     文章分类
		/// </summary>
		private void Modeling1_0_0_ArticleCategory()
		{
			if (!ObjektFactory.IsExists("FeArticleCategory@Klass"))
			{
				Klass feArticleCategory = ObjektFactory.New<Klass>("FeArticleCategory@Klass", Klass.ForId("Klass@Klass"));
				feArticleCategory.Name = "FeArticleCategory";
				feArticleCategory.Label = "文章分类";
				feArticleCategory.Description = "咨询文章分类，如“体育”、“娱乐”等等";
				feArticleCategory.EntityClass = typeof(FeArticleCategory).FullName;
				feArticleCategory.EntityClassAssembly = typeof(FeArticleCategory).Assembly.GetName().Name;
				feArticleCategory.Sealed = true;
				feArticleCategory.Save();
				Property p6 = new Property();
				p6.Source = feArticleCategory;
				p6.Name = "name";
				p6.Label = "名称";
				p6.SortOrder = 3000;
				p6.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p6.CombinedLabelOrder = 1;
				p6.Description = "文章分类的名称";
				p6.IsRequired = true;
				p6.StoredLength = 32;
				p6.Save();
				p6 = new Property();
				p6.Source = feArticleCategory;
				p6.Name = "parent";
				p6.SortOrder = 3100;
				p6.Label = "父分类";
				p6.DataType = ObjektFactory.Find<Value>("c80693211fc4426a88ebca05b34a5f2d@Value");
				p6.Description = "文章分类的父分类";
				p6.ObjektDataSource = feArticleCategory;
				p6.Save();
				p6 = new Property();
				p6.Source = feArticleCategory;
				p6.Name = "sortOrder";
				p6.SortOrder = 3200;
				p6.Label = "序号";
				p6.DataType = ObjektFactory.Find<Value>("a862c03d5cdf4355b5d3a438c81cfbfe@Value");
				p6.Prec = 11;
				p6.Scale = 3;
				p6.Description = "文章分类的排序序号";
				p6.Save();
				p6 = new Property();
				p6.Source = feArticleCategory;
				p6.Name = "seoTitle";
				p6.Label = "SEO标题";
				p6.SortOrder = 3300;
				p6.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p6.Description = "SEO标题";
				p6.IsRequired = false;
				p6.StoredLength = 128;
				p6.Save();
				p6 = new Property();
				p6.Source = feArticleCategory;
				p6.Name = "seoDescription";
				p6.Label = "SEO描述";
				p6.SortOrder = 3400;
				p6.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p6.Description = "SEO描述";
				p6.IsRequired = false;
				p6.StoredLength = 512;
				p6.Save();
				p6 = new Property();
				p6.Source = feArticleCategory;
				p6.Name = "isDisplay";
				p6.SortOrder = 3500;
				p6.Label = "显示";
				p6.DataType = ObjektFactory.Find<Value>("f7036b21e6e34919b504df2cfc2d88e2@Value");
				p6.Description = "文章分类的文章是否在前台显示";
				p6.Save();
				Relationship feArticleCategorySeoKey = ObjektFactory.New<Relationship>("a115140002db4e92963705c6c9eb612d@Relationship", Klass.ForId("Relationship@Klass"));
				feArticleCategorySeoKey.Name = "FeArticleCategorySeoKey";
				feArticleCategorySeoKey.Label = "文章分类SEO关键字";
				feArticleCategorySeoKey.Description = "文章分类的SEO关键字";
				feArticleCategorySeoKey.Source = feArticleCategory;
				feArticleCategorySeoKey.Related = ObjektFactory.Find<Klass>("FeSeoKey@Klass");
				feArticleCategorySeoKey.RelatedNotnull = true;
				feArticleCategorySeoKey.Save();
				Klass i = Klass.ForId("FeArticleCategorySeoKey@Klass");
				i.EntityClass = typeof(FeArticleCategorySeoKey).FullName;
				i.EntityClassAssembly = typeof(FeArticleCategorySeoKey).Assembly.GetName().Name;
				i.Save();
			}
		}

		/// <summary>
		///     文章属性（特征）
		/// </summary>
		private void Modeling1_0_0_Character()
		{
			if (!ObjektFactory.IsExists("FeCharacter@Klass"))
			{
				Klass feCharacter = ObjektFactory.New<Klass>("FeCharacter@Klass", Klass.ForId("Klass@Klass"));
				feCharacter.Name = "FeCharacter";
				feCharacter.Label = "文章属性";
				feCharacter.Description = "文章属性（特性）";
				feCharacter.EntityClass = typeof(FeCharacter).FullName;
				feCharacter.EntityClassAssembly = typeof(FeCharacter).Assembly.GetName().Name;
				feCharacter.Sealed = true;
				feCharacter.Save();
				Property p5 = new Property();
				p5.Source = feCharacter;
				p5.Name = "name";
				p5.Label = "名称";
				p5.SortOrder = 3000;
				p5.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p5.Description = "文章属性的名称";
				p5.IsRequired = true;
				p5.StoredLength = 64;
				p5.Save();
				p5 = new Property();
				p5.Source = feCharacter;
				p5.Name = "label";
				p5.Label = "显示标签";
				p5.SortOrder = 3100;
				p5.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p5.CombinedLabelOrder = 1;
				p5.Description = "文章属性的显示标签";
				p5.IsRequired = true;
				p5.StoredLength = 64;
				p5.Save();
				p5 = new Property();
				p5.Source = feCharacter;
				p5.Name = "sortOrder";
				p5.SortOrder = 3200;
				p5.Label = "序号";
				p5.DataType = ObjektFactory.Find<Value>("a862c03d5cdf4355b5d3a438c81cfbfe@Value");
				p5.Prec = 11;
				p5.Scale = 3;
				p5.Description = "文章属性的排序序号";
				p5.Save();
				p5 = new Property();
				p5.Source = feCharacter;
				p5.Name = "faIcon";
				p5.Label = "图标";
				p5.Description = "Font Awesome字体图标名称";
				p5.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p5.StoredLength = 48;
				p5.Save();
				p5 = new Property();
				p5.Source = feCharacter;
				p5.Name = "isDisplay";
				p5.SortOrder = 3400;
				p5.Label = "显示";
				p5.DataType = ObjektFactory.Find<Value>("f7036b21e6e34919b504df2cfc2d88e2@Value");
				p5.Description = "文章属性是否在前台显示";
				p5.Save();
			}
		}

		private void Modeling1_0_0_ApprovalStatusList()
		{
			if (!ObjektFactory.IsExists("bafd0042a5fd470c98e214b72518d906@List"))
			{
				List articleApprovalStatus = ObjektFactory.New<List>("bafd0042a5fd470c98e214b72518d906@List", Klass.ForId("List@Klass"));
				articleApprovalStatus.Name = "FeApprovalStatus";
				articleApprovalStatus.Label = "文章审核状态";
				articleApprovalStatus.Description = "文章审核状态，分为待审核、审核通过、审核不通过";
				articleApprovalStatus.Save();
				Value v3 = ObjektFactory.New<Value>("fd96691ff35f46db9fb42464db910972@Value", Klass.ForId("Value@Klass"));
				v3.Source = articleApprovalStatus;
				v3.SortOrder = 100;
				v3.Value_ = "Pending";
				v3.Label = "待审核";
				v3.SetProperty("description", "待审核");
				v3.Save();
				v3 = ObjektFactory.New<Value>("e87630c5ca374e98a454287ff8484b68@Value", Klass.ForId("Value@Klass"));
				v3.Source = articleApprovalStatus;
				v3.SortOrder = 200;
				v3.Value_ = "Approved";
				v3.Label = "审核通过";
				v3.SetProperty("description", "审核通过");
				v3.Save();
				v3 = ObjektFactory.New<Value>("7e0b95a2e28f4c82ad25bed27ea3b912@Value", Klass.ForId("Value@Klass"));
				v3.Source = articleApprovalStatus;
				v3.SortOrder = 300;
				v3.Value_ = "NotApproved";
				v3.Label = "审核不通过";
				v3.SetProperty("description", "审核不通过");
				v3.Save();
			}
		}

		/// <summary>
		///     文章
		/// </summary>
		private void Modeling1_0_0_Article()
		{
			if (!ObjektFactory.IsExists("FeArticle@Klass"))
			{
				Klass feArticle = ObjektFactory.New<Klass>("FeArticle@Klass", Klass.ForId("Klass@Klass"));
				feArticle.Name = "FeArticle";
				feArticle.Label = "文章";
				feArticle.Description = "咨询文章";
				feArticle.EntityClass = typeof(FeArticle).FullName;
				feArticle.EntityClassAssembly = typeof(FeArticle).Assembly.GetName().Name;
				feArticle.Sealed = true;
				feArticle.Save();
				Property p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "title";
				p19.SortOrder = 3000;
				p19.Label = "标题";
				p19.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p19.CombinedLabelOrder = 1;
				p19.Description = "文章的标题";
				p19.IsRequired = true;
				p19.IsRichText = true;
				p19.StoredLength = 2000;
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "titleValue";
				p19.SortOrder = 3100;
				p19.Label = "标题值";
				p19.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p19.CombinedLabelOrder = 1;
				p19.Description = "文章标题的内部值";
				p19.IsRequired = true;
				p19.StoredLength = 2000;
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "category";
				p19.SortOrder = 3100;
				p19.Label = "分类";
				p19.DataType = ObjektFactory.Find<Value>("c80693211fc4426a88ebca05b34a5f2d@Value");
				p19.ObjektDataSource = Klass.ForId("FeArticleCategory@Klass");
				p19.Description = "文章的所属分类";
				p19.IsRequired = true;
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "sortOrder";
				p19.SortOrder = 3200;
				p19.Label = "序号";
				p19.DataType = ObjektFactory.Find<Value>("a862c03d5cdf4355b5d3a438c81cfbfe@Value");
				p19.Prec = 11;
				p19.Scale = 3;
				p19.Description = "文章在所属分类中的排序序号";
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "content";
				p19.SortOrder = 3300;
				p19.Label = "内容";
				p19.DataType = ObjektFactory.Find<Value>("867da97346bc4eb5b802ed6b8b3b54cc@Value");
				p19.Description = "文章内容";
				p19.IsRichText = true;
				p19.IsRequired = true;
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "author";
				p19.SortOrder = 3400;
				p19.Label = "作者";
				p19.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p19.Description = "文章的作者";
				p19.IsRequired = false;
				p19.StoredLength = 32;
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "hitsNum";
				p19.SortOrder = 3500;
				p19.Label = "点击数";
				p19.DataType = ObjektFactory.Find<Value>("346df36bea7945e5a1a395eb476e6607@Value");
				p19.Description = "文章的浏览（点击）数";
				p19.IsRequired = false;
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "commentNum";
				p19.SortOrder = 3550;
				p19.Label = "评论数";
				p19.DataType = ObjektFactory.Find<Value>("346df36bea7945e5a1a395eb476e6607@Value");
				p19.Description = "文章的评论数";
				p19.IsRequired = false;
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "seoTitle";
				p19.Label = "SEO标题";
				p19.SortOrder = 3600;
				p19.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p19.Description = "SEO标题";
				p19.IsRequired = false;
				p19.StoredLength = 128;
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "seoDescription";
				p19.Label = "SEO描述";
				p19.SortOrder = 3700;
				p19.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p19.Description = "SEO描述";
				p19.IsRequired = false;
				p19.StoredLength = 512;
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "isDisplay";
				p19.SortOrder = 3800;
				p19.Label = "显示";
				p19.DataType = ObjektFactory.Find<Value>("f7036b21e6e34919b504df2cfc2d88e2@Value");
				p19.Description = "文章是否在前台显示";
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "approvalStatus";
				p19.SortOrder = 3900;
				p19.Label = "审核状态";
				p19.DataType = ObjektFactory.Find<Value>("ab097f34fd9d4b7ca216084e6386b99e@Value");
				p19.Description = "文章审核状态";
				p19.ListDataSource = ObjektFactory.Find<List>("bafd0042a5fd470c98e214b72518d906@List");
				p19.IsRequired = true;
				p19.ListDefaultValue = ObjektFactory.Find<Value>("fd96691ff35f46db9fb42464db910972@Value");
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "approver";
				p19.SortOrder = 4000;
				p19.Label = "审核人";
				p19.DataType = ObjektFactory.Find<Value>("c80693211fc4426a88ebca05b34a5f2d@Value");
				p19.Description = "文章审核人";
				p19.ObjektDataSource = Klass.ForId("User@Klass");
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "approvedOn";
				p19.SortOrder = 4100;
				p19.Label = "审核时间";
				p19.DataType = ObjektFactory.Find<Value>("229104957d384e72aa32ba288658dd3a@Value");
				p19.Description = "文章审核时间";
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "image";
				p19.SortOrder = 4200;
				p19.Label = "主题图片";
				p19.DataType = ObjektFactory.Find<Value>("c80693211fc4426a88ebca05b34a5f2d@Value");
				p19.Description = "文章主题图片";
				p19.ObjektDataSource = Klass.ForId("File@Klass");
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "canComment";
				p19.SortOrder = 4300;
				p19.Label = "允许评论";
				p19.DataType = ObjektFactory.Find<Value>("f7036b21e6e34919b504df2cfc2d88e2@Value");
				p19.Description = "文章是否允许评论";
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "isTrash";
				p19.SortOrder = 4400;
				p19.Label = "回收";
				p19.DataType = ObjektFactory.Find<Value>("f7036b21e6e34919b504df2cfc2d88e2@Value");
				p19.Description = "文章是否回收";
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "isDraft";
				p19.SortOrder = 4500;
				p19.Label = "草稿";
				p19.DataType = ObjektFactory.Find<Value>("f7036b21e6e34919b504df2cfc2d88e2@Value");
				p19.Description = "文章是否草稿";
				p19.Save();
				p19 = new Property();
				p19.Source = feArticle;
				p19.Name = "from";
				p19.SortOrder = 4600;
				p19.Label = "来源";
				p19.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p19.StoredLength = 128;
				p19.Description = "文章来源";
				p19.Save();
				Relationship feArticleTag = ObjektFactory.New<Relationship>("28ed0b0098594534a6c7b99ba78b3306@Relationship", Klass.ForId("Relationship@Klass"));
				feArticleTag.Name = "FeArticleTag";
				feArticleTag.Label = "文章标签";
				feArticleTag.Description = "文章的标签";
				feArticleTag.Source = feArticle;
				feArticleTag.Related = ObjektFactory.Find<Klass>("FeTag@Klass");
				feArticleTag.RelatedNotnull = true;
				feArticleTag.Save();
				Klass l = Klass.ForId("FeArticleTag@Klass");
				l.EntityClass = typeof(FeArticleTag).FullName;
				l.EntityClassAssembly = typeof(FeArticleTag).Assembly.GetName().Name;
				l.Save();
				Relationship feArticleSeoKey = ObjektFactory.New<Relationship>("ebf1d3b2769b4e06a15f5aa4e161e3df@Relationship", Klass.ForId("Relationship@Klass"));
				feArticleSeoKey.Name = "FeArticleSeoKey";
				feArticleSeoKey.Label = "文章SEO关键字";
				feArticleSeoKey.Description = "文章的SEO关键字";
				feArticleSeoKey.Source = feArticle;
				feArticleSeoKey.Related = ObjektFactory.Find<Klass>("FeSeoKey@Klass");
				feArticleSeoKey.RelatedNotnull = true;
				feArticleSeoKey.Save();
				l = Klass.ForId("FeArticleSeoKey@Klass");
				l.EntityClass = typeof(FeArticleSeoKey).FullName;
				l.EntityClassAssembly = typeof(FeArticleSeoKey).Assembly.GetName().Name;
				l.Save();
				Relationship feArticleCharacter = ObjektFactory.New<Relationship>("2dd2178cc8994ffbaa05be7a94660e22@Relationship", Klass.ForId("Relationship@Klass"));
				feArticleCharacter.Name = "FeArticleCharacter";
				feArticleCharacter.Label = "文章属性";
				feArticleCharacter.Description = "文章的属性";
				feArticleCharacter.Source = feArticle;
				feArticleCharacter.Related = ObjektFactory.Find<Klass>("FeCharacter@Klass");
				feArticleCharacter.RelatedNotnull = true;
				feArticleCharacter.Save();
				l = Klass.ForId("FeArticleCharacter@Klass");
				l.EntityClass = typeof(FeArticleCharacter).FullName;
				l.EntityClassAssembly = typeof(FeArticleCharacter).Assembly.GetName().Name;
				l.Save();
				Relationship feArticleComment = ObjektFactory.New<Relationship>("7e594113a60245459be0423341c825c7@Relationship", Klass.ForId("Relationship@Klass"));
				feArticleComment.Name = "FeArticleComment";
				feArticleComment.Label = "文章评论";
				feArticleComment.Description = "文章的评论";
				feArticleComment.Source = feArticle;
				feArticleComment.Related = ObjektFactory.Find<Klass>("FeComment@Klass");
				feArticleComment.RelatedNotnull = true;
				feArticleComment.Save();
				l = Klass.ForId("FeArticleComment@Klass");
				l.EntityClass = typeof(FeArticleComment).FullName;
				l.EntityClassAssembly = typeof(FeArticleComment).Assembly.GetName().Name;
				l.Save();
			}
		}

		/// <summary>
		///     评论
		/// </summary>
		private void Modeling1_0_0_Comment()
		{
			if (!ObjektFactory.IsExists("FeComment@Klass"))
			{
				Klass feComment = ObjektFactory.New<Klass>("FeComment@Klass", Klass.ForId("Klass@Klass"));
				feComment.Name = "FeComment";
				feComment.Label = "评论";
				feComment.Description = "评论";
				feComment.EntityClass = typeof(FeComment).FullName;
				feComment.EntityClassAssembly = typeof(FeComment).Assembly.GetName().Name;
				feComment.Save();
				Property p7 = new Property();
				p7.Source = feComment;
				p7.Name = "content";
				p7.Label = "内容";
				p7.SortOrder = 3000;
				p7.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p7.Description = "评论内容";
				p7.IsRequired = true;
				p7.IsRichText = true;
				p7.StoredLength = 1024;
				p7.Save();
				p7 = new Property();
				p7.Source = feComment;
				p7.Name = "approvalStatus";
				p7.SortOrder = 3100;
				p7.Label = "审核状态";
				p7.DataType = ObjektFactory.Find<Value>("ab097f34fd9d4b7ca216084e6386b99e@Value");
				p7.Description = "评论及其回复的审核状态";
				p7.ListDataSource = List.ForName("FeApprovalStatus");
				p7.IsRequired = true;
				p7.ListDefaultValue = ObjektFactory.Find<Value>("fd96691ff35f46db9fb42464db910972@Value");
				p7.Save();
				p7 = new Property();
				p7.Source = feComment;
				p7.Name = "isDisplay";
				p7.SortOrder = 3200;
				p7.Label = "显示";
				p7.DataType = ObjektFactory.Find<Value>("f7036b21e6e34919b504df2cfc2d88e2@Value");
				p7.Description = "评论（回复）是否在前台显示";
				p7.Save();
				p7 = new Property();
				p7.Source = feComment;
				p7.Name = "approver";
				p7.SortOrder = 3300;
				p7.Label = "审核人";
				p7.DataType = ObjektFactory.Find<Value>("c80693211fc4426a88ebca05b34a5f2d@Value");
				p7.Description = "评论（回复）审核人";
				p7.ObjektDataSource = Klass.ForId("User@Klass");
				p7.Save();
				p7 = new Property();
				p7.Source = feComment;
				p7.Name = "approvedOn";
				p7.SortOrder = 3400;
				p7.Label = "审核时间";
				p7.DataType = ObjektFactory.Find<Value>("229104957d384e72aa32ba288658dd3a@Value");
				p7.Description = "评论（回复） 审核时间";
				p7.Save();
				p7 = new Property();
				p7.Source = feComment;
				p7.Name = "parent";
				p7.SortOrder = 3500;
				p7.Label = "父评论";
				p7.DataType = ObjektFactory.Find<Value>("c80693211fc4426a88ebca05b34a5f2d@Value");
				p7.Description = "回复的评论";
				p7.ObjektDataSource = feComment;
				p7.Save();
				p7 = new Property();
				p7.Source = feComment;
				p7.Name = "isTrash";
				p7.SortOrder = 3600;
				p7.Label = "回收";
				p7.DataType = ObjektFactory.Find<Value>("f7036b21e6e34919b504df2cfc2d88e2@Value");
				p7.Description = "评论是否回收";
				p7.Save();
			}
		}

		/// <summary>
		/// 模板
		/// </summary>
		private void Modeling1_0_0_Template()
		{
			if (!ObjektFactory.IsExists("FeTemplate@Klass"))
			{
				List templateType = ObjektFactory.New<List>("f0521f6f46f94bafa72ed0d29e5a67f2@List", "List@Klass");
				templateType.Name = "FeTemplateType";
				templateType.Label = "模板类型";
				templateType.Description = "模板类型，如文章系统模板、频道模板、商品模板等";
				templateType.Save();
				Value v2 = ObjektFactory.New<Value>("43571f3a1cd54d01926c49bd5547538c@Value", "Value@Klass");
				v2.Source = templateType;
				v2.SortOrder = 100;
				v2.Value_ = "ArticleHome";
				v2.Label = "文章系统模板";
				v2.Description = "文章系统模板";
				v2.Save();
				v2 = ObjektFactory.New<Value>("a6f2960f50044a91941b3133678f5936@Value", "Value@Klass");
				v2.Source = templateType;
				v2.SortOrder = 200;
				v2.Value_ = "ArticleChannel";
				v2.Label = "频道模板";
				v2.SetProperty("description", "频道模板");
				v2.Save();
				Klass feTemplate = ObjektFactory.New<Klass>("FeTemplate@Klass", "Klass@Klass");
				feTemplate.Name = "FeTemplate";
				feTemplate.Label = "模板";
				feTemplate.Description = "页面模板包，包含一组模板页面";
				feTemplate.EntityClass = typeof(FeTemplate).FullName;
				feTemplate.EntityClassAssembly = typeof(FeTemplate).Assembly.GetName().Name;
				feTemplate.Save();
				Property p7 = new Property();
				p7.Source = feTemplate;
				p7.Name = "name";
				p7.Label = "名称";
				p7.SortOrder = 3000;
				p7.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p7.CombinedLabelOrder = 1;
				p7.Description = "模板名称";
				p7.IsRequired = true;
				p7.StoredLength = 48;
				p7.Save();
				p7 = new Property();
				p7.Source = feTemplate;
				p7.Name = "description";
				p7.Label = "描述";
				p7.SortOrder = 3100;
				p7.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p7.Description = "模板描述";
				p7.IsRequired = false;
				p7.StoredLength = 1024;
				p7.Save();
				p7 = new Property();
				p7.Source = feTemplate;
				p7.Name = "directory";
				p7.Label = "目录";
				p7.SortOrder = 3200;
				p7.DataType = ObjektFactory.Find<Value>("c80693211fc4426a88ebca05b34a5f2d@Value");
				p7.ObjektDataSource = Klass.ForId("File@Klass");
				p7.Description = "模板包对应的文件目录";
				p7.IsRequired = true;
				p7.Save();
				p7 = new Property();
				p7.Source = feTemplate;
				p7.Name = "type";
				p7.Label = "类型";
				p7.SortOrder = 3300;
				p7.DataType = ObjektFactory.Find<Value>("ab097f34fd9d4b7ca216084e6386b99e@Value");
				p7.ListDataSource = templateType;
				p7.Description = "模板类型";
				p7.IsRequired = true;
				p7.Save();
				p7 = new Property();
				p7.Source = feTemplate;
				p7.Name = "isDefault";
				p7.SortOrder = 3400;
				p7.Label = "默认";
				p7.DataType = ObjektFactory.Find<Value>("f7036b21e6e34919b504df2cfc2d88e2@Value");
				p7.Description = "是否默认模板";
				p7.Save();
				p7 = new Property();
				p7.Source = feTemplate;
				p7.Name = "isEnable";
				p7.SortOrder = 3500;
				p7.Label = "启用";
				p7.DataType = ObjektFactory.Find<Value>("f7036b21e6e34919b504df2cfc2d88e2@Value");
				p7.Description = "模板是否启用";
				p7.Save();
				p7 = new Property();
				p7.Source = feTemplate;
				p7.Name = "image";
				p7.Label = "图片";
				p7.SortOrder = 3600;
				p7.DataType = ObjektFactory.Find<Value>("c80693211fc4426a88ebca05b34a5f2d@Value");
				p7.ObjektDataSource = Klass.ForId("File@Klass");
				p7.Description = "模板包展示图片";
				p7.IsRequired = false;
				p7.Save();
			}
		}

		/// <summary>
		///     频道
		/// </summary>
		private void Modeling1_0_0_Channel()
		{
			if (!ObjektFactory.IsExists("FeChannel@Klass"))
			{
				Klass feChannel = ObjektFactory.New<Klass>("FeChannel@Klass", Klass.ForId("Klass@Klass"));
				feChannel.Name = "FeChannel";
				feChannel.Label = "频道";
				feChannel.Description = "文章频道";
				feChannel.EntityClass = typeof(FeChannel).FullName;
				feChannel.EntityClassAssembly = typeof(FeChannel).Assembly.GetName().Name;
				feChannel.Save();
				Property p8 = new Property();
				p8.Source = feChannel;
				p8.Name = "name";
				p8.Label = "名称";
				p8.SortOrder = 3000;
				p8.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p8.Description = "频道名称";
				p8.IsRequired = true;
				p8.StoredLength = 64;
				p8.CombinedLabelOrder = 1;
				p8.Save();
				p8 = new Property();
				p8.Source = feChannel;
				p8.Name = "category";
				p8.SortOrder = 3100;
				p8.Label = "绑定文章分类";
				p8.DataType = ObjektFactory.Find<Value>("c80693211fc4426a88ebca05b34a5f2d@Value");
				p8.Description = "频道绑定的文章分类";
				p8.ObjektDataSource = Klass.ForId("FeArticleCategory@Klass");
				p8.Save();
				p8 = new Property();
				p8.Source = feChannel;
				p8.Name = "template";
				p8.SortOrder = 3200;
				p8.Label = "模板";
				p8.DataType = ObjektFactory.Find<Value>("c80693211fc4426a88ebca05b34a5f2d@Value");
				p8.Description = "频道对应的模板";
				p8.ObjektDataSource = Klass.ForId("FeTemplate@Klass");
				p8.Save();
				p8 = new Property();
				p8.Source = feChannel;
				p8.Name = "isDisplay";
				p8.SortOrder = 3300;
				p8.Label = "显示";
				p8.DataType = ObjektFactory.Find<Value>("f7036b21e6e34919b504df2cfc2d88e2@Value");
				p8.Description = "频道是否在前台显示";
				p8.Save();
				p8 = new Property();
				p8.Source = feChannel;
				p8.Name = "sortOrder";
				p8.SortOrder = 3400;
				p8.Label = "序号";
				p8.DataType = ObjektFactory.Find<Value>("a862c03d5cdf4355b5d3a438c81cfbfe@Value");
				p8.Prec = 11;
				p8.Scale = 3;
				p8.Description = "频道的排序序号";
				p8.Save();
				p8 = new Property();
				p8.Source = feChannel;
				p8.Name = "seoTitle";
				p8.Label = "SEO标题";
				p8.SortOrder = 3500;
				p8.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p8.Description = "SEO标题";
				p8.IsRequired = false;
				p8.StoredLength = 128;
				p8.Save();
				p8 = new Property();
				p8.Source = feChannel;
				p8.Name = "seoDescription";
				p8.Label = "SEO描述";
				p8.SortOrder = 3600;
				p8.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p8.Description = "SEO描述";
				p8.IsRequired = false;
				p8.StoredLength = 512;
				p8.Save();
				p8 = new Property();
				p8.Source = feChannel;
				p8.Name = "url";
				p8.Label = "URL";
				p8.SortOrder = 3700;
				p8.DataType = ObjektFactory.Find<Value>("c80693211fc4426a88ebca05b34a5f2d@Value");
				p8.Description = "频道的访问URL，可以是子域名、独立域名、目录方式";
				p8.IsRequired = true;
				p8.ObjektDataSource = Klass.ForId("Url@Klass");
				p8.Save();
				Relationship feChannelSeoKey = ObjektFactory.New<Relationship>("94069372bafb45519fbf8eb8e6d4da0b@Relationship", Klass.ForId("Relationship@Klass"));
				feChannelSeoKey.Name = "FeChannelSeoKey";
				feChannelSeoKey.Label = "频道SEO关键字";
				feChannelSeoKey.Description = "频道的SEO关键字";
				feChannelSeoKey.Source = feChannel;
				feChannelSeoKey.Related = ObjektFactory.Find<Klass>("FeSeoKey@Klass");
				feChannelSeoKey.RelatedNotnull = true;
				feChannelSeoKey.Save();
				Klass i = Klass.ForId("FeChannelSeoKey@Klass");
				i.EntityClass = typeof(FeChannelSeoKey).FullName;
				i.EntityClassAssembly = typeof(FeChannelSeoKey).Assembly.GetName().Name;
				i.Save();
			}
		}

		private void Modeling1_0_0_Channel_DomainType()
		{
			if (!ObjektFactory.IsExists("bafd0042a7214cec98e214b72518d906@List"))
			{
				List domainType = ObjektFactory.New<List>("bafd0042a7214cec98e214b72518d906@List", Klass.ForId("List@Klass"));
				domainType.Name = "FeDomainType";
				domainType.Label = "访问域名类型";
				domainType.Description = "访问域名类型，使用目录名称、二级域名、顶级域名";
				domainType.Save();
				Value v3 = ObjektFactory.New<Value>("75d222296b3d40ad802c9d6b04d61a9b@Value", Klass.ForId("Value@Klass"));
				v3.Source = domainType;
				v3.SortOrder = 100;
				v3.Value_ = "DirectoryName";
				v3.Label = "使用目录名称";
				v3.SetProperty("description", "使用目录名称");
				v3.Save();
				v3 = ObjektFactory.New<Value>("75d222396b3d40ad802c9d6b04d61a9b@Value", Klass.ForId("Value@Klass"));
				v3.Source = domainType;
				v3.SortOrder = 200;
				v3.Value_ = "SecondDomain";
				v3.Label = "二级域名";
				v3.SetProperty("description", "使用二级域名");
				v3.Save();
				v3 = ObjektFactory.New<Value>("75d222496b3d40ad802c9d6b04d61a9b@Value", Klass.ForId("Value@Klass"));
				v3.Source = domainType;
				v3.SortOrder = 300;
				v3.Value_ = "TopDomain";
				v3.Label = "顶级域名";
				v3.SetProperty("description", "使用顶级域名");
				v3.Save();
				Klass feChannel = ObjektFactory.Find<Klass>("FeChannel@Klass");
				Property p2 = new Property();
				p2.Source = feChannel;
				p2.Name = "domainType";
				p2.SortOrder = 4000;
				p2.Label = "访问域名类型";
				p2.DataType = ObjektFactory.Find<Value>("ab097f34fd9d4b7ca216084e6386b99e@Value");
				p2.Description = "访问域名类型";
				p2.ListDataSource = domainType;
				p2.IsRequired = true;
				p2.Save();
				p2 = new Property();
				p2.Source = feChannel;
				p2.Name = "domainText";
				p2.SortOrder = 4100;
				p2.Label = "用户输入的域名文本内容";
				p2.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				p2.Description = "用户输入的域名文本内容";
				p2.StoredLength = 1024;
				p2.Save();
			}
		}

		/// <summary>
		/// 菜单项
		/// </summary>
		private void Modeling1_0_0_MenuItem()
		{
			if (!ObjektFactory.IsExists("0854fcccc45a402fb35540e5296a9337@DirectoryMenuItem"))
			{
				DirectoryMenuItem feOperations = ObjektFactory.New<DirectoryMenuItem>("0854fcccc45a402fb35540e5296a9337@DirectoryMenuItem", "DirectoryMenuItem@Klass");
				feOperations.Name = "FeOperations";
				feOperations.Label = "运营";
				feOperations.Description = "运营";
				feOperations.FaIcon = "fa fa-desktop";
				feOperations.Save();
				SubMenuItem smi12 = new SubMenuItem();
				smi12.Source = ObjektFactory.Find<DirectoryMenuItem>("ec8a73007c0b4e55b5764a9f53cb3be3@DirectoryMenuItem");
				smi12.Related = feOperations;
				smi12.SortOrder = 500;
				smi12.Save();
				DirectoryMenuItem feArticleManagement = ObjektFactory.New<DirectoryMenuItem>("2c853627d5eb458cad1127ba2f819dac@DirectoryMenuItem", "DirectoryMenuItem@Klass");
				feArticleManagement.Name = "FeArticleManagement";
				feArticleManagement.Label = "文章管理";
				feArticleManagement.Description = "文章管理";
				feArticleManagement.FaIcon = "fa fa-newspaper-o";
				feArticleManagement.Save();
				smi12 = new SubMenuItem();
				smi12.Source = feOperations;
				smi12.Related = feArticleManagement;
				smi12.SortOrder = 100;
				smi12.Save();
				NavigationMenuItem nmi10 = ObjektFactory.New<NavigationMenuItem>("a11a16db677e43e0ad6e88827d7a41a7@NavigationMenuItem", "NavigationMenuItem@Klass");
				nmi10.IsWithinThePlatform = true;
				nmi10.OpenedMode = ObjektFactory.Find<Value>("db3acb3464bf4c3293ac0a069b890eed@Value");
				nmi10.SetProperty("isPage", true);
				nmi10.Url = "/FeArticle";
				nmi10.Name = "FeArticleList";
				nmi10.Label = "文章列表";
				nmi10.FaIcon = "fa fa-list-alt";
				nmi10.Description = "打开文章列表";
				nmi10.Save();
				smi12 = new SubMenuItem();
				smi12.Source = feArticleManagement;
				smi12.Related = nmi10;
				smi12.SortOrder = 100;
				smi12.Save();
				nmi10 = ObjektFactory.New<NavigationMenuItem>("544405f9bef14277b5cb632627d38175@NavigationMenuItem", "NavigationMenuItem@Klass");
				nmi10.IsWithinThePlatform = true;
				nmi10.OpenedMode = ObjektFactory.Find<Value>("db3acb3464bf4c3293ac0a069b890eed@Value");
				nmi10.SetProperty("isPage", true);
				nmi10.Url = "/FeArticleTrash";
				nmi10.Name = "FeArticleTrash";
				nmi10.Label = "文章回收站";
				nmi10.FaIcon = "fa fa-trash";
				nmi10.Description = "打开文章回收站";
				nmi10.Save();
				smi12 = new SubMenuItem();
				smi12.Source = feArticleManagement;
				smi12.Related = nmi10;
				smi12.SortOrder = 200;
				smi12.Save();
				nmi10 = ObjektFactory.New<NavigationMenuItem>("87f616c82f044f8b9c8460a93dbbd1ac@NavigationMenuItem", "NavigationMenuItem@Klass");
				nmi10.IsWithinThePlatform = true;
				nmi10.OpenedMode = ObjektFactory.Find<Value>("db3acb3464bf4c3293ac0a069b890eed@Value");
				nmi10.SetProperty("isPage", true);
				nmi10.Url = "/FeTagLibrary";
				nmi10.Name = "FeTag";
				nmi10.Label = "标签库";
				nmi10.FaIcon = "fa fa-tags";
				nmi10.Description = "打开标签库";
				nmi10.Save();
				smi12 = new SubMenuItem();
				smi12.Source = feArticleManagement;
				smi12.Related = nmi10;
				smi12.SortOrder = 300;
				smi12.Save();
				nmi10 = ObjektFactory.New<NavigationMenuItem>("27c37a85dc924af58821c2b8efee41c8@NavigationMenuItem", "NavigationMenuItem@Klass");
				nmi10.IsWithinThePlatform = true;
				nmi10.OpenedMode = ObjektFactory.Find<Value>("db3acb3464bf4c3293ac0a069b890eed@Value");
				nmi10.SetProperty("isPage", true);
				nmi10.Url = "/FeArticleCategory";
				nmi10.Name = "FeArticleCategory";
				nmi10.Label = "文章分类";
				nmi10.FaIcon = "fa fa-th-large";
				nmi10.Description = "打开文章分类";
				nmi10.Save();
				smi12 = new SubMenuItem();
				smi12.Source = feArticleManagement;
				smi12.Related = nmi10;
				smi12.SortOrder = 400;
				smi12.Save();
				nmi10 = ObjektFactory.New<NavigationMenuItem>("8115c1598d1c4420ad1a6ccfdb401965@NavigationMenuItem", "NavigationMenuItem@Klass");
				nmi10.IsWithinThePlatform = true;
				nmi10.OpenedMode = ObjektFactory.Find<Value>("db3acb3464bf4c3293ac0a069b890eed@Value");
				nmi10.SetProperty("isPage", true);
				nmi10.Url = "/FeCharacter";
				nmi10.Name = "FeArticleCharacter";
				nmi10.Label = "文章属性";
				nmi10.FaIcon = "fa fa-bookmark";
				nmi10.Description = "打开文章属性";
				nmi10.Save();
				smi12 = new SubMenuItem();
				smi12.Source = feArticleManagement;
				smi12.Related = nmi10;
				smi12.SortOrder = 600;
				smi12.Save();
				nmi10 = ObjektFactory.New<NavigationMenuItem>("ac305a86a9874854a8c22f4c40d1c0b3@NavigationMenuItem", "NavigationMenuItem@Klass");
				nmi10.IsWithinThePlatform = true;
				nmi10.OpenedMode = ObjektFactory.Find<Value>("db3acb3464bf4c3293ac0a069b890eed@Value");
				nmi10.SetProperty("isPage", true);
				nmi10.Url = "/FeArticleComment";
				nmi10.Name = "FeArticleComment";
				nmi10.Label = "文章评论";
				nmi10.FaIcon = "fa fa-commenting-o";
				nmi10.Description = "打开文章评论";
				nmi10.Save();
				smi12 = new SubMenuItem();
				smi12.Source = feArticleManagement;
				smi12.Related = nmi10;
				smi12.SortOrder = 700;
				smi12.Save();
				nmi10 = ObjektFactory.New<NavigationMenuItem>("2bc6cc144e0a416b93c63cc48ae6a3b0@NavigationMenuItem", "NavigationMenuItem@Klass");
				nmi10.IsWithinThePlatform = true;
				nmi10.OpenedMode = ObjektFactory.Find<Value>("db3acb3464bf4c3293ac0a069b890eed@Value");
				nmi10.SetProperty("isPage", true);
				nmi10.Url = "/FeArticleCommentTrash";
				nmi10.Name = "FeArticleCommentTrash";
				nmi10.Label = "评论回收站";
				nmi10.FaIcon = "fa fa-trash";
				nmi10.Description = "打开文章评论回收站";
				nmi10.Save();
				smi12 = new SubMenuItem();
				smi12.Source = feArticleManagement;
				smi12.Related = nmi10;
				smi12.SortOrder = 800;
				smi12.Save();
				nmi10 = ObjektFactory.New<NavigationMenuItem>("a5388403cff24419bbdb2ee13273b6c7@NavigationMenuItem", "NavigationMenuItem@Klass");
				nmi10.IsWithinThePlatform = true;
				nmi10.OpenedMode = ObjektFactory.Find<Value>("db3acb3464bf4c3293ac0a069b890eed@Value");
				nmi10.SetProperty("isPage", true);
				nmi10.Url = "/FeChannel";
				nmi10.Name = "FeChannel";
				nmi10.Label = "频道管理";
				nmi10.FaIcon = "fa fa-th-list";
				nmi10.Description = "打开频道管理";
				nmi10.Save();
				smi12 = new SubMenuItem();
				smi12.Source = feArticleManagement;
				smi12.Related = nmi10;
				smi12.SortOrder = 900;
				smi12.Save();
				nmi10 = ObjektFactory.New<NavigationMenuItem>("819bd01b07c74d83b326878d8c84f909@NavigationMenuItem", "NavigationMenuItem@Klass");
				nmi10.IsWithinThePlatform = true;
				nmi10.OpenedMode = ObjektFactory.Find<Value>("db3acb3464bf4c3293ac0a069b890eed@Value");
				nmi10.SetProperty("isPage", true);
				nmi10.Url = "/FeArticleTemplate";
				nmi10.Name = "FeArticleTemplate";
				nmi10.Label = "模板管理";
				nmi10.FaIcon = "fa fa-folder-o";
				nmi10.Description = "打开文章模板管理";
				nmi10.Save();
				smi12 = new SubMenuItem();
				smi12.Source = feArticleManagement;
				smi12.Related = nmi10;
				smi12.SortOrder = 1000;
				smi12.Save();
				nmi10 = ObjektFactory.New<NavigationMenuItem>("c454a1f879bf4edabfcf549994dd73f2@NavigationMenuItem", "NavigationMenuItem@Klass");
				nmi10.IsWithinThePlatform = true;
				nmi10.OpenedMode = ObjektFactory.Find<Value>("db3acb3464bf4c3293ac0a069b890eed@Value");
				nmi10.SetProperty("isPage", false);
				nmi10.Url = "/FeArticleConfiguration";
				nmi10.Name = "FeArticleConfiguration";
				nmi10.Label = "文章配置";
				nmi10.FaIcon = "fa fa-sliders";
				nmi10.Description = "打开文章配置";
				nmi10.Save();
				smi12 = new SubMenuItem();
				smi12.Source = feArticleManagement;
				smi12.Related = nmi10;
				smi12.SortOrder = 1100;
				smi12.Save();
			}
		}

		/// <summary>
		/// 菜单项
		/// </summary>
		private void Modeling1_0_0_SystemConfiguration()
		{
			if (!ObjektFactory.IsExists("2a387c9abde041ddbdebf3d4e77b07db@Value"))
			{
				PersistenceContext.Accept();
				PersistenceContext.BeginTransaction(new SessionContext("建模", ObjektFactory.Find<User>("e2c4e2f4ecec4d6d8dce2fe6c831352e@User")));
				List configurationType = ObjektFactory.Find<List>("d522d23ba5b648b0819751eb746d8e1f@List");
				Value v4 = ObjektFactory.New<Value>("2a387c9abde041ddbdebf3d4e77b07db@Value", "Value@Klass");
				v4.Source = configurationType;
				v4.SortOrder = 500;
				v4.Value_ = "FeArticle";
				v4.Label = "文章配置";
				v4.Description = "文章管理相关配置";
				v4.Save();
				SystemConfiguration con8 = ObjektFactory.New<SystemConfiguration>("58e502ecf56144cdb67ff129b6e6e4d5@SystemConfiguration", "SystemConfiguration@Klass");
				con8.ConfigurationType = v4;
				con8.Name = "文章审核开关";
				SystemConfiguration systemConfiguration = con8;
				bool flag = true;
				systemConfiguration.Value = flag.ToString();
				con8.DataType = ObjektFactory.Find<Value>("f7036b21e6e34919b504df2cfc2d88e2@Value");
				con8.SortOrder = 100;
				con8.Description = "文章是否需要审核";
				con8.Save();
				List articleCommentType = ObjektFactory.New<List>("13ba6deda4854abb873a1388d7eee6df@List", "List@Klass");
				articleCommentType.Name = "FeArticleCommentType";
				articleCommentType.Label = "文章评论开关";
				articleCommentType.Description = "文章全局评论开关，分为全部开启、全部开启、文章内决定";
				articleCommentType.Save();
				Value v3 = ObjektFactory.New<Value>("48632d3bf3dd4c899480044e5b0355da@Value", Klass.ForId("Value@Klass"));
				v3.Source = articleCommentType;
				v3.SortOrder = 100;
				v3.Value_ = "On";
				v3.Label = "全部开启";
				v3.SetProperty("description", "文章评论全部开启");
				v3.Save();
				v3 = ObjektFactory.New<Value>("377ed2338daa463090f7c0c19195eb5c@Value", Klass.ForId("Value@Klass"));
				v3.Source = articleCommentType;
				v3.SortOrder = 200;
				v3.Value_ = "ByArticle";
				v3.Label = "文章内决定";
				v3.SetProperty("description", "文章评论是否开启由文章决定");
				v3.Save();
				v3 = ObjektFactory.New<Value>("6d5504f429304db5914989c2f2f98f3f@Value", Klass.ForId("Value@Klass"));
				v3.Source = articleCommentType;
				v3.SortOrder = 300;
				v3.Value_ = "Off";
				v3.Label = "全部关闭";
				v3.SetProperty("description", "文章评论全部关闭");
				v3.Save();
				con8 = ObjektFactory.New<SystemConfiguration>("249509bf3a0d414ead15911013ad7dbb@SystemConfiguration", "SystemConfiguration@Klass");
				con8.ConfigurationType = v4;
				con8.Name = "文章全局评论开关";
				con8.Value = "48632d3bf3dd4c899480044e5b0355da@Value";
				con8.DataType = ObjektFactory.Find<Value>("ab097f34fd9d4b7ca216084e6386b99e@Value");
				con8.ListDataSource = articleCommentType;
				con8.SortOrder = 200;
				con8.Description = "文章全局评论开关";
				con8.Save();
				con8 = ObjektFactory.New<SystemConfiguration>("f7fa2e0a9bc4462892e19210c804fdf3@SystemConfiguration", "SystemConfiguration@Klass");
				con8.ConfigurationType = v4;
				con8.Name = "文章评论审核";
				SystemConfiguration systemConfiguration2 = con8;
				flag = true;
				systemConfiguration2.Value = flag.ToString();
				con8.DataType = ObjektFactory.Find<Value>("f7036b21e6e34919b504df2cfc2d88e2@Value");
				con8.SortOrder = 300;
				con8.Description = "文章评论审核是否开启";
				con8.Save();
				con8 = ObjektFactory.New<SystemConfiguration>("466370a15ebc45f2959909b13c9d5b20@SystemConfiguration", "SystemConfiguration@Klass");
				con8.ConfigurationType = v4;
				con8.Name = "评论回复审核";
				SystemConfiguration systemConfiguration3 = con8;
				flag = true;
				systemConfiguration3.Value = flag.ToString();
				con8.DataType = ObjektFactory.Find<Value>("f7036b21e6e34919b504df2cfc2d88e2@Value");
				con8.SortOrder = 400;
				con8.Description = "评论回复审核是否开启";
				con8.Save();
				con8 = ObjektFactory.New<SystemConfiguration>("f86f83ae533849388c3f8349c2b7488d@SystemConfiguration", "SystemConfiguration@Klass");
				con8.ConfigurationType = v4;
				con8.Name = "是否允许游客评论";
				SystemConfiguration systemConfiguration4 = con8;
				flag = true;
				systemConfiguration4.Value = flag.ToString();
				con8.DataType = ObjektFactory.Find<Value>("f7036b21e6e34919b504df2cfc2d88e2@Value");
				con8.SortOrder = 500;
				con8.Description = "是否允许游客评论";
				con8.Save();
				con8 = ObjektFactory.New<SystemConfiguration>("e74209135c9b4212823ac1e30608e8b8@SystemConfiguration", "SystemConfiguration@Klass");
				con8.ConfigurationType = v4;
				con8.Name = "无评论时缺省文字";
				con8.Value = "如果您对本文章有什么评论或经验,欢迎分享!";
				con8.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				con8.SortOrder = 600;
				con8.Description = "无评论时缺省文字";
				con8.Save();
				con8 = ObjektFactory.New<SystemConfiguration>("60988a1e1ef34ab6a315e5866d720533@SystemConfiguration", "SystemConfiguration@Klass");
				con8.ConfigurationType = v4;
				con8.Name = "评论等待审核提示";
				con8.Value = "您的问题已经提交成功,管理员会尽快回复!";
				con8.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				con8.SortOrder = 700;
				con8.Description = "评论等待审核提示";
				con8.Save();
				con8 = ObjektFactory.New<SystemConfiguration>("0e80e1bcb4404286bf226c547beeb4fd@SystemConfiguration", "SystemConfiguration@Klass");
				con8.ConfigurationType = v4;
				con8.Name = "评论回复成功提示";
				con8.Value = "评论提交成功!";
				con8.DataType = ObjektFactory.Find<Value>("0ce934524195428aa506260b0f97baf0@Value");
				con8.SortOrder = 800;
				con8.Description = "评论回复成功提示";
				con8.Save();
			}
		}

		private void Modeling1_0_1()
		{
		}
	}
}
