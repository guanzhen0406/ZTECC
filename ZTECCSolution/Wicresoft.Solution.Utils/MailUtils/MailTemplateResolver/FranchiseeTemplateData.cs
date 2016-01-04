
using System;

namespace Wicresoft.Solution.Utils.MailUtils
{
    public class FranchiseeTemplateData
    {
        //基本信息
        public string HomeURL { get; set; }
        public string LogoURL { get; set; }
        public string DetailURL { get; set; }
        public string NameEn { get; set; }
        public string NameCn { get; set; }
        public string CreateTime { get; set; }
        public string NCCode { get; set; }
        public string HotelName { get; set; }
        public string ApprovalURL { get; set; }

        private string _copyright;
        public string Copyright
        {
            get { return DateTime.Now.Year.ToString(); }
            set { _copyright = value; }
        }

        //操作人
        public string CreatorNO { get; set; }
        public string CreatorEn { get; set; }
        public string CreatorCn { get; set; }
        public string ApproverNO { get; set; }
        public string ApproverEn { get; set; }
        public string ApproverCn { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        //备注
        public string RejectReason { get; set; }
        public string CreateReson { get; set; }

        //部门
        public string DepartmentName { get; set; }
        public string Position { get; set; }
        public string SubDepartmentName { get; set; }
        public string ReportTo { get; set; }

        //加盟店日志
        public string OperateScrore { get; set; }
        public string OperateDescription { get; set; }

        //加盟费支付
        public string FranchiseeFeeMoney { get; set; }//加盟费欠费金额
        public string FranchiseeFeeAdjustMoney { get; set; }//调整后欠费金额

        //提醒
        public string CustomerName { get; set; }//加盟店客服
        public string Day { get; set; }//天

        #region 投诉与建议

        /// <summary>
        /// 投诉/建议主题
        /// </summary>
        public string TSSubject { get; set; }

        /// <summary>
        /// 投诉/建议单号
        /// </summary>
        public string TSNumber { get; set; }

        /// <summary>
        /// 投诉建议单分类(投诉 or 建议)
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 酒店开发编号和名称
        /// </summary>
        public string HotelDevNoWithName { get; set; }

        /// <summary>
        /// 加盟客服反馈时间(指派给直接主管,内部解决人的时间)
        /// </summary>
        public string FeedBackAt { get; set; }

        /// <summary>
        /// 投诉与建议模块首页Url
        /// </summary>
        public string TSModuleIndexUrl { get; set; }

        /// <summary>
        /// 满意度
        /// </summary>
        public string SatisfactionDegree { get; set; }

        /// <summary>
        /// 投诉建议单URL
        /// </summary>
        public string TSInfoDetailURL { get; set; }

        /// <summary>
        /// 事件大类
        /// </summary>
        public string IncidentBigCategory { get; set; }

        /// <summary>
        /// 事件小类
        /// </summary>
        public string IncidentSmallCategory { get; set; }

        /// <summary>
        /// 推送至总部详细描述
        /// </summary>
        public string PushToHQDescr { get; set; }

        #endregion

        /// <summary>
        /// 本系统标识(CRM or FCRM)
        /// </summary>
        public string SourceID { get; set; }

        #region 商机

        /// <summary>
        /// 意向编号
        /// </summary>
        public string IntentionNumber { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 省份-城市
        /// </summary>
        public string ProvinceWithCity { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 意向
        /// </summary>
        public string HotelProperty { get; set; }

        /// <summary>
        /// 是否有物业
        /// </summary>
        public string IsHaveTenement { get; set; }

        /// <summary>
        /// 物业地址
        /// </summary>
        public string TenementAddress { get; set; }

        /// <summary>
        /// 产权情况
        /// </summary>
        public string PropertyRight { get; set; }

        /// <summary>
        /// 意向详情页Url
        /// </summary>
        public string IntentionDetailUrl { get; set; }

        #endregion

        #region 运营成本
        public string HalfYear { get; set; }

        #endregion

        #region 对账单
        /// <summary>
        /// 对账单年度
        /// </summary>
        public string BillYear { get; set; }

        /// <summary>
        /// 对账单月度
        /// </summary>
        public string BillMonth { get; set; }

        /// <summary>
        /// 本月合计
        /// </summary>
        public string strTotalBillValue { get; set; }

        /// <summary>
        /// 加盟费总计
        /// </summary>
        public string strTotalFranchisingFee { get; set; }

        /// <summary>
        /// 加盟费户名
        /// </summary>
        public string FAccountName { get; set; }

        /// <summary>
        /// 加盟费开户行
        /// </summary>
        public string FBankName { get; set; }

        /// <summary>
        /// 加盟费账号
        /// </summary>
        public string FBankAccountNumber { get; set; }

        /// <summary>
        /// 盟广总计
        /// </summary>
        public string strTotalMGFee { get; set; }

        /// <summary>
        /// 盟广户名
        /// </summary>
        public string MAccountName { get; set; }

        /// <summary>
        /// 盟广开户行
        /// </summary>
        public string MBankName { get; set; }

        /// <summary>
        /// 盟广账号
        /// </summary>
        public string MBankAccountNumber { get; set; }

        /// <summary>
        /// 会计姓名
        /// </summary>
        public string AccountantName { get; set; }

        /// <summary>
        /// 联系邮箱
        /// </summary>
        public string AEmail { get; set; }

        /// <summary>
        /// 分机号码
        /// </summary>
        public string AExtNumber { get; set; }

        /// <summary>
        /// 对账单ID
        /// </summary>
        public string CallBackID { get; set; }

        #endregion
    }
}
