using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;

namespace Wicresoft.Solution.Utils
{
    public static class GeneralAPI
    {
        public static IDCardInfo GetIDCardInfoByIDCard(string idCard)
        {
            var idCardInfo = new IDCardInfo();
            if (!string.IsNullOrWhiteSpace(idCard))
            {
                try
                {
                    var apiUrl = "http://apistore.baidu.com/microservice/icardinfo?id=" + idCard;
                    WebClient wc = new WebClient();
                    byte[] bResponse = wc.DownloadData(apiUrl);
                    string strResponse = Encoding.ASCII.GetString(bResponse);

                    var apiRR = JsonConvert.DeserializeObject<IDCardAPIRequestResult>(strResponse);
                    if (apiRR != null && apiRR.retData != null)
                    {
                        idCardInfo = apiRR.retData;
                        switch (idCardInfo.sex)
                        {
                            case "M":
                                idCardInfo.sex = "男";
                                break;
                            case "F":
                                idCardInfo.sex = "女";
                                break;
                            case "N":
                                idCardInfo.sex = "未知";
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }

            return idCardInfo;
        }
    }

    /// <summary>
    /// 身份证信息
    /// </summary>
    public class IDCardInfo
    {
        /// <summary>
        /// 性别(男,女，未知)
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 生日(1990-01-01)
        /// </summary>
        public DateTime birthday { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }
    }

    internal class IDCardAPIRequestResult
    {
        public int errNum { get; set; }

        public string retMsg { get; set; }

        public IDCardInfo retData { get; set; }
    }
}
