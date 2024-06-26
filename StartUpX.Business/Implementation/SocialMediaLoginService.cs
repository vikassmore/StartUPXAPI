using Microsoft.EntityFrameworkCore;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class SocialMediaLoginService : ISocialMediaLogin
    {
        StartUpDBContext _startupContext;
        private readonly IUserAuditLogService _userAuditLogService;
        public SocialMediaLoginService(StartUpDBContext startUpDBContext, IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            _userAuditLogService = userAuditLogService;
        }
        public AuthModel AddSocialMediaUser(SocialMediaUserLoginModel socialmediaLogin, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingRecord = _startupContext.UserMasters.Any(x => x.EmailId == socialmediaLogin.EmailId && x.IsActive == true);
            if (!existingRecord)
            {
                var userEntity = new UserMaster();
                userEntity.RoleId = socialmediaLogin.RoleId;
                userEntity.FirstName = socialmediaLogin.FirstName;
                userEntity.LastName = socialmediaLogin.LastName;
                userEntity.EmailId = socialmediaLogin.EmailId;
                userEntity.FounderTypeId = socialmediaLogin.FounderTypeId;
                userEntity.IsActive = true;
                _startupContext.UserMasters.Add(userEntity);
                _startupContext.SaveChanges();

                var socialmediaEntity = new SocialMediaLogin();
                socialmediaEntity.UserId = userEntity.UserId;
                socialmediaEntity.Provider = socialmediaLogin.Provider;
                socialmediaEntity.ProviderId = socialmediaLogin.ProviderId;
                socialmediaEntity.AccessToken = socialmediaLogin.AccessToken;
                socialmediaEntity.CreatedBy = userEntity.UserId;
                socialmediaEntity.CreatedDate = DateTime.Now;
                socialmediaEntity.IsActive = true;
                _startupContext.SocialMediaLogins.Add(socialmediaEntity);
                _startupContext.SaveChanges();
                var userExist = _startupContext.UserMasters.Include(x => x.Role).Include(x => x.FounderType).FirstOrDefault(x => x.EmailId == socialmediaLogin.EmailId && x.IsActive == true);

                return new AuthModel
                {
                    UserId = userExist.UserId,
                    UserName = userExist.FirstName + ' ' + userExist.LastName,
                    Email = userExist.EmailId,
                    Password = userExist.Password,
                    RoleId = (int)userExist.RoleId,
                    FounderName = userExist.FounderType.FounderName,
                    RoleName = userEntity.Role.RoleName,
                    Token = socialmediaLogin.AccessToken,
                    StatusCode = HttpStatusCode.OK,
                    Message = GlobalConstants.UserLoginMessage
                };

                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = " Add SocialMedia User";
                userAuditLog.Description = "Social Media User Added";
                userAuditLog.UserId = socialmediaLogin.UserId;
                userAuditLog.CreatedBy = socialmediaLogin.UserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                var userEntity = _startupContext.UserMasters.FirstOrDefault(x => x.EmailId == socialmediaLogin.EmailId && x.IsActive == true);
               if(userEntity != null)
                {
                    var socialEntity = _startupContext.SocialMediaLogins.FirstOrDefault(x => x.UserId == userEntity.UserId);
                    if (socialEntity != null)
                    {
                        socialEntity.UserId = userEntity.UserId;
                        socialEntity.AccessToken = socialmediaLogin.AccessToken;
                        socialEntity.UpdatedBy = userEntity.UserId;
                        socialEntity.UpdatedDate = DateTime.Now;
                        _startupContext.SocialMediaLogins.Update(socialEntity);
                        _startupContext.SaveChanges();
                    }
                }     
                var userExist = _startupContext.UserMasters.Include(x => x.Role).Include(x => x.FounderType).FirstOrDefault(x => x.EmailId == socialmediaLogin.EmailId && x.IsActive == true);

                return new AuthModel
                {
                    UserId = userExist.UserId,
                    UserName = userExist.FirstName + ' ' + userExist.LastName,
                    Email = userExist.EmailId,
                    Password = userExist.Password,
                    RoleId = (int)userExist.RoleId,
                    FounderName = userExist.FounderType.FounderName,
                    RoleName = userExist.Role.RoleName,
                    Token = socialmediaLogin.AccessToken,
                    StatusCode = HttpStatusCode.OK,
                    Message = GlobalConstants.UserLoginMessage
                };
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = " Edit SocialMedia User";
                userAuditLog.Description = "Social Media User Updated";
                userAuditLog.UserId = socialmediaLogin.UserId;
                userAuditLog.CreatedBy = socialmediaLogin.UserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }


        }

        public SocialMediaUserLoginModel GetSocialMedialUserByEmail(string emailId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var socialmodel = new SocialMediaUserLoginModel();
            var socialEntity = _startupContext.UserMasters.FirstOrDefault(x => x.EmailId == emailId && x.IsActive == true);

            if (socialEntity != null)
            {
                var social = _startupContext.SocialMediaLogins.FirstOrDefault(x => x.UserId == socialEntity.UserId && x.IsActive == true);

                socialmodel.UserId = socialEntity.UserId;
                socialmodel.FirstName = socialEntity.FirstName;
                socialmodel.LastName = socialEntity.LastName;
                socialmodel.RoleId = socialEntity.RoleId;
                socialmodel.FounderTypeId = socialEntity.FounderTypeId;
                socialmodel.AccessToken = social.AccessToken;
                socialmodel.Provider = social.Provider;
                socialmodel.ProviderId = social.ProviderId;
                socialmodel.SocialId = social.SocialId;



            }
            return socialmodel;
        }
    }
}
