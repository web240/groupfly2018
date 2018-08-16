using GroupflyGroup.Platform.ObjectFramework;
using GroupflyGroup.Platform.ObjectFramework.Persistence;
using System;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// <summary>
	/// 文章评论
	/// </summary>
	[Serializable]
	public class FeArticleComment : RelationshipObjekt
	{
		/// <summary>
		/// 文章回收后自动回收评论
		/// </summary>
		public override void AfterTrash()
		{
			base.AfterTrash();
			base.Related.Trash();
		}

		/// <summary>
		/// 文章还原后自动还原评论
		/// </summary>
		public override void AfterRevert()
		{
			base.AfterRevert();
			base.Related.Revert();
		}

		/// <summary>
		/// 文章删除后自动删除评论
		/// </summary>
		public override void AfterDelete()
		{
			base.AfterDelete();
			base.Related.Delete();
			base.Related.Save();
		}

		/// <summary>
		/// 保存之前检查文章配置
		/// </summary>
		public override void BeforeSave()
		{
			if (base.ObjektStatus == ObjektStatus.NewModified)
			{
				SystemConfiguration sysArticCommentGlobal = ObjektFactory.Find<SystemConfiguration>("249509bf3a0d414ead15911013ad7dbb@SystemConfiguration");
				switch (sysArticCommentGlobal.Value)
				{
				case "6d5504f429304db5914989c2f2f98f3f@Value":
					throw new Exception("不允许评论");
				case "377ed2338daa463090f7c0c19195eb5c@Value":
					if (!(base.Source as FeArticle).CanComment)
					{
						throw new Exception("该文章不允许评论");
					}
					break;
				default:
					throw new Exception("未检测到文章配置");
				case "48632d3bf3dd4c899480044e5b0355da@Value":
					break;
				}
				FeComment feComment = base.Related as FeComment;
				if (feComment.Parent == null)
				{
					SystemConfiguration sysArticCommentApproved2 = ObjektFactory.Find<SystemConfiguration>("f7fa2e0a9bc4462892e19210c804fdf3@SystemConfiguration");
					if (sysArticCommentApproved2.Value == "True")
					{
						feComment.ApprovalStatus = ObjektFactory.Find<Value>("fd96691ff35f46db9fb42464db910972@Value");
					}
					else
					{
						feComment.ApprovalStatus = ObjektFactory.Find<Value>("e87630c5ca374e98a454287ff8484b68@Value");
					}
				}
				else
				{
					SystemConfiguration sysArticCommentApproved = ObjektFactory.Find<SystemConfiguration>("466370a15ebc45f2959909b13c9d5b20@SystemConfiguration");
					if (sysArticCommentApproved.Value == "True")
					{
						feComment.ApprovalStatus = ObjektFactory.Find<Value>("fd96691ff35f46db9fb42464db910972@Value");
					}
					else
					{
						feComment.ApprovalStatus = ObjektFactory.Find<Value>("e87630c5ca374e98a454287ff8484b68@Value");
					}
				}
				SystemConfiguration sysArticCommentTourist = ObjektFactory.Find<SystemConfiguration>("f86f83ae533849388c3f8349c2b7488d@SystemConfiguration");
				if (sysArticCommentTourist.Value == "False" && User.Current.Id == "28ca8458ea9748c1a496c567a36fad31@User")
				{
					throw new Exception("不允许游客评论");
				}
			}
			base.BeforeSave();
		}
	}
}
