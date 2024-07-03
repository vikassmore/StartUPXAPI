using Amazon.Auth.AccessControlPolicy;
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
    public class MasterService : IMasterService
    {
        StartUpDBContext _startupContext;
        private readonly IUserAuditLogService _userAuditLogService;
        public MasterService(StartUpDBContext startUpDBContext, IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            _userAuditLogService = userAuditLogService;
        }

        /// <summary>
        /// Add FAQ master data 
        /// </summary>
        /// <param name="faqModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddFAQ(FAQModel faqModel, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingRecord = _startupContext.Faqmasters.Any(x => x.FrequentlyAqid == faqModel.FrequentlyAqid && x.IsActive == true);
            if (faqModel.FrequentlyAqid ==0)
            {
                var faqEntity = new Faqmaster();
                faqEntity.Question = faqModel.Question;
                faqEntity.Answer = faqModel.Answer;
                faqEntity.CreatedDate = DateTime.Now;
                faqEntity.CreatedBy = faqModel.LoggedUserId;
                faqEntity.IsActive = true;
                _startupContext.Faqmasters.Add(faqEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordSaveMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add FAQ";
                userAuditLog.Description = "FAQ details Added";
                userAuditLog.UserId = faqModel.LoggedUserId;
                userAuditLog.CreatedBy = faqModel.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                var faqEntity = _startupContext.Faqmasters.Where(x => x.FrequentlyAqid == faqModel.FrequentlyAqid && x.IsActive == true).FirstOrDefault();
                if (faqEntity != null)
                {
                    faqEntity.FrequentlyAqid = faqModel.FrequentlyAqid;
                    faqEntity.Question = faqModel.Question;
                    faqEntity.Answer = faqModel.Answer;
                    faqEntity.UpdatedDate = DateTime.Now;
                    faqEntity.UpdatedBy = faqModel.LoggedUserId;
                    faqEntity.IsActive = true;
                    _startupContext.Faqmasters.Update(faqEntity);
                    _startupContext.SaveChanges();
                    message = GlobalConstants.RecordUpdateMessage;
                    /// User Audit Log
                    var userAuditLog = new UserAuditLogModel();
                    userAuditLog.Action = "Edit FAQ";
                    userAuditLog.Description = "FAQ details Updated";
                    userAuditLog.UserId = faqModel.LoggedUserId;
                    userAuditLog.CreatedBy = faqModel.LoggedUserId;
                    _userAuditLogService.AddUserAuditLog(userAuditLog);
                }
            }

            return message;
        }
        /// <summary>
        /// Delete FAQ Master record
        /// </summary>
        /// <param name="faqMasterId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteFAQ(int faqMasterId, int userId, ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
           
            var faqEntity = _startupContext.Faqmasters.Where(x => x.FrequentlyAqid == faqMasterId && x.IsActive == true).FirstOrDefault();
            if (faqEntity != null)
            {
                faqEntity.IsActive = false;
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordDeleteMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Delete FAQ";
                userAuditLog.Description = "FAQ details deleted";
                userAuditLog.UserId = userId;
                userAuditLog.CreatedBy = userId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.NotFoundMessage;
            }
            return message;
        }

        /// <summary>
        /// Get all FAQ Detail Data
        /// </summary>
        /// <returns></returns>
        public List<FAQModel> GetAllFAQ()
        {
            var faqEntity = _startupContext.Faqmasters.Where(x => x.IsActive == true).ToList();
            var faqMasterList = faqEntity.Select(x => new FAQModel
            {
                FrequentlyAqid = x.FrequentlyAqid,
                Question = x.Question,
                Answer = x.Answer,
            }).ToList();
            return faqMasterList;

        }

        /// <summary>
        /// Get FAQ Master data By Id
        /// </summary>
        /// <param name="faqMasterId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public FAQModel GetFAQById(long faqMasterId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var faqModel = new FAQModel();
            var faqEntity = _startupContext.Faqmasters.FirstOrDefault(x => x.FrequentlyAqid == faqMasterId && x.IsActive == true);
            if (faqEntity != null)
            {
                faqModel.FrequentlyAqid = faqEntity.FrequentlyAqid;
                faqModel.Question = faqEntity.Question;
                faqModel.Answer = faqEntity.Answer;
            }
            return faqModel;
        }

        /// <summary>
        /// Add Policy master data 
        /// </summary>
        /// <param name="policyModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddPolicy(PolicyModel policyModel, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingRecord = _startupContext.PolicyMasters.Any(x => x.PolicyId == policyModel.PolicyId && x.IsActive == true);
            if (policyModel.PolicyId == 0)
            {
                var policyEntity = new PolicyMaster();
                policyEntity.PolicyName = policyModel.PolicyName;
                policyEntity.PolicyDescription = policyModel.PolicyDescription;
                policyEntity.CreatedDate = DateTime.Now;
                policyEntity.CreatedBy = policyModel.LoggedUserId;
                policyEntity.IsActive = true;
                _startupContext.PolicyMasters.Add(policyEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordSaveMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add Policy";
                userAuditLog.Description = "Policy details Added";
                userAuditLog.UserId = policyModel.LoggedUserId;
                userAuditLog.CreatedBy = policyModel.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                var policyEntity = _startupContext.PolicyMasters.Where(x => x.PolicyId == policyModel.PolicyId && x.IsActive == true).FirstOrDefault();
                if (policyEntity != null)
                {
                    policyEntity.PolicyId = policyModel.PolicyId;
                    policyEntity.PolicyName = policyModel.PolicyName;
                    policyEntity.PolicyDescription = policyModel.PolicyDescription;
                    policyEntity.UpdatedDate = DateTime.Now;
                    policyEntity.UpdatedBy = policyModel.LoggedUserId;
                    policyEntity.IsActive = true;
                    _startupContext.PolicyMasters.Update(policyEntity);
                    _startupContext.SaveChanges();
                    message = GlobalConstants.RecordUpdateMessage;
                    /// User Audit Log
                    var userAuditLog = new UserAuditLogModel();
                    userAuditLog.Action = "Edit Policy";
                    userAuditLog.Description = "Policy details Updated";
                    userAuditLog.UserId = policyModel.LoggedUserId;
                    userAuditLog.CreatedBy = policyModel.LoggedUserId;
                    _userAuditLogService.AddUserAuditLog(userAuditLog);
                }
            }

            return message;
        }
        /// <summary>
        /// Delete Policy Master record
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeletePolicy(int policyId,int userId, ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
           
            var policyEntity = _startupContext.PolicyMasters.Where(x => x.PolicyId == policyId && x.IsActive == true).FirstOrDefault();
            if (policyEntity != null)
            {
                policyEntity.IsActive = false;
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordDeleteMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Delete Policy";
                userAuditLog.Description = "Policy details Deleted";
                userAuditLog.UserId = userId;
                userAuditLog.CreatedBy = userId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);

            }
            else
            {
                message = GlobalConstants.NotFoundMessage;
            }
            return message;
        }

        /// <summary>
        /// Get all Policy Detail Data
        /// </summary>
        /// <returns></returns>
        public List<PolicyModel> GetAllPolicy()
        {
            var policyEntity = _startupContext.PolicyMasters.Where(x => x.IsActive == true).ToList();
            var policyMasterList = policyEntity.Select(x => new PolicyModel
            {
                PolicyId = x.PolicyId,
                PolicyName = x.PolicyName,
                PolicyDescription = x.PolicyDescription,
            }).ToList();
            return policyMasterList;

        }


        /// <summary>
        /// Get Policy Master data By Id
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public PolicyModel GetPolicyById(long policyId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var policyModel = new PolicyModel();
            var policyEntity = _startupContext.PolicyMasters.FirstOrDefault(x => x.PolicyId == policyId && x.IsActive == true);
            if (policyEntity != null)
            {
                policyModel.PolicyId = policyEntity.PolicyId;
                policyModel.PolicyName = policyEntity.PolicyName;
                policyModel.PolicyDescription = policyEntity.PolicyDescription;
            }
            return policyModel;
        }
        /// <summary>
        /// Get all Employee Count
        /// </summary>
        /// <returns></returns>
        public List<EmployeeCountModel> GetAllEmployeeCount(ref ErrorResponseModel errorResponseModel)
        {
            var employeeEntity = _startupContext.EmployeeCountMasters.Where(x => x.IsActive == true).ToList();
            var employeeMasterList = employeeEntity.Select(x => new EmployeeCountModel
            {
                EmployeeCountId = x.EmployeeCountId,
                EmployeeCount = x.EmployeeCount,
                IsActive = x.IsActive,
            }).ToList();
            return employeeMasterList;

        }
    }
}
