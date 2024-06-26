using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IAuthService
    {
        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="roleId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        AuthModel EmailLogin(string email, string password, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Create OTP
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string CreateNewOTP(int userId);
        /// <summary>
        /// OTP verify
        /// </summary>
        /// <param name="otp"></param>
        /// <param name="email"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
       OTPModel OTPVerify(string otp, string email, ref ErrorResponseModel errorResponseModel);
        //AuthModel GetByUserId(int userId, ref ErrorResponseModel errorResponseModel);
        // AuthModel AuthenticateUser(string MobileNo, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Login By EmailId
        /// </summary>
        /// <param name="email"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        AuthModel LoginByEmail(string email, ref ErrorResponseModel errorResponseModel);

    }
}
