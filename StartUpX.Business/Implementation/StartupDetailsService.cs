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
    public class StartupDetailsService : IStartupDetailsService
    {
        StartUpDBContext _startupContext;
        private readonly IUserAuditLogService _userAuditLogService;
        public StartupDetailsService(StartUpDBContext startUpDBContext, IUserAuditLogService userAuditLogService)
        {
            _startupContext = startUpDBContext;
            _userAuditLogService = userAuditLogService;
        }

        /// <summary>
        /// Add Startup Detail data 
        /// </summary>
        /// <param name="startup"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddStartup(StartupDeatailModel startup, byte[] Logo, string fileName, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingRecord = _startupContext.StartUpDetails.Any(x => x.StartupId == startup.StartupId && x.UserId == startup.LoggedUserId && x.IsActive == true);
            if (!existingRecord)
            {
                var startupEntity = new StartUpDetail();
                startupEntity.StartUpName = startup.StartUpName;
                startupEntity.Address = startup.Address;
                startupEntity.CountryId = startup.CountryId;
                startupEntity.StateId = startup.StateId;
                startupEntity.CityId = startup.CityId;
                startupEntity.FoundingYear = startup.FoundingYear;
                startupEntity.CompanyDescription = startup.CompanyDescription;
                startupEntity.WebsiteUrl = startup.WebsiteUrl;
                if(fileName != null && fileName != "")
                {
                    startupEntity.LogoFileName = fileName;
                    startupEntity.Logo = Logo;
                }
                startupEntity.IsActive = true;
                startupEntity.EmployeeCount =startup.EmployeeCount;
                startupEntity.SectorId = startup.SectorId;
                startupEntity.CompanyEmailId  = startup.CompanyEmailId;
                startupEntity.CompanyLegalName = startup.CompanyLegalName;
                startupEntity.CompanyHeadquartersAddress = startup.CompanyHeadquartersAddress;
                startupEntity.CompanyContact = startup.CompanyContact;
                startupEntity.ServiceDescription = startup.ServiceDescription;
                startupEntity.BusinessModel = startup.BusinessModel;
                startupEntity.TargetCustomerBase = startup.TargetCustomerBase;
                startupEntity.TargetMarket = startup.TargetMarket;
                startupEntity.ManagementInfo = startup.ManagementInfo;
                startupEntity.IsStealth = startup.IsStealth;
                startupEntity.CreatedDate = DateTime.Now;
                startupEntity.CreatedBy = startup.LoggedUserId;
                startupEntity.UserId = startup.LoggedUserId;
                _startupContext.StartUpDetails.Add(startupEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordSaveMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add Founder Company";
                userAuditLog.Description = "Founder Company Details Added";
                userAuditLog.UserId = startup.LoggedUserId;
                userAuditLog.CreatedBy = startup.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                var startupEntity = _startupContext.StartUpDetails.Where(x => x.StartupId == startup.StartupId && x.UserId == startup.LoggedUserId && x.IsActive == true).FirstOrDefault();
                if (startupEntity != null)
                {
                    startupEntity.StartupId = startup.StartupId;
                    startupEntity.StartUpName = startup.StartUpName;
                    startupEntity.Address = startup.Address;
                    startupEntity.CountryId = startup.CountryId;
                    startupEntity.StateId = startup.StateId;
                    startupEntity.CityId = startup.CityId;
                    startupEntity.FoundingYear = startup.FoundingYear;
                    startupEntity.CompanyDescription = startup.CompanyDescription;
                    startupEntity.WebsiteUrl = startup.WebsiteUrl;
                    if (fileName != null && fileName != "")
                    {
                        startupEntity.LogoFileName = fileName;
                        startupEntity.Logo = Logo;
                    }
                    startupEntity.IsActive = true;
                    startupEntity.EmployeeCount = startup.EmployeeCount;
                    startupEntity.SectorId = startup.SectorId;
                    startupEntity.CompanyEmailId = startup.CompanyEmailId;
                    startupEntity.CompanyLegalName = startup.CompanyLegalName;
                    startupEntity.CompanyHeadquartersAddress = startup.CompanyHeadquartersAddress;
                    startupEntity.CompanyContact = startup.CompanyContact;
                    startupEntity.ServiceDescription = startup.ServiceDescription;
                    startupEntity.BusinessModel = startup.BusinessModel;
                    startupEntity.TargetCustomerBase = startup.TargetCustomerBase;
                    startupEntity.TargetMarket = startup.TargetMarket;
                    startupEntity.ManagementInfo = startup.ManagementInfo;
                    startupEntity.IsStealth = startup.IsStealth;
                    startupEntity.UpdatedDate = DateTime.Now;
                    startupEntity.UpdatedBy = startup.LoggedUserId;
                    _startupContext.StartUpDetails.Update(startupEntity);
                    _startupContext.SaveChanges();
                    message = GlobalConstants.RecordUpdateMessage;
                    /// User Audit Log
                    var userAuditLog = new UserAuditLogModel();
                    userAuditLog.Action = "Edit Founder Company";
                    userAuditLog.Description = "Founder Company Details Updated";
                    userAuditLog.UserId = startup.LoggedUserId;
                    userAuditLog.CreatedBy = startup.LoggedUserId;
                    _userAuditLogService.AddUserAuditLog(userAuditLog);
                }
            }

            return message;
        }

        public string DeleteStartup(int startupId, ErrorResponseModel errorResponseModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all Startup Detail Data
        /// </summary>
        /// <returns></returns>
        public List<StartupDeatailModelList> GetAllStartup()
        {
            var startupEntity = _startupContext.StartUpDetails.Include(x=>x.Sector).Where(x => x.IsActive == true).ToList();
            var startupList = startupEntity.Select(x => new StartupDeatailModelList
            {
                StartupId = x.StartupId,
                StartUpName = x.StartUpName,
                Address = x.Address,
                CountryId = x.CountryId,
                StateId = x.StateId,
                CityId = x.CityId,
                FoundingYear = x.FoundingYear,
                CompanyDescription = x.CompanyDescription,
                WebsiteUrl = x.WebsiteUrl,
                LogoFileName = x.LogoFileName,
                Logo = x.Logo,
                IsActive = x.IsActive,
                EmployeeCount = x.EmployeeCount,
                SectorId = x.SectorId,
                SectorName = x.Sector.SectorName,
                CompanyEmailId = x.CompanyEmailId,
                CompanyLegalName = x.CompanyLegalName,
                CompanyHeadquartersAddress = x.CompanyHeadquartersAddress,
                CompanyContact = x.CompanyContact,
                ServiceDescription = x.ServiceDescription,
                BusinessModel = x.BusinessModel,
                TargetCustomerBase = x.TargetCustomerBase,
                TargetMarket = x.TargetMarket,
                ManagementInfo = x.ManagementInfo,
                IsStealth = x.IsStealth,
            }).ToList();
            return startupList;

        }


        /// <summary>
        /// GetStartupById Satartup detail
        /// </summary>
        /// <param name="startupId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public StartupDeatailModelList GetStartupById(long startupId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var startupmodelList = new StartupDeatailModelList();
            var startupEntity = _startupContext.StartUpDetails.FirstOrDefault(x => x.StartupId == startupId && x.IsActive == true);
            if (startupEntity != null)
            {
                startupmodelList.StartupId = startupEntity.StartupId;
                startupmodelList.StartUpName = startupEntity.StartUpName;
                startupmodelList.Address = startupEntity.Address;
                startupmodelList.CountryId = startupEntity.CountryId;
                startupmodelList.Address = startupEntity.Address;
                startupmodelList.StateId = startupEntity.StateId;
                startupmodelList.CityId = startupEntity.CityId;
                startupmodelList.FoundingYear = startupEntity.FoundingYear;
                startupmodelList.CompanyDescription = startupEntity.CompanyDescription;
                startupmodelList.WebsiteUrl = startupEntity.WebsiteUrl;
                startupmodelList.LogoFileName = startupEntity.LogoFileName;
                startupmodelList.Logo = startupEntity.Logo;
                startupmodelList.EmployeeCount = startupEntity.EmployeeCount;
                startupmodelList.SectorId = startupEntity.SectorId;
                startupmodelList.CompanyEmailId = startupEntity.CompanyEmailId;
                startupmodelList.CompanyLegalName = startupEntity.CompanyLegalName;
                startupmodelList.CompanyHeadquartersAddress = startupEntity.CompanyHeadquartersAddress;
                startupmodelList.CompanyContact = startupEntity.CompanyContact;
                startupmodelList.ServiceDescription = startupEntity.ServiceDescription;
                startupmodelList.BusinessModel = startupEntity.BusinessModel;
                startupmodelList.TargetCustomerBase = startupEntity.TargetCustomerBase;
                startupmodelList.TargetMarket = startupEntity.TargetMarket;
                startupmodelList.ManagementInfo = startupEntity.ManagementInfo;
                startupmodelList.IsStealth = startupEntity.IsStealth;
                startupmodelList.IsActive = startupEntity.IsActive;
            }
            return startupmodelList;
        }

        /// <summary>
        /// Get Startup Details By User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public StartupDeatailModelList GetStartupByuserId(long userId, ref ErrorResponseModel errorResponseModel)
        {
            var startupEntity = _startupContext.StartUpDetails.Include(x => x.Sector).FirstOrDefault(x => x.UserId == userId && x.IsActive == true);
            var startupDetailModel = new StartupDeatailModelList();
            if (startupEntity != null)
            {
                startupDetailModel.StartupId = startupEntity.StartupId;
                startupDetailModel.StartUpName = startupEntity.StartUpName;
                startupDetailModel.Address = startupEntity.Address;
                startupDetailModel.CountryId = startupEntity.CountryId;
                startupDetailModel.StateId = startupEntity.StateId;
                startupDetailModel.CityId = startupEntity.CityId;
                startupDetailModel.FoundingYear = startupEntity.FoundingYear;
                startupDetailModel.CompanyDescription = startupEntity.CompanyDescription;
                startupDetailModel.WebsiteUrl = startupEntity.WebsiteUrl;
                startupDetailModel.LogoFileName = startupEntity.LogoFileName;
                startupDetailModel.Logo = startupEntity.Logo;
                startupDetailModel.IsActive = startupEntity.IsActive;
                startupDetailModel.EmployeeCount = startupEntity.EmployeeCount;
                startupDetailModel.SectorId = startupEntity.SectorId;
                startupDetailModel.SectorName = startupEntity.Sector.SectorName;
                startupDetailModel.CompanyEmailId = startupEntity.CompanyEmailId;
                startupDetailModel.CompanyLegalName = startupEntity.CompanyLegalName;
                startupDetailModel.CompanyHeadquartersAddress = startupEntity.CompanyHeadquartersAddress;
                startupDetailModel.CompanyContact = startupEntity.CompanyContact;
                startupDetailModel.ServiceDescription = startupEntity.ServiceDescription;
                startupDetailModel.BusinessModel = startupEntity.BusinessModel;
                startupDetailModel.TargetCustomerBase = startupEntity.TargetCustomerBase;
                startupDetailModel.TargetMarket = startupEntity.TargetMarket;
                startupDetailModel.ManagementInfo = startupEntity.ManagementInfo;
                startupDetailModel.IsStealth = startupEntity.IsStealth;
            }
            return startupDetailModel;

        }
    }
}

