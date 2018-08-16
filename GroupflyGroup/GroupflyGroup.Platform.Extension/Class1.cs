using GroupflyGroup.Platform.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;

internal class Class1<T, U> : ExtensionPoint where T : Class0<U>
{
	public void method_0(Event event_0, U gparam_0)
	{
		List<Class0<U>> list = new List<Class0<U>>();
		IEnumerable<Lazy<T>> exports = ((ExportProvider)CompositionContainer()).GetExports<T>();
		try
		{
			foreach (Lazy<T> item in exports)
			{
				Class0<U> value = item.Value;
				list.Add(value);
			}
			list.Sort();
			foreach (Class0<U> item2 in list)
			{
				item2.vmethod_0(event_0, gparam_0);
			}
		}
		catch
		{
			throw;
		}
		finally
		{
			CompositionContainer().ReleaseExports<T>(exports);
		}
	}
}
