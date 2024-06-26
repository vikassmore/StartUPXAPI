using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StartUpX.Business.Implementation;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;

namespace StartUpX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestorVerificationController : BaseAPIController
    {
        IInvestorVerificationService _investorVerificationService;
        public InvestorVerificationController(IInvestorVerificationService investorVerificationService)
        {
            _investorVerificationService = investorVerificationService;
        }
        /// <summary>
        /// Get All Investor details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(InvestorDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllInvestorDetails()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var investorModel = _investorVerificationService.GetAllInvestorDetails(ref errorResponseModel);

                if (investorModel != null)
                {
                    return Ok(investorModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Get All verified investor details
        /// </summary>
        /// <param name="ServiceId"></param>
        /// <returns></returns>
        [HttpGet("GetAllVerifiedDetails/{verified}")]
        //[Authorize]
        [ProducesResponseType(typeof(InvestorDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(bool verified)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var investorModel = _investorVerificationService.GetAllVerifiedNonInvestorDetails(verified, ref errorResponseModel);

                if (investorModel != null)
                {
                    return Ok(investorModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// Get All investor details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="investorVerifyId"></param>
        /// <returns></returns>
        [HttpGet("GetAllDetailsById/{userId}/{investorVerifyId}")]
        //[Authorize]
        [ProducesResponseType(typeof(InvestorDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long userId, long investorVerifyId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var investorModel = _investorVerificationService.GetAllInvestorDetailsById(userId, investorVerifyId, ref errorResponseModel);

                if (investorModel != null)
                {
                    return Ok(investorModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Send for verification investor
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("SendForVerification")]
        public IActionResult SendForVerification(InvestorVerificationModel model)
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
                var investorModel = _investorVerificationService.SendForVerification(model, ref errorMessage);
                if (!string.IsNullOrEmpty(investorModel))
                {
                    return Ok(investorModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Approve/Notpprove investor
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("VerificationApprove")]
        public IActionResult VerificationApprove(InvestorVerificationModel model)
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
                var investorModel = _investorVerificationService.VerificationApprove(model, ref errorMessage);
                if (!string.IsNullOrEmpty(investorModel))
                {
                    return Ok(investorModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Founder Profile completion
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("InvestorProfileCompletion/{userId}")]
        //[Authorize]
        [ProducesResponseType(typeof(InvestorVerificationModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult InvestorProfileCompletion(long userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var investorModel = _investorVerificationService.InvestorProfileCompletion(userId, ref errorResponseModel);

                if (investorModel != null)
                {
                    return Ok(investorModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Investor Status Count
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("InvestorStatusCount")]
        //[Authorize]
        [ProducesResponseType(typeof(InvestorVerificationModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult InvestorStatusCount()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var investorModel = _investorVerificationService.InvestorStatusCount(ref errorResponseModel);

                if (investorModel != null)
                {
                    return Ok(investorModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Get All live preview founder details
        /// </summary>
        /// <param name="live"></param>
        /// <param name="preview"></param>
        /// <returns></returns>
        [HttpGet("GetAllLivePreviewStartupDetails/{live}/{preview}")]
        //[Authorize]
        [ProducesResponseType(typeof(FounderModelDetails), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(bool live, bool preview)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var startupModel = _investorVerificationService.GetAllLivePreviewStartupDetails(live, preview, ref errorResponseModel);

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
        /// Get All Startup By Sector
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetAllStartupDetailsBySector/{userId}")]
        //[Authorize]
        [ProducesResponseType(typeof(FounderModelDetails), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllStartupDetailsBySector(long userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var startupModel = _investorVerificationService.GetAllStartupDetailsBySector(userId, ref errorResponseModel);

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

