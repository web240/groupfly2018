using GroupflyGroup.Platform.ObjectFramework.Persistence;
using log4net;
using System;
using System.Text;
using System.Web;

namespace GroupflyGroup.Platform.ObjectFramework.WebAdapter
{
	/// <summary>
	/// 对象框架-Web HttpModule
	/// </summary>
	public class ObjektFrameworkHttpModule : IHttpModule
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(ObjektFrameworkHttpModule));

		private static ILog diagnosis = LogManager.GetLogger("Diagnosis");

		private static HttpApplication httpApplication;

		public void Init(HttpApplication context)
		{
			httpApplication = context;
			context.BeginRequest += BeginRequest;
			context.EndRequest += EndRequest;
			context.Error += Error;
		}

		private void BeginRequest(object sender, EventArgs e)
		{
			if (PersistenceContext.IsExisting)
			{
				PersistenceContext.Discard();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(" ObjetkFrameworkWebAdapter ").Append("PersitentContextAutoClear(Discard) ").Append(PersistenceContext.GetString());
				diagnosis.Info(stringBuilder);
			}
		}

		private void EndRequest(object sender, EventArgs e)
		{
			if (PersistenceContext.IsExisting)
			{
				if (PersistenceContext.SessionContext.IsDiagnosis)
				{
					string loginName = User.Current.LoginName;
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(loginName).Append(" ObjetkFrameworkWebAdapter ").Append("PersitentContextAutoFinish(Success:Accept) ")
						.Append(PersistenceContext.GetString());
					diagnosis.Info(stringBuilder);
				}
				if (!PersistenceContext.IsTransaction)
				{
					try
					{
						PersistenceContext.Accept();
					}
					catch (Exception exception)
					{
						log.Warn("自动结束（Accept）当前持久化上下文出错。", exception);
					}
				}
				else
				{
					PersistenceContext.Accept();
				}
			}
			PersistenceContext.ClearNotProcessedSuspendeds();
		}

		private void Error(object sender, EventArgs e)
		{
			SessionContext sessionContext = null;
			if (PersistenceContext.IsExisting)
			{
				if (PersistenceContext.SessionContext.IsDiagnosis)
				{
					string loginName = User.Current.LoginName;
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(loginName).Append(" ObjetkFrameworkWebAdapter ").Append("PersitentContextAutoFinish(Failure:Discard) ")
						.Append(PersistenceContext.GetString());
					diagnosis.Info(stringBuilder);
				}
				sessionContext = PersistenceContext.SessionContext;
				if (!PersistenceContext.IsTransaction)
				{
					try
					{
						PersistenceContext.Discard();
					}
					catch (Exception exception)
					{
						log.Warn("未处理异常检查，自动结束（Discard）当前持久化上下文出错。", exception);
					}
				}
				else
				{
					PersistenceContext.Discard();
				}
			}
			string message = httpApplication.Server.GetLastError().Message;
			string stackTrace = httpApplication.Server.GetLastError().StackTrace;
			log.Error(message + stackTrace);
			try
			{
				if (sessionContext == null)
				{
					sessionContext = new SessionContext("Web未处理异常", ObjektFactory.Find<User>("e2c4e2f4ecec4d6d8dce2fe6c831352e@User"));
				}
				if (!PersistenceContext.IsExisting)
				{
					PersistenceContext.BeginTransaction(sessionContext);
				}
				Objekt value = ObjektFactory.Find("3c757b8d46364133a2061a455394d640@Value");
				Objekt value2 = ObjektFactory.Find("97dff0c8a32542bc8c28e5c10c9cc3c9@Value");
				string value3 = $"{message}发生在{HttpContext.Current.Request.RawUrl},错误堆栈{stackTrace}";
				SystemLog systemLog = new SystemLog();
				systemLog.SetProperty("type", value);
				systemLog.SetProperty("logType", value2);
				systemLog.SetProperty("abstract", message);
				systemLog.SetProperty("detail", value3);
				systemLog.Save();
				PersistenceContext.Accept();
			}
			catch
			{
				PersistenceContext.Discard();
			}
		}

		public void Dispose()
		{
		}
	}
}
