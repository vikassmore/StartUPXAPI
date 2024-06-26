using StartUpX.Business.Implemetation;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class FounderDetailService : IFounderDetailService
    {
        StartUpDBContext _startupContext;
        private readonly IEmailSender _emailSender;
        private readonly IUserAuditLogService _userAuditLogService;
        public FounderDetailService(StartUpDBContext startUpDBContext, IEmailSender emailSender, IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            _emailSender = emailSender;
            _userAuditLogService = userAuditLogService;

        }

        /// <summary>
        /// Add Founder Data
        /// </summary>
        /// <param name="founder"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddFounder(List<FounderDeatailModel> model, int LoggedUserId, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            foreach (var item in model)
            {
                var isExist = _startupContext.FounderDetails.Any(x => x.EmailId == item.EmailId && x.LastName == item.LastName && x.UserId == LoggedUserId && x.IsActive == true);
                if (!isExist)
                {
                    var founderEntity = new FounderDetail();
                    founderEntity.FirstName = item.FirstName;
                    founderEntity.LastName = item.LastName;
                    founderEntity.EmailId = item.EmailId;
                    founderEntity.MobileNo = item.MobileNo;
                    founderEntity.Gender = item.Gender;
                    founderEntity.Description = item.Description;
                    founderEntity.CreatedDate = DateTime.Now;
                    founderEntity.CreatedBy = LoggedUserId;
                    founderEntity.UserId = LoggedUserId;
                    founderEntity.IsActive = true;
                    var startupEntity = _startupContext.StartUpDetails.FirstOrDefault(x => x.UserId == LoggedUserId);
                    StringBuilder strBody = new StringBuilder();
                    strBody.Append("<body>");
                    strBody.Append("<P>StartUpX</P>");
                    strBody.Append("<h2>You have added as a founder in the" + " " + startupEntity.StartUpName + " " + "company</h2 >");
                    strBody.Append("</body>");
                    var emailSenderModel = new EmailModel();
                    emailSenderModel.ToAddress = founderEntity.EmailId;
                    emailSenderModel.Body = strBody.ToString();
                    emailSenderModel.isHtml = true;
                    emailSenderModel.Subject = GlobalConstants.FounderAddedMessage;
                    emailSenderModel.sentStatus = true;
                    if (!string.IsNullOrEmpty(emailSenderModel.ToAddress))
                    {
                        _emailSender.Execute(emailSenderModel.ToAddress, emailSenderModel.Subject, emailSenderModel.Body);
                    }
                    _startupContext.FounderDetails.Add(founderEntity);
                }
            }
            _startupContext.SaveChanges();
            message = "Founder Added Succesfully ";
            /// User Audit Log
            var userAuditLog = new UserAuditLogModel();
            userAuditLog.Action = "Founder Add";
            userAuditLog.Description = "Founder Added";
            userAuditLog.UserId = LoggedUserId;
            userAuditLog.CreatedBy = LoggedUserId;
            _userAuditLogService.AddUserAuditLog(userAuditLog);

            return message;
        }


        /// <summary>
        /// Delete Founder Data
        /// </summary>
        /// <param name="founderId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteFounder(int founderId, int LoggedUserId, ErrorResponseModel errorResponseModel)
        {

            var message = string.Empty;
            var founderEntity = _startupContext.FounderDetails.FirstOrDefault(x => x.FounderId == founderId);
            if (founderEntity == null)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                founderEntity.IsActive = false;
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordDeleteMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Delete Founder";
                userAuditLog.Description = "Founder deleted";
                userAuditLog.UserId = LoggedUserId;
                userAuditLog.CreatedBy = LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }

            return message;
        }

        /// <summary>
        /// Edit Founder Data
        /// </summary>
        /// <param name="founder"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>

        public string EditFounder(FounderDeatailModel founder, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var founderEntity = _startupContext.FounderDetails.FirstOrDefault(x => x.FounderId == founder.FounderId);
            if (founderEntity != null)
            {
                founderEntity.FounderId = founder.FounderId;
                founderEntity.IsActive = true;
                founderEntity.FirstName = founder.FirstName;
                founderEntity.LastName = founder.LastName;
                founderEntity.EmailId = founder.EmailId;
                founderEntity.MobileNo = founder.MobileNo;
                founderEntity.Gender = founder.Gender;
                founderEntity.Description = founder.Description;
                founderEntity.UserId = founder.LoggedUserId;
                _startupContext.FounderDetails.Update(founderEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordUpdateMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Edit Founder";
                userAuditLog.Description = "Founder Updated.";
                userAuditLog.UserId = founder.LoggedUserId;
                userAuditLog.CreatedBy = founder.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            return message;
        }

        /// <summary>
        /// Getallfounder Data
        /// </summary>
        /// <returns></returns>
        public List<FounderDeatailModel> GetAllFounder()
        {
            var founderEntity = _startupContext.FounderDetails.Where(x => x.IsActive == true).ToList();
            var founderList = founderEntity.Select(x => new FounderDeatailModel
            {
                FounderId = x.FounderId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                EmailId = x.EmailId,
                MobileNo = x.MobileNo,
                Gender = x.Gender,
                Description = x.Description,

            }).ToList();
            return founderList;
        }


        /// <summary>
        /// GetAllFounder by userId
        /// </summary>
        /// <returns></returns>
        public List<FounderDeatailModel> GetAllFounderbyuserId(int userId)
        {
            var founderEntity = _startupContext.FounderDetails.Where(x => x.UserId == userId && x.IsActive == true).ToList();
            var founderList = founderEntity.Select(x => new FounderDeatailModel
            {
                FounderId = x.FounderId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                EmailId = x.EmailId,
                MobileNo = x.MobileNo,
                Gender = x.Gender,
                Description = x.Description,

            }).ToList();
            return founderList;
        }

        /// <summary>
        /// GetFounderById founder
        /// </summary>
        /// <param name="founderId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public FounderDeatailModel GetFounderById(long founderId, ref ErrorResponseModel errorResponseModel)
        {

            errorResponseModel = new ErrorResponseModel();
            var founderList = new FounderDeatailModel();
            var founderEntity = _startupContext.FounderDetails.FirstOrDefault(x => x.FounderId == founderId);
            if (founderEntity != null)
            {
                founderList.FounderId = founderEntity.FounderId;
                founderList.FirstName = founderEntity.FirstName;
                founderList.LastName = founderEntity.LastName;
                founderList.EmailId = founderEntity.EmailId;
                founderList.MobileNo = founderEntity.MobileNo;
                founderList.Gender = founderEntity.Gender;
                founderList.Description = founderEntity.Description;
            }
            return founderList;
        }
    }
}

