using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface INotificationService
    {
        /// <summary>
        /// Notification add
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        void AddNotification(NotificationModel notification);

        /// <summary>
        /// Get Notification By user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<NotificationModel> GetNotificationByUserId(int userId, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Delete notification
        /// </summary>
        /// <param name="notificationId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteNotification(int notificationId, ErrorResponseModel errorResponseModel);
    }
}
