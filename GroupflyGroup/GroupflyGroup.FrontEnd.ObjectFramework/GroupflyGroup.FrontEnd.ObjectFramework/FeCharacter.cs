using GroupflyGroup.Platform.ObjectFramework;
using GroupflyGroup.Platform.ObjectFramework.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// <summary>
	///     文章属性（特征），如“热点”、“头条”、“推荐”
	/// </summary>
	[Serializable]
	public class FeCharacter : Objekt
	{
		/// <summary>
		///     文章属性的名称
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
		///     文章属性的显示内容
		/// </summary>
		public string Label
		{
			get
			{
				return GetProperty<string>("label");
			}
			set
			{
				SetProperty("label", value);
			}
		}

		/// <summary>
		///     文章属性的排序序号
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
		///     Font Awesome字体图标名称
		/// </summary>
		public string Icon
		{
			get
			{
				return GetProperty<string>("faIcon");
			}
			set
			{
				SetProperty("faIcon", value);
			}
		}

		/// <summary>
		///     文章属性是否在前台显示
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
		///     创建新增对象排序。
		/// </summary>
		/// <returns></returns>
		public static decimal NewSortOrder()
		{
			ObjektCollection<FeCharacter> oc = new ObjektCollection<FeCharacter>(Klass.ForId("FeCharacter@Klass"), new WhereClause("\"sortOrder\" is not null"));
			oc.OrderByClause.Add(new OrderByCell("sortOrder", Order.Desc));
			FeCharacter character = oc.FirstOrDefault();
			int sort2 = 1;
			if (character != null && character.IsExists() && character.SortOrder.HasValue)
			{
				sort2 = (int)character.SortOrder.Value;
				sort2++;
			}
			return sort2;
		}

		/// <summary>
		///     设置指定对象的属性。
		/// </summary>
		/// <param name="characters"></param>
		/// <param name="relationshipName"></param>
		/// <param name="sourceId"></param>
		public static void SetObjektCharacter(List<string> characters, string relationshipName, string sourceId)
		{
			Objekt objekt = ObjektFactory.Find(sourceId);
			List<RelationshipObjekt> relationshipList = objekt.ROCC.GetROC(relationshipName).ToList();
			foreach (RelationshipObjekt item in relationshipList)
			{
				item.Delete();
				item.Save();
			}
			foreach (string character in characters)
			{
				RelationshipObjekt relationship = ObjektFactory.New<RelationshipObjekt>(Klass.ForName(relationshipName));
				relationship.Source = objekt;
				relationship.Related = ObjektFactory.Find(character);
				relationship.Save();
			}
		}
	}
}
