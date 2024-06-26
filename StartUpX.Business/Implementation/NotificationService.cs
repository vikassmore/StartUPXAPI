using Microsoft.EntityFrameworkCore;
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
    public class NotificationService : INotificationService
    {
        StartUpDBContext _startupContext;

        public NotificationService(StartUpDBContext startUpDBContext)
        {
            _startupContext = startUpDBContext;

        }
        /// <summary>
        /// Notification add
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public void AddNotification(NotificationModel notification)
        {
            try
            {
                var notificationEntity = new NotificationDetail();
                notificationEntity.Message = notification.Message;
                notificationEntity.UserId = notification.UserId;
                notificationEntity.CreatedBy = notification.LoggedUserId;
                notificationEntity.CreatedDate = DateTime.Now;
                notificationEntity.IsActive = true;
                _startupContext.NotificationDetails.Add(notificationEntity);
                _startupContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Get Notification By user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<NotificationModel> GetNotificationByUserId(int userId, ref ErrorResponseModel errorResponseModel)
        {
            var notificationEntity = _startupContext.NotificationDetails.Where(x => x.UserId == userId && x.IsActive == true).OrderByDescending(x => x.NotificationId).ToList();
            var notificationList = notificationEntity.Select(x => new NotificationModel
            {
                NotificationId = x.NotificationId,
                Message = x.Message,
                UserId = x.UserId,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,

            }).ToList();
            return notificationList;
        }
        /// <summary>
        /// Delete notification
        /// </summary>
        /// <param name="notificationId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteNotification(int notificationId, ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var notificationEntity = _startupContext.NotificationDetails.Where(x => x.NotificationId == notificationId && x.IsActive == true).FirstOrDefault();
            if (notificationEntity == null)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            notificationEntity.IsActive = false;
            _startupContext.SaveChanges();
            message = GlobalConstants.RecordDeleteMessage;
            return message;
        }
    }
}
