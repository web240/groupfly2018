using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;

namespace GroupflyGroup.Platform.Extension
{
	[Description("创建组件扩展点。\r\nTCreateComponentEventListener 事件监听器类型，一个继承自CreateComponentEventListener的抽象类。扩展插件代码继承此抽象类，实现，并[Export]。\r\nTComponent 可由扩展开发插件创建的组件的类型")]
	public class CreateComponentExtensionPoint<TCreateComponentEventListener, TComponent> : ExtensionPoint where TCreateComponentEventListener : CreateComponentEventListener<TComponent>
	{
		[Description("创建组件。\r\nEvent e 事件 \r\n返回创建的组件")]
		public TComponent CreateComponent(Event event_0)
		{
			List<CreateComponentEventListener<TComponent>> list = new List<CreateComponentEventListener<TComponent>>();
			IEnumerable<Lazy<TCreateComponentEventListener>> exports = ((ExportProvider)CompositionContainer()).GetExports<TCreateComponentEventListener>();
			try
			{
				foreach (Lazy<TCreateComponentEventListener> item in exports)
				{
					CreateComponentEventListener<TComponent> value = item.Value;
					list.Add(value);
				}
				list.Sort();
				TComponent val = default(TComponent);
				foreach (CreateComponentEventListener<TComponent> item2 in list)
				{
					val = item2.CreateComponent(event_0);
					if (val != null)
					{
						return val;
					}
				}
				return val;
			}
			catch
			{
				throw;
			}
			finally
			{
				CompositionContainer().ReleaseExports<TCreateComponentEventListener>(exports);
			}
		}
	}
}
