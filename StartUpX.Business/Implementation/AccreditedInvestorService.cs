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


    public class AccreditedInvestorService:IAccreditedInvestor
    {
        StartUpDBContext _startupContext;
        private readonly IUserAuditLogService _userAuditLogService;
        public AccreditedInvestorService(StartUpDBContext startUpDBContext, IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            _userAuditLogService = userAuditLogService;
        }

        public AccreditedInvestorModel GetAccreditedInvestorById(long accreditedInvestorId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var accreditedModel = new AccreditedInvestorModel();
            var accreditedEntity = _startupContext.AccreditedInvestorMasters.FirstOrDefault(x => x.AccreditedInvestorId == accreditedInvestorId && x.IsActive == true);
            if (accreditedEntity != null)
            {
                accreditedModel.AccreditedInvestorId = accreditedEntity.AccreditedInvestorId;
                accreditedModel.AccreditedInvestorName = accreditedEntity.AccreditedInvestorName;
                accreditedModel.Description = accreditedEntity.Description;
                accreditedModel.IsActive = accreditedEntity.IsActive;
            }
            return accreditedModel;
        }

        public List<AccreditedInvestorModel> GetAllAccreditdInvestor()
        {
            var AccreditedInvestorEntity = _startupContext.AccreditedInvestorMasters.Where(x => x.IsActive == true).ToList();
            var AccreditedInvestorList = AccreditedInvestorEntity.Select(x => new AccreditedInvestorModel
            {
                AccreditedInvestorId = x.AccreditedInvestorId,
                AccreditedInvestorName = x.AccreditedInvestorName,
                Description = x.Description,
                IsActive = x.IsActive,

            }).ToList();
            return AccreditedInvestorList;
        }

        public string AddAccreditedInvestor(AccreditedInvestorModel investor, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var existingRecord = _startupContext.AccreditedInvestorMasters.Any(x =>x.AccreditedInvestorId==investor.AccreditedInvestorId && x.IsActive == true);
            if (!existingRecord)
            {
                var AccreditedInvestorEntity = new AccreditedInvestorMaster();
                AccreditedInvestorEntity.IsActive = true;
                AccreditedInvestorEntity.AccreditedInvestorName = investor.AccreditedInvestorName;
                AccreditedInvestorEntity.Description = investor.Description;
                AccreditedInvestorEntity.CreatedBy = investor.LoggedUserId;
                AccreditedInvestorEntity.CreatedDate = DateTime.Now;
                _startupContext.AccreditedInvestorMasters.Add(AccreditedInvestorEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordSaveMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "AccreditedInvestor Add";
                userAuditLog.Description = "AccreditedInvestor Add.";
                userAuditLog.UserId = (int)investor.LoggedUserId;
                userAuditLog.CreatedBy = investor.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.ExistingRecordMessage;
            }

            return message;
        }

        public string EditAccreditedInvestor(AccreditedInvestorModel investor, ref ErrorResponseModel errorResponseModel)
        {
           var message = string.Empty;

            var AccreditedInvestorEntity = _startupContext.AccreditedInvestorMasters.Where(x => x.AccreditedInvestorId == investor.AccreditedInvestorId && x.IsActive==true).FirstOrDefault();
            if (AccreditedInvestorEntity != null)
            {
                AccreditedInvestorEntity.AccreditedInvestorId = investor.AccreditedInvestorId;
                AccreditedInvestorEntity.AccreditedInvestorName = investor.AccreditedInvestorName;
                AccreditedInvestorEntity.Description = investor.Description;
                AccreditedInvestorEntity.IsActive = true;
                AccreditedInvestorEntity.UpdatedBy = investor.LoggedUserId;
                AccreditedInvestorEntity.UpdatedDate = DateTime.Now;
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordUpdateMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "AccreditedInvestor Edit";
                userAuditLog.Description = "AccreditedInvestor updated.";
                userAuditLog.UserId = (int)investor.LoggedUserId;
                userAuditLog.CreatedBy = investor.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.NotFoundMessage;
            }

            return message;
        }

        public string DeleteAccreditedInvestor(int accreditedInvestorId, ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var accredited = new AccreditedInvestorModel();
            var accreditedInvestorEntity = _startupContext.AccreditedInvestorMasters.Where(x => x.AccreditedInvestorId == accreditedInvestorId && x.IsActive == true).FirstOrDefault();
            if (accreditedInvestorEntity == null)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                accreditedInvestorEntity.IsActive = false;
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordDeleteMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "AccreditedInvestor Delete";
                userAuditLog.Description = "AccreditedInvestor deleted.";
                userAuditLog.UserId = (int)accredited.LoggedUserId;
                userAuditLog.CreatedBy = accredited.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }

            return message;
        }
    }
}
