using GroupflyGroup.Platform.ObjectFramework;
using GroupflyGroup.Platform.ObjectFramework.Persistence;
using System;
using System.Collections.Generic;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// <summary>
	/// 评论
	/// </summary>
	[Serializable]
	public class FeComment : Objekt
	{
		/// <summary>
		/// 评论内容
		/// </summary>
		public string Content
		{
			get
			{
				return GetProperty<string>("content");
			}
			set
			{
				SetProperty("content", value);
			}
		}

		/// <summary>
		/// 审核状态
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
		/// 是否显示
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
		/// 审核人
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
		/// 审核时间
		/// </summary>
		public DateTime ApprovedOn
		{
			get
			{
				return GetProperty<DateTime>("approvedOn");
			}
			set
			{
				SetProperty("approvedOn", value);
			}
		}

		/// <summary>
		/// 父评论
		/// </summary>
		public FeComment Parent
		{
			get
			{
				return GetProperty<FeComment>("parent");
			}
			set
			{
				SetProperty("parent", value);
			}
		}

		public override void BeforeDelete()
		{
			ChildrenDeleteCheck();
			base.BeforeDelete();
		}

		public void ChildrenDeleteCheck()
		{
			List<FeComment> feComment = new ObjektCollection<FeComment>(Klass.ForId("FeComment@Klass"), new WhereClause("\"parent\" = '" + base.Id + "'")).ToList();
			if (feComment.Count > 0)
			{
				foreach (FeComment item in feComment)
				{
					List<FeArticleComment> feArticleCommentList = new ObjektCollection<FeArticleComment>(Klass.ForId("FeArticleComment@Klass"), new WhereClause("\"related\" = '" + item.Id + "'")).ToList();
					foreach (FeArticleComment item2 in feArticleCommentList)
					{
						item2.Delete();
						item2.Save();
					}
				}
			}
		}

		public override void BeforeTrash()
		{
			ChildrenTrashCheck();
			base.BeforeTrash();
		}

		/// <summary>
		/// 子评论回收处理
		/// </summary>
		public void ChildrenTrashCheck()
		{
			ObjektCollection<FeComment> feComment = new ObjektCollection<FeComment>(Klass.ForId("FeComment@Klass"), new WhereClause("\"parent\" = '" + base.Id + "'"));
			if (feComment.Count > 0)
			{
				foreach (FeComment item in feComment)
				{
					item.Trash();
				}
			}
		}

		public override void BeforeRevert()
		{
			ChildrenRevertCheck();
			base.BeforeRevert();
		}

		/// <summary>
		/// 子评论恢复处理
		/// </summary>
		public void ChildrenRevertCheck()
		{
			ObjektCollection<FeComment> feComment = new ObjektCollection<FeComment>(Klass.ForId("FeComment@Klass"), new WhereClause("\"parent\" = '" + base.Id + "'"));
			if (feComment.Count > 0)
			{
				foreach (FeComment item in feComment)
				{
					item.Revert();
				}
			}
		}

		public override void BeforeUpdate()
		{
			ChildrenUpdateCheck();
			base.BeforeUpdate();
		}

		public void ChildrenUpdateCheck()
		{
		}
	}
}
