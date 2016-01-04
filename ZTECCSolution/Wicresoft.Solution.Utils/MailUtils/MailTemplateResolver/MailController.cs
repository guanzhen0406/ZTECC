using System;

namespace Wicresoft.Solution.Utils.MailUtils
{
    public class MailController : TemplateController
    {
        private static readonly MailController instance = new MailController();
        private MailController() { }
        public static MailController Instance
        {
            get
            {
                return instance;
            }
        }

        protected string TemplatePath
        {
            get
            {
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MailTemplate");
            }
        }

        protected string MessageTemplatePath
        {
            get
            {
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MessageTemplate");
            }
        }

        /// <summary>
        /// 不发函--待审批
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string FranchiseeFeeNoLetter_Pending(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranchiseeFeeNoLetter_Pending.xslt", TemplatePath);
            return Template<FranchiseeTemplateData>(path, mailData).Execute();
        }

        /// <summary>
        /// 不发函--审批通过
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string FranchiseeFeeNoLetter_Approve(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranchiseeFeeNoLetter_Approve.xslt", TemplatePath);
            return Template<FranchiseeTemplateData>(path, mailData).Execute();
        }

        /// <summary>
        /// 不发函--拒绝
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string FranchiseeFeeNoLetter_Reject(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranchiseeFeeNoLetter_Reject.xslt", TemplatePath);
            return Template<FranchiseeTemplateData>(path, mailData).Execute();
        }

        /// <summary>
        /// 不发函--撤销
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string FranchiseeFeeNoLetter_Cancel(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranchiseeFeeNoLetter_Cancel.xslt", TemplatePath);
            return Template<FranchiseeTemplateData>(path, mailData).Execute();
        }

        /// <summary>
        /// 金额调整--待审批
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string FranchiseeFeeAmount_Pending(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranchiseeFeeAmount_Pending.xslt", TemplatePath);
            return Template<FranchiseeTemplateData>(path, mailData).Execute();
        }

        /// <summary>
        /// 金额调整--审批通过
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string FranchiseeFeeAmount_Approve(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranchiseeFeeAmount_Approve.xslt", TemplatePath);
            return Template<FranchiseeTemplateData>(path, mailData).Execute();
        }

        /// <summary>
        /// 金额调整--拒绝
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string FranchiseeFeeAmount_Reject(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranchiseeFeeAmount_Reject.xslt", TemplatePath);
            return Template<FranchiseeTemplateData>(path, mailData).Execute();
        }

        /// <summary>
        /// 金额调整--撤销
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string FranchiseeFeeAmount_Cancel(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranchiseeFeeAmount_Cancel.xslt", TemplatePath);
            return Template<FranchiseeTemplateData>(path, mailData).Execute();
        }

        /// <summary>
        /// 加盟商日志--待审批
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string FranchiseeHotelLog_Pending(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranchiseeHotelLog_Pending.xslt", TemplatePath);
            return Template<FranchiseeTemplateData>(path, mailData).Execute();
        }

        /// <summary>
        /// 加盟商日志--审批通过
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string FranchiseeHotelLog_Approve(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranchiseeHotelLog_Approve.xslt", TemplatePath);
            return Template<FranchiseeTemplateData>(path, mailData).Execute();
        }

        /// <summary>
        /// 加盟商日志--拒绝
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string FranchiseeHotelLog_Reject(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranchiseeHotelLog_Reject.xslt", TemplatePath);
            return Template<FranchiseeTemplateData>(path, mailData).Execute();
        }

        /// <summary>
        /// 加盟商日志--撤销
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string FranchiseeHotelLog_Cancel(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranchiseeHotelLog_Cancel.xslt", TemplatePath);
            return Template<FranchiseeTemplateData>(path, mailData).Execute();
        }

        /// <summary>
        /// 政策执行提醒
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string PolicyImplAutoRemind(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\PolicyImplAutoRemind.xslt", TemplatePath);
            return Template<FranchiseeTemplateData>(path, mailData).Execute();
        }

        /// <summary>
        /// 加盟费支付提醒
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string FranciseeFeeAutoRemind(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranciseeFeeAutoRemind.xslt", TemplatePath);
            return Template<FranchiseeTemplateData>(path, mailData).Execute();
        }

        #region 投诉与建议邮件

        /// <summary>
        /// 主管同意邮件
        /// </summary>
        /// <param name="mailData"></param>
        public string TS_ManagerAgreeMail(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\TS_ManagerAgreeMail.xslt", TemplatePath);
            return Template(path, mailData).Execute();
        }

        /// <summary>
        /// 主管拒绝邮件
        /// </summary>
        /// <param name="mailData"></param>
        public string TS_ManagerRejectMail(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\TS_ManagerRejectMail.xslt", TemplatePath);
            return Template(path, mailData).Execute();
        }

        /// <summary>
        /// 内部解决人反馈邮件
        /// </summary>
        /// <param name="mailData"></param>
        public string TS_InnerResolverFeedBackMail(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\TS_InnerResolverFeedBackMail.xslt", TemplatePath);
            return Template(path, mailData).Execute();
        }

        /// <summary>
        /// 加盟客服指派给主管审查邮件
        /// </summary>
        /// <param name="mailData"></param>
        public string TS_AssignToManagerMail(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\TS_AssignToManagerMail.xslt", TemplatePath);
            return Template(path, mailData).Execute();
        }

        /// <summary>
        /// 加盟客服指派给内部解决人邮件
        /// </summary>
        /// <param name="mailData"></param>
        public string TS_AssignToInnerSolverMail(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\TS_AssignToInnerSolverMail.xslt", TemplatePath);
            return Template(path, mailData).Execute();
        }

        /// <summary>
        /// 加盟商点击未解决邮件
        /// </summary>
        /// <param name="mailData"></param>
        public string TS_UnsolvedMail(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\TS_UnsolvedMail.xslt", TemplatePath);
            return Template(path, mailData).Execute();
        }

        /// <summary>
        /// 加盟商点击已解决邮件
        /// </summary>
        /// <param name="mailData"></param>
        public string TS_ResolvedMail(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\TS_ResolvedMail.xslt", TemplatePath);
            return Template(path, mailData).Execute();
        }

        /// <summary>
        /// 加盟客服反馈至加盟商邮件
        /// </summary>
        /// <param name="mailData"></param>
        public string TS_FeedBackToFranchiseeMail(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\TS_FeedBackToFranchiseeMail.xslt", TemplatePath);
            return Template(path, mailData).Execute();
        }

        /// <summary>
        /// 加盟商发起投诉与建议邮件
        /// </summary>
        /// <param name="mailData"></param>
        public string TS_SubmitMail(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\TS_SubmitMail.xslt", TemplatePath);
            return Template(path, mailData).Execute();
        }

        /// <summary>
        /// 推送至总部邮件
        /// </summary>
        /// <param name="mailData"></param>
        public string TS_PushToHQMail(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\TS_PushToHQMail.xslt", TemplatePath);
            return Template(path, mailData).Execute();
        }

        #endregion

        #region 成本控制
        /// <summary>
        /// 店长提交上半年或下半年运营成本
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string DZ_SubmitToMail(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranciseeCostControl_Submit.xslt", TemplatePath);
            return Template(path, mailData).Execute();
        }
        /// <summary>
        /// 加盟商同意运营成本数据
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string DZ_AgreeToMail(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranciseeCostControl_Agree.xslt", TemplatePath);
            return Template(path, mailData).Execute();
        }
        /// <summary>
        /// 管理员撤销固化
        /// </summary>
        public string DZ_BackoutSolidifyToMail(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranciseeCostControl_BackoutSolidify.xslt", TemplatePath);
            return Template(path, mailData).Execute();
        }
        /// <summary>
        /// 管理员全局固化
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string DZ_SolidifyNoApplicationToMail(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranciseeCostControl_SolidifyNoApplication.xslt", TemplatePath);
            return Template(path, mailData).Execute();
        }
        /// <summary>
        /// 加盟商质疑店长提交的运营成本
        /// </summary>
        /// <param name="mailData"></param>
        /// <returns></returns>
        public string DZ_QueryToMail(FranchiseeTemplateData mailData)
        {
            string path = string.Format("{0}\\FranciseeCostControl_Query.xslt", TemplatePath);
            return Template(path, mailData).Execute();
        }
        #endregion

        #region 投诉与建议消息提醒

        /// <summary>
        /// 加盟商发起投诉与建议消息提醒
        /// </summary>
        /// <param name="templateData">模板数据</param>
        public string TS_SubmitMessage(FranchiseeTemplateData templateData)
        {
            string path = string.Format("{0}\\TS_SubmitMessage.xslt", MessageTemplatePath);
            return Template(path, templateData).Execute();
        }

        /// <summary>
        /// 加盟商点击未解决消息提醒
        /// </summary>
        /// <param name="templateData"></param>
        public string TS_UnsolvedMessage(FranchiseeTemplateData templateData)
        {
            string path = string.Format("{0}\\TS_UnsolvedMessage.xslt", MessageTemplatePath);
            return Template(path, templateData).Execute();
        }

        /// <summary>
        /// 加盟商点击已解决消息提醒
        /// </summary>
        /// <param name="templateData"></param>
        public string TS_ResolvedMessage(FranchiseeTemplateData templateData)
        {
            string path = string.Format("{0}\\TS_ResolvedMessage.xslt", MessageTemplatePath);
            return Template(path, templateData).Execute();
        }

        /// <summary>
        /// 加盟客服反馈至加盟商消息提醒
        /// </summary>
        /// <param name="templateData"></param>
        public string TS_FeedBackToFranchiseeMessage(FranchiseeTemplateData templateData)
        {
            string path = string.Format("{0}\\TS_FeedBackToFranchiseeMessage.xslt", MessageTemplatePath);
            return Template(path, templateData).Execute();
        }

        /// <summary>
        /// 加盟客服指派给主管审查消息提醒
        /// </summary>
        /// <param name="templateData"></param>
        public string TS_AssignToManagerMessage(FranchiseeTemplateData templateData)
        {
            string path = string.Format("{0}\\TS_AssignToManagerMessage.xslt", MessageTemplatePath);
            return Template(path, templateData).Execute();
        }

        /// <summary>
        /// 加盟客服指派给内部解决人消息提醒
        /// </summary>
        /// <param name="templateData"></param>
        public string TS_AssignToInnerSolverMessage(FranchiseeTemplateData templateData)
        {
            string path = string.Format("{0}\\TS_AssignToInnerSolverMessage.xslt", MessageTemplatePath);
            return Template(path, templateData).Execute();
        }

        /// <summary>
        /// 主管同意消息提醒
        /// </summary>
        /// <param name="templateData"></param>
        public string TS_ManagerAgreeMessage(FranchiseeTemplateData templateData)
        {
            string path = string.Format("{0}\\TS_ManagerAgreeMessage.xslt", MessageTemplatePath);
            return Template(path, templateData).Execute();
        }

        /// <summary>
        /// 主管拒绝消息提醒
        /// </summary>
        /// <param name="templateData"></param>
        public string TS_ManagerRejectMessage(FranchiseeTemplateData templateData)
        {
            string path = string.Format("{0}\\TS_ManagerRejectMessage.xslt", MessageTemplatePath);
            return Template(path, templateData).Execute();
        }

        /// <summary>
        /// 内部解决人反馈消息提醒
        /// </summary>
        /// <param name="templateData"></param>
        public string TS_InnerResolverFeedBackMessage(FranchiseeTemplateData templateData)
        {
            string path = string.Format("{0}\\TS_InnerResolverFeedBackMessage.xslt", MessageTemplatePath);
            return Template(path, templateData).Execute();
        }

        #endregion

        /// <summary>
        /// 用户名登录忘记密码
        /// </summary>
        /// <param name="mailData"></param>
        /// <param name="type">忘记登录类型,0 忘记登录密码 1 忘记手机号</param>
        /// <returns></returns>
        public string Login_Forget_PasswordOrTelNo(Login_Forget_Password_TelNo mailData, int type)
        {
            var path = string.Empty;
            switch (type)
            {
                case (int)EumForgetLoginType.ForgetPassword:
                    path = string.Format("{0}\\Login_Forget_Password.xslt", TemplatePath);
                    break;
                case (int)EumForgetLoginType.ForgetTelNO:
                    path = string.Format("{0}\\Login_Forget_TelNo.xslt", TemplatePath);
                    break;
            }
            return Template(path, mailData).Execute();
        }

        #region 商机

        /// <summary>
        /// 对外平台新增意向提醒邮件
        /// </summary>
        public string SJ_IntentionRemindMail(FranchiseeTemplateData templateData)
        {
            string path = string.Format("{0}\\SJ_IntentionRemindMail.xslt", TemplatePath);
            return Template(path, templateData).Execute();
        }

        #endregion

        #region 对账单
        /// <summary>
        /// 加盟商对账单
        /// </summary>
        public string CW_FranchiseeStatements(FranchiseeTemplateData templateData)
        {
            string path = string.Format("{0}\\CW_FranchiseeStatements.xslt", TemplatePath);
            return Template(path, templateData).Execute();
        }

        #endregion

    }
}
