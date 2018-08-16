using GroupflyGroup.Platform.Extension;
using GroupflyGroup.Platform.ObjectFramework.Extension;
using GroupflyGroup.Platform.ObjectFramework.Utils;
using System.ComponentModel.Composition;

namespace GroupflyGroup.Platform.ObjectFramework.WebAdapter.EventListener
{
	/// <summary>
	/// Web模式下自动创建会话上下文插件
	/// by James
	/// </summary>
	[Export(typeof(CreateSessionContextHolderEventListener))]
	[PartCreationPolicy(CreationPolicy.Shared)]
	public class WebCreateSessionContextHolderEventListener : CreateSessionContextHolderEventListener
	{
		/// <summary>
		/// 描述
		/// </summary>
		public override string Description => "Web模式下自动创建会话上下文持有器。";

		/// <summary>
		/// 优先级
		/// </summary>
		public override int Priority => 100;

		/// <summary>
		/// 创建组件
		/// </summary>
		/// <param name="e">事件</param>
		/// <returns>组件</returns>
		public override SessionContextHolder CreateComponent(Event e)
		{
			return WebSessionContextHolder.GetInstance();
		}
	}
}
