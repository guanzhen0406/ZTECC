using System.Data;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wicresoft.Solution.Utils
{
    public class DataJsonHelper
    {
        /// <summary>
        /// 将对象转化为json字符串
        /// 该方法解决反序列化时，时间格式报错问题
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="dateTimeFormat">时间格式,如：yyyy-MM-dd HH:mm:ss</param>
        /// <returns></returns>
        public static string SerilizeObject2JsonStr(object obj, string dateTimeFormat)
        {
            var timeFormat = new IsoDateTimeConverter { DateTimeFormat = dateTimeFormat };
            return JsonConvert.SerializeObject(obj, Formatting.None, timeFormat);
        }

        /// <summary>
        /// 将json数据反序列化成model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonObjStr"></param>
        /// <returns></returns>
        public static T DeserializeJson2Object<T>(string jsonObjStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonObjStr);
        }

        /// <summary>
        /// 将table数据转化为json格式
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static string DataTableJson(DataTable dt, int total)
        {
            var jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{");
            jsonBuilder.AppendFormat("\"total\":{0}, ", total);
            jsonBuilder.Append("\"rows\":[ ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        /// <summary>
        /// 处理搜索参数,若为null,返回"",否则返回去除空格后的字符串
        /// </summary>
        /// <param name="searchKeywords">搜索参数字符串</param>
        /// <returns></returns>
        public static string GetSearchKeywords(string searchKeywords)
        {
            if (string.IsNullOrEmpty(searchKeywords))
            {
                return string.Empty;
            }
            return searchKeywords.Trim();
        }

    }
}
