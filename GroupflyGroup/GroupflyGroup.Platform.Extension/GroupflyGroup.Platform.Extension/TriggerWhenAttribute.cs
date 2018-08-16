using System;

namespace GroupflyGroup.Platform.Extension
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public sealed class TriggerWhenAttribute : Attribute
	{
		private readonly string string_0;

		public string Occasion => string_0;

		public TriggerWhenAttribute(string occasion)
		{
			string_0 = occasion;
		}
	}
}
