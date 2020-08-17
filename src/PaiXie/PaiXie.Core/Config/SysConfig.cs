using PaiXie.Cache;
using PaiXie.Utils;
using SecretHelp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace PaiXie.Core {
	/// <summary>
	/// 系统级信息配置类
	/// </summary>
	public class SysConfig {
		/// <summary>
		/// 系统配置的缓存键名
		/// </summary>
		public const string KEY_SysConfig = "Cache_PaiXieSysConfig";
		/// <summary>
		/// 系统配置的文件路径
		/// </summary>
		private const string SysConfigFilePath = "Config/Custom/Sys.config";

		 /// <summary>
        /// 系统配置信息
        /// </summary>
		public class SysConfigInfo {
			DateTime _LastModifyTime = DateTime.Now;


			string _SystemTitle = "";
			/// <summary>
			/// 系统标题
			/// </summary>
			public string SystemTitle {
				get { return _SystemTitle; }
				set { _SystemTitle = value; }
			}

			bool _IsSingleWarehouse = false;
			/// <summary>
			/// 是否单仓
			/// </summary>
			public bool IsSingleWarehouse {
				get { return _IsSingleWarehouse; }
				set { _IsSingleWarehouse = value; }
			}

			string _SystemVersion = "1.0.0";
			/// <summary>
			/// 系统版本号
			/// </summary>
			public string SystemVersion {
				get { return _SystemVersion; }
				set { _SystemVersion = value; }
			}
			DateTime _InstallTime = DateTime.Now;
			/// <summary>
			/// 系统初始安装时间
			/// </summary>
			public DateTime InstallTime {
				get { return _InstallTime; }
				set { _InstallTime = value; }
			}

			/// <summary>
			/// 系统配置信息文件最后生成时间
			/// </summary>
			public DateTime LastModifyTime {
				get { return _LastModifyTime; }
				set { _LastModifyTime = value; }
			}
		}

		/// <summary>
		/// 获取系统配置信息
		/// </summary>
		/// <returns></returns>
		public static SysConfigInfo GetSysConfigInfo() {
			SysConfigInfo info = CacheHelper.Get<SysConfigInfo>(KEY_SysConfig);
			if (info == null) {
				info = GetSysConfigCacheData();
				string _filePath = "";
				HttpContext current = HttpContext.Current;
				try {
					if (current != null) {
						_filePath = current.Server.MapPath("~/" + SysConfigFilePath);
					}
					else {
						_filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SysConfigFilePath);
					}
				}
				catch (Exception ex) {
					PlanLog.WriteLog(ex.ToString(), LogType.Error.ToString());
				}
				CacheHelper.Add<SysConfigInfo>(KEY_SysConfig, info, DateTime.Now.AddDays(7));
			}
			return info;
		}
		/// <summary>
		/// 获取用于缓存的系统配置信息原始数据
		/// </summary>
		/// <returns></returns>
		private static SysConfigInfo GetSysConfigCacheData() {
			SysConfigInfo info = new SysConfigInfo();
			string _filePath = "";
			HttpContext current = HttpContext.Current;
			try {
				if (current != null) {
					_filePath = current.Server.MapPath("~/" + SysConfigFilePath);
				}
				else {
					_filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SysConfigFilePath);
				}
				string authCode = File.ReadAllText(_filePath, Encoding.Default);
				string xmlStr = "";

				if (ZConfig.GetConfigBool("IsTestSys")) //是否为明文
                {
					try {
						XDocument.Parse(authCode);
						xmlStr = authCode;

					}
					catch //非明文
					{
						try {
							//兼容已加密文件不绑定mac
							xmlStr = SecretAuth.DeAuth(authCode, true);
						}
						catch {
							//兼容已加密文件绑定mac
							xmlStr = SecretAuth.DeAuth(authCode);
						}
					}
				}
				else {
					//正式使用绑定mac
					xmlStr = SecretAuth.DeAuth(authCode);
				}

				//解析配置信息
				if (xmlStr.Length > 10) {
					XDocument data = XDocument.Parse(xmlStr);
					XElement xe = data.Root.Element("setting");

					for (var i = 0; i < xe.Elements("item").Count(); i++) {
						var xeItem = xe.Elements("item").ToArray()[i];

						string val = xeItem.Attribute("value").Value;
						string key = xeItem.Attribute("key").Value.Trim();
						switch (key) {
							case "SystemTitle":
								info.SystemTitle = val;
								break;
							case "SystemVersion":
								info.SystemVersion = val;
								break;
							case "InstallTime":
								info.InstallTime = ZConvert.StrToDateTime(val, DateTime.Now);
								break;
							case "LastModifyTime":
								info.LastModifyTime = ZConvert.StrToDateTime(val, DateTime.Now);
								break;
							case "IsSingleWarehouse":
								info.IsSingleWarehouse = ZConvert.StrToBool(val);
								break;
							default:
								break;
						}
					}
				}
			}
			catch {
				throw;
			}

			return info;
		}
		/// <summary>
		/// 重置数据缓存
		/// </summary>
		/// <returns></returns>
		public static void ResetSysConfigInfo() {
			CacheHelper.Remove(KEY_SysConfig);
		}

		/// <summary>
		/// 系统标题
		/// </summary>
		public static string SystemTitle {
			get {
				SysConfigInfo info = GetSysConfigInfo();
				return info.SystemTitle;
			}
		}

		/// <summary>
		/// 是否单仓
		/// </summary>
		public static bool IsSingleWarehouse {
			get {
				SysConfigInfo info = GetSysConfigInfo();
				return info.IsSingleWarehouse;
			}
		}

		/// <summary>
		/// 系统版本号 如 1.0.0
		/// </summary>
		public static string SystemVersion {
			get {
				SysConfigInfo info = GetSysConfigInfo();
				return info.SystemVersion;
			}
		}
		/// <summary>
		/// 系统初始安装时间
		/// </summary>
		public static DateTime InstallTime {
			get {
				SysConfigInfo info = GetSysConfigInfo();
				return info.InstallTime;
			}
		}

		/// <summary>
		/// 系统配置信息文件最后生成时间
		/// </summary>
		public static DateTime LastModifyTime {
			get {
				SysConfigInfo info = GetSysConfigInfo();
				return info.LastModifyTime;
			}
		}
	}
}
