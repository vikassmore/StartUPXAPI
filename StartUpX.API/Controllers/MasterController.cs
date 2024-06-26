using Microsoft.AspNetCore.Mvc;
using StartUpX.Business.Implementation;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;

namespace StartUpX.API.Controllers
{
    /// <summary>
    /// Api Master
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : BaseAPIController
    {
        IMasterService _masterService;
        INotificationService _notificationService;
        public MasterController(IMasterService masterService, INotificationService notificationService)
        {
            _masterService = masterService;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Get All Faq Data
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllFAQ")]

        [ProducesResponseType(typeof(FAQModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllFAQ()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var faqModel = _masterService.GetAllFAQ();

                if (faqModel != null)
                {
                    return Ok(faqModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Get Faq By Id 
        /// </summary>
        /// <param name="faqMasterId"></param>
        /// <returns></returns>
        [HttpGet("GetFAQById/{faqMasterId}")]
        public IActionResult GetFAQById(long faqMasterId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
               var faqModel = _masterService.GetFAQById(faqMasterId, ref errorResponseModel);

                if (faqModel != null)
                {
                    return Ok(faqModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// Add/Update faq Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddFAQ")]
        [ProducesResponseType(typeof(FAQModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult AddFAQ(FAQModel model)
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
                var faqModel = _masterService.AddFAQ(model, ref errorMessage);
                if (!string.IsNullOrEmpty(faqModel))
                {
                    return Ok(faqModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Delete faq Data
        /// </summary>
        /// <param name="faqMasterId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteFAQ")]
        public IActionResult DeleteFAQ(int faqMasterId)
        {

            var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            var LoggedUserId = Convert.ToInt32(userId);
            try
            {
                var errorMessage = new ErrorResponseModel();
                var faqModel = _masterService.DeleteFAQ(faqMasterId, LoggedUserId, errorMessage);
                if (!string.IsNullOrEmpty(faqModel))
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
        /// Get All Policy Data
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllPolicy")]

        [ProducesResponseType(typeof(PolicyModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllPolicy()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var policyModel = _masterService.GetAllPolicy();

                if (policyModel != null)
                {
                    return Ok(policyModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Get Policy By Id 
        /// </summary>
        /// <param name="policyId"></param>
        /// <returns></returns>
        [HttpGet("GetPolicyById/{policyId}")]
        public IActionResult GetPolicyById(long policyId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var policyModel = _masterService.GetPolicyById(policyId, ref errorResponseModel);

                if (policyModel != null)
                {
                    return Ok(policyModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// Add/Update Policy Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddPolicy")]
        [ProducesResponseType(typeof(PolicyModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult AddPolicy(PolicyModel model)
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
                var policyModel = _masterService.AddPolicy(model, ref errorMessage);
                if (!string.IsNullOrEmpty(policyModel))
                {
                    return Ok(policyModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Delete policy Data
        /// </summary>
        /// <param name="policyId"></param>
        /// <returns></returns>
        [HttpDelete("DeletePolicy")]
        public IActionResult DeletePolicy(int policyId)
        {
            var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            var LoggedUserId = Convert.ToInt32(userId);
            try
            {
                var errorMessage = new ErrorResponseModel();
                var policyModel = _masterService.DeletePolicy(policyId, LoggedUserId, errorMessage);
                if (!string.IsNullOrEmpty(policyModel))
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
        /// Get All Employee Count
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllEmployeeCount")]
        [ProducesResponseType(typeof(EmployeeCountModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllEmployeeCount()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var employeeCountModel = _masterService.GetAllEmployeeCount(ref errorResponseModel);

                if (employeeCountModel != null)
                {
                    return Ok(employeeCountModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Get Notification By Id 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetNotificationByUserId/{userId}")]
        public IActionResult GetNotificationByUserId(int userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var notificationModel = _notificationService.GetNotificationByUserId(userId, ref errorResponseModel);

                if (notificationModel != null)
                {
                    return Ok(notificationModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Delete Notification 
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteNotification")]
        public IActionResult DeleteNotification(int notificationId)
        {
            try
            {
                var errorMessage = new ErrorResponseModel();
                var categoryModel = _notificationService.DeleteNotification(notificationId, errorMessage);
                if (!string.IsNullOrEmpty(categoryModel))
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
