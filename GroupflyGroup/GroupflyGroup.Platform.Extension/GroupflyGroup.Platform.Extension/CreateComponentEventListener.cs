using System.ComponentModel;

namespace GroupflyGroup.Platform.Extension
{
	[Description("创建组件事件监听器。扩展开发插件定义的具体事件监听器由此类派生。\r\nTComponent 要创建的组件类型")]
	public abstract class CreateComponentEventListener<TComponent> : EventListener
	{
		[Description("创建组件,Event e 事件，返回值类型 TComponent 创建的组件")]
		public abstract TComponent CreateComponent(Event event_0);
	}
}
