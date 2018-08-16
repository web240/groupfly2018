using System.ComponentModel;

namespace GroupflyGroup.Platform.Extension
{
	[Description("处理事件监听器。扩展开发插件定义的具体事件监听器由此类派生。")]
	public abstract class DoEventListener : EventListener
	{
		[Description("处理,Event e 事件上下文")]
		public abstract void Do(Event event_0);
	}
}
