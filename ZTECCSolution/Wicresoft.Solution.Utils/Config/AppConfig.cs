using System.Configuration;

namespace Wicresoft.Solution.Utils
{
    public class AppConfig
    {
        private static readonly AppConfig instance = new AppConfig();

        public static AppConfig Instance
        {
            get
            {
                return instance;
            }
        }

        public string WebHomeURL
        {
            get
            {
                return ConfigurationManager.AppSettings["WebHomeURL"];
            }
        }

        public string ApprovalURL
        {
            get
            {
                return ConfigurationManager.AppSettings["ApprovalURL"];
            }
        }
    }
}
