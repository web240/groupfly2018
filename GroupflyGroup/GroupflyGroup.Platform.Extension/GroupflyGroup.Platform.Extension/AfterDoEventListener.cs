using System.ComponentModel;

namespace GroupflyGroup.Platform.Extension
{
	[Description("后处理事件监听器。扩展开发定义的具体事件监听器由此类派生。")]
	public abstract class AfterDoEventListener : EventListener
	{
		[Description("后处理, Event e 事件")]
		public abstract void DoAfter(Event event_0);
	}
}
