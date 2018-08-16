using System;

namespace GroupflyGroup.Platform.Extension.Doc.Internal
{
	public sealed class ReflectedDescriptorFactory : DescriptorFactory
	{
		protected override EventListenerDescriptor CreateDescriptorCore(Type eventListenerType)
		{
			return new ReflectedEventListenerDescriptor(eventListenerType);
		}
	}
}
