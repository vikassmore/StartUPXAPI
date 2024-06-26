using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StartUpX.Business.Implementation;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System.Text.Json;

namespace StartUpX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FounderDetailController : BaseAPIController
    {
        IFounderDetailService _founderDetailService;
        public FounderDetailController(IFounderDetailService founderDetailService)
        {
            _founderDetailService = founderDetailService;
        }

        /// <summary>
        /// Post Method Founder Detail
        /// </summary>
        /// <param name="formdata"></param>
        /// <returns></returns>
        [HttpPost]
        // [Authorize(Roles = "Admin")]
        //[Authorize]
        public IActionResult Post(List<FounderDeatailModel> model)
        {
            int LoggedUserId = 0;

            if (model == null || !ModelState.IsValid)
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
                string founderModel = "";

                founderModel = _founderDetailService.AddFounder(model, LoggedUserId, ref errorMessage);
                if (founderModel != null)
                {
                    return Ok(founderModel);
                }

                return ReturnErrorResponse(errorMessage);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost("Edit")]
        public IActionResult Put(FounderDeatailModel model)
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
                var founderModel = _founderDetailService.EditFounder(model, ref errorMessage);
                if (!string.IsNullOrEmpty(founderModel))
                {
                    return Ok(founderModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetAllFounderbyuserId/{userId}")]
        [ProducesResponseType(typeof(FounderDeatailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllByUserId(int userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var founderModel = _founderDetailService.GetAllFounderbyuserId(userId);

                if (founderModel != null)
                {
                    return Ok(founderModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// GetFounderById Method Fundinfg Data
        /// </summary>
        /// <param name="FounderId"></param>
        /// <returns></returns>
        [HttpGet("GetFounderById/{FounderId}")]
        [ProducesResponseType(typeof(FounderDeatailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long FounderId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var founderModel = _founderDetailService.GetFounderById(FounderId, ref errorResponseModel);

                if (founderModel != null)
                {
                    return Ok(founderModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="founderId"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int founderId)
        {
            try
            {
                int LoggedUserId = 0;
                var errorMessage = new ErrorResponseModel();
                if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                {
                    var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                    LoggedUserId = Convert.ToInt32(userId);
                }
                var founderModel = _founderDetailService.DeleteFounder(founderId, LoggedUserId, errorMessage);
                if (!string.IsNullOrEmpty(founderModel))
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
        [HttpGet]
        [ProducesResponseType(typeof(FounderDeatailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAll()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var founderModel = _founderDetailService.GetAllFounder();

                if (founderModel != null)
                {
                    return Ok(founderModel);
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


