using Microsoft.Extensions.Configuration;
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
    public class CreateOTPandSendMessageService : ICreateOTPandSendMessage
    {
        StartUpDBContext _startupContext;
        //EmailService emailSenderService = new EmailService();
        IEmailSender _emailSender;
     // IAuthService _authService;
        // private ConfigurationModel _url;
        //IConfiguration _configuration;
        public CreateOTPandSendMessageService(StartUpDBContext startupContext,IEmailSender emailSender)
        {

            _startupContext = startupContext;
            _emailSender = emailSender;
           // _authService = authService;
            //_createOTPandSendMessage = createOTPandSendMessage;
        }

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
                    existingRecord.OtpvalidDateTime = DateTime.UtcNow;
                    _startupContext.SaveChanges();
                }
                else
                {
                    otpEntity.Otp = otp;
                    otpEntity.OtpvalidDateTime = DateTime.UtcNow;
                    otpEntity.UserId = userId;
                    otpEntity.IsActive = true;
                    _startupContext.Otpautherizations.Add(otpEntity);
                    _startupContext.SaveChanges();
                }
                message = "record added.";
                return otp;
            }
        }

        public string CreateOTPandSendMessage(string email, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            //var DecryptUserId = EncryptionHelper.Decrypt(model.UserId));       
            var userEntity = _startupContext.UserMasters.FirstOrDefault(x => x.EmailId == email && x.IsActive == true);
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
                    var otp =CreateNewOTP((int)userEntity.UserId);
                    var emailModel = new EmailModel();
                    emailModel.ToAddress = email;
                    emailModel.isHtml = true;
                    emailModel.Subject = GlobalConstants.ForgotPasswordMessage;
                    emailModel.Body = otp;
                    _emailSender.Execute(userEntity.EmailId, emailModel.Subject, emailModel.Body); message = GlobalConstants.ForgotPasswordMessage;
                }
                catch (Exception ex)
                { }
            }
            return message;
        }
    }
}
