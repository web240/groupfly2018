using System;
using System.Reflection;

namespace GroupflyGroup.Platform.Extension.Doc
{
	public abstract class PropertyDescriptor : ICustomAttributeProvider
	{
		public abstract Type Type
		{
			get;
		}

		public abstract string Name
		{
			get;
		}

		public abstract string Description
		{
			get;
		}

		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return GetCustomAttributes(typeof(object), inherit);
		}

		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			int num = 17;
			if (attributeType == (Type)null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return (object[])Array.CreateInstance(attributeType, 0);
		}

		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			int num = 4;
			if (attributeType == (Type)null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return false;
		}
	}
}
