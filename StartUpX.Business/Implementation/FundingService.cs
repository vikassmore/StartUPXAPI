using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
    public class FundingService : IFundingService
    {
        StartUpDBContext _startupContext;
        private readonly IUserAuditLogService _userAuditLogService;

        public FundingService(StartUpDBContext startUpDBContext,IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            _userAuditLogService=userAuditLogService;
        }

        /// <summary>
        /// Add Funding Data
        /// </summary>
        /// <param name="funding"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddFundingDetails(FundingModel funding, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingRecord = _startupContext.FundingMasters.Any(x => x.Name == funding.Name && x.IsActive == true);
            if (!existingRecord)
            {
                var fundingEntity = new FundingMaster();
                fundingEntity.IsActive = funding.IsActive;
                fundingEntity.Name = funding.Name;
                fundingEntity.Description = funding.Description;
                fundingEntity.CreatedDate = DateTime.Now;
                fundingEntity.CreatedBy = funding.LoggedUserId;
                fundingEntity.IsActive = true;
                _startupContext.FundingMasters.Add(fundingEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordSaveMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add funding Series";
                userAuditLog.Description = "funding Series Added";
                userAuditLog.UserId = (int)funding.LoggedUserId;
                userAuditLog.CreatedBy = funding.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.ExistingRecordMessage;
            }
            return message;
        }

        /// <summary>
        /// Delete Funding Data
        /// </summary>
        /// <param name="fundingId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteFunding(int fundingId,int userId, ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var fundingEntity = _startupContext.FundingMasters.Where(x => x.FundingId == fundingId && x.IsActive == true).FirstOrDefault();
            if (fundingEntity == null)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                fundingEntity.IsActive = false;
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordDeleteMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Delete funding";
                userAuditLog.Description = "Funding details deleted";
                userAuditLog.UserId = userId;
                userAuditLog.CreatedBy = userId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }

            return message;
        }

        /// <summary>
        /// Edit Funding Record
        /// </summary>
        /// <param name="funding"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string EditFunding(FundingModel funding, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingRecord = _startupContext.FundingMasters.Where(x => x.FundingId == funding.FundingId && x.IsActive == true);

            var fundingEntity = new FundingMaster();
            fundingEntity.FundingId = funding.FundingId;
            fundingEntity.IsActive = funding.IsActive;
            fundingEntity.Name = funding.Name;
            fundingEntity.Description = funding.Description;
            fundingEntity.UpdatedDate = DateTime.Now;
            fundingEntity.UpdatedBy = funding.UpadateBy;
            fundingEntity.IsActive = true;
            _startupContext.FundingMasters.Update(fundingEntity);
            _startupContext.SaveChanges();
            message = GlobalConstants.RecordUpdateMessage;
            /// User Audit Log
            var userAuditLog = new UserAuditLogModel();
            userAuditLog.Action = "Edit Funding";
            userAuditLog.Description = "Funding Details Updated";
            userAuditLog.UserId = (int)funding.LoggedUserId;
            userAuditLog.CreatedBy = funding.LoggedUserId;
            _userAuditLogService.AddUserAuditLog(userAuditLog);
            return message;
        }


        /// <summary>
        /// Getall funding Data
        /// </summary>
        /// <returns></returns>
        public List<FundingModel> GetAllFundingDetails()
        {
            var fundingEntity = _startupContext.FundingMasters.Where(x => x.IsActive == true).ToList();
            var FundingList = fundingEntity.Select(x => new FundingModel
            {
                FundingId = x.FundingId,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive,

            }).ToList();
            return FundingList;
        }

        /// <summary>
        /// GetFundingById funding Data
        /// </summary>
        /// <param name="fundingId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public FundingModel GetFundingById(long fundingId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var fundingModelList = new FundingModel();
            var fundingEntity = _startupContext.FundingMasters.FirstOrDefault(x => x.FundingId == fundingId && x.IsActive == true);
            if (fundingEntity != null)
            {
                fundingModelList.FundingId = fundingEntity.FundingId;
                fundingModelList.Name = fundingEntity.Name;
                fundingModelList.Description = fundingEntity.Description;
                fundingModelList.IsActive = fundingEntity.IsActive;
            }
            return fundingModelList;
        }
    }
}

