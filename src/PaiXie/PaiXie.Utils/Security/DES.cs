using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using PaiXie.Utils;
namespace PaiXie.Utils
{
    public partial class ZEncypt
    {
        /// <summary>
        /// 32λKeyֵ��
        /// </summary>
        private static byte[] DESKey = new byte[] { 0x03, 0x0B, 0x13, 0x1B, 0x23, 0x2B, 0x33, 0x3B, 0x43, 0x4B, 0x9B, 0x93, 0x8B, 0x83, 0x7B, 0x73, 0x6B, 0x63, 0x5B, 0x53, 0xF3, 0xFB, 0xA3, 0xAB, 0xB3, 0xBB, 0xC3, 0xEB, 0xE3, 0xDB, 0xD3, 0xCB };

        #region DES����
        /// <summary>
        /// DES����
        /// </summary>
        /// <param name="strSource">�������ִ�</param>
        /// <returns>���ܺ���ַ���</returns>
        public static string DESEncrypt(string strSource)
        {
            return DESEncrypt(strSource, DESKey);
        }
        /// <summary>
        /// DES����
        /// </summary>
        /// <param name="strSource">�������ִ�</param>
        /// <param name="key">Keyֵ</param>
        /// <returns>���ܺ���ַ���</returns>
        public static string DESEncrypt(string strSource, byte[] key)
        {
            SymmetricAlgorithm sa = Rijndael.Create();
            sa.Key = key;
            sa.Mode = CipherMode.ECB;
            sa.Padding = PaddingMode.Zeros;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, sa.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] byt = Encoding.Unicode.GetBytes(strSource);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }
        #endregion

        #region DES����
        /// <summary>
        /// DES����
        /// </summary>
        /// <param name="strSource">�����ܵ��ִ�</param>
        /// <returns>���ܺ���ַ���</returns>
        public static string DESDecrypt(string strSource)
        {
            return DESDecrypt(strSource, DESKey);
        }
        /// <summary>
        /// DES����
        /// </summary>
        /// <param name="strSource">�����ܵ��ִ�</param>
        /// <param name="key">32λKeyֵ</param>
        /// <returns>���ܺ���ַ���</returns>
        public static string DESDecrypt(string strSource, byte[] key)
        {
            SymmetricAlgorithm sa = Rijndael.Create();
            sa.Key = key;
            sa.Mode = CipherMode.ECB;
            sa.Padding = PaddingMode.Zeros;
            ICryptoTransform ct = sa.CreateDecryptor();
            byte[] byt = Convert.FromBase64String(strSource);
            MemoryStream ms = new MemoryStream(byt);
            CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs, Encoding.Unicode);
            return sr.ReadToEnd().Trim('\0');
        }
        #endregion

    }
}