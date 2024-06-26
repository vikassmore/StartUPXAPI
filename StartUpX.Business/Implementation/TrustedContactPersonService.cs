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
    public class TrustedContactPersonService : ITrustedContactPersonService
    {

        StartUpDBContext _startupContext;
        private readonly IUserAuditLogService _userAuditLogService;
        public TrustedContactPersonService(StartUpDBContext startUpDBContext, IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            _userAuditLogService = userAuditLogService;
        }
        /// <summary>
        /// Add Trusted Content
        /// </summary>
        /// <param name="model"></param>
        /// <param name="smtpSettingsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddTrustedContact(TrustedContactPersonModel trustedContect, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var existingRecord = _startupContext.TrustedContactPeople.Any(x =>   x.EmailId == trustedContect.EmailId && x.UserId == trustedContect.LoggedUserId && x.IsActive == true);
            if (!existingRecord)
            {
                var trustedContectEntity = new TrustedContactPerson();
                trustedContectEntity.FirstName = trustedContect.FirstName;
                trustedContectEntity.LastName = trustedContect.LastName;
                trustedContectEntity.EmailId = trustedContect.EmailId;                
                trustedContectEntity.CountryId = trustedContect.CountryId;
                trustedContectEntity.StateId = trustedContect.StateId;
                trustedContectEntity.CityId = trustedContect.CityId;
                trustedContectEntity.ZipCode = trustedContect.ZipCode;
                trustedContectEntity.MobileNo = trustedContect.MobileNo;
                trustedContectEntity.Address1 = trustedContect.Address1;
                trustedContectEntity.Address2 = trustedContect.Address2;
                trustedContectEntity.IsTrustedContact = trustedContect.Types;
                trustedContectEntity.CreatedDate = DateTime.Now;
                trustedContectEntity.CreatedBy = trustedContect.LoggedUserId;
                trustedContectEntity.UserId = trustedContect.LoggedUserId;
                trustedContectEntity.IsActive = true;
                _startupContext.TrustedContactPeople.Add(trustedContectEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordSaveMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add Trustd Contact";
                userAuditLog.Description = "Trusted Contact person details Added";
                userAuditLog.UserId = trustedContect.LoggedUserId;
                userAuditLog.CreatedBy = trustedContect.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                var trustedContectEntity = _startupContext.TrustedContactPeople.FirstOrDefault(x=>x.TrustedContactId == trustedContect.TrustedContactId && x.UserId == trustedContect.LoggedUserId && x.IsActive == true);
                if(trustedContectEntity != null)
                {
                    trustedContectEntity.TrustedContactId = trustedContect.TrustedContactId;
                    trustedContectEntity.FirstName = trustedContect.FirstName;
                    trustedContectEntity.LastName = trustedContect.LastName;
                    trustedContectEntity.EmailId = trustedContect.EmailId;
                    trustedContectEntity.CountryId = trustedContect.CountryId;
                    trustedContectEntity.StateId = trustedContect.StateId;
                    trustedContectEntity.CityId = trustedContect.CityId;
                    trustedContectEntity.ZipCode = trustedContect.ZipCode;
                    trustedContectEntity.MobileNo = trustedContect.MobileNo;
                    trustedContectEntity.Address1 = trustedContect.Address1;
                    trustedContectEntity.Address2 = trustedContect.Address2;
                    trustedContectEntity.IsTrustedContact = trustedContect.Types;
                    trustedContectEntity.UpdatedDate = DateTime.Now;
                    trustedContectEntity.UpdatedBy = trustedContect.LoggedUserId;
                    trustedContectEntity.UserId = trustedContect.LoggedUserId;
                    trustedContectEntity.IsActive = true;
                    _startupContext.TrustedContactPeople.Update(trustedContectEntity);
                    _startupContext.SaveChanges();
                    message = GlobalConstants.RecordUpdateMessage;
                    /// User Audit Log
                    var userAuditLog = new UserAuditLogModel();
                    userAuditLog.Action = "Edit Trustd Contact";
                    userAuditLog.Description = "Trusted Contact person details Updated";
                    userAuditLog.UserId = trustedContect.LoggedUserId;
                    userAuditLog.CreatedBy = trustedContect.LoggedUserId;
                    _userAuditLogService.AddUserAuditLog(userAuditLog);
                }
            }
            return message;
        }

        /// <summary>
        /// Get All Trusted Content
        /// </summary>
        /// <returns></returns>
        public List<TrustedContactPersonModel> GetAllTrustedContact()
        {
            var trustedContectEntity = _startupContext.TrustedContactPeople.Where(x => x.IsActive == true).ToList();
            var trustedContectList = trustedContectEntity.Select(x => new TrustedContactPersonModel
            {
                TrustedContactId = x.TrustedContactId,               
                FirstName = x.FirstName,
                LastName = x.LastName,
                EmailId = x.EmailId,                
                CountryId = x.CountryId,
                StateId = x.StateId,
                CityId = x.CityId,
                ZipCode = x.ZipCode,
                MobileNo= x.MobileNo,
                Address1 = x.Address1,
                Address2 = x.Address2,
                Types = x.IsTrustedContact
            }).ToList();
            return trustedContectList;
        }

        /// <summary>
        /// Get Trusted Content By Id
        /// </summary>
        /// <param name="TrustedContectId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public TrustedContactPersonModel GetTrustedContactById(long TrustedContectId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var trustedContectList = new TrustedContactPersonModel();
            var trustedContectEntity = _startupContext.TrustedContactPeople.FirstOrDefault(x => x.TrustedContactId == TrustedContectId && x.IsActive == true);
            if (trustedContectEntity != null)
            {
                trustedContectList.TrustedContactId = trustedContectEntity.TrustedContactId;               
                trustedContectList.FirstName = trustedContectEntity.FirstName;
                trustedContectList.LastName = trustedContectEntity.LastName;
                trustedContectList.EmailId = trustedContectEntity.EmailId;
                trustedContectList.CountryId = trustedContectEntity.CountryId;
                trustedContectList.StateId = trustedContectEntity.StateId;
                trustedContectList.CityId = trustedContectEntity.CityId;
                trustedContectList.ZipCode = trustedContectEntity.ZipCode;
                trustedContectList.MobileNo= trustedContectEntity.MobileNo;
                trustedContectList.Address1 = trustedContectEntity.Address1;
                trustedContectList.Address2 = trustedContectEntity.Address2;
                trustedContectList.Types = trustedContectEntity.IsTrustedContact;
            }
            return trustedContectList;
        }
        /// <summary>
        /// Get Trusted Content By user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public TrustedContactPersonModel GetTrustedContactByuserId(long userId, ref ErrorResponseModel errorResponseModel)
        {
            var trustedContectEntity = _startupContext.TrustedContactPeople.Where(x => x.UserId == userId && x.IsActive == true).FirstOrDefault();
            var trustedContectPersonModel = new TrustedContactPersonModel();
            if (trustedContectEntity != null)
            {
                trustedContectPersonModel.TrustedContactId = trustedContectEntity.TrustedContactId;                
                trustedContectPersonModel.FirstName = trustedContectEntity.FirstName;
                trustedContectPersonModel.LastName = trustedContectEntity.LastName;
                trustedContectPersonModel.EmailId = trustedContectEntity.EmailId;                
                trustedContectPersonModel.CountryId = trustedContectEntity.CountryId;
                trustedContectPersonModel.StateId = trustedContectEntity.StateId;
                trustedContectPersonModel.CityId = trustedContectEntity.CityId;
                trustedContectPersonModel.ZipCode = trustedContectEntity.ZipCode;
                trustedContectPersonModel.MobileNo= trustedContectEntity.MobileNo;
                trustedContectPersonModel.Address1 = trustedContectEntity.Address1;
                trustedContectPersonModel.Address2 = trustedContectEntity.Address2;
                trustedContectPersonModel.Types = trustedContectEntity.IsTrustedContact;

            }
            return trustedContectPersonModel;
        }
    }
}
