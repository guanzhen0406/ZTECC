using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Wicresoft.Solution.Utils
{
    public class EncryptTool
    {
        const string Inputkey = "CE3A8100-4702-43C0-A358-4507E8013CEE";
        public static string Encrypt(string text, string key)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text");
            }
            RijndaelManaged rijndaelManaged = RijndaelManagedExt(key);
            ICryptoTransform transform = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV);
            MemoryStream memoryStream = new MemoryStream();
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                {
                    streamWriter.Write(text);
                }
            }
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public static string Decrypt(string text, string key)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text");
            }
            if (!IsBase64String(text))
            {
                throw new Exception("The text input parameter is not base64 encoded");
            }
            RijndaelManaged rijndaelManaged = RijndaelManagedExt(key);
            ICryptoTransform transform = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
            byte[] buffer = Convert.FromBase64String(text);
            string result;
            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        result = streamReader.ReadToEnd();
                    }
                }
            }
            return result;
        }

        public static string ToMD5(string text)
        {
            MD5 md5Hasher = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(text));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static bool IsBase64String(string base64String)
        {
            base64String = base64String.Trim();
            return base64String.Length % 4 == 0 && Regex.IsMatch(base64String, "^[a-zA-Z0-9\\+/]*={0,3}$", RegexOptions.None);
        }

        private static RijndaelManaged RijndaelManagedExt(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(Inputkey, bytes);
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
            rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
            return rijndaelManaged;
        }

        /// <summary>
        /// 解密字符串(目前用于项目中营建模块)
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <returns></returns>
        public static string DesDecrypt(string pToDecrypt)
        {
            try
            {
                Byte[] key = { 142, 16, 93, 156, 78, 4, 218, 32 };
                Byte[] IV = { 55, 103, 246, 79, 36, 99, 167, 30 };
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                for (int x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                des.Key = key;
                des.IV = IV;
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                StringBuilder ret = new StringBuilder();

                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 加密字符串(目前用于项目中营建模块)
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <returns></returns>
        public static string DesEncrypt(string pToEncrypt)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

            Byte[] key = { 142, 16, 93, 156, 78, 4, 218, 32 };
            Byte[] IV = { 55, 103, 246, 79, 36, 99, 167, 30 };

            des.Key = key;
            des.IV = IV;

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
    }
}
