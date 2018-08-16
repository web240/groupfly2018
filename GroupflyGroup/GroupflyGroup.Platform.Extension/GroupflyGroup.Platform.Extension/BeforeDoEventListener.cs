using System.ComponentModel;

namespace GroupflyGroup.Platform.Extension
{
	[Description("前处理事件监听器。扩展开发插件定义的具体事件监听器由此类派生。")]
	public abstract class BeforeDoEventListener : EventListener
	{
		[Description("前处理,Event e 事件。")]
		public abstract void DoBefore(Event event_0);
	}
}
