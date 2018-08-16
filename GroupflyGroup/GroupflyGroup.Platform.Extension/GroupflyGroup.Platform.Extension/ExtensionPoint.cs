using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;

namespace GroupflyGroup.Platform.Extension
{
	public abstract class ExtensionPoint
	{
		private static CompositionContainer compositionContainer_0;

		static ExtensionPoint()
		{
			compositionContainer_0 = new CompositionContainer(new DirectoryCatalog(Path.GetDirectoryName(new Uri(typeof(ExtensionPoint).Assembly.CodeBase).AbsolutePath)), true, Array.Empty<ExportProvider>());
		}

		protected CompositionContainer CompositionContainer()
		{
			return compositionContainer_0;
		}
	}
}
