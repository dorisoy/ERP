using System.Security.Cryptography;
using System.Text;

namespace SecretHelp
{
	public class MD5Helper {
		public static string Encrypt_MD5(string AppKey) {
			MD5 mD = new MD5CryptoServiceProvider();
			byte[] bytes = Encoding.GetEncoding("gb2312").GetBytes(AppKey);
			byte[] array = mD.ComputeHash(bytes);
			StringBuilder stringBuilder = new StringBuilder(32);
			for (int i = 0; i < array.Length; i++) {
				stringBuilder.Append(array[i].ToString("x").PadLeft(2, '0'));
			}
			return stringBuilder.ToString();
		}
	}
}
