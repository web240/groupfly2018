using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;

namespace GroupflyGroup.Platform.Extension
{
	public class BeforeDoExtensionPoint<TBeforeDoEventListener> : ExtensionPoint where TBeforeDoEventListener : BeforeDoEventListener
	{
		public void DoBefore(Event event_0)
		{
			List<BeforeDoEventListener> list = new List<BeforeDoEventListener>();
			IEnumerable<Lazy<TBeforeDoEventListener>> exports = ((ExportProvider)CompositionContainer()).GetExports<TBeforeDoEventListener>();
			try
			{
				foreach (Lazy<TBeforeDoEventListener> item in exports)
				{
					BeforeDoEventListener value = item.Value;
					list.Add(value);
				}
				list.Sort();
				foreach (BeforeDoEventListener item2 in list)
				{
					item2.DoBefore(event_0);
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				CompositionContainer().ReleaseExports<TBeforeDoEventListener>(exports);
			}
		}
	}
}
