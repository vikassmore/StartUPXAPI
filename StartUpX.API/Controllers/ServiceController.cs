using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Core;
using StartUpX.Business.Implementation;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;

namespace StartUpX.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : BaseAPIController
    {
        IServiceService _serviceService;
        private readonly IWebHostEnvironment _iwebhostingEnvironment;
        public ServiceController(IServiceService serviceService, IWebHostEnvironment iwebhostingEnvironment)
        {
            _serviceService = serviceService;
            _iwebhostingEnvironment = iwebhostingEnvironment;
        }
        /// <summary>
        /// Get All Services
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        [ProducesResponseType(typeof(ServiceModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAll()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var servicemodel = _serviceService.GetAllService();

                if (servicemodel != null)
                {
                    return Ok(servicemodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Get All Services for admin
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllAdminService")]

        [ProducesResponseType(typeof(ServiceModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllAdminService()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var servicemodel = _serviceService.GetAllAdminService();

                if (servicemodel != null)
                {
                    return Ok(servicemodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Get service By Id
        /// </summary>
        /// <param name="ServiceId"></param>
        /// <returns></returns>
        [HttpGet("GetServiceById/{ServiceId}")]
        //[Authorize]
        [ProducesResponseType(typeof(ServiceModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long ServiceId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var servicemodel = _serviceService.GetServiceById(ServiceId, ref errorResponseModel);

                if (servicemodel != null)
                {
                    return Ok(servicemodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Get service By userId
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetServiceByUserId/{userId}")]

        [ProducesResponseType(typeof(ServiceModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetServiceByUserId(int userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var serviceModel = _serviceService.GetServiceByUserId(userId, ref errorResponseModel);

                if (serviceModel != null)
                {
                    return Ok(serviceModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Add service details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(ServiceModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                model.LoggedUserId = Convert.ToInt32(userId);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var serviceModel = _serviceService.AddService(model, ref errorMessage);
                if (!string.IsNullOrEmpty(serviceModel))
                {
                    return Ok(serviceModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Edit Service details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Edit")]
        public IActionResult Put(ServiceModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                model.LoggedUserId = Convert.ToInt32(userId);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var serviceModel = _serviceService.EditService(model, ref errorMessage);
                if (!string.IsNullOrEmpty(serviceModel))
                {
                    return Ok(serviceModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        ///  Delete  Service  Data
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int serviceId)
        {
            var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            var LoggedUserId = Convert.ToInt32(userId);
            try
            {
                var errorMessage = new ErrorResponseModel();
                var serviceModel = _serviceService.DeleteService(serviceId, LoggedUserId,errorMessage);
                if (!string.IsNullOrEmpty(serviceModel))
                {
                    return Ok();
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// Get all Case management details
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllServiceCase")]
        [ProducesResponseType(typeof(ServiceLeadManagementModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllServiceCase()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var serviceCaseModel = _serviceService.GetAllServiceCase();

                if (serviceCaseModel != null)
                {
                    return Ok(serviceCaseModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// get Case management details by id
        /// </summary>
        /// <param name="serviceCaseId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        [HttpGet("GetServiceCaseById/{serviceCaseId}")]
        //[Authorize]
        [ProducesResponseType(typeof(ServiceLeadManagementModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetServiceCaseById(long serviceCaseId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var serviceCaseModel = _serviceService.GetServiceCaseById(serviceCaseId, ref errorResponseModel);

                if (serviceCaseModel != null)
                {
                    return Ok(serviceCaseModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Get service case By userId
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetServiceCaseByUserId/{userId}")]

        [ProducesResponseType(typeof(ServiceCaseModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetServiceCaseByUserId(int userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var serviceCaseModel = _serviceService.GetServiceCaseByUserId(userId, ref errorResponseModel);

                if (serviceCaseModel != null)
                {
                    return Ok(serviceCaseModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Get service case By service userId
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetServiceCaseByServiceUserId/{userId}")]

        [ProducesResponseType(typeof(ServiceCaseModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetServiceCaseByServiceUserId(int userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var serviceCaseModel = _serviceService.GetServiceCaseByServiceUserId(userId, ref errorResponseModel);

                if (serviceCaseModel != null)
                {
                    return Ok(serviceCaseModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Add the founder Interest in service
        /// </summary>
        /// <param name="serviceModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        [HttpPost("AddServiceCase")]
        public IActionResult AddServiceCase(ServiceLeadManagementModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                model.LoggedUserId = Convert.ToInt32(userId);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var serviceCaseModel = _serviceService.AddServiceCase(model, ref errorMessage);
                if (!string.IsNullOrEmpty(serviceCaseModel))
                {
                    return Ok(serviceCaseModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Accept service provider
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        [HttpPost("ServiceAccept")]
        public IActionResult ServiceAccept(ServiceLeadManagementModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                model.LoggedUserId = Convert.ToInt32(userId);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var serviceCaseModel = _serviceService.ServiceAccept(model, ref errorMessage);
                if (!string.IsNullOrEmpty(serviceCaseModel))
                {
                    return Ok(serviceCaseModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Deny service provider
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        [HttpPost("ServiceDeny")]
        public IActionResult ServiceDeny(ServiceLeadManagementModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                model.LoggedUserId = Convert.ToInt32(userId);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var serviceCaseModel = _serviceService.ServiceDeny(model, ref errorMessage);
                if (!string.IsNullOrEmpty(serviceCaseModel))
                {
                    return Ok(serviceCaseModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// get service case by founderuserId
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetServiceCaseByfounderUserId/{userId}")]

        [ProducesResponseType(typeof(ServiceCaseModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetServiceCaseByfounderUserId(int userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var serviceCaseModel = _serviceService.GetServiceCaseByfounderUserId(userId, ref errorResponseModel);

                if (serviceCaseModel != null)
                {
                    return Ok(serviceCaseModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Add Service Portfolio documents
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddServicePortfolio")]
        // [Authorize]
        [ProducesResponseType(typeof(ServicePortfolioModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult AddServicePortfolio(IFormCollection formdata)
        {
            var model = new ServicePortfolioModel();
            if (formdata == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }

            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                model.LoggedUserId = Convert.ToInt32(userId);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                model.ServiceId = Convert.ToInt32(formdata["serviceId"]);
                model.DocumentName = formdata["documentName"];
                bool isExist = _serviceService.CheckDocumentByServiceId(model.ServiceId, model.DocumentName);
                if (isExist == false)
                {
                    if (Request.Form.Files.Count > 0)
                    {
                        var folder = _iwebhostingEnvironment.WebRootPath;
                        var files = Request.Form.Files;
                        foreach (var file in files)
                        {
                            var folderName = Path.Combine("Resources", "ServicePortFolioDocument");
                            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                            if (file.Length > 0)
                            {
                                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                                var uniqueId = Guid.NewGuid().ToString();
                                var fileExtension = Path.GetExtension(fileName);
                                var uniqueFileName = $"{uniqueId}{fileExtension}";
                                var fullPath = Path.Combine(pathToSave, uniqueFileName);
                                var dbPath = Path.Combine(folderName, uniqueFileName);
                                using (var stream = new FileStream(fullPath, FileMode.Create))
                                {
                                    file.CopyTo(stream);
                                }
                                model.FilePath = "/ServicePortFolioDocument/" + uniqueFileName;
                                model.FileName = fileName;
                            }
                        }
                    }
                    var documentModel = _serviceService.AddPortFolioDocument(model, ref errorMessage);
                    if (documentModel != null)
                    {
                        return Ok(documentModel);
                    }

                    return ReturnErrorResponse(errorMessage);
                }
                else
                {
                    return Ok(null);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        [HttpGet("GetAllDocumentByServiceId/{serviceId}")]
        //[Authorize]
        [ProducesResponseType(typeof(ServicePortfolioModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllDocumentByServiceId(long serviceId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var documentModel = _serviceService.GetAllDocumentByServiceId(serviceId, ref errorResponseModel);

                if (documentModel != null)
                {
                    return Ok(documentModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Delete document
        /// </summary>
        /// <param name="servicePortFolioId"></param>
        /// <returns></returns>
        [HttpDelete("DeletePortfolioDocument")]
        public IActionResult DeletePortfolioDocument(int servicePortFolioId)
        {
            var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            var LoggedUserId = Convert.ToInt32(userId);
            try
            {
                var errorMessage = new ErrorResponseModel();
                var documentModel = _serviceService.DeletePortfolioDocument(servicePortFolioId, LoggedUserId, errorMessage);
                if (!string.IsNullOrEmpty(documentModel))
                {
                    return Ok();
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Add Service invoice documents
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddServiceInvoice")]
        // [Authorize]
        [ProducesResponseType(typeof(ServiceLeadInvoiceModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult AddServiceInvoice(IFormCollection formdata)
        {
            int LoggedUserId = 0;
            int ServiceCaseId;
            var modelList = new List<ServiceLeadInvoiceModel>();
            if (formdata == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }

            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                LoggedUserId = Convert.ToInt32(userId);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                ServiceCaseId = Convert.ToInt32(formdata["serviceCaseId"]);
                if (Request.Form.Files.Count > 0)
                {
                    var folder = _iwebhostingEnvironment.WebRootPath;
                    var files = Request.Form.Files;
                    foreach (var file in files)
                    {
                        var model = new ServiceLeadInvoiceModel();
                        var folderName = Path.Combine("Resources", "ServiceInvoiceDocument");
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                        if (file.Length > 0)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            var uniqueId = Guid.NewGuid().ToString();
                            var fileExtension = Path.GetExtension(fileName);
                            var uniqueFileName = $"{uniqueId}{fileExtension}";
                            var fullPath = Path.Combine(pathToSave, uniqueFileName);
                            var dbPath = Path.Combine(folderName, uniqueFileName);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            model.InvoiceFilePath = "/ServiceInvoiceDocument/" + uniqueFileName;
                            model.InvoiceFileName = fileName;
                            model.ServiceCaseId = ServiceCaseId;
                            model.LoggedUserId = LoggedUserId;
                            modelList.Add(model);
                        }
                    }
                }
                var documentModel = _serviceService.AddServiceInvoiceDocument(modelList, ref errorMessage);
                if (documentModel != null)
                {
                    return Ok(documentModel);
                }

                return ReturnErrorResponse(errorMessage);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// download portfolio document
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="servicePortFolioId"></param>
        /// <returns></returns>
        [HttpGet("DownloadPortfolioById/{serviceId}/{servicePortFolioId}")]
        public async Task<IActionResult> DownloadPortfolioById(long serviceId, long servicePortFolioId)
        {
            try
            {
                var documentModel = _serviceService.GetportfolioDocumentById(serviceId, servicePortFolioId);
                if (documentModel != null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources" + "/" + documentModel.FilePath);
                    if (!System.IO.File.Exists(filePath))
                        return NotFound();
                    var memory = new MemoryStream();
                    using (var stream = new FileStream(filePath, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;
                    return File(memory, GetContentType(filePath) ,documentModel.FileName);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// download portfolio document
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        [HttpGet("DownloadAllPortfolioById/{serviceId}")]
        public async Task<IActionResult> DownloadAllPortfolioById(long serviceId)
        {
            try
            {
                ErrorResponseModel errorResponseModel = null;
                var documentModel = _serviceService.GetAllDocumentByServiceId(serviceId, ref errorResponseModel);
                if (documentModel != null)
                {
                    var zipName = $"archive-EvidenceFiles-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, true))
                        {
                            foreach (var file in documentModel)
                            {
                                if (file != null)
                                {
                                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources" + "/" + file.FilePath);

                                    var entry = archive.CreateEntry(file.FileName, CompressionLevel.Fastest);
                                    using (var zipStream = entry.Open())
                                    {
                                        var bytes = System.IO.File.ReadAllBytes(filePath);
                                        zipStream.Write(bytes, 0, bytes.Length);
                                    }
                                }
                            }
                        }
                        return File(ms.ToArray(), "application/zip", zipName);
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        // Get content type
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        // Get mime types
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
           {
             {".pdf", "application/pdf"}
           };
        }
        /// <summary>
        /// download invoice document
        /// </summary>
        /// <param name="serviceCaseId"></param>
        /// <param name="serviceLeadInvoiceId"></param>
        /// <returns></returns>
        [HttpGet("DownloadInvoiceById/{serviceCaseId}")]
        public async Task<IActionResult> DownloadInvoiceById(long serviceCaseId)
        {
            try
            {
                var documentModel = _serviceService.GetInvoiceDocumentById(serviceCaseId);
                if (documentModel != null)
                {
                    var zipName = $"archive-EvidenceFiles-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, true))
                        {
                            foreach (var file in documentModel)
                            {
                                if (file != null)
                                {
                                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources" + "/" + file.InvoiceFilePath);

                                    var entry = archive.CreateEntry(file.InvoiceFileName, CompressionLevel.Fastest);
                                    using (var zipStream = entry.Open())
                                    {
                                        var bytes = System.IO.File.ReadAllBytes(filePath);
                                        zipStream.Write(bytes, 0, bytes.Length);
                                    }
                                }
                            }
                        }
                        return File(ms.ToArray(), "application/zip", zipName);
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
    }
}


