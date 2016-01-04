using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Wicresoft.Solution.Utils
{
    public class Security
    {
        const String Key = "ASBHR";
        const String IV = "~!#@$%HGF^&%&^hggk)KJKL989#er345(&*(HBh(u%g6HJ($jhWk7&!hg4ui%$hjk";

        static RijndaelManaged myRijndael = new RijndaelManaged();
        /// <summary>
        /// 获得密钥
        /// </summary>
        /// <returns>密钥</returns>
        private static byte[] GetLegalKey()
        {
            string sTemp = Key;
            myRijndael.GenerateKey();
            byte[] bytTemp = myRijndael.Key;
            int KeyLength = bytTemp.Length;
            if (sTemp.Length > KeyLength)
            {
                sTemp = sTemp.Substring(0, KeyLength);
            }
            else if (sTemp.Length < KeyLength)
            {
                sTemp = sTemp.PadRight(KeyLength, ' ');
            }
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        /// <summary>
        /// 获得初始向量IV
        /// </summary>
        /// <returns>初试向量IV</returns>
        private static byte[] GetLegalIV()
        {
            string sTemp = IV;
            myRijndael.GenerateIV();
            byte[] bytTemp = myRijndael.IV;
            int IVLength = bytTemp.Length;
            if (sTemp.Length > IVLength)
            {
                sTemp = sTemp.Substring(0, IVLength);
            }
            else if (sTemp.Length < IVLength)
            {
                sTemp = sTemp.PadRight(IVLength, ' ');
            }
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="Source">待加密的串</param>
        /// <returns>经过加密的串</returns>
        public static string Encrypt(string Source)
        {
            try
            {
                byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
                MemoryStream ms = new MemoryStream();
                myRijndael.Key = GetLegalKey();
                myRijndael.IV = GetLegalIV();
                ICryptoTransform encrypto = myRijndael.CreateEncryptor();
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();
                byte[] bytOut = ms.ToArray();
                cs.Close();
                ms.Close();
                return Convert.ToBase64String(bytOut);
            }
            catch (Exception er)
            {
                System.Diagnostics.Trace.WriteLine(er.Message);
                throw new Exception(er.Message);
            }
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="Source">待解密的串</param>
        /// <returns>经过解密的串</returns>
        public static string Decrypt(string Source)
        {
            try
            {
                byte[] bytIn = Convert.FromBase64String(Source);
                MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
                myRijndael.Key = GetLegalKey();
                myRijndael.IV = GetLegalIV();
                ICryptoTransform encrypto = myRijndael.CreateDecryptor();
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch (Exception er)
            {
                throw new Exception("在文件解密的时候出现错误！错误提示： \n" + er.Message);
            }
        }
    }
}
