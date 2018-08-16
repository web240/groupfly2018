using GroupflyGroup.Platform.ObjectFramework.Caching;
using GroupflyGroup.Platform.ObjectFramework.Persistence;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace GroupflyGroup.Platform.ObjectFramework.WebAdapter
{
	/// <summary>
	/// Web会话数据缓存
	/// </summary>
	public class WebSessionDataCache : IDisposable
	{
		private Dictionary<string, Objekt> _objektBuffer;

		private Dictionary<string, object> _objectBuffer;

		private static readonly string webSessionDataCacheKey = "WebSessionDataCache";

		private static WebSessionDataCache CurrentInstance
		{
			get
			{
				object data = CallContext.GetData(webSessionDataCacheKey);
				if (data == null)
				{
					CurrentInstance = new WebSessionDataCache();
				}
				return data as WebSessionDataCache;
			}
			set
			{
				CallContext.SetData(webSessionDataCacheKey, value);
			}
		}

		private WebSessionDataCache()
		{
			_objektBuffer = new Dictionary<string, Objekt>();
			_objectBuffer = new Dictionary<string, object>();
			PersistenceContext.RegisterAutoDisposed(this);
		}

		private static string GenerateKey(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new Exception("会话数据缓存键不允许为空！");
			}
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
			key = key + "_" + session.SessionID;
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.Unicode.GetBytes(key);
			byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		/// <summary>
		/// 设置数据（可序列化对象）
		/// </summary>
		/// <param name="key">键（在当前会话中保证唯一）</param>
		/// <param name="value">数据</param>
		public static void Set(string key, object value)
		{
			key = GenerateKey(key);
			if (CurrentInstance._objectBuffer.ContainsKey(key))
			{
				CurrentInstance._objectBuffer.Remove(key);
			}
			CurrentInstance._objectBuffer.Add(key, value);
			Cache.Current.Set(key, value, new TimeSpan(0, 1, 0, 0));
		}

		/// <summary>
		/// 设置实体对象
		/// </summary>
		/// <param name="objekt">实体对象</param>
		public static void SetObjekt(Objekt objekt)
		{
			string key = GenerateKey(objekt.Id);
			if (CurrentInstance._objektBuffer.ContainsKey(key))
			{
				CurrentInstance._objektBuffer.Remove(key);
			}
			CurrentInstance._objektBuffer.Add(key, objekt);
			Cache.Current.Set(key, objekt, new TimeSpan(0, 1, 0, 0));
		}

		/// <summary>
		/// 获取数据，不存在则返回null
		/// </summary>
		/// <param name="key">键（在当前会话中保证唯一）</param>
		/// <returns>数据</returns>
		public static object Get(string key)
		{
			key = GenerateKey(key);
			if (CurrentInstance._objectBuffer.TryGetValue(key, out object value))
			{
				return value;
			}
			value = Cache.Current.Get(key);
			if (value != null)
			{
				CurrentInstance._objectBuffer.Add(key, value);
				return value;
			}
			return null;
		}

		/// <summary>
		/// 获取实体对象，不存在则返回null
		/// </summary>
		/// <param name="objektId">对象id</param>
		/// <returns>实体对象</returns>
		public static Objekt GetObjekt(string objektId)
		{
			objektId = GenerateKey(objektId);
			if (CurrentInstance._objektBuffer.TryGetValue(objektId, out Objekt value))
			{
				return value;
			}
			value = (Cache.Current.Get(objektId) as Objekt);
			if (value != null)
			{
				CurrentInstance._objektBuffer.Add(objektId, value);
				return value;
			}
			return null;
		}

		/// <summary>
		/// 移除数据，如不存在项，则忽略
		/// </summary>
		/// <param name="key">键（在当前会话中保证唯一）</param>
		public static void Remove(string key)
		{
			key = GenerateKey(key);
			CurrentInstance._objectBuffer.Remove(key);
		}

		/// <summary>
		/// 移除对象
		/// </summary>
		/// <param name="objektId">对象id</param>
		public static void RemoveObjekt(string objektId)
		{
			objektId = GenerateKey(objektId);
			CurrentInstance._objektBuffer.Remove(objektId);
		}

		/// <summary>
		/// 消除
		/// </summary>
		public void Dispose()
		{
			_objektBuffer = null;
			_objectBuffer = null;
			CurrentInstance = null;
		}
	}
}
