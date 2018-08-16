using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace GroupflyGroup.Platform.Extension
{
	[Description("TAfterDoEventListener 后处理事件接收器类型，一个继承自 AfterEventListener 的抽象类。扩展开发继承此抽象类，实现，并[Export]。")]
	public class AfterDoExtensionPoint<TAfterDoEventListener> : ExtensionPoint where TAfterDoEventListener : AfterDoEventListener
	{
		[Description("后处理\r\nEvent e 事件")]
		public void DoAfter(Event event_0)
		{
			List<AfterDoEventListener> list = new List<AfterDoEventListener>();
			IEnumerable<Lazy<TAfterDoEventListener>> exports = ((ExportProvider)CompositionContainer()).GetExports<TAfterDoEventListener>();
			try
			{
				list.AddRange(Enumerable.Select<Lazy<TAfterDoEventListener>, TAfterDoEventListener>(exports, (Func<Lazy<TAfterDoEventListener>, TAfterDoEventListener>)_003C_003Ec._003C_003E9.method_0));
				list.Sort();
				foreach (AfterDoEventListener item in list)
				{
					item.DoAfter(event_0);
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				CompositionContainer().ReleaseExports<TAfterDoEventListener>(exports);
			}
		}
	}
}
