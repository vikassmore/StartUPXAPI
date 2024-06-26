using Microsoft.AspNetCore.Mvc;
using StartUpX.Business.Implementation;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;

namespace StartUpX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TrustedContactPersonController : BaseAPIController
    {


        ITrustedContactPersonService _trustedContactPersonService;

        public TrustedContactPersonController(ITrustedContactPersonService trustedContactPersonService)
        {
            _trustedContactPersonService = trustedContactPersonService;
        }
        /// <summary>
        /// Post Method Investor Detail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult Post(TrustedContactPersonModel model)
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
                var trustedContectPersonModel = _trustedContactPersonService.AddTrustedContact(model, ref errorMessage);
                if (!string.IsNullOrEmpty(trustedContectPersonModel))
                {
                    return Ok(trustedContectPersonModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// Getall Method Inveator  Detail
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        [ProducesResponseType(typeof(TrustedContactPersonModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAll()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var trustedContectPersonModel = _trustedContactPersonService.GetAllTrustedContact();

                if (trustedContectPersonModel != null)
                {
                    return Ok(trustedContectPersonModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// Get Investor Detail
        /// </summary>
        /// <param name="InvestorId"></param>
        /// <returns></returns>
        [HttpGet("GetTrustedContactById/{TrustedContectId}")]
        //[Authorize]
        [ProducesResponseType(typeof(TrustedContactPersonModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long TrustedContectId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var trustedContectPersonModel = _trustedContactPersonService.GetTrustedContactById(TrustedContectId, ref errorResponseModel);

                if (trustedContectPersonModel != null)
                {
                    return Ok(trustedContectPersonModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetTrustedContactByuserId/{userId}")]
        //[Authorize]
        [ProducesResponseType(typeof(TrustedContactPersonModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetByUserId(long userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var trustedContectPersonModel = _trustedContactPersonService.GetTrustedContactByuserId(userId, ref errorResponseModel);

                if (trustedContectPersonModel != null)
                {
                    return Ok(trustedContectPersonModel);
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
