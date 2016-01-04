
namespace Wicresoft.Solution.Utils
{
    public class ConfigConst
    {
        public const string ServiceUrl = "ServiceUrl";
        public const string IsDisableBrowser = "IsDisableBrowser";
        public const string DoLDAPAuth = "DoLDAPAuth";
        public const string AD_KEY = "ADKEY";
        public const string AuthencationKey = "AuthencationKey";
        public const string IsCheckDeviceId = "IsCheckDeviceId";
        public const string IsServiceNeedAuthencation = "IsServiceNeedAuthencation";
        public const string PasswordEncryKey = "PasswordEncryKey";

        public const string SessionTimeout = "SessionTimeout";

        //是否检查SSO密钥
        public const string IsCheckSecurityKeySSO = "IsCheckSecurityKeySSO";
        //SSO登录密钥
        public const string SecurityKeySSO = "SecurityKeySSO";
        //SSO访问地址
        public const string SSOUrl = "SSOUrl";
        //文件上传根路径
        public const string UploadBasePath = "UploadBasePath";
        //文件上传根路径,不从配置获取,此处固定,2015-03-11
        public const string FileUploadBasePath = "FileUpload";

        // 是否检测验证码
        public const string IsCheckVerifyCode = "IsCheckVerifyCode";
        // 对外平台登录是否验证
        public const string IsVerifyForOutter = "IsVerifyForOutter";
        // 对内平台登录是否验证AD
        public const string IsVerifyAD = "IsVerifyAD";
        // 对内平台登录ServiceUrl
        public const string ADService = "ADService";

        // 对内平台登录ADKey
        public const string ADKey = "ADKey";

        // 对外平台登录,是否验证手机校验码
        public const string IsCheckMobileCode = "IsCheckMobileCode";

        // 对外平台登录,发送手机验证码模板
        public const string SMSContent = "SMSContent";

        // 对内系统首页URL
        public const string WebInnerHomeURL = "WebInnerHomeURL";

        // 对外系统首页URL
        public const string WebOutterHomeURL = "WebOutterHomeURL";

        // 推送至总部收件人邮箱
        public const string PushToHQReciverEmail = "PushToHQReciverEmail";

        // 本系统标识
        public const string SourceID = "SourceID";

        // 手机验证码过期分钟数
        public const string MobileCodeTimeout = "MobileCodeTimeout";

        // 获取手机验证码间隔分钟数
        public const string GetMobileCodeInterval = "GetMobileCodeInterval";

        // 手机验证码输入错误次数过多锁定账户时间
        public const string MobileCodeErrorUnableTime = "MobileCodeErrorUnableTime";

        // 主数据模块按钮是否显示，一期上线时不显示
        public const string MainDataButtonSwitch = "MainDataButtonSwitch";

        public const string Domain_Directory = "Domain_Directory";
        public const string UserName_Directory = "UserName_Directory";
        public const string Password_Directory = "Password_Directory";

        //邮件
        public const string WebHomeURL = "WebHomeURL";
        public const string ApprovalURL = "ApprovalURL";

        //  用户名登录Key
        public const string LoginSecurityKey = "LoginSecurityKey";

        public const string LDAP_URL = "LDAP_URL";

        public const string IS_REDIRECT_TO_HTTPS = "IsRedirectHTTPToHttps";

        /// <summary>
        /// 财务导入,excel导入需要校验为金额格式的数据字段配置
        /// </summary>
        public const string ExcelImportDataValidateFields = "ExcelImportDataValidateFields";

        /// <summary>
        /// excel导入,字段名,默认为 "财务专员"  值为财务专员的员工编号
        /// </summary>
        public const string FinanceEmployeeNo = "财务专员";

        /// <summary>
        /// 财务excel导入系统默认表名--CW_ImportDataFinance
        /// </summary>
        public const string FinanceImportDataTableName = "CW_ImportDataFinance";

        /// <summary>
        /// BI同步Main表,默认表名--Main表
        /// </summary>
        public const string BiMainDataTableName = "DMA.FIN_JM_CRS_MAIN_F";

        /// <summary>
        /// BI同步加减项默认表名--DMA.FIN_JM_CRS_ITEM_MON_F
        /// </summary>
        public const string BiTotalAndSubTableName = "DMA.FIN_JM_CRS_ITEM_MON_F";

        /// <summary>
        /// v0.9公式计算后所得字段--V0Point9Calculate
        /// </summary>
        public const string CalculateTableName = "V0Point9Calculate";

        /// <summary>
        /// 财务基础数据--财务基础数据导入
        /// </summary>
        public const string FinanceImportSheetNameBase = "财务基础数据";

        /// <summary>
        /// 各类活动返还--财务基础数据导入
        /// </summary>
        public const string FinanceImportSheetNameActiviesReturn = "各类活动返还";

        /// <summary>
        /// 第三方中介代收加盟店预付房费款--财务基础数据导入
        /// </summary>
        public const string FinanceImportSheetNameThirdPayRoomFee = "第三方中介代收加盟店预付房费款";

        /// <summary>
        /// 加盟店代总部发放各类奖金款--财务基础数据导入
        /// </summary>
        public const string FinanceImportSheetNameHomePrize = "加盟店代总部发放各类奖金款";

    }

    public class OperateConst
    {
        public const string Approve = "Approve";
        public const string Reject = "Reject";
    }

    /// <summary>
    /// 财务导入4个sheetname名称
    /// </summary>
    public class ConfigExcelImportDataSheetName4Cw
    {
        public const string BaseData = "财务基础数据";
        public const string ManyActivies = "各类活动返还";
        public const string HomePrize = "加盟店代总部发放各类奖金款";
        public const string ThirdRoomFee = "第三方中介代收加盟店预付房费款";
    }
}
