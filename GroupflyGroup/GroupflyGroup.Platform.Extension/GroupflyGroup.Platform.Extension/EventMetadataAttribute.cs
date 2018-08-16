using System;
using System.Runtime.CompilerServices;

namespace GroupflyGroup.Platform.Extension
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class EventMetadataAttribute : Attribute
	{
		private static readonly Type type_0 = typeof(Event);

		[CompilerGenerated]
		private Type type_1;

		[CompilerGenerated]
		private Type type_2;

		[CompilerGenerated]
		private string string_0;

		public Type EventType
		{
			get;
			set;
		}

		public Type ParameterType
		{
			get;
			set;
		}

		public string ParameterDescription
		{
			get;
			set;
		}

		public EventMetadataAttribute()
			: this(type_0, null, "")
		{
		}

		public EventMetadataAttribute(Type eventType)
			: this(type_0, null, "")
		{
		}

		public EventMetadataAttribute(Type parameterType, string parameterDescription)
			: this(type_0, parameterType, parameterDescription)
		{
		}

		private EventMetadataAttribute(Type type_3, Type type_4, string string_1)
		{
			EventType = type_3;
			ParameterType = type_4;
			ParameterDescription = string_1;
		}
	}
}
