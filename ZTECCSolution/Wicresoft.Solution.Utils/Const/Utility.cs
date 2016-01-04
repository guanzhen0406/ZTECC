
namespace Wicresoft.Solution.Utils
{
    /// <summary>
    /// 列表类型
    /// </summary>
    public enum TaskListTypeEnum
    {
        [System.ComponentModel.Description("我的草稿")]
        MyDraft,
        [System.ComponentModel.Description("我的待办")]
        MyPending,
        [System.ComponentModel.Description("已审批的")]
        MyApproved
    }

    /// <summary>
    /// 业务类型，对应k2的ProcessName
    /// </summary>
    public enum BussinessType
    {
        /// <summary>
        /// 不发函申请
        /// </summary>
        [EnumItemDescription("zh-CN", "不发函申请")]
        [EnumItemDescription("en-US", "WithoutLetter")]
        WithoutLetter = 0,

        /// <summary>
        /// 金额调整
        /// </summary>
        [EnumItemDescription("zh-CN", "金额调整")]
        [EnumItemDescription("en-US", "AmountAdjust")]
        AmountAdjust = 1,

        /// <summary>
        /// 日志申请
        /// </summary>
        [EnumItemDescription("zh-CN", "加盟店日志")]
        [EnumItemDescription("en-US", "HotelLog")]
        HotelLog = 2
    }

    public enum ApplicationStatus
    {
        [EnumItemDescription("zh-CN", "草稿")]
        [EnumItemDescription("en-US", "Draft")]
        Draft = 0,

        //[EnumItemDescription("zh-CN", "新建")]
        //[EnumItemDescription("en-US", "Create")]
        //Create=1,

        /// <summary>
        /// 审批中
        /// </summary>
        [EnumItemDescription("zh-CN", "审批中")]
        [EnumItemDescription("en-US", "Pending")]
        Pending = 2,

        /// <summary>
        /// 已撤销
        /// </summary>
        [EnumItemDescription("zh-CN", "已撤销")]
        [EnumItemDescription("en-US", "Cancelled")]
        Cancelled = 3,

        /// <summary>
        /// 审批通过
        /// </summary>
        [EnumItemDescription("zh-CN", "审批通过")]
        [EnumItemDescription("en-US", "Approved")]
        Approved = 4,

        /// <summary>
        /// 审批拒绝
        /// </summary>
        [EnumItemDescription("zh-CN", "审批拒绝")]
        [EnumItemDescription("en-US", "Rejected")]
        Rejected = 5,

        [EnumItemDescription("zh-CN", "自动拒绝")]
        [EnumItemDescription("en-US", "SysDisapprove")]
        SysDisapprove = 6,

        //[EnumItemDescription("zh-CN", "要求更多")]
        //[EnumItemDescription("en-US", "AskForMore")]
        //AskForMore=6,

        //[EnumItemDescription("zh-CN", "已关闭")]
        //[EnumItemDescription("en-US", "Closed")]
        //Closed=7,
        [EnumItemDescription("zh-CN", "处理中")]
        [EnumItemDescription("en-US", "Processing")]
        Processing = 8
    }

    public enum ApprovalAction
    {
        /// <summary>
        /// 提交
        /// </summary>
        [EnumItemDescription("zh-CN", "提交")]
        [EnumItemDescription("en-US", "Create")]
        Create = 0,

        /// <summary>
        /// 拒绝
        /// </summary>
        [EnumItemDescription("zh-CN", "拒绝")]
        [EnumItemDescription("en-US", "Reject")]
        Reject = 1,

        /// <summary>
        /// 同意
        /// </summary>
        [EnumItemDescription("zh-CN", "同意")]
        [EnumItemDescription("en-US", "Approve")]
        Approve = 2,

        [EnumItemDescription("zh-CN", "要求更多")]
        [EnumItemDescription("en-US", "AskForMore")]
        AskForMore = 3,

        [EnumItemDescription("zh-CN", "撤销")]
        [EnumItemDescription("en-US", "Withdraw")]
        Withdraw = 4,

        /// <summary>
        /// 延期
        /// </summary>
        [EnumItemDescription("zh-CN", "延期")]
        [EnumItemDescription("en-US", "Defer")]
        Defer = 5,

        /// <summary>
        /// 提前
        /// </summary>
        [EnumItemDescription("zh-CN", "提前")]
        [EnumItemDescription("en-US", "Advance")]
        Advance = 6
    }

    public enum ApprovalActionClient
    {
        /// <summary>
        /// 提交
        /// </summary>
        [EnumItemDescription("zh-CN", "提交")]
        [EnumItemDescription("en-US", "Create")]
        Create = 0,

        /// <summary>
        /// 拒绝
        /// </summary>
        [EnumItemDescription("zh-CN", "拒绝")]
        [EnumItemDescription("en-US", "Reject")]
        Reject = 1,

        /// <summary>
        /// 同意
        /// </summary>
        [EnumItemDescription("zh-CN", "同意")]
        [EnumItemDescription("en-US", "Approve")]
        Approve = 2,

        [EnumItemDescription("zh-CN", "要求更多")]
        [EnumItemDescription("en-US", "AskForMore")]
        AskForMore = 3,

        [EnumItemDescription("zh-CN", "撤销")]
        [EnumItemDescription("en-US", "Withdraw")]
        Withdraw = 4,

        /// <summary>
        /// 延期
        /// </summary>
        [EnumItemDescription("zh-CN", "延期")]
        [EnumItemDescription("en-US", "Defer")]
        Defer = 5,

        /// <summary>
        /// 提前
        /// </summary>
        [EnumItemDescription("zh-CN", "提前")]
        [EnumItemDescription("en-US", "Advance")]
        Advance = 6
    }

    /// <summary>
    /// 邮件发送状态
    /// </summary>
    public enum MailSendStatus
    {
        /// <summary>
        /// 未发送
        /// </summary>
        NotSend = 0,

        /// <summary>
        /// 发送中
        /// </summary>
        Sending = 1,

        /// <summary>
        /// 已发送
        /// </summary>
        Send = 2,

        /// <summary>
        /// 发送失败
        /// </summary>
        Fail = 3
    }

    /// <summary>
    /// 邮件发送类型
    /// </summary>
    public enum MailSendType
    {
        /// <summary>
        /// 实时
        /// </summary>
        realTime = 0,

        /// <summary>
        /// 间隔8分钟发50条
        /// </summary>
        Interval1 = 1,
    }

    /// <summary>
    /// 对账单邮件发送状态
    /// </summary>
    public enum CWMailSendStatus
    {
        /// <summary>
        /// 未发送
        /// </summary>
        [EnumItemDescription("zh-CN", "未发送")]
        NotSend = 0,

        /// <summary>
        /// 待重新发送
        /// </summary>
        [EnumItemDescription("zh-CN", "待重新发送")]
        PendingSend = 1,

        /// <summary>
        /// 已发送
        /// </summary>
        [EnumItemDescription("zh-CN", "已发送")]
        Send = 2,

        /// <summary>
        /// 发送失败
        /// </summary>
        [EnumItemDescription("zh-CN", "发送失败")]
        Fail = 3
    }

    public enum EumTSCategory
    {
        /// <summary>
        /// 投诉
        /// </summary>
        [EnumItemDescription("zh-CN", "投诉")]
        [EnumItemDescription("en-US", "Complaint")]
        Complaint = 0,

        /// <summary>
        /// 建议
        /// </summary>
        [EnumItemDescription("zh-CN", "建议")]
        [EnumItemDescription("en-US", "Suggest")]
        Suggest = 1
    }

    /// <summary>
    /// 投诉单对外状态
    /// </summary>
    public enum TSOutterStatus
    {
        /// <summary>
        /// 解决中
        /// </summary>
        [EnumItemDescription("zh-CN", "解决中")]
        [EnumItemDescription("en-US", "Solving")]
        Solving = 0,

        /// <summary>
        /// 已反馈
        /// </summary>
        [EnumItemDescription("zh-CN", "已反馈")]
        [EnumItemDescription("en-US", "HaveFeedBack")]
        HaveFeedBack = 1,

        /// <summary>
        /// 待评价
        /// </summary>
        [EnumItemDescription("zh-CN", "待评价")]
        [EnumItemDescription("en-US", "ToEvaluate")]
        ToEvaluate = 2,

        /// <summary>
        /// 已关闭
        /// </summary>
        [EnumItemDescription("zh-CN", "已关闭")]
        [EnumItemDescription("en-US", "Closed")]
        Closed = 3
    }

    /// <summary>
    /// 投诉单对内状态
    /// </summary>
    public enum TSInnerStatus
    {
        /// <summary>
        /// 待解决
        /// </summary>
        [EnumItemDescription("zh-CN", "待解决")]
        [EnumItemDescription("en-US", "Solving")]
        Solving = 0,

        /// <summary>
        /// 内部沟通
        /// </summary>
        [EnumItemDescription("zh-CN", "内部沟通")]
        [EnumItemDescription("en-US", "InternalCom")]
        InternalCom = 1,

        /// <summary>
        /// 主管审查
        /// </summary>
        [EnumItemDescription("zh-CN", "主管审查")]
        [EnumItemDescription("en-US", "ManagerReview")]
        ManagerReview = 2,

        /// <summary>
        /// 已反馈
        /// </summary>
        [EnumItemDescription("zh-CN", "已反馈")]
        [EnumItemDescription("en-US", "HaveFeedBack")]
        HaveFeedBack = 3,

        /// <summary>
        /// 已关闭
        /// </summary>
        [EnumItemDescription("zh-CN", "已关闭")]
        [EnumItemDescription("en-US", "Closed")]
        Closed = 4
    }

    /// <summary>
    /// 日志操作类型
    /// </summary>
    public enum TSLogOperateType
    {
        /// <summary>
        /// 发起
        /// </summary>
        [EnumItemDescription("zh-CN", "发起")]
        [EnumItemDescription("en-US", "Submit")]
        Submit = 0,

        /// <summary>
        /// 已反馈
        /// </summary>
        [EnumItemDescription("zh-CN", "已反馈")]
        [EnumItemDescription("en-US", "HaveFeedBack")]
        HaveFeedBack = 1,

        /// <summary>
        /// 已解决
        /// </summary>
        [EnumItemDescription("zh-CN", "已解决")]
        [EnumItemDescription("en-US", "Resolved")]
        Resolved = 2,

        /// <summary>
        /// 未解决
        /// </summary>
        [EnumItemDescription("zh-CN", "未解决")]
        [EnumItemDescription("en-US", "Unsolved")]
        Unsolved = 3,

        /// <summary>
        /// 评价
        /// </summary>
        [EnumItemDescription("zh-CN", "评价")]
        [EnumItemDescription("en-US", "Evaluate")]
        Evaluate = 4,

        /// <summary>
        /// 内部沟通
        /// </summary>
        [EnumItemDescription("zh-CN", "内部沟通")]
        [EnumItemDescription("en-US", "AssignToSolver")]
        AssignToSolver = 5,

        /// <summary>
        /// 主管审查
        /// </summary>
        [EnumItemDescription("zh-CN", "主管审查")]
        [EnumItemDescription("en-US", "AssignToManager")]
        AssignToManager = 6,

        /// <summary>
        /// 反馈至加盟商
        /// </summary>
        [EnumItemDescription("zh-CN", "反馈至加盟商")]
        [EnumItemDescription("en-US", "FeedbackToFranchisee")]
        FeedbackToFranchisee = 7,

        /// <summary>
        /// 同意(主管)
        /// </summary>
        [EnumItemDescription("zh-CN", "同意")]
        [EnumItemDescription("en-US", "Agree")]
        Agree = 8,

        /// <summary>
        /// 拒绝(主管)
        /// </summary>
        [EnumItemDescription("zh-CN", "拒绝")]
        [EnumItemDescription("en-US", "Reject")]
        Reject = 9,

        /// <summary>
        /// 内部沟通已反馈
        /// </summary>
        [EnumItemDescription("zh-CN", "内部沟通已反馈")]
        [EnumItemDescription("en-US", "Confirm")]
        Confirm = 10
    }

    /// <summary>
    /// 投诉与建议---内部角色
    /// </summary>
    public enum EumTSInnerRole
    {
        /// <summary>
        /// 内部解决人
        /// </summary>
        [EnumItemDescription("zh-CN", "内部解决人")]
        [EnumItemDescription("en-US", "Solver")]
        Solver = 0,

        /// <summary>
        /// 主管
        /// </summary>
        [EnumItemDescription("zh-CN", "主管")]
        [EnumItemDescription("en-US", "Manager")]
        Manager = 1
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum EumMessageType
    {
        /// <summary>
        /// 投诉与建议
        /// </summary>
        [EnumItemDescription("zh-CN", "投诉与建议")]
        [EnumItemDescription("en-US", "TS")]
        TS = 0,

        /// <summary>
        /// 上线通知
        /// </summary>
        [EnumItemDescription("zh-CN", "上线通知")]
        [EnumItemDescription("en-US", "Online")]
        Online = 1
    }

    /// <summary>
    /// 基础数据类型
    /// </summary>
    public enum EumBasicDataTypeCode
    {
        /// <summary>
        /// 营建投诉小类
        /// </summary>
        YJTSSmallCategory = 0,

        /// <summary>
        /// 运营投诉小类
        /// </summary>
        YYTSSmallCategory = 1,

        /// <summary>
        /// 满意度
        /// </summary>
        SatisfactionDegree = 2,

        /// <summary>
        /// 评分分类
        /// </summary>
        GradeCategory = 3,

        /// <summary>
        /// 投诉与建议模块主管审查人员
        /// </summary>
        TSReviewer = 4,

        /// <summary>
        /// 事件大类
        /// </summary>
        IncidentBigCategory = 5,

        /// <summary>
        /// 事件小类
        /// </summary>
        IncidentSmallCategory = 6,

        /// <summary>
        /// 其他投诉小类
        /// </summary>
        OtherTSSmallCategory = 7,

        /// <summary>
        /// 加盟商建议小类
        /// </summary>
        JMSJYSmallCategory = 8,

        /// <summary>
        /// 采购满意度
        /// </summary>
        CGSatisfactionDegree = 9,

        /// <summary>
        /// 采购评分分类
        /// </summary>
        CGGradeCategory = 10,

        /// <summary>
        /// 酒店品牌
        /// </summary>
        HotelBrand = 11,

        /// <summary>
        /// 酒店性质
        /// </summary>
        HotelProperty = 12
    }

    /// <summary>
    /// 投诉与建议，对内平台列表页搜索
    /// </summary>
    public enum EumTSInnerSearchCondition
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,

        /// <summary>
        /// 解决中
        /// </summary>
        Solving = 1,

        /// <summary>
        /// 已关闭
        /// </summary>
        Closed = 2,

        /// <summary>
        /// 已反馈
        /// </summary>
        HaveFeedBack = 3
    }

    public class EumCultureType
    {
        public const string Chinese = "zh-CN";
        public const string English = "en-US";
    }

    public class MailCommonName
    {
        public const string MailFrom = "System";
        public const string System = "System";
        public const string LogoImage = "images/emaillogobak.jpg";
    }

    public class VerifyCodeKey
    {
        public const string OutterVerifyCode = "OutterVerifyCode";
        public const string InnerVerifyCode = "InnerVerifyCode";
        public const string OutterPhoneCode = "OutterPhoneCode";
        public const string OutterIntentionCode = "OutterIntentionCode";
    }

    /// <summary>
    /// 员工状态
    /// </summary>
    public class EmployeeStatus
    {
        public const string Probation = "Probation";    // 试用期
        public const string Active = "Active";  // 在职
    }

    /// <summary>
    /// 上传文件类型,按业务模块区分
    /// </summary>
    public class UploadFileType
    {
        /// <summary>
        /// 金额调整
        /// </summary>
        public const string BillAmountAdjust = "BillAmountAdjust";

        /// <summary>
        /// 部分到帐
        /// </summary>
        public const string ConfirmBillArrivePart = "ConfirmBillArrivePart";

        /// <summary>
        /// 确认到帐
        /// </summary>
        public const string ConfirmBillArrive = "ConfirmBillArrive";

        /// <summary>
        /// 加盟店日志
        /// </summary>
        public const string HotelLog = "HotelLog";

        /// <summary>
        /// 财务数据导入
        /// </summary>
        public const string InportCurrentMonthData = "InportCurrentMonthData";

        /// <summary>
        /// 投诉与建议
        /// </summary>
        public const string ComplaintAndSuggestion = "ComplaintAndSuggestion";

        /// <summary>
        /// 政策执行
        /// </summary>
        public const string KP_PolicyImpl = "KP_PolicyImpl";

        /// <summary>
        /// 风险提示
        /// </summary>
        public const string FX_RiskTips = "FX_RiskTips";

        /// <summary>
        /// 财务模块-基础数据导入
        /// </summary>
        public const string CW_InportFinanceData = "CW_InportFinanceData";

        /// <summary>
        /// 商机加盟活动
        /// </summary>
        public const string SJ_Activity = "SJ_Activity";

        /// <summary>
        /// 标准文档管理
        /// </summary>
        public const string WD_DocumentManage = "WD_DocumentManage";

        /// <summary>
        /// 开发示例
        /// </summary>
        public const string Test_DevelopDemo = "Test_DevelopDemo";

        /// <summary>
        /// 未分类
        /// </summary>
        public const string UnSorted = "UnSorted";
    }

    /// <summary>
    /// 考评词典typecode
    /// </summary>
    public class BasicDataSetConfigType
    {
        /// <summary>
        /// 加盟费支付Typecode
        /// </summary>
        public const string FranchiseeFeePay = "FranchiseeFeePay";

        /// <summary>
        /// 政策执行Typecode
        /// </summary>        
        public const string PolicyExecute = "PolicyExecute";

        /// <summary>
        /// 产品评估Typecode
        /// </summary>      
        public const string ProductAssert = "ProductAssert";

        /// <summary>
        /// 硬件检测Typecode
        /// </summary>        
        public const string HardCheck = "HardCheck";

        /// <summary>
        /// 分数权重Typecode
        /// </summary>        
        public const string ScroreRatio = "ScroreRatio";

        /// <summary>
        /// 星级评分规则Typecode
        /// </summary>       
        public const string AssertScroreRule = "AssertScroreRule";

        /// <summary>
        /// 酒店日志评分Typecode
        /// </summary>        
        public const string HotelLog = "HotelLog";

    }

    /// <summary>
    /// 对外平台登录类型
    /// </summary>
    public enum EumLoginType
    {
        /// <summary>
        /// 用户名密码登录
        /// </summary>
        UserName,

        /// <summary>
        /// 手机号登录
        /// </summary>
        Phone
    }

    /// <summary>
    /// 登录提示信息
    /// </summary>
    public class LoginMessage
    {
        public const string UserNameOrPasswordError = "用户名或者密码错误";
        public const string VerifyCodeError = "验证码输入错误";
        public const string VerifyCodeIsEmpty = "请输入验证码";
        public const string RepeatGet = "{0}分钟之内不能重复获取";
        public const string MobilePhoneCodeError = "动态密码错误";
        public const string CheckMobilePhoneIsCorrect = "请检查手机号是否输入正确";
        public const string CheckMobilePhoneIsRegisted = "请检查您的手机号是否已注册";
        public const string AccountIsLocked = "用户被锁定，请于{0}分钟后重试";
        public const string VerifyCodeOutOfDate = "验证码已过期，请重新输入";
    }

    /// <summary>
    /// 数据组类型
    /// </summary>
    public enum EumDataGroupType
    {
        /// <summary>
        /// 品牌
        /// </summary>
        BrandGroup = 0,

        /// <summary>
        /// 城区
        /// </summary>
        CityAreaGroup = 1,

        /// <summary>
        /// 区域
        /// </summary>
        RegionGroup = 2,

        /// <summary>
        /// 酒店组(自定义组)
        /// </summary>
        SelfDefinedHotelGroup = 3,

        /// <summary>
        /// 全量酒店,数量为主数据中NCCode不为空的
        /// </summary>
        WholeHotelGroup = 4
    }


    /// <summary>
    /// 加盟部办公操作类型
    /// </summary>
    public enum EumOaOperateType
    {
        //操作类型:0表示不发函,1表示发函,2表示追加发函,3表示确认到帐,4表示金额调整,5表示部分到帐,NULL表示无,6表示财务数据导入,7财务数据更新

        /// <summary>
        /// 不发函
        /// </summary>
        NoSendMail = 0,

        /// <summary>
        /// 发函
        /// </summary>
        SendMail = 1,

        /// <summary>
        /// 追加发函
        /// </summary>
        AgainSendMail = 2,

        /// <summary>
        /// 确认到帐)
        /// </summary>
        ConfirmArrive = 3,

        /// <summary>
        /// 金额调整
        /// </summary>
        FeeAdjust = 4,

        /// <summary>
        /// 部分到帐
        /// </summary>
        PartConfirmArrive = 5,

        /// <summary>
        /// 财务数据导入
        /// </summary>
        ImportDataFirstInitial = 6,

        /// <summary>
        /// 财务数据更新
        /// </summary>
        ImportDataUpdate = 7
    }

    /// <summary>
    /// 加盟部办公发函状态
    /// </summary>
    public enum EumOaSendMailStatus
    {
        //发函状态:0表示未发函,1表示已发函,2表示不发函,3表示不发函申请中,NULL表示无
        /// <summary>
        /// 未发函
        /// </summary>
        NoneSendMail = 0,

        /// <summary>
        /// 已发函
        /// </summary>
        AlreadySendMail = 1,

        /// <summary>
        /// 不发函
        /// </summary>
        NoSendMail = 2,

        /// <summary>
        /// 不发函申请中
        /// </summary>
        NoSendMailWaitAuditing = 3
    }

    /// <summary>
    /// 加盟部办公追加发函类型
    /// </summary>
    public enum EumOaAgainSendMailType
    {
        //追加发函类型:0关PMS、1撤店长、2单方面解除协议3.6月份未到账,4.12月份未到账,5其他
        /// <summary>
        /// 关PMS
        /// </summary>
        ClosePms = 0,

        /// <summary>
        /// 撤店长
        /// </summary>
        CancelHotelManager = 1,

        /// <summary>
        /// 单方面解除协议
        /// </summary>
        CancelDateInByOnepoint = 2,

        /// <summary>
        /// 6月份未到账
        /// </summary>
        UnConfirmArriveByJune = 3,

        /// <summary>
        /// 12月份未到账
        /// </summary>
        UnConfirmArriveByDecember = 4,

        /// <summary>
        /// 其他
        /// </summary>
        Others = 5
    }


    /// <summary>
    /// 加盟部办公确认到帐状态
    /// </summary>
    public enum EumOaConfrimArriveStatus
    {
        //确认到帐状态,0表示未到账,1表示已到帐,NULL表示无
        /// <summary>
        /// 未到账
        /// </summary>
        NoConfimArrive = 0,

        /// <summary>
        /// 已到帐
        /// </summary>
        AlreadyConfirmArrive = 1
    }

    /// <summary>
    /// 加盟部办公金额调整状态
    /// </summary>
    public enum EumOaFeeAdjustStatus
    {
        //金额调整状态:0表示金额调整审批中,1表示金额调整审批通过,NULL表示无
        /// <summary>
        /// 审批中
        /// </summary>
        WaitAuditing = 0,

        /// <summary>
        /// 审批通过
        /// </summary>
        AuditPassed = 1
    }

    /// <summary>
    /// 加盟部办公欠费状态
    /// </summary>
    public enum EumOaOwningFeeStatus
    {
        //欠费状态,0表示不欠费,1表示欠费
        /// <summary>
        /// 不欠费
        /// </summary>
        NoOwningFee = 0,

        /// <summary>
        /// 欠费
        /// </summary>
        OwningFee = 1
    }

    /// <summary>
    /// 加盟部办公不发函申请审批状态
    /// </summary>
    public enum EumOaNoSendMailStatus
    {
        /// <summary>
        /// 审批中
        /// </summary>
        WaitAuditing = 0,

        /// <summary>
        /// 审批通过
        /// </summary>
        AuditPassed = 1
    }

    /// <summary>
    /// 分数权重Itemcode
    /// </summary>
    public class ItemCodeOfScroreRatio
    {
        /// <summary>
        /// 加盟费支付
        /// </summary>
        public const string FeepayRatio = "FeepayRatio";

        /// <summary>
        /// 政策执行
        /// </summary>        
        public const string PolicyExecuteRatio = "PolicyExecuteRatio";

        /// <summary>
        /// 产品评估
        /// </summary>      
        public const string ProductAssertRation = "ProductAssertRation";

        /// <summary>
        /// 硬件检测
        /// </summary>        
        public const string HardcheckRatio = "HardcheckRatio";
    }

    /// <summary>
    /// 星级评分规则Itemcode
    /// </summary>
    public class ItemCodeOfAssertScore
    {
        //星级起评分
        public const string ScoreStart1 = "ScoreStart1";
        public const string ScoreStart2 = "ScoreStart2";
        public const string ScoreStart3 = "ScoreStart3";
        public const string ScoreStart4 = "ScoreStart4";
        public const string ScoreStart5 = "ScoreStart5";
        //星级止评分
        public const string ScoreEnd1 = "ScoreEnd1";
        public const string ScoreEnd2 = "ScoreEnd2";
        public const string ScoreEnd3 = "ScoreEnd3";
        public const string ScoreEnd4 = "ScoreEnd4";
        public const string ScoreEnd5 = "ScoreEnd5";
    }

    /// <summary>
    /// 加盟费支付Itemcode
    /// </summary>
    public class ItemCodeOfFeePay
    {
        public const string Scrore1To15Days = "Scrore1To15Days";
        public const string Scrore16To60Days = "Scrore16To60Days";
        public const string Scrore61To90Days = "Scrore61To90Days";
        public const string ScroreUp90Days = "ScroreUp90Days";
    }

    /// <summary>
    /// 报表范围
    /// </summary>
    public enum EumSummaryReportRange
    {
        /// <summary>
        /// 季度报表
        /// </summary>
        QuarterReport = 0,

        /// <summary>
        /// 半年度报表
        /// </summary>
        HalfYearReport = 1,

        /// <summary>
        /// 年度报表
        /// </summary>
        WholeYearReport = 2
    }

    /// <summary>
    /// 报表半年度编号
    /// </summary>
    public enum EumSummaryReportHalfYear
    {
        /// <summary>
        /// 上半年
        /// </summary>
        FirstHalfYear = 0,

        /// <summary>
        /// 下半年
        /// </summary>
        SecondHalfYear = 1
    }

    /// <summary>
    /// 报表季度编号
    /// </summary>
    public enum EumSummaryReportQuarter
    {
        /// <summary>
        /// 第一季度
        /// </summary>
        FirstQuarter = 1,

        /// <summary>
        /// 第二季度
        /// </summary>
        SecondQuarter = 2,

        /// <summary>
        /// 第三季度
        /// </summary>
        ThirdQuarter = 3,

        /// <summary>
        /// 第四季度
        /// </summary>
        ForthQuarter = 4
    }

    /// <summary>
    /// 是否良性店
    /// </summary>
    public enum EumSummaryReportIsGoodHotel
    {
        /// <summary>
        /// 非良性店
        /// </summary>
        NoGoodHotel = 0,

        /// <summary>
        /// 良性店
        /// </summary>
        GoodHotel = 1
    }

    /// <summary>
    /// 在用状态
    /// </summary>
    public enum EumIsValid
    {
        /// <summary>
        /// 停用
        /// </summary>
        [EnumItemDescription("zh-CN", "停用")]
        [EnumItemDescription("en-US", "Invalid")]
        Invalid = 0,

        /// <summary>
        /// 在用
        /// </summary>
        [EnumItemDescription("zh-CN", "在用")]
        [EnumItemDescription("en-US", "Valid")]
        Valid = 1
    }

    /// <summary>
    /// 加盟酒店营建期状态
    /// </summary>
    public enum EnumHotelConstructionStatus
    {
        [EnumItemDescription("zh-CN", "待签约(直营通过管控会加盟通过决策会)")]
        [EnumItemDescription("en-US", "ToBeSigned")]
        ToBeSigned = 10,

        [EnumItemDescription("zh-CN", "已签约（签约流程已完成并通过）")]
        [EnumItemDescription("en-US", "Signed")]
        Signed = 20,

        [EnumItemDescription("zh-CN", "已交房（交房流程已完成并结果为已接房，仅直营）")]
        [EnumItemDescription("en-US", "Submitted")]
        Submitted = 30,

        [EnumItemDescription("zh-CN", "已开工（仅加盟）")]
        [EnumItemDescription("en-US", "Started")]
        Started = 35,

        [EnumItemDescription("zh-CN", "已交底（设计交底流程已完成并通过，仅直营）")]
        [EnumItemDescription("en-US", "Submitted_Send")]
        Submitted_Send = 40,

        [EnumItemDescription("zh-CN", "隐蔽工程已验收（隐蔽验收流程已完成并通过）")]
        [EnumItemDescription("en-US", "HiddenWorksChecked")]
        HiddenWorksChecked = 50,

        [EnumItemDescription("zh-CN", "工程已验收（工程验收流程已完成并通过）")]
        [EnumItemDescription("en-US", "ProjectChecked")]
        ProjectChecked = 60,

        [EnumItemDescription("zh-CN", "试营业（试营业流程已完成并通过）")]
        [EnumItemDescription("en-US", "TrialOperation")]
        TrialOperation = 80,

        [EnumItemDescription("zh-CN", "正式营业（正式营业流程已完成并通过）")]
        [EnumItemDescription("en-US", "Operation")]
        Operation = 90,

        [EnumItemDescription("zh-CN", "拟终止")]
        [EnumItemDescription("en-US", "PreTermination")]
        PreTermination = 96,

        [EnumItemDescription("zh-CN", "终止")]
        [EnumItemDescription("en-US", "Termination")]
        Termination = 97,

        [EnumItemDescription("zh-CN", "转直营")]
        [EnumItemDescription("en-US", "ToBeDirect")]
        ToBeDirect = 98

    }

    /// <summary>
    /// 加盟商用户类型
    /// </summary>
    public enum EumFranchiseeUserType
    {
        /// <summary>
        /// 法人
        /// </summary>
        [EnumItemDescription("zh-CN", "法人")]
        [EnumItemDescription("en-US", "LegalPerson")]
        LegalPerson = 0,

        /// <summary>
        /// 第一联系人
        /// </summary>
        [EnumItemDescription("zh-CN", "第一联系人")]
        [EnumItemDescription("en-US", "FirstContact")]
        FirstContact = 1,

        /// <summary>
        /// 第二联系人
        /// </summary>
        [EnumItemDescription("zh-CN", "第二联系人")]
        [EnumItemDescription("en-US", "SecondContact")]
        SecondContact = 2,

        /// <summary>
        /// 第三联系人
        /// </summary>
        [EnumItemDescription("zh-CN", "第三联系人")]
        [EnumItemDescription("en-US", "ThirdContact")]
        ThirdContact = 3,

        /// <summary>
        /// 财务
        /// </summary>
        [EnumItemDescription("zh-CN", "财务")]
        [EnumItemDescription("en-US", "Finance")]
        Finance = 4,

        /// <summary>
        /// 其它
        /// </summary>
        [EnumItemDescription("zh-CN", "其它")]
        [EnumItemDescription("en-US", "Other")]
        Other = 5
    }

    /// <summary>
    /// 菜单是否公用,设置公用菜单用
    /// </summary>
    public enum EumIsCommonMenuType
    {
        /// <summary>
        /// 私有菜单,非通用菜单
        /// </summary>
        PrivateMenu = 0,

        /// <summary>
        /// 公共菜单
        /// </summary>
        CommonMenu = 1
    }

    /// <summary>
    /// 加盟商类型
    /// </summary>
    public enum EumFranchiseeType
    {
        [EnumItemDescription("zh-CN", "公司")]
        [EnumItemDescription("en-US", "Company")]
        Company = 1,

        [EnumItemDescription("zh-CN", "个人")]
        [EnumItemDescription("en-US", "Person")]
        Person = 2
    }

    public enum EumOperateType
    {
        Add = 0,

        View = 1,

        Modify = 2
    }

    /// <summary>
    /// 审批页面跳转类型
    /// </summary>
    public enum EumWorkflowTaskType
    {
        /// <summary>
        /// 我的申请
        /// </summary>
        MyApplications = 0,
        /// <summary>
        /// 我的待办
        /// </summary>
        MyPendingTasks = 1,
        /// <summary>
        /// 我的审批历史
        /// </summary>
        MyAuditHistory = 2
    }

    /// <summary>
    /// 敏感数据记录日志模块
    /// </summary>
    public enum EumModule
    {
        /// <summary>
        /// 加盟商信息
        /// </summary>
        Franchisee = 1
    }

    /// <summary>
    /// 敏感数据记录日志子模块
    /// </summary>
    public enum EumChildModule
    {
        /// <summary>
        /// 加盟商基本信息
        /// </summary>
        BasicInfo = 1,

        /// <summary>
        /// 加盟商用户
        /// </summary>
        User = 2,

        /// <summary>
        /// 加盟商银行卡
        /// </summary>
        BankAccount = 3,

        /// <summary>
        /// 加盟商公司
        /// </summary>
        Company = 4,

        /// <summary>
        /// 酒店
        /// </summary>
        Hotel = 5,

        /// <summary>
        /// 结果列
        /// </summary>
        Index = 6
    }

    /// <summary>
    /// 风险提示职位
    /// </summary>
    public enum EumRiskReportPerPositionType
    {
        /// <summary>
        /// 内控经理
        /// </summary>
        RiskManager = 0,
        /// <summary>
        /// 内控主管
        /// </summary>
        RiskPerIncharge
    }

    /// <summary>
    /// 风险提示,版本号类型
    /// </summary>
    public enum EumRiskReportVersionNoType
    {
        /// <summary>
        /// 内控主管经理
        /// </summary>
        InnerPersonRisk = 0,
        /// <summary>
        /// 应收账款
        /// </summary>
        CheckBillRisk = 1,
        /// <summary>
        /// 财务风险
        /// </summary>
        FinanceRisk = 2
    }

    /// <summary>
    /// 短信发送状态
    /// </summary>
    public enum SMSSendStatus
    {
        /// <summary>
        /// 未发送
        /// </summary>
        NotSend = 0,

        /// <summary>
        /// 发送中
        /// </summary>
        Sending = 1,

        /// <summary>
        /// 已发送
        /// </summary>
        Send = 2,

        /// <summary>
        /// 发送失败
        /// </summary>
        Fail = 3
    }

    /// <summary>
    /// 忘记登录类型
    /// </summary>
    public enum EumForgetLoginType
    {
        /// <summary>
        /// 忘记密码
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "忘记密码")]
        [EnumItemDescription(EumCultureType.English, "ForgetPassword")]
        ForgetPassword = 0,

        /// <summary>
        /// 忘记手机号
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "忘记手机号")]
        [EnumItemDescription(EumCultureType.English, "ForgetTelNO")]
        ForgetTelNO = 1
    }

    /// <summary>
    /// 十二星座
    /// </summary>
    public enum EumConstellation
    {
        /// <summary>
        /// 白羊座
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "白羊座")]
        BY,

        /// <summary>
        /// 金牛座
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "金牛座")]
        JN,

        /// <summary>
        /// 双子座
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "双子座")]
        SZ,

        /// <summary>
        /// 巨蟹座
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "巨蟹座")]
        JX,

        /// <summary>
        /// 狮子座
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "狮子座")]
        Lion,

        /// <summary>
        /// 处女座
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "处女座")]
        CN,

        /// <summary>
        /// 天秤座
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "天秤座")]
        TC,

        /// <summary>
        /// 天蝎座
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "天蝎座")]
        TX,

        /// <summary>
        /// 射手座
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "射手座")]
        SS,

        /// <summary>
        /// 摩羯座
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "摩羯座")]
        MJ,

        /// <summary>
        /// 水瓶座
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "水瓶座")]
        SP,

        /// <summary>
        /// 双鱼座
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "双鱼座")]
        SY
    }

    /// <summary>
    /// 财务模块操作日志表的操作类型--财务模块
    /// </summary>
    public enum EumCW_ManyOperateType
    {
        /// <summary>
        /// BI导入
        /// </summary>
        BiImport = 0,

        /// <summary>
        /// 数据导入
        /// </summary>
        ImportData = 1,

        /// <summary>
        /// 预设值生成,封版
        /// </summary>
        PresetCreateOrConfirmVersion = 2,

        /// <summary>
        /// 预设值提交
        /// </summary>
        PresetSubmit = 3,

        /// <summary>
        /// MainV1.0保存,固化
        /// </summary>
        MainSaveOrSolidify = 4,

        /// <summary>
        ///  生产对账单
        /// </summary>
        GenMonthlyStatement = 6,

        /// <summary>
        /// 发送账单
        /// </summary>
        SendStatement = 7,

        /// <summary>
        /// 下月调整
        /// </summary>
        NextMonthAdjust = 8
    }

    /// <summary>
    /// 发送短信业务Code
    /// </summary>
    public enum EumSMSBusinessCode
    {
        /// <summary>
        /// 登录
        /// </summary>
        Login,

        /// <summary>
        /// 日经营报告
        /// </summary>
        AllianceSms
    }

    /// <summary>
    /// 是,否
    /// </summary>
    public enum EumYesOrNo
    {
        /// <summary>
        /// 是
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "是")]
        [EnumItemDescription(EumCultureType.English, "Yes")]
        Yes = 1,

        /// <summary>
        /// 否
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "否")]
        [EnumItemDescription(EumCultureType.English, "No")]
        No = 2
    }
    public enum EumEnableOrDisable
    {
        /// <summary>
        /// 是
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "启用")]
        [EnumItemDescription(EumCultureType.English, "Enable")]
        Enable = 1,

        /// <summary>
        /// 否
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "停用")]
        [EnumItemDescription(EumCultureType.English, "Disable")]
        Disable = 0
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum EumGender
    {
        /// <summary>
        /// 男
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "男")]
        [EnumItemDescription(EumCultureType.English, "Male")]
        Male,

        /// <summary>
        /// 女
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "女")]
        [EnumItemDescription(EumCultureType.English, "Female")]
        Female
    }

    /// <summary>
    /// 商机状态
    /// </summary>
    public enum EumBusOpportunityStatus
    {
        /// <summary>
        /// 未推送商机
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "未推送商机")]
        [EnumItemDescription(EumCultureType.English, "Undistributed")]
        Undistributed = 1,

        /// <summary>
        /// 不活跃商机
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "不活跃商机")]
        [EnumItemDescription(EumCultureType.English, "Inactivity")]
        Inactivity = 2,

        /// <summary>
        /// 开发中商机
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "开发中商机")]
        [EnumItemDescription(EumCultureType.English, "Developing")]
        Developing = 3,

        /// <summary>
        /// 已签约商机
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "已签约商机")]
        [EnumItemDescription(EumCultureType.English, "Sighed")]
        Sighed = 4
    }

    /// <summary>
    /// 意向数据来源
    /// </summary>
    public enum EumIntentionDataSource
    {
        /// <summary>
        /// 加盟商
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "加盟商")]
        [EnumItemDescription(EumCultureType.English, "Franchisee")]
        Franchisee,

        /// <summary>
        /// 拓展专员
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "拓展专员")]
        [EnumItemDescription(EumCultureType.English, "Attache")]
        Attache,

        /// <summary>
        /// HWorld
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "HWorld")]
        [EnumItemDescription(EumCultureType.English, "HWorld")]
        HWorld
    }

    /// <summary>
    /// 加盟活动类型
    /// </summary>
    public enum EumActivityTypeDataSource
    {
        /// <summary>
        /// 推介会
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "推介会")]
        [EnumItemDescription(EumCultureType.English, "Seminar")]
        Seminar = 1,

        /// <summary>
        /// 华住世界大会
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "华住世界大会")]
        [EnumItemDescription(EumCultureType.English, "HZWorldCongress")]
        HZWorldCongress = 2,

        /// <summary>
        /// 培训会
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "培训会")]
        [EnumItemDescription(EumCultureType.English, "Training")]
        Training = 3,

        /// <summary>
        /// 其它活动
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "其它活动")]
        [EnumItemDescription(EumCultureType.English, "Other")]
        Other = 4

    }

    /// <summary>
    /// 发票类型
    /// </summary>
    public enum EnumInvoiceType
    {
        /// <summary>
        /// 公司发票
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "公司发票")]
        [EnumItemDescription(EumCultureType.English, "Company")]
        Company = 1,

        /// <summary>
        /// 个人发票
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "个人发票")]
        [EnumItemDescription(EumCultureType.English, "Personal")]
        Personal = 2

    }

    /// <summary>
    /// 采购订单状态
    /// </summary>
    public enum EnumTradeStatus
    {
        [EnumItemDescription(EumCultureType.Chinese, "待供应商确认")]
        [EnumItemDescription(EumCultureType.English, "WaitForConfirm")]
        WaitForConfirm = 1,

        [EnumItemDescription(EumCultureType.Chinese, "供应商已确认")]
        [EnumItemDescription(EumCultureType.English, "Confirmed")]
        Confirmed = 2,

        [EnumItemDescription(EumCultureType.Chinese, "供应商修改交货日期")]
        [EnumItemDescription(EumCultureType.English, "ModifyArrivalDate")]
        ModifyArrivalDate = 3,

        [EnumItemDescription(EumCultureType.Chinese, "供应商拒绝")]
        [EnumItemDescription(EumCultureType.English, "Rejected")]
        Rejected = 4,

        [EnumItemDescription(EumCultureType.Chinese, "已发货")]
        [EnumItemDescription(EumCultureType.English, "Deliverying")]
        Deliverying = 5,

        [EnumItemDescription(EumCultureType.Chinese, "收货完成")]
        [EnumItemDescription(EumCultureType.English, "ReceiveFinished")]
        ReceiveFinished = 6,

        [EnumItemDescription(EumCultureType.Chinese, "安装完成")]
        [EnumItemDescription(EumCultureType.English, "InstallFinished")]
        InstallFinished = 7,

        [EnumItemDescription(EumCultureType.Chinese, "已评论")]
        [EnumItemDescription(EumCultureType.English, "Evaludated")]
        Evaludated = 8,

        [EnumItemDescription(EumCultureType.Chinese, "已结束")]
        [EnumItemDescription(EumCultureType.English, "Finished")]
        Finished = 9
    }

    /// <summary>
    /// CRM中采购订单状态
    /// </summary>
    public enum EnumOrderStatus
    {
        [EnumItemDescription(EumCultureType.Chinese, "待处理")]
        [EnumItemDescription(EumCultureType.English, "Pending")]
        Pending = -1,

        [EnumItemDescription(EumCultureType.Chinese, "未结束")]
        [EnumItemDescription(EumCultureType.English, "NotFinished")]
        NotFinished = -2,

        [EnumItemDescription(EumCultureType.Chinese, "待供应商确认")]
        [EnumItemDescription(EumCultureType.English, "WaitForConfirm")]
        WaitForConfirm = 1,

        [EnumItemDescription(EumCultureType.Chinese, "供应商已确认")]
        [EnumItemDescription(EumCultureType.English, "Confirmed")]
        Confirmed = 2,

        [EnumItemDescription(EumCultureType.Chinese, "供应商修改交货日期")]
        [EnumItemDescription(EumCultureType.English, "ModifyArrivalDate")]
        ModifyArrivalDate = 3,

        [EnumItemDescription(EumCultureType.Chinese, "供应商拒绝")]
        [EnumItemDescription(EumCultureType.English, "Rejected")]
        Rejected = 4,

        [EnumItemDescription(EumCultureType.Chinese, "已发货")]
        [EnumItemDescription(EumCultureType.English, "Deliverying")]
        Deliverying = 5,

        [EnumItemDescription(EumCultureType.Chinese, "收货完成")]
        [EnumItemDescription(EumCultureType.English, "ReceiveFinished")]
        ReceiveFinished = 6,

        [EnumItemDescription(EumCultureType.Chinese, "安装完成")]
        [EnumItemDescription(EumCultureType.English, "InstallFinished")]
        InstallFinished = 7,

        [EnumItemDescription(EumCultureType.Chinese, "已评论")]
        [EnumItemDescription(EumCultureType.English, "Evaludated")]
        Evaludated = 8,

        [EnumItemDescription(EumCultureType.Chinese, "已结束")]
        [EnumItemDescription(EumCultureType.English, "Finished")]
        Finished = 9
    }


    /// <summary>
    /// 参数配置增删改
    /// </summary>
    public enum EumParamSetType
    {
        /// <summary>
        /// 增
        /// </summary>
        Add = 0,
        /// <summary>
        /// 删
        /// </summary>
        Delete = 1,
        /// <summary>
        /// 改
        /// </summary>
        Modify = 2
    }

    /// <summary>
    /// 预设值类型
    /// </summary>
    public enum EumPreSetType
    {
        /// <summary>
        /// 浮动
        /// </summary>
        Float = 0,
        /// <summary>
        /// 打折
        /// </summary>
        Discount = 1,
        /// <summary>
        /// 减免
        /// </summary>
        Nopay = 2,
        /// <summary>
        /// 定额
        /// </summary>
        SolidFee = 3
    }

    /// <summary>
    /// 预设值持续类型 
    /// </summary>
    public enum EumContinueSetType
    {
        //持续类型,1表示永久,0表示不永久2表示无,此时,开始批次结束批次有效
        /// <summary>
        /// 不永久
        /// </summary>
        NoForever = 0,
        /// <summary>
        /// 永久
        /// </summary>
        Forever = 1
    }

    /// <summary>
    /// 账单类型
    /// </summary>
    public enum EnumBillType
    {
        /// <summary>
        /// 主账套
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "主账套")]
        MainZT = 0,
        /// <summary>
        /// 盟广
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "盟广")]
        MG = 1
    }

    /// <summary>
    /// 缴费状态
    /// </summary>
    public enum EnumPaymentStatus
    {
        /// <summary>
        /// 付清
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "付清")]
        Payup = 0,
        /// <summary>
        /// 部分付清
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "部分付清")]
        PartialPayup = 1,
        /// <summary>
        /// 未付
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "未付")]
        NotPay = 2
    }

    /// <summary>
    /// 订单状态
    /// </summary>
    public enum EnumCWOrderStatus
    {
        /// <summary>
        /// 打开
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "打开")]
        Open = 0,
        /// <summary>
        /// 关闭
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "关闭")]
        Close = 1
    }

    /// <summary>
    /// 产权情况
    /// </summary>
    public enum EumPropertyRight
    {
        [EnumItemDescription(EumCultureType.Chinese, "自有物业")]
        [EnumItemDescription(EumCultureType.English, "Personal")]
        Personal = 1,

        [EnumItemDescription(EumCultureType.Chinese, "租赁物业")]
        [EnumItemDescription(EumCultureType.English, "Rent")]
        Rent = 2,

        [EnumItemDescription(EumCultureType.Chinese, "我是中介")]
        [EnumItemDescription(EumCultureType.English, "Intermediary")]
        Intermediary = 3
    }

    /// <summary>
    /// 商机变更类型
    /// </summary>
    public enum EumBusOpportunityChnageType
    {
        /// <summary>
        /// 状态
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "状态")]
        [EnumItemDescription(EumCultureType.English, "Personal")]
        Status = 1,

        /// <summary>
        /// 商机信息
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "商机信息")]
        [EnumItemDescription(EumCultureType.English, "BusOpportunityInfo")]
        BusOpportunityInfo = 2,

        /// <summary>
        /// 跟踪与反馈
        /// </summary>
        [EnumItemDescription(EumCultureType.Chinese, "跟踪与反馈")]
        [EnumItemDescription(EumCultureType.English, "tracking")]
        Tracking = 3
    }

    /// <summary>
    /// 意向状态
    /// </summary>
    public enum EumIntentionStatus
    {
        /// <summary>
        /// 待处理
        /// </summary>
        Pending = 0,

        /// <summary>
        /// 保存
        /// </summary>
        Save = 1,

        /// <summary>
        /// 已提交
        /// </summary>
        Sumbit = 2
    }

    /// <summary>
    /// Main表1.0数据固化类型
    /// </summary>
    public enum EumIsSolidifyMainDataStatus
    {
        /// <summary>
        /// 保存
        /// </summary>
        Save = 0,

        /// <summary>
        /// 固化
        /// </summary>
        Solidify = 1
    }

    /// <summary>
    /// 财务人员类型--预设值情况用
    /// </summary>
    public enum EumFinanceEmployeeType
    {
        /// <summary>
        /// 财务专员
        /// </summary>
        CommonEmployee = 0,

        /// <summary>
        /// 财务专员
        /// </summary>
        Leader = 1,

        /// <summary>
        /// 其他,不是财务专员或财务主管
        /// </summary>
        None = 2
    }

    /// <summary>
    /// 财务基础数据导入名称类型
    /// </summary>
    public enum EumFinanceImportSheetNameType
    {
        /// <summary>
        /// 财务基础数据
        /// </summary>
        FinanceImportSheetNameBase = 0,
        /// <summary>
        /// 各类活动返还
        /// </summary>
        FinanceImportSheetNameActiviesReturn = 1,
        /// <summary>
        /// 第三方中介代收加盟店预付房费款
        /// </summary>
        FinanceImportSheetNameThirdPayRoomFee = 2,
        /// <summary>
        /// 加盟店代总部发放各类奖金款
        /// </summary>
        FinanceImportSheetNameHomePrize = 3
    }
}
