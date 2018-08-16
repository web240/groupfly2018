using System;
using System.Linq;
using System.Reflection;

namespace GroupflyGroup.Platform.Extension.Doc.Internal
{
	public static class ICustomAttributeProviderExtension
	{
		public static TProp GetCustomAttributeProperty<TAttr, TProp>(this ICustomAttributeProvider provider, Func<TAttr, TProp> selector, TProp defaultValue = default(TProp), bool inherit = false)
		{
			TAttr customAttribute = provider.GetCustomAttribute<TAttr>(inherit);
			if (customAttribute == null)
			{
				return defaultValue;
			}
			return selector(customAttribute);
		}

		public static TAttr GetCustomAttribute<TAttr>(this ICustomAttributeProvider provider, bool inherit = false)
		{
			return provider.GetCustomAttributes(typeof(TAttr), inherit).Cast<TAttr>().FirstOrDefault();
		}
	}
}
