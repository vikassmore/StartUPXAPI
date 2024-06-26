using StartUpX.Business.Interface;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using StartUpX.Common;
using StartUpX.Business.Implemetation;

namespace StartUpX.Business.Implementation
{
    public class AuthService : IAuthService
    {
        StartUpDBContext _startupContext;
        private readonly IEmailSender _emailSender;
        private readonly ICreateOTPandSendMessage _createOTPandSendMessage;
        private readonly IUserAuditLogService _userAuditLogService;
        public AuthService(StartUpDBContext startupContext, IEmailSender emailSender, ICreateOTPandSendMessage createOTPandSendMessage, IUserAuditLogService userAuditLogService)
        {

            _emailSender = emailSender;
            _startupContext = startupContext;
            _createOTPandSendMessage = createOTPandSendMessage;
            _userAuditLogService = userAuditLogService;
        }

        //public AuthModel GetByUserId(int userId, ref ErrorResponseModel errorResponseModel)
        //{
        //    errorResponseModel = new ErrorResponseModel();
        //    var userExist = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == userId && !x.DeleteStatus);
        //    if (userExist == null)
        //    {
        //        errorResponseModel.StatusCode = HttpStatusCode.NotFound;
        //        errorResponseModel.Message = GlobalConstants.UserNotFoundMessage;
        //        return null;
        //    }
        //    var userEntity = (from user in _startupContext.UserMasters
        //                      join role in _startupContext.RoleMasters
        //                      on user.RoleId equals role.RoleId
        //                      where user.UserId == userExist.UserId
        //                      select new
        //                      {

        //                          user.EmailId,
        //                          user.Password


        //                      }).FirstOrDefault();

        //    if (userEntity == null)
        //    {
        //        errorResponseModel.StatusCode = HttpStatusCode.NotFound;
        //        errorResponseModel.Message = GlobalConstants.UserNotFoundMessage;
        //        return null;
        //    }
        //    return new AuthModel
        //    {

        //        Email = userEntity.EmailId,
        //        Password=userEntity.Password

        //    };
        //}

        /// <summary>
        /// Created OTP
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string CreateNewOTP(int userId)
        {
            string message = "";
            Random generator = new Random();
            string otp = generator.Next(0, 999999).ToString("D6");

            var otpEntity = new Otpautherization();
            if (userId == 0)
            {
                message = "error";
                return message;
            }
            else
            {
                var existingRecord = _startupContext.Otpautherizations.Where(m => m.UserId == userId && m.IsActive == true).FirstOrDefault();
                if (existingRecord != null)
                {
                    existingRecord.Otp = otp;
                    existingRecord.IsActive = true;
                    existingRecord.OtpvalidDateTime = DateTime.UtcNow.AddMinutes(15);
                    _startupContext.SaveChanges();
                }
                else
                {
                    otpEntity.Otp = otp;
                    otpEntity.OtpvalidDateTime = DateTime.UtcNow.AddMinutes(15);
                    otpEntity.UserId = userId;
                    otpEntity.IsActive = true;
                    _startupContext.Otpautherizations.Add(otpEntity);
                    _startupContext.SaveChanges();
                }
                message = "record added.";
                return otp;
            }

        }

        /// <summary>
        /// Email Login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public AuthModel EmailLogin(string email, string password, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var abc = EncryptionHelper.Encrypt(password);
            var userExist = _startupContext.UserMasters.FirstOrDefault(x => x.EmailId == email && x.Password == EncryptionHelper.Encrypt(password) && x.IsActive == true);
            if (userExist == null)
            {
                var userActivated = _startupContext.UserMasters.FirstOrDefault(x => x.EmailId == email.ToLower() && x.Password == EncryptionHelper.Encrypt(password));
                if (userActivated == null)
                {
                    var authModel = new AuthModel();
                    authModel.StatusCode = HttpStatusCode.Unauthorized;
                    authModel.Message = GlobalConstants.UserNotActivatedMessage;
                    return authModel;
                }
                else
                {
                    var authModel = new AuthModel();
                    authModel.StatusCode = HttpStatusCode.NotFound;
                    authModel.Message = GlobalConstants.UserNotFoundMessage;
                    return authModel;
                }

            }
            var userEntity = (from user in _startupContext.UserMasters
                              join role in _startupContext.Roles
                              on user.RoleId equals role.RoleId
                              join founder in _startupContext.FounderTypes
                             on user.FounderTypeId equals founder.FounderTypeId

                              where user.RoleId == userExist.RoleId && user.EmailId == email
                              select new
                              {
                                  user.UserId,
                                  user.EmailId,
                                  user.Password,
                                  role.RoleId,
                                  role.RoleName,
                                  founder.FounderName
                              }).FirstOrDefault();
            _createOTPandSendMessage.CreateOTPandSendMessage(userExist.EmailId, ref errorResponseModel);
            /// User Audit Log
            var userAuditLog = new UserAuditLogModel();
            userAuditLog.Action = "User Login";
            userAuditLog.Description = "User login";
            userAuditLog.UserId = userExist.UserId;
            userAuditLog.CreatedBy = userExist.UserId;
            _userAuditLogService.AddUserAuditLog(userAuditLog);
            return new AuthModel
            {
                UserId = userExist.UserId,
                UserName = userExist.FirstName + ' ' + userExist.LastName,
                Email = userExist.EmailId,
                Password = userExist.Password,
                RoleId = (int)userExist.RoleId,
                FounderName = userEntity.FounderName,
                RoleName = userEntity.RoleName,
                StatusCode = HttpStatusCode.OK,
                Message = GlobalConstants.UserLoginMessage
            };
        }


        /// <summary>
        /// OTP verify
        /// </summary>
        /// <param name="otp"></param>
        /// <param name="email"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public OTPModel OTPVerify(string otp, string email, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            OTPModel otpModel = new OTPModel();
            var User = _startupContext.UserMasters.Where(m => m.EmailId == email.ToLower() && m.IsActive == true).FirstOrDefault();
            if (User == null)
            {
                return null;
            }
            var otpEntity = _startupContext.Otpautherizations.FirstOrDefault(x => x.Otp == otp && x.UserId == User.UserId && x.IsActive == true);
            if (otpEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                return null;
            }
            //TimeSpan ts = otpEntity.OtpvalidDateTime.Value - DateTime.UtcNow;
            var isValidOtp = DateTime.UtcNow.Subtract(otpEntity.OtpvalidDateTime.Value).TotalMinutes <= 5;
            if (isValidOtp)
            {
                otpModel.UserId = User.UserId;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Otp verify";
                userAuditLog.Description = "Otp verification";
                userAuditLog.UserId = User.UserId;
                userAuditLog.CreatedBy = User.UserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
                return otpModel;
                //    errorResponseModel.Message = "Valid OTP"; 
                /// User Audit Log
      
            }
            else
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                return null;
                //errorResponseModel.Message = "OTP Expired";
            }
        }

        /// <summary>
        /// Login By Email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public AuthModel LoginByEmail(string email, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var userExist = _startupContext.UserMasters.FirstOrDefault(x => x.EmailId == email.ToLower() && x.IsActive == true);
            if (userExist == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.UserNotFoundMessage;
                return null;
            }

            var userEntity = (from user in _startupContext.UserMasters
                              join role in _startupContext.Roles
                              on user.RoleId equals role.RoleId
                              join foundertype in _startupContext.FounderTypes
                              on user.FounderTypeId equals foundertype.FounderTypeId

                              where user.RoleId == userExist.RoleId && user.EmailId == email && user.IsActive == true
                              select new
                              {
                                  //user.UserName,
                                  user.UserId,
                                  user.EmailId,
                                  user.Password,
                                  role.RoleId,
                                  role.RoleName,
                                  foundertype.FounderTypeId,
                                  foundertype.FounderName

                              }).FirstOrDefault();


            if (userEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = GlobalConstants.UserNotFoundMessage;
                return null;
            }

            // string otpCreate = CreateNewOTP(Convert.ToInt32(userExist.UserId));

            return new AuthModel
            {
                //UserName=userEntity.UserName,
                //UserId=userEntity.UserId,
                // Email = userEntity.EmailId,
                //Password=userEntity.Password
                UserId = userExist.UserId,
                //UserName = userExist.FirstName + ' ' + userExist.LastName,
                Email = userEntity.EmailId,
                Password = userExist.Password,
                RoleId = (int)userEntity.RoleId,
                RoleName = userEntity.RoleName,
                // FounderTypeId=userEntity.FounderTypeId,
                FounderName = userEntity.FounderName
            };
        }

    }
}
