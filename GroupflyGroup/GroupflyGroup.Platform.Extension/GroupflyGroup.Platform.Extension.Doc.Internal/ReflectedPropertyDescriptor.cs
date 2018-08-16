using System;
using System.ComponentModel;
using System.Reflection;

namespace GroupflyGroup.Platform.Extension.Doc.Internal
{
	public class ReflectedPropertyDescriptor : PropertyDescriptor
	{
		private PropertyInfo propertyInfo_0;

		public PropertyInfo Property => propertyInfo_0;

		public override Type Type => Property.PropertyType;

		public override string Name => Property.Name;

		public override string Description => this.GetCustomAttributeProperty<DescriptionAttribute, string>(_003C_003Ec._003C_003E9.method_0, string.Empty, false);

		public ReflectedPropertyDescriptor(PropertyInfo property)
		{
			int num = 2;
			base._002Ector();
			if (property == (PropertyInfo)null)
			{
				throw new ArgumentNullException("property");
			}
			propertyInfo_0 = property;
		}

		public override object[] GetCustomAttributes(bool inherit)
		{
			return Attribute.GetCustomAttributes(Property, inherit);
		}

		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttributes(Property, attributeType, inherit);
		}

		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return Attribute.IsDefined(Property, attributeType, inherit);
		}
	}
}
