using GroupflyGroup.Platform.ObjectFramework;
using System;

namespace GroupflyGroup.FrontEnd.ObjectFramework
{
	/// <summary>
	/// 频道
	/// </summary>
	[Serializable]
	public class FeImageTypeImageSize : RelationshipObjekt
	{
		public Value cutType
		{
			get
			{
				return GetProperty<Value>("cutType");
			}
			set
			{
				SetProperty("cutType", value);
			}
		}
	}
}
