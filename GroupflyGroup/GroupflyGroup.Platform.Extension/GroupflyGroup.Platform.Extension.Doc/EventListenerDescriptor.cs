using System;
using System.Reflection;

namespace GroupflyGroup.Platform.Extension.Doc
{
	public abstract class EventListenerDescriptor : ICustomAttributeProvider
	{
		public abstract Type EventListenerType
		{
			get;
		}

		public abstract string DisplayName
		{
			get;
		}

		public abstract string Description
		{
			get;
		}

		public abstract string TriggerWhen
		{
			get;
		}

		public abstract EventContextDescriptor EventContextMetadata
		{
			get;
		}

		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return GetCustomAttributes(typeof(object), inherit);
		}

		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			int num = 2;
			if (attributeType == (Type)null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return (object[])Array.CreateInstance(attributeType, 0);
		}

		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			int num = 16;
			if (attributeType == (Type)null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return false;
		}
	}
}
