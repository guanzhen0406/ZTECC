using System.Web;

namespace Wicresoft.Solution.Utils
{
    public class WebHelper
    {
        /// <summary>
        /// 获取访问URL
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUrl()
        {
            return HttpContext.Current.Request.Url.AbsoluteUri;
        }

        /// <summary>
        /// 获取访问IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrWhiteSpace(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            return result;
        }
    }
}
