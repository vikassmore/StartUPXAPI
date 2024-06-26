using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IUserAuditLogService
    {
        /// <summary>
        /// User Audit log add
        /// </summary>
        /// <param name="auditLog"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        void AddUserAuditLog(UserAuditLogModel auditLog);
    }
}
