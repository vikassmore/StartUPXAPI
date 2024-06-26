using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StartUpX.Business.Implemetation;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class InvestmentDetailService : IInvestmentDetailService
    {


        StartUpDBContext _startupContext;
        private ConfigurationModel _url;
        private readonly IEmailSender _emailSender;
        private readonly IUserAuditLogService _userAuditLogService;

        public InvestmentDetailService(StartUpDBContext startUpDBContext, IEmailSender emailSender, IOptions<ConfigurationModel> hostName, IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            this._url = hostName.Value;
            _emailSender = emailSender;
            _userAuditLogService = userAuditLogService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="investmentDetail"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string AddInvestmentDetail(InvestmentDetailModel investmentDetail, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
           
            var existingRecord = _startupContext.InvestmentDetails.Any(x => x.InvestmentStage == investmentDetail.InvestmentStage && x.UserId == investmentDetail.LoggedUserId && x.IsActive == true);
            if (!existingRecord)
            {
                var investmentDetailEntity = new InvestmentDetail();
                investmentDetailEntity.InvestmentStage = investmentDetail.InvestmentStage;
                investmentDetailEntity.InvestmentSector = investmentDetail.InvestmentSector;
                investmentDetailEntity.InvestmentAmount = investmentDetail.InvestmentAmount;
                investmentDetailEntity.CreatedDate = DateTime.Now;
                investmentDetailEntity.CreatedBy = investmentDetail.LoggedUserId;
                investmentDetailEntity.UserId = investmentDetail.LoggedUserId;
                investmentDetailEntity.IsActive = true;
                _startupContext.InvestmentDetails.Add(investmentDetailEntity);
                _startupContext.SaveChanges();
                var userEntity = _startupContext.UserMasters.Where(x => x.UserId == investmentDetailEntity.UserId).FirstOrDefault();
                if (userEntity != null)
                {
                    var EncryptedUserId = EncryptionHelper.Encrypt(investmentDetailEntity.UserId.ToString());
                    StringBuilder strBody = new StringBuilder();
                    var siteUrl = _url.SiteUrl;
                    strBody.Append("<body>");
                    strBody.Append("<P>Click below link to verify your Account</P>");
                    strBody.Append("<h2><a href='" + siteUrl + "#/useractivate?UserId=" + EncryptedUserId + "'>Click here to redirect</a></h2>");
                    strBody.Append("</body>");
                    var emailSenderModel = new EmailModel();
                    emailSenderModel.ToAddress = userEntity.EmailId;
                    emailSenderModel.Body = strBody.ToString();
                    emailSenderModel.isHtml = true;
                    emailSenderModel.Subject = GlobalConstants.ActivationLinkMessage;
                    emailSenderModel.sentStatus = true;
                    if (!string.IsNullOrEmpty(emailSenderModel.ToAddress))
                    {
                        _emailSender.Execute(emailSenderModel.ToAddress, emailSenderModel.Subject, emailSenderModel.Body);
                    }
                    message = GlobalConstants.ActivationLinkMessage;         
                }
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add Investment";
                userAuditLog.Description = "Investment Details Added";
                userAuditLog.UserId = investmentDetailEntity.UserId; ;
                userAuditLog.CreatedBy = investmentDetailEntity.UserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.ExistingRecordMessage;
            }
            return message;
        }

        public string DeleteInvestmentDetail(int investmentId, ErrorResponseModel errorResponseModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="investmentDetail"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string EditInvestmentDetail(InvestmentDetailModel investmentDetail, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            
            var investmentDetailEntity = _startupContext.InvestmentDetails.Where(x => x.InvestmentId == investmentDetail.InvestmentId && x.UserId == investmentDetail.LoggedUserId && x.IsActive == true).FirstOrDefault();
            if (investmentDetailEntity != null)
            {
                investmentDetailEntity.InvestmentId = investmentDetail.InvestmentId;
                investmentDetailEntity.InvestmentStage = investmentDetail.InvestmentStage;
                investmentDetailEntity.InvestmentSector = investmentDetail.InvestmentSector;
                investmentDetailEntity.InvestmentAmount = investmentDetail.InvestmentAmount;
                investmentDetailEntity.UpdatedDate = DateTime.Now;
                investmentDetailEntity.UpdatedBy = investmentDetail.LoggedUserId;
                investmentDetailEntity.IsActive = true;
                _startupContext.InvestmentDetails.Update(investmentDetailEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordUpdateMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Edit Investment";
                userAuditLog.Description = "Investment Details Updated";
                userAuditLog.UserId = investmentDetail.LoggedUserId;
                userAuditLog.CreatedBy = investmentDetail.LoggedUserId; ;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.NotFoundMessage;
            }
            return message;
        }
        /// <summary>
        /// Get All Investment Details
        /// </summary>
        /// <returns></returns>
        public List<InvestmentDetailModel> GetAllInvestmentDetail()
        {
            var investmentDetailEntity = _startupContext.InvestmentDetails.Where(x => x.IsActive == true).ToList();
            var investmentDetailList = investmentDetailEntity.Select(x => new InvestmentDetailModel
            {
                InvestmentId = x.InvestmentId,
                InvestmentStage = x.InvestmentStage,
                InvestmentSector = x.InvestmentSector,
                InvestmentAmount = x.InvestmentAmount

            }).ToList();
            return investmentDetailList;
        }



        /// <summary>
        /// Get Investment Details BY Id
        /// </summary>
        /// <param name="investmentId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public InvestmentDetailModel GetInvestmentDetailById(long investmentId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var investmentDetailList = new InvestmentDetailModel();
            var investmentDetailEntity = _startupContext.InvestmentDetails.FirstOrDefault(x => x.InvestmentId == investmentId && x.IsActive == true);
            if (investmentDetailEntity != null)
            {
                investmentDetailList.InvestmentId = investmentDetailEntity.InvestmentId;
                investmentDetailList.InvestmentStage = investmentDetailEntity.InvestmentStage;
                investmentDetailList.InvestmentSector = investmentDetailEntity.InvestmentSector;
                investmentDetailList.InvestmentAmount = investmentDetailEntity.InvestmentAmount;

            }
            return investmentDetailList;
        }
        /// <summary>
        /// Get Investment Details By user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public InvestmentDetailModel GetInvestmentDetailByuserId(long userId, ref ErrorResponseModel errorResponseModel)
        {
            var investmentDetailEntity = _startupContext.InvestmentDetails.Where(x => x.UserId == userId && x.IsActive == true).FirstOrDefault();
            var investmentDetailModel = new InvestmentDetailModel();
            if (investmentDetailEntity != null)
            {
                investmentDetailModel.InvestmentId = investmentDetailEntity.InvestmentId;
                investmentDetailModel.InvestmentStage = investmentDetailEntity.InvestmentStage;
                investmentDetailModel.InvestmentSector = investmentDetailEntity.InvestmentSector;
                investmentDetailModel.InvestmentAmount = investmentDetailEntity.InvestmentAmount;
                investmentDetailModel.LoggedUserId = (int)userId;
            }
            return investmentDetailModel;
        }
    }


}
