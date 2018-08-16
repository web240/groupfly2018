using System;
using System.ComponentModel;

namespace GroupflyGroup.Platform.Extension.Doc.Internal
{
	public class ReflectedEventListenerDescriptor : EventListenerDescriptor
	{
		private readonly Type type_0;

		public sealed override Type EventListenerType => type_0;

		public override string DisplayName => this.GetCustomAttributeProperty<DisplayNameAttribute, string>(_003C_003Ec._003C_003E9.method_0, string.Empty, false);

		public override string Description => this.GetCustomAttributeProperty<DescriptionAttribute, string>(_003C_003Ec._003C_003E9.method_1, string.Empty, false);

		public override string TriggerWhen => this.GetCustomAttributeProperty<TriggerWhenAttribute, string>(_003C_003Ec._003C_003E9.method_2, string.Empty, false);

		public override EventContextDescriptor EventContextMetadata
		{
			get
			{
				EventMetadataAttribute customAttribute = this.GetCustomAttribute<EventMetadataAttribute>(false);
				if (customAttribute == null)
				{
					return null;
				}
				return new ReflectedEventContextDescriptor(customAttribute);
			}
		}

		public ReflectedEventListenerDescriptor(Type eventListenerType)
		{
			int num = 14;
			base._002Ector();
			if (eventListenerType == (Type)null)
			{
				throw new ArgumentNullException("eventListenerType");
			}
			type_0 = eventListenerType;
		}

		public override object[] GetCustomAttributes(bool inherit)
		{
			return EventListenerType.GetCustomAttributes(inherit);
		}

		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return EventListenerType.GetCustomAttributes(attributeType, inherit);
		}

		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return EventListenerType.IsDefined(attributeType, inherit);
		}
	}
}
