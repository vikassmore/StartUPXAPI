using Microsoft.AspNetCore.Mvc;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;

namespace StartUpX.API.Controllers
{
    public class SuitabilityQuestionController : BaseAPIController
    {
        ISuitabilityQuestionService _suitabilityQuestionService;
        public SuitabilityQuestionController(ISuitabilityQuestionService suitabilityQuestionService)
        {
            _suitabilityQuestionService = suitabilityQuestionService;
        }
        /// <summary>
        /// Post Method Startup Detail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddSuitability")]
        [ProducesResponseType(typeof(SuitabilityQuestionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult AddSuitability(SuitabilityQuestionModel model)
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
                var suitabilityQuestionModel = _suitabilityQuestionService.AddSuitabilityQuestion(model, ref errorMessage);
                if (!string.IsNullOrEmpty(suitabilityQuestionModel))
                {
                    return Ok(suitabilityQuestionModel);
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

        [ProducesResponseType(typeof(SuitabilityQuestionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAll()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var suitabilityQuestionModel = _suitabilityQuestionService.GetAllSuitabilityQuestion();

                if (suitabilityQuestionModel != null)
                {
                    return Ok(suitabilityQuestionModel);
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
        [HttpGet("GetSuitabilityQuestionById/{squestionId}")]
        //[Authorize]
        [ProducesResponseType(typeof(SuitabilityQuestionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long squestionId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var suitabilityQuestionModel = _suitabilityQuestionService.GetSuitabilityQuestionById(squestionId, ref errorResponseModel);

                if (suitabilityQuestionModel != null)
                {
                    return Ok(suitabilityQuestionModel);
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
        [HttpGet("GetSuitabilityQuestionByuserId/{userId}")]
        //[Authorize]
        [ProducesResponseType(typeof(SuitabilityQuestionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetByUserId(long userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var suitabilityQuestionModel = _suitabilityQuestionService.GetSuitabilityQuestionByuserId(userId, ref errorResponseModel);

                if (suitabilityQuestionModel != null)
                {
                    return Ok(suitabilityQuestionModel);
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
