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
    public class SectorService : ISectorService
    {
        StartUpDBContext _startupContext;
        private readonly IUserAuditLogService _userAuditLogService;
        public SectorService(StartUpDBContext startUpDBContext, IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            _userAuditLogService = userAuditLogService;
        }

        /// <summary>
        ///  Add sector Data
        /// </summary>
        /// <param name="sector"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddSectors(SectorModel sector, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingRecord = _startupContext.SectorDetails.Any(x => (x.SectorName == sector.SectorName || x.SubSectorName == sector.SubSectorName) && x.IsActive == true);
            if (!existingRecord)
            {
                var sectorEntity = new SectorDetail();
                sectorEntity.SectorId = sector.SectorId;
                sectorEntity.SectorName = sector.SectorName;
                sectorEntity.SubSectorName = sector.SubSectorName;
                sectorEntity.SectorDescription = sector.SectorDescription;
                sectorEntity.CreatedDate = DateTime.Now;
                sectorEntity.CreatedBy = sector.LoggedUserId;
                sectorEntity.IsActive = true;
                _startupContext.SectorDetails.Add(sectorEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordSaveMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add Sector";
                userAuditLog.Description = "Sector Details Added";
                userAuditLog.UserId = sector.LoggedUserId;
                userAuditLog.CreatedBy = sector.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.ExistingRecordMessage;
            }
            return message;

        }


        /// <summary>
        /// Delete sector Data
        /// </summary>
        /// <param name="sectorId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteSector(int sectorId, int userId, ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var sectorEntity = _startupContext.SectorDetails.Where(x => x.SectorId == sectorId && x.IsActive == true).FirstOrDefault();
            if (sectorEntity == null)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                sectorEntity.IsActive = false;
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordDeleteMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Delete Sector";
                userAuditLog.Description = "Sector Details Deleted";
                userAuditLog.UserId = userId;
                userAuditLog.CreatedBy = userId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            return message;
        }

        /// <summary>
        ///  Edit secotr Data
        /// </summary>
        /// <param name="sector"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string EditSector(SectorModel sector, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var sectorEntity = _startupContext.SectorDetails.Where(x => x.SectorId == sector.SectorId && x.IsActive == true).FirstOrDefault();

            if (sectorEntity != null)
            {
                sectorEntity.SectorId = sector.SectorId;
                sectorEntity.SectorName = sector.SectorName;
                sectorEntity.SubSectorName = sector.SubSectorName;
                sectorEntity.SectorDescription = sector.SectorDescription;
                sectorEntity.UpdatedDate = DateTime.Now;
                sectorEntity.UpdatedBy = sector.LoggedUserId;
                sectorEntity.IsActive = true;
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordUpdateMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Edit Sector";
                userAuditLog.Description = "Sector Details Updated";
                userAuditLog.UserId = sector.LoggedUserId;
                userAuditLog.CreatedBy = sector.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.NotFoundMessage;
            }
            return message;
        }

        /// <summary>
        /// Getall Sector Data
        /// </summary>
        /// <returns></returns>
        public List<SectorModel> GetAllSectors()
        {
            var sectorEntity = _startupContext.SectorDetails.Where(x => x.IsActive == true).ToList();
            var sectorList = sectorEntity.Select(x => new SectorModel
            {
                SectorId = x.SectorId,
                SectorName = x.SectorName,
                SubSectorName = x.SubSectorName,
                SectorDescription = x.SectorDescription

            }).ToList();
            return sectorList;
        }


        /// <summary>
        /// Get sector Data
        /// </summary>
        /// <param name="sectorId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public SectorModel GetSectorById(long sectorId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var sectorList = new SectorModel();
            var sectorEntity = _startupContext.SectorDetails.FirstOrDefault(x => x.SectorId == sectorId && x.IsActive == true);
            if (sectorEntity != null)
            {
                sectorList.SectorId = sectorEntity.SectorId;
                sectorList.SectorName = sectorEntity.SectorName;
                sectorList.SubSectorName = sectorEntity.SubSectorName;
                sectorList.SectorDescription = sectorEntity.SectorDescription;
                sectorList.IsActive = sectorEntity.IsActive;
            }
            return sectorList;
        }
    }
}
