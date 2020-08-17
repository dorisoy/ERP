using System;
using System.Management;
using System.Net.NetworkInformation;
namespace SecretHelp {
	public class SecretAuth {
		public const string CheckCode = "PaiXie599488377";
		/// <summary>
		/// 带mac解密
		/// </summary>
		/// <param name="Code">待解密字符串</param>
		/// <returns></returns>
		public static string DeAuth(string Code) {
			return SecretAuth.DeAuth(Code, false);
		}
		/// <summary>
		/// 解密
		/// </summary>
		/// <param name="inCode">待解密字符串</param>
		/// <param name="isTest">是否测试环境 测试环境不绑定mac地址</param>
		/// <returns></returns>
		public static string DeAuth(string Code, bool isTest) {
			string str = SecretAuth.GetMacAddress();
			if (isTest) {
				str = "";
			}
			string appKey = str + CheckCode;
			string sKey = MD5Helper.Encrypt_MD5(appKey).Substring(8, 8);
			return PDesc.Decrypt(Code, sKey);
		}

		/// <summary>
		/// 带mac加密
		/// </summary>
		/// <param name="xmlStr">待加密字符串</param>
		/// <returns></returns>
		public static string EnAuth(string xmlStr) {
			return SecretAuth.EnAuth(xmlStr, false);
		}

		/// <summary>
		/// 加密
		/// </summary>
		/// <param name="xmlStr">待加密字符串</param>
		/// <param name="isTest">是否测试环境 测试环境不绑定mac地址</param>
		/// <returns></returns>
		public static string EnAuth(string xmlStr, bool isTest) {
			string str = SecretAuth.GetMacAddress();
			if (isTest) {
				str = "";
			}
			string appKey = str + CheckCode;
			string sKey = MD5Helper.Encrypt_MD5(appKey).Substring(8, 8);
			return PDesc.Encrypt(xmlStr, sKey);
		}

		/// <summary>
		/// 获取mac地址
		/// </summary>
		/// <returns></returns>
		private static string GetMacAddress() {
			const int MIN_MAC_ADDR_LENGTH = 12;
			string macAddress = string.Empty;
			long maxSpeed = -1;

			foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces()) {
				string tempMac = nic.GetPhysicalAddress().ToString();
				if (nic.Speed > maxSpeed &&
					!string.IsNullOrEmpty(tempMac) &&
					tempMac.Length >= MIN_MAC_ADDR_LENGTH) {
					maxSpeed = nic.Speed;
					macAddress = tempMac;
				}
			}

			return macAddress;
		}
	}
}