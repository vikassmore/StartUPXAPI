using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class ServiceService : IServiceService
    {
        StartUpDBContext _startupContext;
        private readonly IEmailSender _emailSender;
        private readonly IUserAuditLogService _userAuditLogService;
        private readonly INotificationService _notificationService;
        public ServiceService(StartUpDBContext startUpDBContext, IEmailSender emailSender, IUserAuditLogService userAuditLogService, INotificationService notificationService)
        {
            _startupContext = startUpDBContext;
            _emailSender = emailSender;
            _userAuditLogService = userAuditLogService;
            _notificationService = notificationService;
        }
        /// <summary>
        ///  Add Service Data
        /// </summary>
        /// <param name="service"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddService(ServiceModel service, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;


            var existingRecord = _startupContext.ServiceDetails.Any(x => x.UserId == service.UserId);
            if (!existingRecord)
            {
                var serviceEntity = new ServiceDetail();
                serviceEntity.ServiceId = service.ServiceId;
                serviceEntity.UserId = service.UserId;
                serviceEntity.ServiceProviderName = service.ServiceProviderName;
                serviceEntity.Category = service.Category;
                serviceEntity.ContactInformation = service.ContactInformation;
                serviceEntity.TagsKeywords = service.TagsKeywords;
                serviceEntity.CreatedDate = DateTime.Now;
                serviceEntity.CreatedBy = service.LoggedUserId;
                serviceEntity.IsActive = true;
                _startupContext.ServiceDetails.Add(serviceEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordSaveMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add Service Provider";
                userAuditLog.Description = "Service Provider Details Added";
                userAuditLog.UserId = (int)service.UserId;
                userAuditLog.CreatedBy = service.UserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                var serviceEntity = _startupContext.ServiceDetails.Where(x => x.ServiceId == service.ServiceId && x.UserId == service.UserId).FirstOrDefault();
                if (serviceEntity != null)
                {
                    serviceEntity.ServiceId = service.ServiceId;
                    serviceEntity.ServiceProviderName = service.ServiceProviderName;
                    serviceEntity.Category = service.Category;
                    serviceEntity.ContactInformation = service.ContactInformation;
                    serviceEntity.TagsKeywords = service.TagsKeywords;
                    serviceEntity.UpdatedDate = DateTime.Now;
                    serviceEntity.UpdatedBy = service.LoggedUserId;
                    serviceEntity.IsActive = true;
                    _startupContext.ServiceDetails.Update(serviceEntity);
                    _startupContext.SaveChanges();
                    message = GlobalConstants.RecordUpdateMessage;
                    /// User Audit Log
                    var userAuditLog = new UserAuditLogModel();
                    userAuditLog.Action = "Edit Service Provider";
                    userAuditLog.Description = "Service Provider Details Updated";
                    userAuditLog.UserId = (int)service.UserId; ;
                    userAuditLog.CreatedBy = service.UserId;
                    _userAuditLogService.AddUserAuditLog(userAuditLog);
                }
            }
            return message;
        }
        /// <summary>
        /// Delete Service Data
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteService(int serviceId, int userId, ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty; var serviceProvider = new ServiceDetail();

            var serviceEntity = _startupContext.ServiceDetails.FirstOrDefault(x => x.ServiceId == serviceId && x.IsActive == true);
            if (serviceEntity == null)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                serviceEntity.IsActive = false;
                serviceEntity.UpdatedDate = DateTime.Now;
                //serviceEntity.UpadateBy = service.UpadateBy;
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordDeleteMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Delete Service Provider";
                userAuditLog.Description = "Service Provider Details Deleted";
                userAuditLog.UserId = userId;
                userAuditLog.CreatedBy = userId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            return message;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string EditService(ServiceModel service, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var serviceEntity = _startupContext.ServiceDetails.Where(x => x.ServiceId == service.ServiceId && x.IsActive == true).FirstOrDefault();
            if (serviceEntity != null)
            {
                serviceEntity.ServiceId = service.ServiceId;
                serviceEntity.ServiceProviderName = service.ServiceProviderName;
                serviceEntity.Category = service.Category;
                serviceEntity.ContactInformation = service.ContactInformation;
                serviceEntity.TagsKeywords = service.TagsKeywords;
                serviceEntity.UpdatedDate = DateTime.Now;
                serviceEntity.UpdatedBy = service.LoggedUserId;
                serviceEntity.IsActive = true;
                _startupContext.ServiceDetails.Update(serviceEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.RecordUpdateMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Edit Service Provider";
                userAuditLog.Description = "Service Provider Details Updated";
                userAuditLog.UserId = service.LoggedUserId;
                userAuditLog.CreatedBy = service.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            return message;
        }
        /// <summary>
        /// Getall service Data for admin
        /// </summary>
        /// <returns></returns>
        public List<ServiceModelData> GetAllAdminService()
        {
            List<ServiceModelData> serviceModelList = new List<ServiceModelData>();
            var userEntityList = _startupContext.UserMasters.Where(x => x.IsActive == true).ToList();
            foreach (var item in userEntityList)
            {
                var serviceEntity = _startupContext.ServiceDetails.FirstOrDefault(x => x.UserId == item.UserId && x.IsActive == true);
                if (serviceEntity != null)
                {
                    var model = new ServiceModelData();
                    model.ServiceId = serviceEntity.ServiceId;
                    model.ServiceProviderName = serviceEntity.ServiceProviderName;
                    model.Category = serviceEntity.Category;
                    model.ContactInformation = serviceEntity.ContactInformation;
                    model.TagsKeywords = serviceEntity.TagsKeywords;
                    model.UserId = serviceEntity.UserId;
                    var serviceportfolioList = _startupContext.ServicePortfolios.Where(x => x.ServiceId == serviceEntity.ServiceId && x.IsActive == true).ToList();
                    var portfolioModelList = new List<ServicePortfolioModel>();
                    foreach (var servicePort in serviceportfolioList)
                    {
                        var portfolioModel = new ServicePortfolioModel();
                        portfolioModel.ServicePortFolioId = servicePort.ServicePortFolioId;
                        portfolioModel.DocumentName = servicePort.DocumentName;
                        portfolioModel.FileName = servicePort.FileName;
                        portfolioModel.FilePath = servicePort.FilePath;
                        portfolioModel.ServiceId = servicePort.ServiceId;
                        portfolioModelList.Add(portfolioModel);
                    }
                    model.ServicePortfolioModel = portfolioModelList;
                    serviceModelList.Add(model);
                }
            }
            return serviceModelList;
        }
        /// <summary>
        /// Getall service Data
        /// </summary>
        /// <returns></returns>
        public List<ServiceModelData> GetAllService()
        {
            List<ServiceModelData> serviceModelList = new List<ServiceModelData>();
            var userEntityList = _startupContext.UserMasters.Where(x => x.IsActive == true && x.ServiceStatus == true).ToList();
            foreach (var item in userEntityList)
            {
                var serviceEntity = _startupContext.ServiceDetails.FirstOrDefault(x => x.UserId == item.UserId && x.IsActive == true);
                if (serviceEntity != null)
                {
                    var model = new ServiceModelData();
                    model.ServiceId = serviceEntity.ServiceId;
                    model.ServiceProviderName = serviceEntity.ServiceProviderName;
                    model.Category = serviceEntity.Category;
                    model.ContactInformation = serviceEntity.ContactInformation;
                    model.TagsKeywords = serviceEntity.TagsKeywords;
                    model.UserId = serviceEntity.UserId;
                    var serviceportfolioList = _startupContext.ServicePortfolios.Where(x => x.ServiceId == serviceEntity.ServiceId && x.IsActive == true).ToList();
                    var portfolioModelList = new List<ServicePortfolioModel>();
                    foreach (var servicePort in serviceportfolioList)
                    {
                        var portfolioModel = new ServicePortfolioModel();
                        portfolioModel.ServicePortFolioId = servicePort.ServicePortFolioId;
                        portfolioModel.DocumentName = servicePort.DocumentName;
                        portfolioModel.FileName = servicePort.FileName;
                        portfolioModel.FilePath = servicePort.FilePath;
                        portfolioModel.ServiceId = servicePort.ServiceId;
                        portfolioModelList.Add(portfolioModel);
                    }
                    model.ServicePortfolioModel = portfolioModelList;
                    serviceModelList.Add(model);
                }
            }
            return serviceModelList;
        }
        /// <summary>
        /// GetServiceById service data
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public ServiceModelData GetServiceById(long serviceId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var model = new ServiceModelData();
            var serviceEntity = _startupContext.ServiceDetails.FirstOrDefault(x => x.ServiceId == serviceId && x.IsActive == true);
            if (serviceEntity != null)
            {
                model.ServiceId = serviceEntity.ServiceId;
                model.ServiceProviderName = serviceEntity.ServiceProviderName;
                model.Category = serviceEntity.Category;
                model.ContactInformation = serviceEntity.ContactInformation;
                model.TagsKeywords = serviceEntity.TagsKeywords;
                var serviceportfolioList = _startupContext.ServicePortfolios.Where(x => x.ServiceId == serviceEntity.ServiceId && x.IsActive == true).ToList();
                var portfolioModelList = new List<ServicePortfolioModel>();
                foreach (var servicePort in serviceportfolioList)
                {
                    var portfolioModel = new ServicePortfolioModel();
                    portfolioModel.ServicePortFolioId = servicePort.ServicePortFolioId;
                    portfolioModel.DocumentName = servicePort.DocumentName;
                    portfolioModel.FileName = servicePort.FileName;
                    portfolioModel.FilePath = servicePort.FilePath;
                    portfolioModel.ServiceId = servicePort.ServiceId;
                    portfolioModelList.Add(portfolioModel);
                }
                model.ServicePortfolioModel = portfolioModelList;

            }
            return model;
        }

        /// <summary>
        /// get service by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public ServiceModelData GetServiceByUserId(long userId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var model = new ServiceModelData();
            var serviceEntity = _startupContext.ServiceDetails.FirstOrDefault(x => x.UserId == userId);
            if (serviceEntity != null)
            {

                model.ServiceId = serviceEntity.ServiceId;
                model.ServiceProviderName = serviceEntity.ServiceProviderName;
                model.Category = serviceEntity.Category;
                model.ContactInformation = serviceEntity.ContactInformation;
                model.TagsKeywords = serviceEntity.TagsKeywords;
                var serviceportfolioList = _startupContext.ServicePortfolios.Where(x => x.ServiceId == serviceEntity.ServiceId && x.IsActive == true).ToList();
                var portfolioModelList = new List<ServicePortfolioModel>();
                foreach (var servicePort in serviceportfolioList)
                {
                    var portfolioModel = new ServicePortfolioModel();
                    portfolioModel.ServicePortFolioId = servicePort.ServicePortFolioId;
                    portfolioModel.DocumentName = servicePort.DocumentName;
                    portfolioModel.FileName = servicePort.FileName;
                    portfolioModel.FilePath = servicePort.FilePath;
                    portfolioModel.ServiceId = servicePort.ServiceId;
                    portfolioModelList.Add(portfolioModel);
                }
                model.ServicePortfolioModel = portfolioModelList;
            }
            return model;
        }
        /// <summary>
        /// Get all Case management details
        /// </summary>
        /// <returns></returns>
        public List<ServiceCaseModel> GetAllServiceCase()
        {
            var serviceCaseList = new List<ServiceCaseModel>();
            var serviceCaseEntity = _startupContext.ServiceLeadManagements.Include(x => x.Service).Where(x => x.IsActive == true).OrderByDescending(x => x.ServiceCaseId).ToList();
            foreach (var serviceCase in serviceCaseEntity)
            {
                var serviceCaseModel = new ServiceCaseModel();
                serviceCaseModel.ServiceCaseId = serviceCase.ServiceCaseId;
                serviceCaseModel.ServiceId = serviceCase.ServiceId;
                serviceCaseModel.ServiceInterestDate = serviceCase.ServiceInterestDate;
                serviceCaseModel.IntrestedServiceNames = serviceCase.IntrestedServiceNames;
                serviceCaseModel.AcceptedDate = serviceCase.AcceptedDate;
                serviceCaseModel.Status = serviceCase.Status;
                serviceCaseModel.Comment = serviceCase.Comment;
                serviceCaseModel.FounderUserId = serviceCase.FounderUserId;
                serviceCaseModel.IsActive = serviceCase.IsActive;
                serviceCaseModel.InvoiceCount = _startupContext.ServiceLeadInvoices.Where(x => x.ServiceCaseId == serviceCase.ServiceCaseId && x.IsActive == true).Count();
                var startupEntity = _startupContext.StartUpDetails.Include(x => x.Sector).FirstOrDefault(x => x.UserId == serviceCase.FounderUserId);
                if (startupEntity != null)
                {
                    var startupModel = new StartupDeatailModelList();
                    startupModel.StartupId = startupEntity.StartupId;
                    startupModel.StartUpName = startupEntity.StartUpName;
                    startupModel.Address = startupEntity.Address;
                    startupModel.CountryId = startupEntity.CountryId;
                    startupModel.Address = startupEntity.Address;
                    startupModel.StateId = startupEntity.StateId;
                    startupModel.CityId = startupEntity.CityId;
                    startupModel.FoundingYear = startupEntity.FoundingYear;
                    startupModel.CompanyDescription = startupEntity.CompanyDescription;
                    startupModel.WebsiteUrl = startupEntity.WebsiteUrl;
                    startupModel.LogoFileName = startupEntity.LogoFileName;
                    startupModel.Logo = startupEntity.Logo;
                    startupModel.EmployeeCount = startupEntity.EmployeeCount;
                    startupModel.SectorId = startupEntity.SectorId;
                    startupModel.SectorName = startupEntity.Sector.SectorName;
                    startupModel.CompanyLegalName = startupEntity.CompanyLegalName;
                    startupModel.CompanyHeadquartersAddress = startupEntity.CompanyHeadquartersAddress;
                    startupModel.CompanyContact = startupEntity.CompanyContact;
                    startupModel.CompanyEmailId = startupEntity.CompanyEmailId;
                    startupModel.ServiceDescription = startupEntity.ServiceDescription;
                    startupModel.BusinessModel = startupEntity.BusinessModel;
                    startupModel.TargetCustomerBase = startupEntity.TargetCustomerBase;
                    startupModel.TargetMarket = startupEntity.TargetMarket;
                    startupModel.ManagementInfo = startupEntity.ManagementInfo;
                    startupModel.IsStealth = startupEntity.IsStealth;
                    startupModel.IsActive = startupEntity.IsActive;
                    serviceCaseModel.StartupDeatailModel = startupModel;
                }
                var serviceModelEntity = _startupContext.ServiceDetails.FirstOrDefault(x => x.ServiceId == serviceCase.ServiceId);
                if (serviceModelEntity != null)
                {
                    var serviceModel = new ServiceModelData();
                    serviceModel.ServiceId = serviceModelEntity.ServiceId;
                    serviceModel.ServiceProviderName = serviceModelEntity.ServiceProviderName;
                    serviceModel.Category = serviceModelEntity.Category;
                    serviceModel.ContactInformation = serviceModelEntity.ContactInformation;
                    serviceModel.TagsKeywords = serviceModelEntity.TagsKeywords;
                    var serviceportfolioList = _startupContext.ServicePortfolios.Where(x => x.ServiceId == serviceModelEntity.ServiceId && x.IsActive == true).ToList();
                    var portfolioModelList = new List<ServicePortfolioModel>();
                    foreach (var servicePort in serviceportfolioList)
                    {
                        var portfolioModel = new ServicePortfolioModel();
                        portfolioModel.ServicePortFolioId = servicePort.ServicePortFolioId;
                        portfolioModel.DocumentName = servicePort.DocumentName;
                        portfolioModel.FileName = servicePort.FileName;
                        portfolioModel.FilePath = servicePort.FilePath;
                        portfolioModel.ServiceId = servicePort.ServiceId;
                        portfolioModelList.Add(portfolioModel);
                    }
                    serviceModel.ServicePortfolioModel = portfolioModelList;
                    serviceCaseModel.ServiceModel = serviceModel;
                }
                serviceCaseList.Add(serviceCaseModel);
            }
            return serviceCaseList;
        }
        /// <summary>
        /// get Case management details by id
        /// </summary>
        /// <param name="serviceCaseId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public ServiceCaseModel GetServiceCaseById(long serviceCaseId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var serviceCaseModel = new ServiceCaseModel();
            var serviceCaseEntity = _startupContext.ServiceLeadManagements.Include(x => x.Service).FirstOrDefault(x => x.ServiceCaseId == serviceCaseId && x.IsActive == true);
            if (serviceCaseEntity != null)
            {
                serviceCaseModel.ServiceCaseId = serviceCaseEntity.ServiceCaseId;
                serviceCaseModel.ServiceId = serviceCaseEntity.ServiceId;
                serviceCaseModel.ServiceInterestDate = serviceCaseEntity.ServiceInterestDate;
                serviceCaseModel.IntrestedServiceNames = serviceCaseEntity.IntrestedServiceNames;
                serviceCaseModel.AcceptedDate = serviceCaseEntity.AcceptedDate;
                serviceCaseModel.Status = serviceCaseEntity.Status;
                serviceCaseModel.Comment = serviceCaseEntity.Comment;
                serviceCaseModel.FounderUserId = serviceCaseEntity.FounderUserId;
                serviceCaseModel.IsActive = serviceCaseEntity.IsActive;
                var startupEntity = _startupContext.StartUpDetails.Include(x => x.Sector).FirstOrDefault(x => x.UserId == serviceCaseEntity.FounderUserId);
                if (startupEntity != null)
                {
                    var startupModel = new StartupDeatailModelList();
                    startupModel.StartupId = startupEntity.StartupId;
                    startupModel.StartUpName = startupEntity.StartUpName;
                    startupModel.Address = startupEntity.Address;
                    startupModel.CountryId = startupEntity.CountryId;
                    startupModel.Address = startupEntity.Address;
                    startupModel.StateId = startupEntity.StateId;
                    startupModel.CityId = startupEntity.CityId;
                    startupModel.FoundingYear = startupEntity.FoundingYear;
                    startupModel.CompanyDescription = startupEntity.CompanyDescription;
                    startupModel.WebsiteUrl = startupEntity.WebsiteUrl;
                    startupModel.LogoFileName = startupEntity.LogoFileName;
                    startupModel.Logo = startupEntity.Logo;
                    startupModel.EmployeeCount = startupEntity.EmployeeCount;
                    startupModel.SectorId = startupEntity.SectorId;
                    startupModel.SectorName = startupEntity.Sector.SectorName;
                    startupModel.CompanyLegalName = startupEntity.CompanyLegalName;
                    startupModel.CompanyHeadquartersAddress = startupEntity.CompanyHeadquartersAddress;
                    startupModel.CompanyContact = startupEntity.CompanyContact;
                    startupModel.CompanyEmailId = startupEntity.CompanyEmailId;
                    startupModel.ServiceDescription = startupEntity.ServiceDescription;
                    startupModel.BusinessModel = startupEntity.BusinessModel;
                    startupModel.TargetCustomerBase = startupEntity.TargetCustomerBase;
                    startupModel.TargetMarket = startupEntity.TargetMarket;
                    startupModel.ManagementInfo = startupEntity.ManagementInfo;
                    startupModel.IsStealth = startupEntity.IsStealth;
                    startupModel.IsActive = startupEntity.IsActive;
                    serviceCaseModel.StartupDeatailModel = startupModel;
                }
                var serviceModelEntity = _startupContext.ServiceDetails.FirstOrDefault(x => x.ServiceId == serviceCaseEntity.ServiceId);
                if (serviceModelEntity != null)
                {
                    var serviceModel = new ServiceModelData();
                    serviceModel.ServiceId = serviceModelEntity.ServiceId;
                    serviceModel.ServiceProviderName = serviceModelEntity.ServiceProviderName;
                    serviceModel.Category = serviceModelEntity.Category;
                    serviceModel.ContactInformation = serviceModelEntity.ContactInformation;
                    serviceModel.TagsKeywords = serviceModelEntity.TagsKeywords;
                    var serviceportfolioList = _startupContext.ServicePortfolios.Where(x => x.ServiceId == serviceModelEntity.ServiceId && x.IsActive == true).ToList();
                    var portfolioModelList = new List<ServicePortfolioModel>();
                    foreach (var servicePort in serviceportfolioList)
                    {
                        var portfolioModel = new ServicePortfolioModel();
                        portfolioModel.ServicePortFolioId = servicePort.ServicePortFolioId;
                        portfolioModel.DocumentName = servicePort.DocumentName;
                        portfolioModel.FileName = servicePort.FileName;
                        portfolioModel.FilePath = servicePort.FilePath;
                        portfolioModel.ServiceId = servicePort.ServiceId;
                        portfolioModelList.Add(portfolioModel);
                    }
                    serviceModel.ServicePortfolioModel = portfolioModelList;
                    serviceCaseModel.ServiceModel = serviceModel;
                }
            }
            return serviceCaseModel;
        }

        /// <summary>
        /// get service case by  founder userId
        /// </summary>
        /// <param name="founderUserId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<ServiceCaseModel> GetServiceCaseByUserId(long founderUserId, ref ErrorResponseModel errorResponseModel)
        {
            var serviceCaseList = new List<ServiceCaseModel>();

            var serviceCaseEntity = _startupContext.ServiceLeadManagements.Include(x => x.Service).Where(x => x.FounderUserId == founderUserId && x.IsActive == true).OrderByDescending(x => x.ServiceCaseId).ToList();
            foreach (var serviceCase in serviceCaseEntity)
            {
                var serviceCaseModel = new ServiceCaseModel();
                serviceCaseModel.ServiceCaseId = serviceCase.ServiceCaseId;
                serviceCaseModel.ServiceId = serviceCase.ServiceId;
                serviceCaseModel.ServiceInterestDate = serviceCase.ServiceInterestDate;
                serviceCaseModel.IntrestedServiceNames = serviceCase.IntrestedServiceNames;
                serviceCaseModel.AcceptedDate = serviceCase.AcceptedDate;
                serviceCaseModel.Status = serviceCase.Status;
                serviceCaseModel.Comment = serviceCase.Comment;
                serviceCaseModel.FounderUserId = serviceCase.FounderUserId;
                serviceCaseModel.IsActive = serviceCase.IsActive;
                var startupEntity = _startupContext.StartUpDetails.Include(x => x.Sector).FirstOrDefault(x => x.UserId == serviceCase.FounderUserId);
                if (startupEntity != null)
                {
                    var startupModel = new StartupDeatailModelList();
                    startupModel.StartupId = startupEntity.StartupId;
                    startupModel.StartUpName = startupEntity.StartUpName;
                    startupModel.Address = startupEntity.Address;
                    startupModel.CountryId = startupEntity.CountryId;
                    startupModel.Address = startupEntity.Address;
                    startupModel.StateId = startupEntity.StateId;
                    startupModel.CityId = startupEntity.CityId;
                    startupModel.FoundingYear = startupEntity.FoundingYear;
                    startupModel.CompanyDescription = startupEntity.CompanyDescription;
                    startupModel.WebsiteUrl = startupEntity.WebsiteUrl;
                    startupModel.LogoFileName = startupEntity.LogoFileName;
                    startupModel.Logo = startupEntity.Logo;
                    startupModel.EmployeeCount = startupEntity.EmployeeCount;
                    startupModel.SectorId = startupEntity.SectorId;
                    startupModel.SectorName = startupEntity.Sector.SectorName;
                    startupModel.CompanyLegalName = startupEntity.CompanyLegalName;
                    startupModel.CompanyHeadquartersAddress = startupEntity.CompanyHeadquartersAddress;
                    startupModel.CompanyContact = startupEntity.CompanyContact;
                    startupModel.CompanyEmailId = startupEntity.CompanyEmailId;
                    startupModel.ServiceDescription = startupEntity.ServiceDescription;
                    startupModel.BusinessModel = startupEntity.BusinessModel;
                    startupModel.TargetCustomerBase = startupEntity.TargetCustomerBase;
                    startupModel.TargetMarket = startupEntity.TargetMarket;
                    startupModel.ManagementInfo = startupEntity.ManagementInfo;
                    startupModel.IsStealth = startupEntity.IsStealth;
                    startupModel.IsActive = startupEntity.IsActive;
                    serviceCaseModel.StartupDeatailModel = startupModel;
                }
                var serviceModelEntity = _startupContext.ServiceDetails.FirstOrDefault(x => x.ServiceId == serviceCase.ServiceId);
                if (serviceModelEntity != null)
                {
                    var serviceModel = new ServiceModelData();
                    serviceModel.ServiceId = serviceModelEntity.ServiceId;
                    serviceModel.ServiceProviderName = serviceModelEntity.ServiceProviderName;
                    serviceModel.Category = serviceModelEntity.Category;
                    serviceModel.ContactInformation = serviceModelEntity.ContactInformation;
                    serviceModel.TagsKeywords = serviceModelEntity.TagsKeywords;
                    var serviceportfolioList = _startupContext.ServicePortfolios.Where(x => x.ServiceId == serviceModelEntity.ServiceId && x.IsActive == true).ToList();
                    var portfolioModelList = new List<ServicePortfolioModel>();
                    foreach (var servicePort in serviceportfolioList)
                    {
                        var portfolioModel = new ServicePortfolioModel();
                        portfolioModel.ServicePortFolioId = servicePort.ServicePortFolioId;
                        portfolioModel.DocumentName = servicePort.DocumentName;
                        portfolioModel.FileName = servicePort.FileName;
                        portfolioModel.FilePath = servicePort.FilePath;
                        portfolioModel.ServiceId = servicePort.ServiceId;
                        portfolioModelList.Add(portfolioModel);
                    }
                    serviceModel.ServicePortfolioModel = portfolioModelList;
                    serviceCaseModel.ServiceModel = serviceModel;
                }
                serviceCaseList.Add(serviceCaseModel);
            }
            return serviceCaseList;
        }
        /// <summary>
        /// get service case by service userId
        /// </summary>
        /// <param name="serviceUserId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<ServiceCaseModel> GetServiceCaseByServiceUserId(long serviceUserId, ref ErrorResponseModel errorResponseModel)
        {
            var serviceCaseList = new List<ServiceCaseModel>();
            var serviceEntity = _startupContext.ServiceDetails.Where(x => x.UserId == serviceUserId && x.IsActive == true).ToList();
            foreach (var item in serviceEntity)
            {
                var serviceCaseEntity = _startupContext.ServiceLeadManagements.Include(x => x.Service).Where(x => x.ServiceId == item.ServiceId && x.IsActive == true).OrderByDescending(x => x.ServiceCaseId).ToList();
                foreach (var serviceCase in serviceCaseEntity)
                {
                    var serviceCaseModel = new ServiceCaseModel();
                    serviceCaseModel.ServiceCaseId = serviceCase.ServiceCaseId;
                    serviceCaseModel.ServiceId = serviceCase.ServiceId;
                    serviceCaseModel.ServiceInterestDate = serviceCase.ServiceInterestDate;
                    serviceCaseModel.IntrestedServiceNames = serviceCase.IntrestedServiceNames;
                    serviceCaseModel.AcceptedDate = serviceCase.AcceptedDate;
                    serviceCaseModel.Status = serviceCase.Status;
                    serviceCaseModel.Comment = serviceCase.Comment;
                    serviceCaseModel.FounderUserId = serviceCase.FounderUserId;
                    serviceCaseModel.IsActive = serviceCase.IsActive;
                    var startupEntity = _startupContext.StartUpDetails.Include(x => x.Sector).FirstOrDefault(x => x.UserId == serviceCase.FounderUserId);
                    if (startupEntity != null)
                    {
                        var startupModel = new StartupDeatailModelList();
                        startupModel.StartupId = startupEntity.StartupId;
                        startupModel.StartUpName = startupEntity.StartUpName;
                        startupModel.Address = startupEntity.Address;
                        startupModel.CountryId = startupEntity.CountryId;
                        startupModel.Address = startupEntity.Address;
                        startupModel.StateId = startupEntity.StateId;
                        startupModel.CityId = startupEntity.CityId;
                        startupModel.FoundingYear = startupEntity.FoundingYear;
                        startupModel.CompanyDescription = startupEntity.CompanyDescription;
                        startupModel.WebsiteUrl = startupEntity.WebsiteUrl;
                        startupModel.LogoFileName = startupEntity.LogoFileName;
                        startupModel.Logo = startupEntity.Logo;
                        startupModel.EmployeeCount = startupEntity.EmployeeCount;
                        startupModel.SectorId = startupEntity.SectorId;
                        startupModel.SectorName = startupEntity.Sector.SectorName;
                        startupModel.CompanyLegalName = startupEntity.CompanyLegalName;
                        startupModel.CompanyHeadquartersAddress = startupEntity.CompanyHeadquartersAddress;
                        startupModel.CompanyContact = startupEntity.CompanyContact;
                        startupModel.CompanyEmailId = startupEntity.CompanyEmailId;
                        startupModel.ServiceDescription = startupEntity.ServiceDescription;
                        startupModel.BusinessModel = startupEntity.BusinessModel;
                        startupModel.TargetCustomerBase = startupEntity.TargetCustomerBase;
                        startupModel.TargetMarket = startupEntity.TargetMarket;
                        startupModel.ManagementInfo = startupEntity.ManagementInfo;
                        startupModel.IsStealth = startupEntity.IsStealth;
                        startupModel.IsActive = startupEntity.IsActive;
                        serviceCaseModel.StartupDeatailModel = startupModel;
                    }
                    var serviceModelEntity = _startupContext.ServiceDetails.FirstOrDefault(x => x.ServiceId == serviceCase.ServiceId);
                    if (serviceModelEntity != null)
                    {
                        var serviceModel = new ServiceModelData();
                        serviceModel.ServiceId = serviceModelEntity.ServiceId;
                        serviceModel.ServiceProviderName = serviceModelEntity.ServiceProviderName;
                        serviceModel.Category = serviceModelEntity.Category;
                        serviceModel.ContactInformation = serviceModelEntity.ContactInformation;
                        serviceModel.TagsKeywords = serviceModelEntity.TagsKeywords;
                        var serviceportfolioList = _startupContext.ServicePortfolios.Where(x => x.ServiceId == serviceModelEntity.ServiceId && x.IsActive == true).ToList();
                        var portfolioModelList = new List<ServicePortfolioModel>();
                        foreach (var servicePort in serviceportfolioList)
                        {
                            var portfolioModel = new ServicePortfolioModel();
                            portfolioModel.ServicePortFolioId = servicePort.ServicePortFolioId;
                            portfolioModel.DocumentName = servicePort.DocumentName;
                            portfolioModel.FileName = servicePort.FileName;
                            portfolioModel.FilePath = servicePort.FilePath;
                            portfolioModel.ServiceId = servicePort.ServiceId;
                            portfolioModelList.Add(portfolioModel);
                        }
                        serviceModel.ServicePortfolioModel = portfolioModelList;
                        serviceCaseModel.ServiceModel = serviceModel;
                    }
                    serviceCaseList.Add(serviceCaseModel);
                }
            }
            return serviceCaseList;
        }
        /// <summary>
        /// Add the founder Interest in service
        /// </summary>
        /// <param name="serviceModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddServiceCase(ServiceLeadManagementModel serviceModel, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingCaseRecord = _startupContext.ServiceLeadManagements.Any(x => x.ServiceId == serviceModel.ServiceId && x.FounderUserId == serviceModel.FounderUserId && x.IsActive == true);
            if (!existingCaseRecord)
            {
                var serviceLeadEntity = new ServiceLeadManagement();
                serviceLeadEntity.ServiceCaseId = serviceModel.ServiceCaseId;
                serviceLeadEntity.ServiceId = serviceModel.ServiceId;
                serviceLeadEntity.ServiceInterestDate = serviceModel.ServiceInterestDate;
                serviceLeadEntity.IntrestedServiceNames = serviceModel.IntrestedServiceNames;
                serviceLeadEntity.AcceptedDate = serviceModel.AcceptedDate;
                serviceLeadEntity.Status = serviceModel.Status;
                serviceLeadEntity.Comment = serviceModel.Comment;
                serviceLeadEntity.FounderUserId = serviceModel.FounderUserId;

                serviceLeadEntity.CreatedDate = DateTime.Now;
                serviceLeadEntity.CreatedBy = serviceModel.LoggedUserId;
                serviceLeadEntity.IsActive = true;
                _startupContext.ServiceLeadManagements.Add(serviceLeadEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.FounderInterestServiceMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add Service Case";
                userAuditLog.Description = "Founder shown interest in the Service Provider services.";
                userAuditLog.UserId = (int)serviceModel.LoggedUserId;
                userAuditLog.CreatedBy = serviceModel.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.FounderInterestServiceExistMessage;
            }
            return message;
        }
        /// <summary>
        /// Accept service provider
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string ServiceAccept(ServiceLeadManagementModel serviceModel, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var serviceCaseEntity = _startupContext.ServiceLeadManagements.Include(x => x.Service).FirstOrDefault(x => x.ServiceId == serviceModel.ServiceId && x.IsActive == true);
            if (serviceCaseEntity != null)
            {
                serviceCaseEntity.ServiceCaseId = serviceModel.ServiceCaseId;
                serviceCaseEntity.ServiceId = serviceModel.ServiceId;
                serviceCaseEntity.Status = serviceModel.Status;
                serviceCaseEntity.Comment = serviceModel.Comment;
                serviceCaseEntity.FounderUserId = serviceModel.FounderUserId;
                serviceCaseEntity.UpdatedDate = DateTime.Now;
                serviceCaseEntity.UpdatedBy = serviceModel.LoggedUserId;
                serviceCaseEntity.AcceptedDate = DateTime.Now;
                serviceCaseEntity.IsActive = true;
                _startupContext.ServiceLeadManagements.Update(serviceCaseEntity);
                _startupContext.SaveChanges();

                var userEntity = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == serviceModel.FounderUserId);
                StringBuilder strBody = new StringBuilder();
                strBody.Append("<body>");
                strBody.Append("<P>StartupX</P>");
                strBody.Append("<h2>Service provider" + " " + serviceCaseEntity.Service.ServiceProviderName + " " + "accepted you request of interested in services.</h2>");
                strBody.Append("</body>");
                var emailSenderModel = new EmailModel();
                emailSenderModel.ToAddress = userEntity.EmailId;
                emailSenderModel.Body = strBody.ToString();
                emailSenderModel.isHtml = true;
                emailSenderModel.Subject = GlobalConstants.FounderInterestAcceptMessage;
                emailSenderModel.sentStatus = true;
                if (!string.IsNullOrEmpty(emailSenderModel.ToAddress))
                {
                    _emailSender.Execute(emailSenderModel.ToAddress, emailSenderModel.Subject, emailSenderModel.Body);
                }
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Service Case Accept";
                userAuditLog.Description = "Service Provider accepted the founder request.";
                userAuditLog.UserId = (int)serviceModel.LoggedUserId;
                userAuditLog.CreatedBy = serviceModel.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);

                /// Notification Add
                var notification = new NotificationModel();
                notification.Message = "Service provider" + " " + serviceCaseEntity.Service.ServiceProviderName + " " + "accepted your request";
                notification.UserId = serviceModel.FounderUserId;
                notification.LoggedUserId = serviceModel.LoggedUserId;
                _notificationService.AddNotification(notification);

                message = GlobalConstants.FounderInterestAcceptMessage;
            }
            else
            {
                message = GlobalConstants.NotFoundMessage;
            }
            return message;
        }
        /// <summary>
        /// Deny service provider
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string ServiceDeny(ServiceLeadManagementModel serviceModel, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var serviceCaseEntity = _startupContext.ServiceLeadManagements.Include(x => x.Service).FirstOrDefault(x => x.ServiceId == serviceModel.ServiceId && x.IsActive == true);
            if (serviceCaseEntity != null)
            {
                serviceCaseEntity.ServiceCaseId = serviceModel.ServiceCaseId;
                serviceCaseEntity.ServiceId = serviceModel.ServiceId;
                serviceCaseEntity.Status = serviceModel.Status;
                serviceCaseEntity.Comment = serviceModel.Comment;
                serviceCaseEntity.FounderUserId = serviceModel.FounderUserId;
                serviceCaseEntity.UpdatedDate = DateTime.Now;
                serviceCaseEntity.UpdatedBy = serviceModel.LoggedUserId;
                serviceCaseEntity.IsActive = true;
                serviceCaseEntity.AcceptedDate = DateTime.Now;
                _startupContext.ServiceLeadManagements.Update(serviceCaseEntity);
                _startupContext.SaveChanges();

                var userEntity = _startupContext.UserMasters.FirstOrDefault(x => x.UserId == serviceModel.FounderUserId);
                StringBuilder strBody = new StringBuilder();
                strBody.Append("<body>");
                strBody.Append("<P>StartupX</P>");
                strBody.Append("<h2>Service provider" + " " + serviceCaseEntity.Service.ServiceProviderName + " " + "deny you request of interested in services because" + serviceModel.Comment + "</h2>");
                strBody.Append("</body>");
                var emailSenderModel = new EmailModel();
                emailSenderModel.ToAddress = userEntity.EmailId;
                emailSenderModel.Body = strBody.ToString();
                emailSenderModel.isHtml = true;
                emailSenderModel.Subject = GlobalConstants.FounderInterestDenyMessage;
                emailSenderModel.sentStatus = true;
                if (!string.IsNullOrEmpty(emailSenderModel.ToAddress))
                {
                    _emailSender.Execute(emailSenderModel.ToAddress, emailSenderModel.Subject, emailSenderModel.Body);
                }
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Service Case Deny";
                userAuditLog.Description = "Service Provider deny the founder request";
                userAuditLog.UserId = (int)serviceModel.LoggedUserId;
                userAuditLog.CreatedBy = serviceModel.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);

                /// Notification Add
                var notification = new NotificationModel();
                notification.Message = notification.Message = "Service provider" + " " + serviceCaseEntity.Service.ServiceProviderName + " " + "deny your request because," + serviceModel.Comment;
                notification.UserId = serviceModel.FounderUserId;
                notification.LoggedUserId = serviceModel.LoggedUserId;
                _notificationService.AddNotification(notification);

                message = GlobalConstants.FounderInterestDenyMessage;
            }
            else
            {
                message = GlobalConstants.NotFoundMessage;
            }
            return message;
        }
        /// <summary>
        /// get service case by founderuserId
        /// </summary>
        /// <param name="founderUserId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<ServiceCaseModel> GetServiceCaseByfounderUserId(long founderUserId, ref ErrorResponseModel errorResponseModel)
        {
            var serviceCaseList = new List<ServiceCaseModel>();
            var serviceCaseEntity = _startupContext.ServiceLeadManagements.Include(x => x.Service).Where(x => x.FounderUserId == founderUserId && x.IsActive == true).OrderByDescending(x => x.ServiceCaseId).ToList();
            foreach (var serviceCase in serviceCaseEntity)
            {
                var serviceCaseModel = new ServiceCaseModel();
                serviceCaseModel.ServiceCaseId = serviceCase.ServiceCaseId;
                serviceCaseModel.ServiceId = serviceCase.ServiceId;
                serviceCaseModel.ServiceInterestDate = serviceCase.ServiceInterestDate;
                serviceCaseModel.IntrestedServiceNames = serviceCase.IntrestedServiceNames;
                serviceCaseModel.AcceptedDate = serviceCase.AcceptedDate;
                serviceCaseModel.Status = serviceCase.Status;
                serviceCaseModel.Comment = serviceCase.Comment;
                serviceCaseModel.UserId = serviceCase.FounderUserId;
                serviceCaseModel.IsActive = serviceCase.IsActive;
                var startupEntity = _startupContext.StartUpDetails.FirstOrDefault(x => x.UserId == serviceCase.FounderUserId);
                if (startupEntity != null)
                {
                    var startupModel = new StartupDeatailModelList();
                    startupModel.StartupId = startupEntity.StartupId;
                    startupModel.StartUpName = startupEntity.StartUpName;
                    startupModel.Address = startupEntity.Address;
                    startupModel.CountryId = startupEntity.CountryId;
                    startupModel.Address = startupEntity.Address;
                    startupModel.StateId = startupEntity.StateId;
                    startupModel.CityId = startupEntity.CityId;
                    startupModel.FoundingYear = startupEntity.FoundingYear;
                    startupModel.CompanyDescription = startupEntity.CompanyDescription;
                    startupModel.WebsiteUrl = startupEntity.WebsiteUrl;
                    startupModel.LogoFileName = startupEntity.LogoFileName;
                    startupModel.Logo = startupEntity.Logo;
                    startupModel.EmployeeCount = startupEntity.EmployeeCount;
                    startupModel.SectorId = startupEntity.SectorId;
                    startupModel.SectorName = startupEntity.Sector.SectorName;
                    startupModel.CompanyLegalName = startupEntity.CompanyLegalName;
                    startupModel.CompanyHeadquartersAddress = startupEntity.CompanyHeadquartersAddress;
                    startupModel.CompanyContact = startupEntity.CompanyContact;
                    startupModel.CompanyEmailId = startupEntity.CompanyEmailId;
                    startupModel.ServiceDescription = startupEntity.ServiceDescription;
                    startupModel.BusinessModel = startupEntity.BusinessModel;
                    startupModel.TargetCustomerBase = startupEntity.TargetCustomerBase;
                    startupModel.TargetMarket = startupEntity.TargetMarket;
                    startupModel.ManagementInfo = startupEntity.ManagementInfo;
                    startupModel.IsStealth = startupEntity.IsStealth;
                    startupModel.IsActive = startupEntity.IsActive;
                    serviceCaseModel.StartupDeatailModel = startupModel;
                }
                var serviceModelEntity = _startupContext.ServiceDetails.FirstOrDefault(x => x.ServiceId == serviceCase.ServiceId);
                if (serviceModelEntity != null)
                {
                    var serviceModel = new ServiceModelData();
                    serviceModel.ServiceId = serviceModelEntity.ServiceId;
                    serviceModel.ServiceProviderName = serviceModelEntity.ServiceProviderName;
                    serviceModel.Category = serviceModelEntity.Category;
                    serviceModel.ContactInformation = serviceModelEntity.ContactInformation;
                    serviceModel.TagsKeywords = serviceModelEntity.TagsKeywords;
                    var serviceportfolioList = _startupContext.ServicePortfolios.Where(x => x.ServiceId == serviceModelEntity.ServiceId && x.IsActive == true).ToList();
                    var portfolioModelList = new List<ServicePortfolioModel>();
                    foreach (var servicePort in serviceportfolioList)
                    {
                        var portfolioModel = new ServicePortfolioModel();
                        portfolioModel.ServicePortFolioId = servicePort.ServicePortFolioId;
                        portfolioModel.DocumentName = servicePort.DocumentName;
                        portfolioModel.FileName = servicePort.FileName;
                        portfolioModel.FilePath = servicePort.FilePath;
                        portfolioModel.ServiceId = servicePort.ServiceId;
                        portfolioModelList.Add(portfolioModel);
                    }
                    serviceModel.ServicePortfolioModel = portfolioModelList;
                    serviceCaseModel.ServiceModel = serviceModel;
                }
                serviceCaseList.Add(serviceCaseModel);
            }
            return serviceCaseList;
        }
        /// <summary>
        /// Add Service Portfolio Document details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddPortFolioDocument(ServicePortfolioModel model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            var existingRecord = _startupContext.ServicePortfolios.Any(x => x.DocumentName == model.DocumentName && x.ServiceId == model.ServiceId && x.IsActive == true);
            if (!existingRecord)
            {
                var documentEntity = new ServicePortfolio();
                documentEntity.IsActive = true;
                documentEntity.ServiceId = model.ServiceId;
                documentEntity.DocumentName = model.DocumentName;
                documentEntity.FileName = model.FileName;
                documentEntity.FilePath = model.FilePath;
                documentEntity.CreatedBy = model.LoggedUserId;
                documentEntity.CreatedDate = DateTime.Now;
                _startupContext.ServicePortfolios.Add(documentEntity);
                _startupContext.SaveChanges();
                message = GlobalConstants.DocumentUploadedMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add Portfolio document.";
                userAuditLog.Description = "Service provider added Portfolio document.";
                userAuditLog.UserId = (int)model.LoggedUserId;
                userAuditLog.CreatedBy = model.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            else
            {
                message = GlobalConstants.SameDocumentUploadedMessage;
            }
            return message;
        }
        /// <summary>
        /// Check document with same name exist
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="documentName"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public bool CheckDocumentByServiceId(long serviceId, string documentName)
        {
            bool documentEntity = _startupContext.ServicePortfolios.Any(x => x.ServiceId == serviceId && x.DocumentName == documentName && x.IsActive == true);
            if (!documentEntity)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Get All Portfoliodocument by service Id
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<ServicePortfolioModel> GetAllDocumentByServiceId(long serviceId, ref ErrorResponseModel errorResponseModel)
        {
            var documentEntity = _startupContext.ServicePortfolios.Where(x => x.ServiceId == serviceId && x.IsActive == true).ToList();
            var documentList = documentEntity.Select(x => new ServicePortfolioModel
            {
                ServicePortFolioId = x.ServicePortFolioId,
                DocumentName = x.DocumentName,
                FileName = x.FileName,
                FilePath = x.FilePath,
                ServiceId = x.ServiceId

            }).ToList();
            return documentList;
        }
        /// <summary>
        /// delete document
        /// </summary>
        /// <param name="servicePortFolioId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeletePortfolioDocument(int servicePortFolioId, int userId, ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;
            var portfolio = new ServicePortfolioModel();
            var documentEntity = _startupContext.ServicePortfolios.Where(x => x.ServicePortFolioId == servicePortFolioId && x.IsActive == true).FirstOrDefault();
            if (documentEntity == null)
            {
                message = GlobalConstants.NotFoundMessage;
            }
            else
            {
                documentEntity.IsActive = false;
                _startupContext.SaveChanges();
                message = GlobalConstants.DocumentDeleteMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Delete Portfolio document.";
                userAuditLog.Description = "Service provider deleted Portfolio document";
                userAuditLog.UserId = userId;
                userAuditLog.CreatedBy = userId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            return message;
        }
        /// <summary>
        /// Add Service Invoice Document
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string AddServiceInvoiceDocument(List<ServiceLeadInvoiceModel> model, ref ErrorResponseModel errorResponseModel)
        {
            var message = string.Empty;

            foreach (var item in model)
            {
                var existingRecord = _startupContext.ServiceLeadInvoices.Any(x => x.InvoiceFileName == item.InvoiceFileName && x.ServiceCaseId == item.ServiceCaseId && x.IsActive == true);
                if (!existingRecord)
                {
                    var documentEntity = new ServiceLeadInvoice();
                    documentEntity.IsActive = true;
                    documentEntity.ServiceCaseId = item.ServiceCaseId;
                    documentEntity.InvoiceFileName = item.InvoiceFileName;
                    documentEntity.InvoiceFilePath = item.InvoiceFilePath;
                    documentEntity.CreatedBy = item.LoggedUserId;
                    documentEntity.CreatedDate = DateTime.Now;
                    _startupContext.ServiceLeadInvoices.Add(documentEntity);
                }

                _startupContext.SaveChanges();
                message = GlobalConstants.DocumentUploadedMessage;
                /// User Audit Log
                var userAuditLog = new UserAuditLogModel();
                userAuditLog.Action = "Add Service Invoice Document";
                userAuditLog.Description = "Service provider added Invoice Document.";
                userAuditLog.UserId = item.LoggedUserId;
                userAuditLog.CreatedBy = item.LoggedUserId;
                _userAuditLogService.AddUserAuditLog(userAuditLog);
            }
            return message;
        }
        /// <summary>
        /// Get portfolio document by serviceId & servicePortFolioId
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="servicePortFolioId"></param>
        /// <param name="errorResponseModel"></param>
        public ServicePortfolioModel GetportfolioDocumentById(long serviceId, long servicePortFolioId)
        {
            var model = new ServicePortfolioModel();
            var documentEntity = _startupContext.ServicePortfolios.FirstOrDefault(x => x.ServiceId == serviceId && x.ServicePortFolioId == servicePortFolioId && x.IsActive == true);
            if (documentEntity != null)
            {
                model.ServicePortFolioId = documentEntity.ServicePortFolioId;
                model.DocumentName = documentEntity.DocumentName;
                model.FileName = documentEntity.FileName;
                model.FilePath = documentEntity.FilePath;
                model.ServiceId = documentEntity.ServiceId;
            }
            else
            {
                return null;
            }
            return model;
        }
        /// <summary>
        /// Get invoice document by serviceCaseId & serviceLeadInvoiceId
        /// </summary>
        /// <param name="serviceCaseId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<ServiceLeadInvoiceModel> GetInvoiceDocumentById(long serviceCaseId)
        {
            var modelList = new List<ServiceLeadInvoiceModel>();
            var documentEntity = _startupContext.ServiceLeadInvoices.Where(x => x.ServiceCaseId == serviceCaseId && x.IsActive == true).ToList();
            foreach (var item in documentEntity)
            {
                var model = new ServiceLeadInvoiceModel();
                model.ServiceLeadInvoiceId = item.ServiceLeadInvoiceId;
                model.InvoiceFileName = item.InvoiceFileName;
                model.InvoiceFilePath = item.InvoiceFilePath;
                model.ServiceCaseId = item.ServiceCaseId;
                modelList.Add(model);
            }

            return modelList;
        }
    }
}


