using GroupflyGroup.Platform.ObjectFramework.Persistence;
using GroupflyGroup.Platform.ObjectFramework.Utils;
using System;
using System.Web;
using System.Web.SessionState;

namespace GroupflyGroup.Platform.ObjectFramework.WebAdapter
{
	/// <summary>
	/// Web应用会话上下文持有器，单例，多线程安全。
	/// </summary>
	public class WebSessionContextHolder : SessionContextHolder
	{
		private static WebSessionContextHolder _instance;

		private readonly string sessionContextKey = "sessionContext";

		private WebSessionContextHolder()
		{
		}

		static WebSessionContextHolder()
		{
			_instance = new WebSessionContextHolder();
		}

		/// <summary>
		/// 获取实例
		/// </summary>
		/// <returns></returns>
		public static WebSessionContextHolder GetInstance()
		{
			return _instance;
		}

		/// <summary>
		/// 设置（使持有）会话上下文
		/// </summary>
		/// <param name="sc">(使持有）会话上下文，为null，则清空当前持有的会话上下文</param>
		public override void Set(SessionContext sc)
		{
			HttpContext current = HttpContext.Current;
			if (current == null)
			{
				throw new Exception("当前计算无有效HTTP上下文！");
			}
			HttpSessionState session = current.Session;
			if (session == null)
			{
				throw new Exception("当前HTTP上下文无会话状态！");
			}
			session[sessionContextKey] = sc;
		}

		/// <summary>
		/// 获取会话上下文，如没有设置会话上下文，则返回guest身份的会话上下文
		/// </summary>
		/// <returns>持有的会话上下文，如没有设置会话上下文，则返回guest身份的会话上下文</returns>
		public override SessionContext Get()
		{
			HttpContext current = HttpContext.Current;
			if (current == null)
			{
				throw new Exception("当前计算无有效HTTP上下文！");
			}
			HttpSessionState session = current.Session;
			if (session == null)
			{
				return new SessionContext(ObjektFactory.Find<User>("28ca8458ea9748c1a496c567a36fad31@User"));
			}
			if (session[sessionContextKey] != null)
			{
				return session[sessionContextKey] as SessionContext;
			}
			return new SessionContext(ObjektFactory.Find<User>("28ca8458ea9748c1a496c567a36fad31@User"));
		}
	}
}
