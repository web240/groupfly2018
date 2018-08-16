using GroupflyGroup.Platform.Extension.Doc.Internal;
using System;

namespace GroupflyGroup.Platform.Extension.Doc
{
	public abstract class DescriptorFactory
	{
		private static DescriptorFactory descriptorFactory_0 = new ReflectedDescriptorFactory();

		public static DescriptorFactory Instance => descriptorFactory_0;

		public EventListenerDescriptor CreateDescriptor<T>() where T : EventListener
		{
			return CreateDescriptor(typeof(T));
		}

		public EventListenerDescriptor CreateDescriptor(Type eventReceiverType)
		{
			int num = 13;
			if (!typeof(EventListener).IsAssignableFrom(eventReceiverType))
			{
				throw new ArgumentException("eventListenerType 必须是 EventReceiver 类或其子类。");
			}
			return CreateDescriptorCore(eventReceiverType);
		}

		protected abstract EventListenerDescriptor CreateDescriptorCore(Type eventListenerType);
	}
}
