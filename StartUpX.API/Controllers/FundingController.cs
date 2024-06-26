using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;

namespace StartUpX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundingController : BaseAPIController
    {
        IFundingService _fundingService;
        public FundingController(IFundingService fundingService)
        {
            _fundingService = fundingService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        [ProducesResponseType(typeof(FundingModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAll()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var fundingModel = _fundingService.GetAllFundingDetails();

                if (fundingModel != null)
                {
                    return Ok(fundingModel);
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
        /// <param name="FundingId"></param>
        /// <returns></returns>
        
        [HttpGet("GetFundingById/{FundingId}")]
        //[Authorize]
        [ProducesResponseType(typeof(FundingModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long FundingId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var fundingModel = _fundingService.GetFundingById(FundingId, ref errorResponseModel);

                if (fundingModel != null)
                {
                    return Ok(fundingModel);
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
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(FundingModel model)
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
                var fundingModel = _fundingService.AddFundingDetails(model, ref errorMessage);
                if (!string.IsNullOrEmpty(fundingModel))
                {
                    return Ok(fundingModel);
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
        ///  </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Edit")]
        public IActionResult Put(FundingModel model)
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
                var fundingModel = _fundingService.EditFunding(model, ref errorMessage);
                if (!string.IsNullOrEmpty(fundingModel))
                {
                    return Ok(fundingModel);
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
        /// <param name="fundingId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteFunding")]
        public IActionResult DeleteFunding(int fundingId)
        {
            var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            var LoggedUserId = Convert.ToInt32(userId);
            try
            {
                var errorMessage = new ErrorResponseModel();
                var fundingModel = _fundingService.DeleteFunding(fundingId, LoggedUserId,  errorMessage);
                if (!string.IsNullOrEmpty(fundingModel))
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
    }
}
