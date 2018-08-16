using System;

namespace GroupflyGroup.Platform.Extension.Doc
{
	public abstract class EventContextDescriptor
	{
		public abstract Type EventContextType
		{
			get;
		}

		public abstract Type ParameterType
		{
			get;
		}

		public abstract string ParameterDescription
		{
			get;
		}

		public abstract PropertyDescriptor[] GetPropertyMetadata();
	}
}
