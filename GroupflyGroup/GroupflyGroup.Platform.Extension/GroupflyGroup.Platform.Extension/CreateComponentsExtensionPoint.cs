using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;

namespace GroupflyGroup.Platform.Extension
{
	public class CreateComponentsExtensionPoint<TCreateComponentEventListener, TComponent> : ExtensionPoint where TCreateComponentEventListener : CreateComponentEventListener<TComponent>
	{
		public IList<TComponent> CreateComponents(Event event_0)
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
				IList<TComponent> list2 = new List<TComponent>();
				foreach (CreateComponentEventListener<TComponent> item2 in list)
				{
					TComponent val = item2.CreateComponent(event_0);
					if (val != null)
					{
						list2.Add(val);
					}
				}
				return list2;
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
