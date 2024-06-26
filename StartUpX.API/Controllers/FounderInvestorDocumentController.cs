using Microsoft.AspNetCore.Mvc;
using StartUpX.Business.Implementation;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;
using System.IO;
using System.Net.Http.Headers;
using System.Text.Json;

namespace StartUpX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FounderInvestorDocumentController : BaseAPIController
    {
        IFounderInvestorDocumentService _documentService;
        private readonly IWebHostEnvironment _iwebhostingEnvironment;
        public FounderInvestorDocumentController(IFounderInvestorDocumentService documentService, IWebHostEnvironment iwebhostingEnvironment)
        {
            _documentService = documentService;
            _iwebhostingEnvironment = iwebhostingEnvironment;
        }

        /// <summary>
        /// Add founder documents
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddFounderDocument")]
        // [Authorize]
        [ProducesResponseType(typeof(FounderInvestorDocumentModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult AddFounderDocument(IFormCollection formdata)
        {
            var model = new FounderInvestorDocumentModel();
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
                model.UserId = Convert.ToInt32(formdata["userId"]);
                model.DocumentName = formdata["documentName"];
                bool isExist = _documentService.CheckDocumentByUserId(model.UserId, model.DocumentName);
                if (isExist == false)
                {
                    if (Request.Form.Files.Count > 0)
                    {
                        var folder = _iwebhostingEnvironment.WebRootPath;
                        var files = Request.Form.Files;
                        foreach (var file in files)
                        {
                            var folderName = Path.Combine("Resources", "FounderDocument");
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
                                model.FilePath = "/FounderDocument/" + uniqueFileName;
                                model.FileName = fileName;
                            }
                        }
                    }
                    var documentModel = _documentService.AddDocument(model, ref errorMessage);
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
        [HttpGet("GetAllDocumentByUserId/{userId}")]
        //[Authorize]
        [ProducesResponseType(typeof(FounderInvestorDocumentModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllDocumentByUserId(long userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var documentModel = _documentService.GetAllDocumentByUserId(userId, ref errorResponseModel);

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
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteDocument(int documentId)
        {
          
                var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                var LoggedUserId = Convert.ToInt32(userId);

                try
                {
                    var errorMessage = new ErrorResponseModel();
                    var documentModel = _documentService.DeleteDocument(documentId, LoggedUserId,  errorMessage);
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
        /// Add investor documents
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddInvestorDocument")]
        // [Authorize]
        [ProducesResponseType(typeof(FounderInvestorDocumentModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult AddInvestorDocument(IFormCollection formdata)
        {
            var model = new FounderInvestorDocumentModel();
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
                model.UserId = Convert.ToInt32(formdata["userId"]);
                model.DocumentName = formdata["documentName"];
                bool isExist = _documentService.CheckDocumentByUserId(model.UserId, model.DocumentName);
                if (isExist == false)
                {
                    if (Request.Form.Files.Count > 0)
                    {
                        var folder = _iwebhostingEnvironment.WebRootPath;
                        var files = Request.Form.Files;
                        foreach (var file in files)
                        {
                            var folderName = Path.Combine("Resources", "InvestorDocument");
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
                                model.FilePath = "/InvestorDocument/" + uniqueFileName;
                                model.FileName = fileName;
                            }
                        }
                    }
                    var documentModel = _documentService.AddDocument(model, ref errorMessage);
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
        /// <summary>
        /// download document
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpGet("DownloadDocumentByUserId/{userId}/{documentId}")]
        public async Task<IActionResult> DownloadDocumentByUserId(long userId, long documentId)
        {
            try
            {
                var documentModel = _documentService.GetDocumentByUserId(userId, documentId);
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
                    return File(memory, GetContentType(filePath), documentModel.FileName);
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
    }
}
