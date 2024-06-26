using Microsoft.Extensions.Configuration;
using StartUpX.Business.Implemetation;
using StartUpX.Business.Interface;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StartUpX.Common;
using Microsoft.Extensions.Options;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace StartUpX.Business.Implementation
{

    public class UserService : IUserService

    {
        StartUpDBContext _startupContext;
        //EmailService emailSenderService = new EmailService();
        private readonly IEmailSender _emailSender;
        private readonly IAuthService _authService;
        private ConfigurationModel _url;
        IConfiguration _configuration;
        private readonly IUserAuditLogService _userAuditLogService;
        private readonly INotificationService _notificationService;
        public UserService(StartUpDBContext startUpDBContext, IConfiguration configuration, IEmailSender emailSender, IOptions<ConfigurationModel> hostName, IAuthService authService, IUserAuditLogService userAuditLogService, INotificationService notificationService)
        {
            _startupContext = startUpDBContext;
            _configuration = configuration;
            _emailSender = emailSender;
            this._url = hostName.Value;
            _authService = authService;
            _userAuditLogService = userAuditLogService;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Added User Data
        /// </summary>
        /// <param name="model"></param>
        /// <param name="smtpSettingsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public UserRegister AddUser(UserModel model, SMTPSettingsModel smtpSettingsModel, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var userRegister = new UserRegister();
            var existingUser = _startupContext.UserMasters.Any(x => x.EmailId == model.EmailId);
            if (existingUser)
            {
                userRegister.message = GlobalConstants.ExistingUserMessage;
            }
            else
            {
                try
                {
                    var userEntity = new UserMaster();
                    userEntity.FirstName = model.FirstName;
                    userEntity.LastName = model.LastName;
                    userEntity.EmailId = model.EmailId.ToLower();
                    userEntity.Password = EncryptionHelper.Encrypt(model.Password);
                    userEntity.FounderTypeId = model.FounderTypeId;
                    userEntity.RoleId = model.RoleId;
                    userEntity.CreatedDate = DateTime.Now;
                    userEntity.CreatedBy = model.CreatedBy;
                    userEntity.IsActive = false;
                    userEntity.ServiceStatus = model.ServiceStatus;
                    _startupContext.UserMasters.Add(userEntity);
                    _startupContext.SaveChanges();

                    if (userEntity.RoleId != Helper.Invester)
                    {
                        var EncryptedUserId = EncryptionHelper.Encrypt(userEntity.UserId.ToString());
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
                        userRegister.message = GlobalConstants.ActivationLinkMessage;
                    }
                    else
                    {

                    }
                    userRegister.UserId = userEntity.UserId;
                    userRegister.RoleId = userEntity.RoleId;
                    /// User Audit Log
                    var userAuditLog = new UserAuditLogModel();
                    userAuditLog.Action = "User Add";
                    userAuditLog.Description = "New user registered";
                    userAuditLog.UserId = userRegister.UserId;
                    userAuditLog.CreatedBy = userRegister.UserId;
                    _userAuditLogService.AddUserAuditLog(userAuditLog);

                    /// Notification Add
                    var notification = new NotificationModel();
                    notification.Message = "Complete your profile details";
                    notification.UserId = userRegister.UserId;
                    notification.LoggedUserId = userRegister.UserId;
                    _notificationService.AddNotification(notification);
                }
                catch (Exception ex)
                {

                }
            }
            return userRegister;
        }


        /// <summary>
        /// GetUserById user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public UserModel GetUserById(long userId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var userList = new UserModel();
            var userEntity = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == userId && x.IsActive == true);
            if (userEntity != null)
            {
                userList.FirstName = userEntity.FirstName;
                userList.LastName = userEntity.LastName;
                userList.EmailId = userEntity.EmailId;
                userList.Password = userEntity.Password;
                userList.FounderTypeId = userEntity.FounderTypeId;
                userList.RoleId = userEntity.RoleId;
            }

            return userList;
        }


        /// <summary>
        /// Getall User Data
        /// </summary>
        /// <returns></returns>
        public List<UserModel> GetAllUser()
        {
            var userEntity = _startupContext.UserMasters.Where(x => x.IsActive == true).ToList();
            var userList = userEntity.Select(x => new UserModel
            {
                UserId = x.UserId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                EmailId = x.EmailId,
                Password = x.Password,
                IsActive = x.IsActive,
                FounderTypeId = x.FounderTypeId,
                RoleId = x.RoleId,
            }).ToList();
            return userList;
        }

        /// <summary>
        /// Edit User Data
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string EditUser(UserModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var userEntity = _startupContext.UserMasters.Where(x => x.UserId == model.UserId && x.IsActive == true).FirstOrDefault();
            userEntity.FirstName = model.FirstName;
            userEntity.LastName = model.LastName;
            userEntity.EmailId = model.EmailId.ToLower();
            userEntity.Password = model.Password;
            userEntity.FounderTypeId = model.FounderTypeId;
            userEntity.RoleId = model.RoleId;
            userEntity.UpdatedDate = DateTime.Now;
            userEntity.UpdatedBy = model.UpadateBy;
            userEntity.ServiceStatus = model.ServiceStatus;
            _startupContext.SaveChanges();
            message = GlobalConstants.RecordUpdateMessage;
            /// User Audit Log
            var userAuditLog = new UserAuditLogModel();
            userAuditLog.Action = "User Edit";
            userAuditLog.Description = "User details updated.";
            userAuditLog.UserId = model.UserId;
            userAuditLog.CreatedBy = model.UserId;
            _userAuditLogService.AddUserAuditLog(userAuditLog);
            return message;
        }

        /// <summary>
        /// Forgot password OTP
        /// </summary>
        /// <param name="email"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string ForgotPasswordOTP(string email, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var userEntity = _startupContext.UserMasters.FirstOrDefault(x => x.EmailId == email.ToLower() && x.IsActive == true);
            if (userEntity == null)
            {
                errorResponseModel.Message = GlobalConstants.EmailNotFound;
            }
            else
            {
                try
                {
                    //string subject = "Forgot password OTP sent on your email. Please check.";      
                    //  StringBuilder strBody = new StringBuilder();
                    var otp = _authService.CreateNewOTP((int)userEntity.UserId);
                    var emailModel = new EmailModel();
                    emailModel.ToAddress = email;
                    emailModel.isHtml = true;
                    emailModel.Subject = GlobalConstants.ForgotPasswordMessage;
                    emailModel.Body = otp;
                    _emailSender.Execute(userEntity.EmailId, emailModel.Subject, emailModel.Body); message = GlobalConstants.ForgotPasswordMessage;

                    /// User Audit Log
                    var userAuditLog = new UserAuditLogModel();
                    userAuditLog.Action = "Forgot Password OTP";
                    userAuditLog.Description = "Forgot Password OTP sent on email.";
                    userAuditLog.UserId = userEntity.UserId;
                    userAuditLog.CreatedBy = userEntity.UserId;
                    _userAuditLogService.AddUserAuditLog(userAuditLog);
                }
                catch (Exception ex)
                { }
            }
            return message;
        }

        /// <summary>
        /// Forget Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string ForgotPassword(ForgotPasswordModel model)
        {
            var message = string.Empty;
            if (model.UserId > 0)
            {
                //var DecryptUserId = EncryptionHelper.Decrypt(model.UserId);        
                var userEntity = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == Convert.ToInt32(model.UserId) && x.IsActive == true);
                if (userEntity == null)
                {
                    message = GlobalConstants.Status500Message;
                }
                else
                {
                    try
                    {
                        userEntity.Password = EncryptionHelper.Encrypt(model.Password);
                        _startupContext.SaveChanges();
                        message = GlobalConstants.PasswordChangeMessage;
                        /// User Audit Log
                        var userAuditLog = new UserAuditLogModel();
                        userAuditLog.Action = "Forgot Password";
                        userAuditLog.Description = "User changed password.";
                        userAuditLog.UserId = Convert.ToInt32(model.UserId);
                        userAuditLog.CreatedBy = Convert.ToInt32(model.UserId);
                        _userAuditLogService.AddUserAuditLog(userAuditLog);
                    }
                    catch (Exception ex)
                    { }
                }
            }
            message = GlobalConstants.Status500Message;
            return message;
        }
        /// <summary>
        /// Active User Data
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string ActivateUser(UserActivateModel model, ref ErrorResponseModel errorResponseModel)
        {
            var decryptedUserId = EncryptionHelper.Decrypt(model.EncryptedUserId);
            var UserId = Convert.ToInt32(decryptedUserId);
            var userEntity = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == UserId);
            if (userEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.NotFoundMessage;
                return errorResponseModel.Message;
            }
            else
            {
                userEntity.IsActive = true;
                _startupContext.SaveChanges();
                string message = "user activated";
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "User Active";
                userAuditLog.Description = "User activated.";
                userAuditLog.UserId = UserId;
                userAuditLog.CreatedBy = UserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
                return message;
            }
        }

        /// <summary>
        /// Deleted User Data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteUser(int userId, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var userEntity = _startupContext.UserMasters.Where(x => x.UserId == userId && x.IsActive == true).FirstOrDefault();
            if (userEntity != null)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            userEntity.IsActive = false;
            _startupContext.SaveChanges();
            message = GlobalConstants.RecordDeleteMessage;
            /// User Audit Log
            var userAuditLog = new UserAuditLogModel();
            userAuditLog.Action = "User Delete";
            userAuditLog.Description = "User deleted.";
            userAuditLog.UserId = userId;
            userAuditLog.CreatedBy = userId;
            _userAuditLogService.AddUserAuditLog(userAuditLog);
            return message;
        }
        /// <summary>
        /// Method used to change user password
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public UserRegister ChangePassword(ChangePassword model, ref ErrorResponseModel errorResponseModel)
        {
            var changePassword = new UserRegister();
            var userEntity = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == model.UserId);
            if (userEntity == null)
            {
                changePassword.message = GlobalConstants.UserNotFoundMessage;

                return changePassword;
            }
            else
            {
                userEntity.Password = EncryptionHelper.Encrypt(model.NewPassword);
                userEntity.UpdatedBy = model.UserId;
                userEntity.UpdatedDate = DateTime.UtcNow;
                _startupContext.SaveChanges();

                changePassword.RoleId = userEntity.RoleId;
                changePassword.message = GlobalConstants.PasswordChangeMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "ChangePassword";
                userAuditLog.Description = "User Changed Password.";
                userAuditLog.UserId = model.UserId;
                userAuditLog.CreatedBy = model.UserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
                return changePassword;
            }
        }
        /// <summary>
        /// Add Service Provider User
        /// </summary>
        /// <param name="usermodel"></param>
        /// <param name="smtpSettingsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public UserRegister AddServiceProviderUser(ServiceProviderUserModel usermodel, SMTPSettingsModel smtpSettingsModel, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var userRegister = new UserRegister();
            var existingUser = _startupContext.UserMasters.Any(x => x.EmailId == usermodel.EmailId.ToLower());
            if (existingUser)
            {
                userRegister.message = GlobalConstants.ExistingUserMessage;
            }
            else
            {
                try
                {
                    var userEntity = new UserMaster();
                    userEntity.FirstName = usermodel.FirstName;
                    userEntity.LastName = usermodel.LastName;
                    userEntity.EmailId = usermodel.EmailId.ToLower();
                    userEntity.Password = EncryptionHelper.Encrypt(usermodel.Password);
                    userEntity.FounderTypeId = usermodel.FounderTypeId;
                    userEntity.RoleId = usermodel.RoleId;
                    userEntity.CreatedDate = DateTime.Now;
                    userEntity.CreatedBy = usermodel.LoggedUserId;
                    userEntity.IsActive = true;
                    userEntity.ServiceStatus = usermodel.ServiceStatus;
                    _startupContext.UserMasters.Add(userEntity);
                    _startupContext.SaveChanges();

                    if (userEntity.RoleId == Helper.Service)
                    {

                        var EncryptedUserId = EncryptionHelper.Encrypt(userEntity.UserId.ToString());
                        StringBuilder strBody = new StringBuilder();

                        strBody.Append("<body>");
                        strBody.Append("<P>Here are Login Credential .Use and logged in</P>");
                        strBody.Append("<h2>User Name: " + usermodel.FirstName + " " + usermodel.LastName + "</h2>");
                        strBody.Append("<h2>Password: " + usermodel.Password + "</h2>");
                        strBody.Append("</body>");
                        var emailSenderModel = new EmailModel();
                        emailSenderModel.ToAddress = userEntity.EmailId;
                        emailSenderModel.Body = strBody.ToString();
                        emailSenderModel.isHtml = true;
                        emailSenderModel.Subject = GlobalConstants.ServiceProviderCredential;
                        emailSenderModel.sentStatus = true;
                        if (!string.IsNullOrEmpty(emailSenderModel.ToAddress))
                        {
                            _emailSender.Execute(emailSenderModel.ToAddress, emailSenderModel.Subject, emailSenderModel.Body);
                        }
                        userRegister.message = GlobalConstants.ServiceProviderCredential;
                    }
                    else
                    {

                    }
                    userRegister.UserId = userEntity.UserId;
                    userRegister.RoleId = userEntity.RoleId;
                    /// User Audit Log
                    var userAuditLog = new UserAuditLogModel();
                    userAuditLog.Action = "User Add";
                    userAuditLog.Description = "New user registered";
                    userAuditLog.UserId = userRegister.UserId;
                    userAuditLog.CreatedBy = userRegister.UserId;
                    _userAuditLogService.AddUserAuditLog(userAuditLog);


                    /// Notification Add
                    var notification = new NotificationModel();
                    notification.Message = "Complete your profile details";
                    notification.UserId = userRegister.UserId;
                    notification.LoggedUserId = userRegister.UserId;
                    _notificationService.AddNotification(notification);

                    var userEntity1 = new ServiceDetail();
                    userEntity1.UserId = userRegister.UserId;
                    userEntity1.Category = usermodel.Category;
                    userEntity1.IsActive = false;
                    userEntity.CreatedDate = DateTime.Now;
                    userEntity.CreatedBy = usermodel.LoggedUserId;
                    _startupContext.ServiceDetails.Add(userEntity1);
                    _startupContext.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
            return userRegister;
        }
        /// <summary>
        /// Get Service provider User By Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public UserModel GetServiceProviderUserById(int userId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var userModel = new UserModel();
            var userEntity = _startupContext.UserMasters.Include(x => x.Role).Include(x => x.FounderType).FirstOrDefault(x => x.UserId == userId && x.IsActive == true);
            if (userEntity != null)
            {
                var serviceEntity = _startupContext.ServiceDetails.Include(x => x.User).FirstOrDefault(x => x.UserId == userEntity.UserId);
                userModel.UserId = userEntity.UserId;
                userModel.FirstName = userEntity.FirstName;
                userModel.LastName = userEntity.LastName;
                userModel.EmailId = userEntity.EmailId;
                userModel.Password = userEntity.Password;
                userModel.FounderTypeId = userEntity.FounderTypeId;
                userModel.RoleId = userEntity.RoleId;
                userModel.FounderName = userEntity.FounderType.FounderName;
                userModel.RoleName = userEntity.Role.RoleName;
                userModel.Category = serviceEntity.Category;

            }

            return userModel;
        }
        /// <summary>
        /// Get All Service Provider user
        /// </summary>
        /// <returns></returns>
        public List<UserModel> GetAllServiceProviderUser()
        {
            var userModelList = new List<UserModel>();
            var userEntity = _startupContext.UserMasters.Include(x => x.Role).Include(x => x.FounderType).Include(x => x.ServiceDetails).Where(x => x.RoleId == Helper.Service).OrderByDescending(x => x.UserId).ToList();

            foreach (var item in userEntity)
            {
                var userModel = new UserModel();
                var serviceEntity = _startupContext.ServiceDetails.FirstOrDefault(x => x.UserId == item.UserId);
                userModel.UserId = item.UserId;
                userModel.FirstName = item.FirstName;
                userModel.LastName = item.LastName;
                userModel.EmailId = item.EmailId;
                userModel.Password = item.Password;
                userModel.IsActive = item.IsActive;
                userModel.FounderTypeId = item.FounderTypeId;
                userModel.RoleId = item.RoleId;
                userModel.Category = serviceEntity.Category;
                userModel.FounderName = item.FounderType.FounderName;
                userModel.ServiceStatus = item.ServiceStatus;
                userModel.RoleName = item.Role.RoleName;
                var serviceModel = new ServiceModel();
                serviceModel.ServiceProviderName = serviceEntity.ServiceProviderName;
                serviceModel.ServiceId = serviceEntity.ServiceId;
                userModel.serviceDataModel = serviceModel;
                userModelList.Add(userModel);
            }

            return userModelList;
        }
        /// <summary>
        /// get all role
        /// </summary>
        /// <returns></returns>
        public List<RoleModel> GetAllRole()
        {
            var roleEntity = _startupContext.Roles.Where(x => x.RoleId == Helper.Service && x.IsActive == true).ToList();
            var roleList = roleEntity.Select(x => new RoleModel
            {
                RoleId = x.RoleId,
                RoleName = x.RoleName,
                IsActive = x.IsActive,

            }).ToList();
            return roleList;
        }
        /// <summary>
        /// Update service status (active and block)
        /// </summary>
        /// <returns></returns>
        public string EditServiceStatus(ServiceStatusUpdateModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var userEntity = _startupContext.UserMasters.Where(x => x.UserId == model.UserId).FirstOrDefault();

            userEntity.IsActive = model.IsActive;
            userEntity.ServiceStatus = model.ServiceStatus;
            userEntity.ServiceStatus = model.ServiceStatus != null ? model.ServiceStatus : null;

            _startupContext.SaveChanges();

            StringBuilder strBody = new StringBuilder();
            if (model.IsActive == true)
            {
                strBody.Append("<body>");
                strBody.Append("<P>StartupX</P>");
                strBody.Append("<h2>Your details Successfully verified.</h2>");
                strBody.Append("<h2>Admin approved your details and now your details can see the founders.</h2>");
                strBody.Append("</body>");


                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "User Activated";
                userAuditLog.Description = "Service user Activated.";
                userAuditLog.UserId = model.UserId;
                userAuditLog.CreatedBy = model.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);


                /// Notification Add
                var notification = new NotificationModel();
                notification.Message = "You are now active user.";
                notification.UserId = model.UserId;
                notification.LoggedUserId = model.LoggedUserId;
                _notificationService.AddNotification(notification);
            }
            else
            {
                strBody.Append("<body>");
                strBody.Append("<P>StartupX</P>");
                strBody.Append("<h2>Your details not verified.</h2>");
                strBody.Append("<h2>Admin blocked your details and now your details can not be see the founders because" +" "+ model.Comment + "</h2>");
                strBody.Append("</body>");

                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "User Blocked";
                userAuditLog.Description = "Service user Blocked.";
                userAuditLog.UserId = model.UserId;
                userAuditLog.CreatedBy = model.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);


                /// Notification Add
                var notification = new NotificationModel();
                notification.Message = "You have Blocked by admin.";
                notification.UserId = model.UserId;
                notification.LoggedUserId = model.LoggedUserId;
                _notificationService.AddNotification(notification);
            }
            var emailSenderModel = new EmailModel();
            emailSenderModel.ToAddress = userEntity.EmailId;
            emailSenderModel.Body = strBody.ToString();
            emailSenderModel.isHtml = true;
            emailSenderModel.Subject = GlobalConstants.ServiceProviderCredential;
            emailSenderModel.sentStatus = true;
            if (!string.IsNullOrEmpty(emailSenderModel.ToAddress))
            {
                _emailSender.Execute(emailSenderModel.ToAddress, emailSenderModel.Subject, emailSenderModel.Body);
            }
            message = GlobalConstants.RecordUpdateMessage;

            return message;
        }

        public string EditServiceUser(ServiceProviderUserModel serviceusermodel, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var userEntity = _startupContext.UserMasters.Where(x => x.UserId == serviceusermodel.UserId && x.IsActive == true).FirstOrDefault();

            var userEntity1 = _startupContext.ServiceDetails.Where(x => x.UserId == serviceusermodel.UserId).FirstOrDefault();

            userEntity.FirstName = serviceusermodel.FirstName;
            userEntity.LastName = serviceusermodel.LastName;
            userEntity.EmailId = serviceusermodel.EmailId.ToLower();
            userEntity.FounderTypeId = serviceusermodel.FounderTypeId;
            userEntity.RoleId = serviceusermodel.RoleId;
            userEntity.UpdatedDate = DateTime.Now;
            userEntity.UpdatedBy = serviceusermodel.UpadateBy;
            userEntity1.Category = serviceusermodel.Category;

            _startupContext.SaveChanges();
            message = GlobalConstants.RecordUpdateMessage;
            /// User Audit Log
            var userAuditLog = new UserAuditLogModel();
            userAuditLog.Action = "User Edit";
            userAuditLog.Description = "User details updated.";
            userAuditLog.UserId = serviceusermodel.UserId;
            userAuditLog.CreatedBy = serviceusermodel.UserId;
            _userAuditLogService.AddUserAuditLog(userAuditLog);
            return message;
        }
    }
}





