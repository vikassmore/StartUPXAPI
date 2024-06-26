using Microsoft.AspNetCore.Mvc;
using StartUpX.Business.Implementation;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;

namespace StartUpX.API.Controllers
{
    /// <summary>
    /// Api Founder Verification
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FounderVerificationController : BaseAPIController
    {
        IFounderVerificationService _founderVerificationService;
        public FounderVerificationController(IFounderVerificationService founderVerificationService)
        {
            _founderVerificationService = founderVerificationService;
        }
        /// <summary>
        /// Get All founder details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(FounderModelDetails), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllFounderDetails()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var founderModel = _founderVerificationService.GetAllFounderDetails(ref errorResponseModel);

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
        /// Get All verified founder details
        /// </summary>
        /// <param name="ServiceId"></param>
        /// <returns></returns>
        [HttpGet("GetAllVerifiedDetails/{verified}")]
        //[Authorize]
        [ProducesResponseType(typeof(FounderModelDetails), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(bool verified)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var founderModel = _founderVerificationService.GetAllVerifiedNonFounderDetails(verified, ref errorResponseModel);

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
        /// Get All live preview founder details
        /// </summary>
        /// <param name="ServiceId"></param>
        /// <returns></returns>
        [HttpGet("GetAllLivePreViewDetails/{live}/{preview}")]
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
                var founderModel = _founderVerificationService.GetAllLivePreviewFounderDetails(live, preview, ref errorResponseModel);

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
        /// Get All founder details
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="founderVerifyId"></param>
        /// <returns></returns>
        [HttpGet("GetAllDetailsById/{userId}/{founderVerifyId}")]
        //[Authorize]
        [ProducesResponseType(typeof(FounderModelDetails), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long userId, long founderVerifyId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var founderModel = _founderVerificationService.GetAllFounderDetailsById(userId,founderVerifyId, ref errorResponseModel);

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
        /// Get All IsStealth founder details
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllFounderIsStealthDetails")]
        [ProducesResponseType(typeof(FounderModelDetails), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllFounderIsStealthDetails()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var founderModel = _founderVerificationService.GetAllFounderIsStealthDetails(ref errorResponseModel);

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
        /// Send for verification founder
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("SendForVerification")]
        public IActionResult SendForVerification(FounderVerificationModel model)
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
                var founderModel = _founderVerificationService.SendForVerification(model, ref errorMessage);
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
        /// Approve/Notpprove founder
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("VerificationApprove")]
        public IActionResult VerificationApprove(FounderVerificationModel model)
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
                var founderModel = _founderVerificationService.VerificationApprove(model, ref errorMessage);
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
        /// Make it live founder
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("LiveFounder")]
        public IActionResult LiveFounder(FounderVerificationModel model)
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
                var founderModel = _founderVerificationService.LiveFounder(model, ref errorMessage);
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
        /// Make it preview founder
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("PreviewFounder")]
        public IActionResult PreviewFounder(FounderVerificationModel model)
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
                var founderModel = _founderVerificationService.PreviewFounder(model, ref errorMessage);
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
        /// Founder Profile completion
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("FounderProfileCompletion/{userId}")]
        //[Authorize]
        [ProducesResponseType(typeof(FounderVerificationModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult FounderProfileCompletion(long userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var founderModel = _founderVerificationService.FounderProfileCompletion(userId, ref errorResponseModel);

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
        /// Founder Status Count
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("FounderStatusCount")]
        //[Authorize]
        [ProducesResponseType(typeof(FounderVerificationModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult FounderStatusCount()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var founderModel = _founderVerificationService.FounderStatusCount(ref errorResponseModel);

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
        /// Request Raise Funding
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("RequestRaiseFunding")]
        public IActionResult RequestRaiseFunding(FounderVerificationModel model)
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
                var founderModel = _founderVerificationService.RequestRaiseFunding(model, ref errorMessage);
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
        /// Get All Raise funding founder details
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllFounderRaiseDetails")]
        [ProducesResponseType(typeof(FounderModelDetails), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllFounderRaiseDetails()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var founderModel = _founderVerificationService.GetAllFounderRaiseDetails(ref errorResponseModel);

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
