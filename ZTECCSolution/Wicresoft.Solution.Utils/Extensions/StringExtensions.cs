using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Wicresoft.Solution.Utils
{
    public static class StringExtensions
    {
        internal const string TRUE = "TRUE";
        internal const string FALSE = "FALSE";
        internal const char TRIM_CHAR = '\\';
        internal const string SLASH = "/";
        internal const string BACKSLASH = "\\";

        public static bool ToBoolean(this string trueOrFalse)
        {
            var ret = false;
            if (string.IsNullOrEmpty(trueOrFalse) == false)
            {
                ret = trueOrFalse.Trim().EqualsOrdinalIgnoreCase(TRUE);
            }

            return ret;
        }

        public static bool? ToNullableBoolean(this string trueOrFalse)
        {
            if (string.IsNullOrEmpty(trueOrFalse)) return null;

            var ret = false;
            if (string.IsNullOrEmpty(trueOrFalse) == false)
            {
                ret = trueOrFalse.Trim().EqualsOrdinalIgnoreCase(TRUE);
            }

            return ret;
        }

        public static double ToDouble(this string doubleString)
        {
            double dValue = 0.0;
            double.TryParse(doubleString, out dValue);
            return dValue;
        }

        public static double ExcelToDouble(this string doubleString,bool IsThrowException)
        {
            double dValue = 0.0;
            if (!string.IsNullOrEmpty(doubleString))
            {
                var IsSuccess = double.TryParse(doubleString, out dValue);
                if (IsSuccess == false && IsThrowException==true)
                {
                    throw new Exception("字符串：" + doubleString + "格式错误，转换为数字失败！");
                }
            }
            return dValue;
        }

        public static double ToFloat(this string floatString)
        {
            float dValue = 0.0f;
            float.TryParse(floatString, out dValue);
            return dValue;
        }

        /// <summary>
        /// 确定两个指定的 System.String 对象是否具有相同的值,不区分大小写
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static bool EqualsOrdinalIgnoreCase(this string A, string B)
        {
            return string.Equals(A, B, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// yyyyMMddHHmmss
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? ToLongDateTimeFormat(this String str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            else
            {
                return DateTime.ParseExact(str, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
            }
        }

        public static String ToMinuteSecond(this String str)
        {
            var timeString = string.Empty;

            if (string.IsNullOrWhiteSpace(str) == false)
            {
                var timeSpan = new TimeSpan(0, 0, int.Parse(str));

                if (timeSpan.Hours < 1) timeString = timeSpan.ToString().Remove(0, 3);
                else timeString = timeSpan.ToString();
            }

            return timeString;
        }

        public static DateTime? ToDateTime(this String str)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;

            DateTime res;
            if (DateTime.TryParse(str, out res))
            {
                return res;
            }
            return null;
        }

        public static int ToInt32(this string strValue)
        {
            int iValue;
            int.TryParse(strValue, out iValue);
            return iValue;
        }

        public static int? ToNullableInt32(this string strValue)
        {
            if (string.IsNullOrEmpty(strValue)) return null;

            int iValue;
            int.TryParse(strValue, out iValue);
            return iValue;
        }

        public static decimal? ToNullableDecimal(this string strValue)
        {
            if (string.IsNullOrEmpty(strValue)) return null;

            decimal iValue;
            decimal.TryParse(strValue, out iValue);
            return iValue;
        }

        public static string CombineCurrentAppDomainPath(this String path)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        }

        public static string ReplaceWhiteSpacesWithOneSpace(this string strValue)
        {
            Regex r = new Regex(@"\s+");

            return r.Replace(strValue, " ");
        }

        public static string SubStringBySDBC(this string strData, int startindex, int length, int codepage = 0)
        {
            string strRtn = string.Empty;
            byte[] arybytData = Encoding.GetEncoding(codepage).GetBytes(strData);
            byte[] arybytTemp = new byte[length];
            Array.Copy(arybytData, startindex, arybytTemp, 0, length);
            strRtn = Encoding.GetEncoding(codepage).GetString(arybytTemp, 0, length);
            if (Encoding.GetEncoding(codepage).GetByteCount(strRtn) > length)
            {
                strRtn = strRtn.Substring(0, strRtn.Length - 1); ;
            }
            return strRtn;
        }

        public static int GetLengthByBytes(this string strValue, int codepage = 0)
        {
            int iRtn = 0;
            if (string.IsNullOrEmpty(strValue) == false)
                iRtn = Encoding.GetEncoding(codepage).GetByteCount(strValue);
            return iRtn;
        }

        public static string ConfigValue(this string configKey)
        {
            return SingletonBase<ConfigurableSet>.Instance[configKey];
        }

        public static string MD5Hash(this string str)
        {
            var md5 = MD5.Create("MD5");
            byte[] data = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();
            string result = "";
            for (int i = 0; i < md5data.Length - 1; i++)
            {
                result += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return result;
        }
        public static string ReplaceNullWith(this string str, string re)
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                return re;
            }
            else
            {
                return str;
            }
        }

        public static string ObjectToString(this object str)
        {
            if (str == null) return "";
            return str.ToString();
        }

        /// <summary>
        /// 去除字符串前后空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveBlank(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }
            return str.Trim();
        }

        /// <summary>
        /// 特殊字符转义
        /// </summary>
        /// <param name="oldStr"></param>
        /// <returns></returns>
        public static string Replace(string oldStr)
        {
            if (string.IsNullOrEmpty(oldStr))
            {
                return "";
            }
            string str2 = System.Text.RegularExpressions.Regex.Replace(oldStr, @"[\[\+\\\|\(\)\^\*\""\]'%~#-&]",
                delegate(System.Text.RegularExpressions.Match match)
                {
                    if (match.Value == "'")
                    {
                        return "''";
                    }
                    else
                    {
                        return "[" + match.Value + "]";
                    }
                });
            return str2;
        }
    }
}
