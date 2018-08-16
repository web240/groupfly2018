using System;

namespace GroupflyGroup.Platform.Extension.Doc.Internal
{
	public class StaticPropertyDescriptor : PropertyDescriptor
	{
		private Type type_0;

		private string string_0;

		private string string_1;

		public override Type Type => type_0;

		public override string Name => string_0;

		public override string Description => string_1;

		public StaticPropertyDescriptor(Type type, string name, string description)
		{
			type_0 = type;
			string_0 = (string.IsNullOrWhiteSpace(name) ? string.Empty : name);
			string_1 = (string.IsNullOrWhiteSpace(description) ? string.Empty : description);
		}
	}
}
