using GroupflyGroup.Platform.ObjectFramework;
using GroupflyGroup.Platform.ObjectFramework.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// <summary>
	/// 标签
	/// </summary>
	[Serializable]
	public class FeTag : Objekt
	{
		/// <summary>
		/// 标签名称
		/// </summary>
		public string Tag
		{
			get
			{
				return GetProperty<string>("tag");
			}
			set
			{
				SetProperty("tag", value);
			}
		}

		/// <summary>
		/// 标签来源
		/// </summary>
		public Value From
		{
			get
			{
				return GetProperty<Value>("from");
			}
			set
			{
				SetProperty("from", value);
			}
		}

		/// <summary>
		/// 保存前操作
		/// </summary>
		public override void BeforeSave()
		{
			base.BeforeSave();
		}

		/// <summary>
		/// 查询Tag名称是否存在
		/// </summary>
		/// <param name="tag">Tag名称</param>
		/// <returns>true存在,false不存在</returns>
		public bool CheckTagIsExit(string tag)
		{
			ObjektCollection<Objekt> oc = new ObjektCollection<Objekt>(Klass.ForId("FeTag@Klass"), new WhereClause("\"tag\" = '" + tag + "'"));
			if (oc == null)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// 查询tag关键字。
		/// 根据value模糊查询。
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static List<FeTag> FindTags(string value)
		{
			ObjektCollection<FeTag> oc = new ObjektCollection<FeTag>(Klass.ForId("FeTag@Klass"), new WhereClause("\"tag\" like '%" + value + "%'"));
			return oc.ToList();
		}

		/// <summary>
		/// 设置指定对象的seo关键字。
		/// </summary>
		/// <param name="tags"></param>
		/// <param name="relationshipName"></param>
		/// <param name="sourceId"></param>
		public static void SetObjektTag(List<string> tags, string relationshipName, string sourceId)
		{
			List<FeTag> entityTags = new List<FeTag>();
			foreach (string tag in tags)
			{
				FeTag entity = new ObjektCollection<FeTag>(Klass.ForId("FeTag@Klass"), new WhereClause("\"tag\" = '" + tag + "'")).TryGetSingleResult();
				if (entity == null)
				{
					entity = new FeTag();
					entity.Tag = tag;
					entity.From = ObjektFactory.Find<Value>("75d290896b3d40ad802c9d6b04d61a9b@Value");
					entity.Save();
				}
				IEnumerable<FeTag> query = from t in entityTags
				where t.Tag == entity.Tag
				select t;
				if (query.Count() == 0)
				{
					entityTags.Add(entity);
				}
			}
			Objekt objekt = ObjektFactory.Find(sourceId);
			List<RelationshipObjekt> relationshipList = objekt.ROCC.GetROC(relationshipName).ToList();
			foreach (RelationshipObjekt item2 in relationshipList)
			{
				item2.Delete();
				item2.Save();
			}
			foreach (FeTag item3 in entityTags)
			{
				RelationshipObjekt relationship = ObjektFactory.New<RelationshipObjekt>(Klass.ForName(relationshipName));
				relationship.Source = objekt;
				relationship.Related = item3;
				relationship.Save();
			}
		}
	}
}
