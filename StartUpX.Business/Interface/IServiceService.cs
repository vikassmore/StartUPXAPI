using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Interface
{
    public interface IServiceService
    {
        /// <summary>
        /// get service by Id
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        ServiceModelData GetServiceById(long serviceId, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// get service by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        ServiceModelData GetServiceByUserId(long userId, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Get all Services
        /// </summary>
        /// <returns></returns>
        List<ServiceModelData> GetAllService();
        /// <summary>
        /// Get all Services At admin
        /// </summary>
        /// <returns></returns>
        List<ServiceModelData> GetAllAdminService();
        /// <summary>
        /// Add Service details
        /// </summary>
        /// <param name="service"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string AddService(ServiceModel service, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Edit service details
        /// </summary>
        /// <param name="service"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string EditService(ServiceModel service, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Delete service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteService(int serviceId,int userId, ErrorResponseModel errorResponseModel);

        /// <summary>
        /// get Case management details by id
        /// </summary>
        /// <param name="serviceCaseId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        ServiceCaseModel GetServiceCaseById(long serviceCaseId, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// get service case by founder userId
        /// </summary>
        /// <param name="founderUserId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<ServiceCaseModel> GetServiceCaseByUserId(long founderUserId, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// get service case by service userId
        /// </summary>
        /// <param name="serviceUserId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<ServiceCaseModel> GetServiceCaseByServiceUserId(long serviceUserId, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Get all Case management details
        /// </summary>
        /// <returns></returns>
        List<ServiceCaseModel> GetAllServiceCase();
        /// <summary>
        /// Add the founder Interest in service
        /// </summary>
        /// <param name="serviceModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string AddServiceCase(ServiceLeadManagementModel serviceModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Accept service provider
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string ServiceAccept(ServiceLeadManagementModel model, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Deny service provider
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string ServiceDeny(ServiceLeadManagementModel model, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// get service case by founderuserId
        /// </summary>
        /// <param name="founderUserId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<ServiceCaseModel> GetServiceCaseByfounderUserId(long founderUserId, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Add Service Portfolio Document details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string AddPortFolioDocument(ServicePortfolioModel model, ref ErrorResponseModel errorResponseModel);
        /// <summary>
        /// Get All Portfoliodocument by service Id
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<ServicePortfolioModel> GetAllDocumentByServiceId(long serviceId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// delete document
        /// </summary>
        /// <param name="servicePortFolioId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeletePortfolioDocument(int servicePortFolioId,int userId, ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Check document with same name exist
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="documentName"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        bool CheckDocumentByServiceId(long serviceId, string documentName);
        /// <summary>
        /// Add Service Invoice Document
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string AddServiceInvoiceDocument(List<ServiceLeadInvoiceModel> model, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Get portfolio document by serviceId & servicePortFolioId
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="servicePortFolioId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        ServicePortfolioModel GetportfolioDocumentById(long serviceId, long servicePortFolioId);
        /// <summary>
        /// Get invoice document by serviceCaseId & serviceLeadInvoiceId
        /// </summary>
        /// <param name="serviceCaseId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<ServiceLeadInvoiceModel> GetInvoiceDocumentById(long serviceCaseId);
    }
}
