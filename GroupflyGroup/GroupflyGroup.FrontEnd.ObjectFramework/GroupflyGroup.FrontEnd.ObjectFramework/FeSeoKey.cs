using GroupflyGroup.Platform.ObjectFramework;
using GroupflyGroup.Platform.ObjectFramework.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// <summary>
	/// SEO关键字
	/// </summary>
	[Serializable]
	public class FeSeoKey : Objekt
	{
		/// <summary>
		/// SEO关键字
		/// </summary>
		public string Key
		{
			get
			{
				return GetProperty<string>("key");
			}
			set
			{
				SetProperty("key", value);
			}
		}

		/// <summary>
		/// 查询seo关键字。
		/// 根据value模糊查询。
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static List<FeSeoKey> FindSeoKes(string value)
		{
			ObjektCollection<FeSeoKey> oc = new ObjektCollection<FeSeoKey>(Klass.ForId("FeSeoKey@Klass"), new WhereClause("\"key\" like '%" + value + "%'"));
			return oc.ToList();
		}

		/// <summary>
		/// 设置指定对象的seo关键字。
		/// </summary>
		/// <param name="keys"></param>
		/// <param name="relationshipName"></param>
		/// <param name="sourceId"></param>
		public static void SetObjektSeo(List<string> keys, string relationshipName, string sourceId)
		{
			List<FeSeoKey> entityKeys = new List<FeSeoKey>();
			foreach (string key in keys)
			{
				FeSeoKey entity = new ObjektCollection<FeSeoKey>(Klass.ForId("FeSeoKey@Klass"), new WhereClause("\"key\" = '" + key + "'")).TryGetSingleResult();
				if (entity == null)
				{
					entity = new FeSeoKey();
					entity.Key = key;
					entity.Save();
				}
				IEnumerable<FeSeoKey> query = from t in entityKeys
				where t.Key == entity.Key
				select t;
				if (query.Count() == 0)
				{
					entityKeys.Add(entity);
				}
			}
			Objekt objekt = ObjektFactory.Find(sourceId);
			List<RelationshipObjekt> relationshipList = objekt.ROCC.GetROC(relationshipName).ToList();
			List<RelationshipObjekt> deleteList = new List<RelationshipObjekt>();
			foreach (RelationshipObjekt item3 in relationshipList)
			{
				IEnumerable<FeSeoKey> existQuery2 = from t in entityKeys
				where t.Id == item3.Related.Id
				select t;
				if (existQuery2.Count() == 0)
				{
					deleteList.Add(item3);
				}
			}
			foreach (RelationshipObjekt item4 in deleteList)
			{
				item4.Delete();
				item4.Save();
			}
			foreach (FeSeoKey item5 in entityKeys)
			{
				IEnumerable<RelationshipObjekt> existQuery = from t in relationshipList
				where t.Related.Id == item5.Id
				select t;
				if (existQuery.Count() == 0)
				{
					RelationshipObjekt relationship = ObjektFactory.New<RelationshipObjekt>(Klass.ForName(relationshipName));
					relationship.Source = objekt;
					relationship.Related = item5;
					relationship.Save();
				}
			}
		}
	}
}
