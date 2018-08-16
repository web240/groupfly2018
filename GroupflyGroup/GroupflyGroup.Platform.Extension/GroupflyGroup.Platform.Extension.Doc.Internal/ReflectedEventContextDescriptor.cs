using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GroupflyGroup.Platform.Extension.Doc.Internal
{
	public class ReflectedEventContextDescriptor : EventContextDescriptor
	{
		private EventMetadataAttribute eventMetadataAttribute_0;

		public EventMetadataAttribute EventMetadataAttribute => eventMetadataAttribute_0;

		public override Type EventContextType => EventMetadataAttribute.EventType;

		public override Type ParameterType => EventMetadataAttribute.ParameterType;

		public override string ParameterDescription => EventMetadataAttribute.ParameterDescription;

		public ReflectedEventContextDescriptor(EventMetadataAttribute eventMetadataAttribute)
		{
			int num = 13;
			base._002Ector();
			if (eventMetadataAttribute == null)
			{
				throw new ArgumentNullException("eventMetadataAttribute");
			}
			eventMetadataAttribute_0 = eventMetadataAttribute;
		}

		public override PropertyDescriptor[] GetPropertyMetadata()
		{
			int num = 3;
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			if (EventContextType == (Type)null)
			{
				return list.ToArray();
			}
			list.Add(method_2(ParameterType, "Parameter", ParameterDescription));
			if (EventContextType == typeof(Event))
			{
				return list.ToArray();
			}
			IOrderedEnumerable<PropertyInfo> source = EventContextType.GetProperties(method_0()).OrderBy(_003C_003Ec._003C_003E9.method_0);
			list.AddRange(source.Select(method_3));
			return list.ToArray();
		}

		private BindingFlags method_0()
		{
			return BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
		}

		private PropertyDescriptor method_1(PropertyInfo propertyInfo_0)
		{
			return new ReflectedPropertyDescriptor(propertyInfo_0);
		}

		private PropertyDescriptor method_2(Type type_0, string string_0, string string_1)
		{
			return new StaticPropertyDescriptor(type_0, string_0, string_1);
		}

		[CompilerGenerated]
		private PropertyDescriptor method_3(PropertyInfo propertyInfo_0)
		{
			return method_1(propertyInfo_0);
		}
	}
}
