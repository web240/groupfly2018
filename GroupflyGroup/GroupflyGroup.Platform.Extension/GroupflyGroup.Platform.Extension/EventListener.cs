using System;
using System.ComponentModel;

namespace GroupflyGroup.Platform.Extension
{
	[Description("顶层抽象事件监听器")]
	public abstract class EventListener : IComparable<EventListener>
	{
		[Description("事件监听器优先级。数值越大优先级越高，越先接收到事件。")]
		public abstract int Priority
		{
			get;
		}

		[Description("事件接收器描述")]
		public abstract string Description
		{
			get;
		}

		[Description("比较目标, 参数 EventListener 比较目标。返回值Int类型")]
		public virtual int CompareTo(EventListener other)
		{
			if (Priority > other.Priority)
			{
				return -1;
			}
			if (Priority == other.Priority)
			{
				return 0;
			}
			return 1;
		}
	}
}
