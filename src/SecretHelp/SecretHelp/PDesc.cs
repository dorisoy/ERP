using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SecretHelp
{
	public class PDesc {
		public static string Key {
			get {
				return "b90fdc03";
			}
		}
		private PDesc() {
		}
		public static string Encrypt(string pToEncrypt, string sKey) {
			DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
			byte[] bytes = Encoding.Default.GetBytes(pToEncrypt);
			dESCryptoServiceProvider.Key = Encoding.UTF8.GetBytes(sKey);
			dESCryptoServiceProvider.IV = Encoding.UTF8.GetBytes(sKey);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = memoryStream.ToArray();
			for (int i = 0; i < array.Length; i++) {
				byte b = array[i];
				stringBuilder.AppendFormat("{0:X2}", b);
			}
			stringBuilder.ToString();
			return stringBuilder.ToString();
		}
		public static string Decrypt(string pToDecrypt, string sKey) {
			DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
			byte[] array = new byte[pToDecrypt.Length / 2];
			for (int i = 0; i < pToDecrypt.Length / 2; i++) {
				int num = Convert.ToInt32(pToDecrypt.Substring(i * 2, 2), 16);
				array[i] = (byte)num;
			}
			dESCryptoServiceProvider.Key = Encoding.UTF8.GetBytes(sKey);
			dESCryptoServiceProvider.IV = Encoding.UTF8.GetBytes(sKey);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(array, 0, array.Length);
			cryptoStream.FlushFinalBlock();
			new StringBuilder();
			return Encoding.Default.GetString(memoryStream.ToArray());
		}
	}
}
