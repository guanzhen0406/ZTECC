using Wicresoft.Solution.Utils;
using System;
using System.Web;

namespace Wicresoft.Solution.Domain
{
    public class MobileSMSValidate
    {
        /// <summary>
        /// 允许失败次数
        /// </summary>
        private readonly int FAILCOUNT = 3;

        /// <summary>
        /// 短信验证码Session Key
        /// </summary>
        private readonly string smsVerifySessionKey = "smsVerifySessionKey";

        /// <summary>
        /// 短信发送频率
        /// </summary>
        private readonly int RANGE = ConfigConst.GetMobileCodeInterval.ConfigValue().ToInt32();

        public MobileVerifyMode verifyMode
        {
            get { return _session[smsVerifySessionKey] as MobileVerifyMode; }
            private set { }
        }

        private readonly ISessionRepository _session;

        /// <summary>
        /// 有效时长
        /// </summary>
        private readonly int ableMinutes = ConfigConst.MobileCodeTimeout.ConfigValue().ToInt32();

        /// <summary>
        /// 禁用时间
        /// </summary>
        private readonly int UnableMinutes = ConfigConst.MobileCodeErrorUnableTime.ConfigValue().ToInt32();

        /// <summary>
        /// 验证码长度
        /// </summary>
        private readonly int verifyLen;

        public MobileSMSValidate(int verifyCodeLen = 6)
        {
            verifyLen = verifyCodeLen;
            _session = new SessionRepository();
            if (verifyMode == null)
            {
                verifyMode = InitMobileVerifyMode();
            }
        }

        private MobileVerifyMode InitMobileVerifyMode(string phoneNumber = "")
        {
            var now = DateTime.Now;
            var mobileVerifyMode = new MobileVerifyMode
            {
                PhoneNumber = phoneNumber,
                SMSVerifyNumber = Rand.CreatePhoneCode(verifyLen),
                InitSMSVerifyTime = now,
                LastSendTime = now.AddMinutes(-RANGE),
                FailNumber = -1,
                RequestIP = WebHelper.GetClientIP()
            };
            _session.Set(smsVerifySessionKey, mobileVerifyMode, ableMinutes);
            return mobileVerifyMode;
        }

        /// <summary>
        /// 验证手机号码和短信是否符合验证规则
        /// </summary>
        /// <param name="phoneNumber">需要验证手机号</param>
        /// <param name="verifyCode">需要验证的验证码</param>
        /// <param name="failBussiness">验证失败处理</param>
        /// <param name="successBussiness">验证成功处理</param>
        /// <param name="errorMessage">错误信息</param>
        /// <returns></returns>
        public bool IsSMSVerifyCode(string phoneNumber, string verifyCode, Action<string> failBussiness, Action successBussiness, out string errorMessage)
        {
            var ret = false;
            errorMessage = LoginMessage.MobilePhoneCodeError;

            // 账户被锁定
            var currentTime = DateTime.Now;
            if (currentTime <= verifyMode.UnableTime)
            {
                errorMessage = ComputeRemainUnableMinute(currentTime, verifyMode.UnableTime);
                return false;
            }
            if (verifyMode.FailNumber == -1)
            {
                verifyMode.FailNumber = 1;
                return false;
            }

            // 禁用用户时间范围内
            if (DateTime.Compare(DateTime.Now, verifyMode.UnableTime) < 0)
            {
                string clientIP = WebHelper.GetClientIP();
                if (clientIP.EqualsOrdinalIgnoreCase(verifyMode.RequestIP) || verifyMode.PhoneNumber.EqualsOrdinalIgnoreCase(phoneNumber))
                {
                    //  属于阻止IP
                    return false;
                }
            }
            else
            {
                if (verifyMode.PhoneNumber.EqualsOrdinalIgnoreCase(phoneNumber) && verifyMode.SMSVerifyNumber.EqualsOrdinalIgnoreCase(verifyCode)
                    && DateTime.Compare(DateTime.Now.AddMinutes(-ableMinutes), verifyMode.InitSMSVerifyTime) < 0)
                {
                    //verifyMode = InitMobileVerifyMode(phoneNumber);
                    _session.Remove(smsVerifySessionKey);
                    successBussiness();
                    ret = true;
                }
                else
                {
                    //  失败次数大于允许次数
                    if ((verifyMode.FailNumber += 1) >= FAILCOUNT)
                    {
                        verifyMode.FailNumber = -1;
                        verifyMode.SMSVerifyNumber = Rand.CreatePhoneCode(verifyLen);
                        verifyMode.UnableTime = DateTime.Now.AddMinutes(UnableMinutes);
                        _session.Set(smsVerifySessionKey, verifyMode, UnableMinutes);
                        failBussiness(phoneNumber);
                        errorMessage = ComputeRemainUnableMinute(DateTime.Now, verifyMode.UnableTime);
                    }
                }
            }
            if (ret)
            {
                errorMessage = string.Empty;
            }
            return ret;
        }

        /// <summary>
        /// 发送短信给用户
        /// </summary>
        /// <param name="sendsms">发送短信的方法</param>
        /// <param name="phoneNumber">发送电话号码</param>
        /// <param name="requestIP">客户端IP</param>
        /// <param name="funcPhoneNumberVerify">手机号验证,是否存在于白名单内</param>
        public string SendCodeForSMS(Action<string> sendsms, string phoneNumber, string requestIP, Func<string, bool> funcPhoneNumberVerify)
        {
            // 手机号为空
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return MobileLoginMessage.MobileIsEmpty;
            }
            // 用户被锁定
            DateTime currentTime = DateTime.Now;
            if (currentTime <= verifyMode.UnableTime)
            {
                return ComputeRemainUnableMinute(currentTime, verifyMode.UnableTime);
            }

            var now = DateTime.Now;
            if (verifyMode.FailNumber == -1 || (now - verifyMode.InitSMSVerifyTime).TotalMinutes > ableMinutes)
            {
                verifyMode.FailNumber = 0;
                verifyMode.InitSMSVerifyTime = now;
                verifyMode.PhoneNumber = phoneNumber.Trim();
            }

            if (verifyMode.RequestIP == requestIP)
            {
                // 同一台机器限制分钟内不能重复获取
                if (verifyMode.LastSendTime.AddMinutes(RANGE) >= DateTime.Now)
                {
                    return string.Format(LoginMessage.RepeatGet, RANGE);
                }
                try
                {
                    // 手机号不在白名单内
                    if (!funcPhoneNumberVerify(phoneNumber))
                    {
                        return MobileLoginMessage.CheckMobileIsRegisted;
                    }
                    verifyMode.PhoneNumber = phoneNumber.Trim();
                    verifyMode.LastSendTime = now;
                    sendsms(phoneNumber.Trim());
                    _session.Set(smsVerifySessionKey, verifyMode, ableMinutes);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }

            return string.Empty;
        }

        private string ComputeRemainUnableMinute(DateTime comparteTime, DateTime unableTime)
        {
            string message = string.Empty;
            TimeSpan ts = unableTime - comparteTime;
            if (ts.Seconds > 0)
            {
                var totalSeconds = (int)ts.TotalSeconds;
                var minutes = totalSeconds % 60 == 0 ? totalSeconds / 60 : totalSeconds / 60 + 1;
                message = string.Format(MobileLoginMessage.MobileIsLock, minutes);
            }
            return message;
        }
    }

    [Serializable]
    public class MobileVerifyMode
    {
        /// <summary>
        /// 访问IP地址
        /// </summary>
        public string RequestIP { get; set; }

        /// <summary>
        /// 失败次数
        /// </summary>
        public int FailNumber { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 短信验证码
        /// </summary>
        public string SMSVerifyNumber { get; set; }

        /// <summary>
        /// 验证码初始化时间
        /// </summary>
        public DateTime InitSMSVerifyTime { get; set; }

        /// <summary>
        /// 上次发送验证码时间
        /// </summary>
        public DateTime LastSendTime { get; set; }

        /// <summary>
        /// 禁用截止时间
        /// </summary>
        public DateTime UnableTime { get; set; }
    }

    public class MobileLoginMessage
    {
        public const string RepeatGet = "{0}分钟之内不能重复获取";
        public const string MobileIsEmpty = "请输入手机号";
        public const string MobilePhoneCodeIsEmpty = "请输入动态密码";
        public const string MobilePhoneCodeError = "动态密码错误";
        public const string CheckMobilePhoneIsCorrect = "请检查手机号是否输入正确";
        public const string MobileIsLock = "用户被锁定，请于{0}分钟后重试";
        public const string PleaseGetMobileCode = "请获取动态密码";
        public const string CheckMobileIsRegisted = "请检查您的手机号是否已注册";
    }
}
