using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;

namespace GroupflyGroup.Platform.Extension
{
	public class DoExtensionPoint<TDoEventReceiver> : ExtensionPoint where TDoEventReceiver : DoEventListener
	{
		public void Do(Event event_0)
		{
			List<DoEventListener> list = new List<DoEventListener>();
			IEnumerable<Lazy<TDoEventReceiver>> exports = ((ExportProvider)CompositionContainer()).GetExports<TDoEventReceiver>();
			try
			{
				foreach (Lazy<TDoEventReceiver> item in exports)
				{
					DoEventListener value = item.Value;
					list.Add(value);
				}
				list.Sort();
				foreach (DoEventListener item2 in list)
				{
					item2.Do(event_0);
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				CompositionContainer().ReleaseExports<TDoEventReceiver>(exports);
			}
		}
	}
}
