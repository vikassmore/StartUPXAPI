using Microsoft.AspNetCore.Hosting;
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
    public class BankDetailService : IBankDetailService
    {
        StartUpDBContext _startupContext;
        private readonly IUserAuditLogService _userAuditLogService;

        public BankDetailService(StartUpDBContext startUpDBContext, IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            _userAuditLogService = userAuditLogService;


        }
        /// <summary>
        /// Add Back Details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="bankdetail"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddBankDetail(BankAccountDetailModel bankdetail, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
          
            var existingRecord = _startupContext.BankDetails.Any(x => x.UserId == bankdetail.LoggedUserId && x.IsActive == true);
            if (!existingRecord)
            {
                var bankdetailEntity = new BankDetail();
                bankdetailEntity.Ifsccode = bankdetail.Ifsccode;
                bankdetailEntity.BankName = bankdetail.BankName;
                bankdetailEntity.BranchName = bankdetail.BranchName;
                bankdetailEntity.AccountNumber = bankdetail.AccountNumber;
                bankdetailEntity.CreatedDate = DateTime.Now;
                bankdetailEntity.CreatedBy = bankdetail.LoggedUserId;
                bankdetailEntity.UserId = bankdetail.LoggedUserId;
                bankdetailEntity.IsActive = true;
                _startupContext.BankDetails.Add(bankdetailEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordSaveMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add Bank Details";
                userAuditLog.Description = "Bank Details Added";
                userAuditLog.UserId = bankdetail.LoggedUserId;
                userAuditLog.CreatedBy = bankdetail.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);


            }
            else
            {
                var bankdetailEntity = _startupContext.BankDetails.FirstOrDefault(x => x.BankId == bankdetail.BankId && x.UserId == bankdetail.LoggedUserId && x.IsActive == true);
                if (bankdetailEntity != null)
                {
                    bankdetailEntity.BankId = bankdetail.BankId;
                    bankdetailEntity.Ifsccode = bankdetail.Ifsccode;
                    bankdetailEntity.BankName = bankdetail.BankName;
                    bankdetailEntity.BranchName = bankdetail.BranchName;
                    bankdetailEntity.AccountNumber = bankdetail.AccountNumber;
                    bankdetailEntity.UpdatedDate = DateTime.Now;
                    bankdetailEntity.UpdatedBy = bankdetail.LoggedUserId;
                    bankdetailEntity.UserId = bankdetail.LoggedUserId;
                    bankdetailEntity.IsActive = true;
                    _startupContext.BankDetails.Update(bankdetailEntity);
                    _startupContext.SaveChanges();
                    message = GlobalConstants.RecordUpdateMessage;
                    /// User Audit Log
                    var userAuditLog = new UserAuditLogModel();
                    userAuditLog.Action = "Edit Bank Details";
                    userAuditLog.Description = "Bank Details Updated";
                    userAuditLog.UserId = bankdetail.LoggedUserId;
                    userAuditLog.CreatedBy = bankdetail.LoggedUserId;
                    _userAuditLogService.AddUserAuditLog(userAuditLog);

                }

            }

            return message;
        }

        public string DeleteBankDetail(int bankId, ErrorResponseModel errorResponseModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get All Back Details
        /// </summary>
        /// <returns></returns>
        public List<BankAccountDetailModel> GetAllBankDetail()
        {
            var bankdetailEntity = _startupContext.BankDetails.Where(x => x.IsActive == true).ToList();
            var bankdetailList = bankdetailEntity.Select(x => new BankAccountDetailModel
            {
                BankId = x.BankId,
                Ifsccode = x.Ifsccode,
                BankName = x.BankName,
                BranchName = x.BranchName,
                AccountNumber = x.AccountNumber,
            }).ToList();
            return bankdetailList;
        }

        public BankAccountDetailModel GetBankDetailById(long bankId, ref ErrorResponseModel errorResponseModel)
        {
            throw new NotImplementedException();
        }

        public BankAccountDetailModel GetBankDetailByuserId(long userId, ref ErrorResponseModel errorResponseModel)
        {
            var model = new BankAccountDetailModel();
            var bankdetailEntity = _startupContext.BankDetails.FirstOrDefault(x => x.UserId == userId && x.IsActive == true);
            if (bankdetailEntity != null)
            {
                model.BankId = bankdetailEntity.BankId;
                model.Ifsccode = bankdetailEntity.Ifsccode;
                model.BankName = bankdetailEntity.BankName;
                model.BranchName = bankdetailEntity.BranchName;
                model.AccountNumber = bankdetailEntity.AccountNumber;
            }
            return model;
        }
    }
}
