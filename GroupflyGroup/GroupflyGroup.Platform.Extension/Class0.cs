using GroupflyGroup.Platform.Extension;
using System.ComponentModel;

[Description("加工组件事件接收器。扩展开发者定义的具体事件接收器由此类派生。TComponent 加工的组件类型")]
internal abstract class Class0<T> : EventListener
{
	[Description("加工组件,Event e 事件, TComponent component 要加工处理的组件")]
	public abstract void vmethod_0(Event event_0, T gparam_0);
}
