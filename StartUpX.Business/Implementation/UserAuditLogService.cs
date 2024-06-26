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
    public class UserAuditLogService: IUserAuditLogService
    {
        StartUpDBContext _startupContext;

        public UserAuditLogService(StartUpDBContext startUpDBContext)
        {
            _startupContext = startUpDBContext;

        }

        /// <summary>
        /// User Audit log add
        /// </summary>
        /// <param name="auditLog"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public void AddUserAuditLog(UserAuditLogModel auditLog)
        {
            try
            {
                var userAuditLogEntity = new UserAuditLog();
                userAuditLogEntity.Action = auditLog.Action;
                userAuditLogEntity.Description = auditLog.Description;
                userAuditLogEntity.UserId = auditLog.UserId;
                userAuditLogEntity.CreatedBy= auditLog.UserId;
                userAuditLogEntity.CreatedDate = DateTime.Now;
                _startupContext.UserAuditLogs.Add(userAuditLogEntity);
                _startupContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }

        }
    }
}
