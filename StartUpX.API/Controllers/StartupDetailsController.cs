using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StartUpX.Business.Implementation;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;
using System.Security.Claims;
using System.Text.Json;

namespace StartUpX.API.Controllers
{
    /// <summary>
    /// Api Startup Detail
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class StartupDetailsController : BaseAPIController
    {
        IStartupDetailsService _startupDetailsService;
        public StartupDetailsController(IStartupDetailsService startupDetailsService)
        {
            _startupDetailsService = startupDetailsService;
        }
        /// <summary>
        /// Post Method Startup Detail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(StartupDeatailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Post( [FromForm]IFormCollection formdata)
        {

            var a1 = formdata["statupmodel"];
            StartupDeatailModel model = JsonSerializer.Deserialize<StartupDeatailModel>(a1);
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
                byte[] Logo = null;
                var fileName="";
                if (Request?.Form?.Files?.Count > 0)
                {
                    var files = Request.Form.Files;
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            fileName = file.FileName;
                            MemoryStream ms = new MemoryStream();
                            file.CopyTo(ms);
                            Logo = ms.ToArray();
                          
                        }
                    }
                }
                var errorMessage = new ErrorResponseModel();
                var startupModel = _startupDetailsService.AddStartup(model, Logo,fileName, ref errorMessage);
                if (!string.IsNullOrEmpty(startupModel))
                {
                    return Ok(startupModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// Getall Method Startup Detail
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        [ProducesResponseType(typeof(StartupDeatailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAll()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var startupmodel = _startupDetailsService.GetAllStartup();

                if (startupmodel != null)
                {
                    return Ok(startupmodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }


        /// <summary>
        /// GetStartupById Satrtup Detail
        /// </summary>
        /// <param name="StartupId"></param>
        /// <returns></returns>
        [HttpGet("GetStartupById/{StartupId}")]
        //[Authorize]
        [ProducesResponseType(typeof(StartupDeatailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long StartupId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var startupModel = _startupDetailsService.GetStartupById(StartupId, ref errorResponseModel);

                if (startupModel != null)
                {
                    return Ok(startupModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// GetStartupById Satrtup Detail
        /// </summary>
        /// <param name="StartupId"></param>
        /// <returns></returns>
        [HttpGet("GetStartupByuserId/{userId}")]
        //[Authorize]
        [ProducesResponseType(typeof(StartupDeatailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetByUserId(long userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var startupModel = _startupDetailsService.GetStartupByuserId(userId, ref errorResponseModel);

                if (startupModel != null)
                {
                    return Ok(startupModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }


    }
}
