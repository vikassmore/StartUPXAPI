using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class NotableInvestorService : INotableInvestorService
    {

        StartUpDBContext _startupContext;
        private readonly IUserAuditLogService _userAuditLogService;
        public NotableInvestorService(StartUpDBContext startUpDBContext, IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            _userAuditLogService = userAuditLogService;
        }
        public string AddNotableInvestor(NotableInvestorModel notableinvestor, ref ErrorResponseModel errorResponseModel)
        {
           var message=string.Empty;
          
            var existingRecord = _startupContext.NotableInvestorMasters.Any(x => x.EmailId==notableinvestor.EmailId && x.IsActive == true);
            if(!existingRecord)
            {
                var notableinvestorEntity = new NotableInvestorMaster();

                notableinvestorEntity.NotableInvestorId = notableinvestor.NotableInvestorId;
                notableinvestorEntity.IsActive = true;
                notableinvestorEntity.FirstName = notableinvestor.FirstName;
                notableinvestorEntity.LastName = notableinvestor.LastName;
                notableinvestorEntity.EmailId = notableinvestor.EmailId;
                notableinvestorEntity.Gender = notableinvestor.Gender;
                notableinvestorEntity.MobileNo=notableinvestor.MobileNo;
                notableinvestorEntity.Description=notableinvestor.Description;
                notableinvestorEntity.CreatedDate=DateTime.Now;
                notableinvestorEntity.CreatedBy = notableinvestor.LoggedUserId;
                
                _startupContext.NotableInvestorMasters.Add(notableinvestorEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordSaveMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add Notable Investor";
                userAuditLog.Description = "Notable Investor Added";
                userAuditLog.UserId = notableinvestor.LoggedUserId;
                userAuditLog.CreatedBy = notableinvestor.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.ExistingRecordMessage;
            }
            return message;
        }

        public string DeleteNotableInvestor(int notableInvestorId,int userId, ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var notableInvestor = new NotableInvestorMaster();
            var notableInvestorEntity=_startupContext.NotableInvestorMasters.Where(x=>x.NotableInvestorId==notableInvestorId).FirstOrDefault();
            if (notableInvestorEntity == null)
            {
                message=GlobalConstants.NotFoundMessage;
            }
            else
            {
                notableInvestorEntity.IsActive = false;
                notableInvestorEntity.UpdatedDate = DateTime.Now;
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordDeleteMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Delete Notable Investor";
                userAuditLog.Description = "Notable Investor Deleted";
                userAuditLog.UserId = userId;
                userAuditLog.CreatedBy = userId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            return message;
        }

        public string EditNotableInvestor(NotableInvestorModel notableinvestor, ref ErrorResponseModel errorResponseModel)
        {
           var message=string.Empty;
            var notableInvestor = new NotableInvestorMaster();
            var notableInvestorEntity = _startupContext.NotableInvestorMasters.Where(x => x.NotableInvestorId == notableinvestor.NotableInvestorId && x.IsActive == true).FirstOrDefault();
            if(notableInvestorEntity != null)
            {
                notableInvestorEntity.NotableInvestorId = notableinvestor.NotableInvestorId;
                notableInvestorEntity.FirstName = notableinvestor.FirstName;
                notableInvestorEntity.LastName = notableinvestor.LastName;
                notableInvestorEntity.Gender = notableinvestor.Gender;
                notableInvestorEntity.EmailId=notableinvestor.EmailId;
                notableInvestorEntity.MobileNo = notableinvestor.MobileNo;
                notableInvestorEntity.Description = notableinvestor.Description;
                notableInvestorEntity.UpdatedDate = DateTime.Now;
                notableInvestorEntity.UpdatedBy = notableinvestor.NotableInvestorId;
                notableInvestorEntity.IsActive = true;
                _startupContext.NotableInvestorMasters.Update(notableInvestorEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordUpdateMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Edit Notable Investor";
                userAuditLog.Description = "Notable Investor Updated";
                userAuditLog.UserId = notableinvestor.LoggedUserId;
                userAuditLog.CreatedBy = notableinvestor.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            
            return message; 
        }

        public List<NotableInvestorModel> GetAllNotableInvestor()
        {
            var notableInvestorEntity = _startupContext.NotableInvestorMasters.Where(x => x.IsActive == true).ToList();
            var notableInvestorList = notableInvestorEntity.Select(x => new NotableInvestorModel
            {
                NotableInvestorId = x.NotableInvestorId,
                FirstName=x.FirstName,
                LastName=x.LastName,
                Gender=x.Gender,
                EmailId=x.EmailId,
                MobileNo=x.MobileNo,
                Description=x.Description

            }

                 ).ToList();
            return notableInvestorList;
        }

        public NotableInvestorModel GetNotableInvestorById(int notableInvestorId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var notableInvestorList = new NotableInvestorModel();
            var notableInvestorentity = _startupContext.NotableInvestorMasters.FirstOrDefault(x => x.NotableInvestorId == notableInvestorId && x.IsActive == true);
            if(notableInvestorentity != null)
            {
                notableInvestorList.NotableInvestorId= notableInvestorentity.NotableInvestorId;
                notableInvestorList.FirstName = notableInvestorentity.FirstName;
                notableInvestorList.LastName=notableInvestorentity.LastName;
                notableInvestorList.EmailId= notableInvestorentity.EmailId;
                notableInvestorList.MobileNo = notableInvestorentity.MobileNo;
                notableInvestorList.Description = notableInvestorentity.Description;
                notableInvestorList.Gender = notableInvestorentity.Gender;
                notableInvestorList.IsActive = notableInvestorentity.IsActive;

            }
            return notableInvestorList;
        }
    }
}
