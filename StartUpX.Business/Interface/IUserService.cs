using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IUserService
    {
        UserModel GetUserById(long userId, ref ErrorResponseModel errorResponseModel);
        List<UserModel> GetAllUser();

        /// <summary>
        /// Method used to create user
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        UserRegister AddUser(UserModel model, SMTPSettingsModel smtpSettingsModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method used to create user
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string EditUser(UserModel model, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string ForgotPasswordOTP(string email, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string ForgotPassword(ForgotPasswordModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteUser(int userId, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Method is used to update user
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string ActivateUser(UserActivateModel model, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method used to change user password
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        UserRegister ChangePassword(ChangePassword model, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Add Service Provider User
        /// </summary>
        /// <param name="usermodel"></param>
        /// <param name="smtpSettingsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        UserRegister AddServiceProviderUser(ServiceProviderUserModel usermodel, SMTPSettingsModel smtpSettingsModel, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Get Service provider User By Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        UserModel GetServiceProviderUserById(int userId, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Get All Service Provider user
        /// </summary>
        /// <returns></returns>
        List<UserModel> GetAllServiceProviderUser();
        List<RoleModel>GetAllRole();

        string EditServiceStatus(ServiceStatusUpdateModel model, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// service Status update
        /// </summary>
        /// <param name="email"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        /// 
        string EditServiceUser(ServiceProviderUserModel serviceusermodel, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// edit service user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
    }
}
