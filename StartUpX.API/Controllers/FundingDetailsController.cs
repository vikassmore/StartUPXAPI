using Microsoft.AspNetCore.Mvc;
using StartUpX.Business.Implementation;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;

namespace StartUpX.API.Controllers
{
    /// <summary>
    ///  API Funding Detail
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FundingDetailsController : BaseAPIController
    {
        IFundingDetailsServices _fundingDetailsService;
        public FundingDetailsController(IFundingDetailsServices fundingDetailsService)
        {
            _fundingDetailsService = fundingDetailsService;
        }

        /// <summary>
        /// Get All Funding Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        [ProducesResponseType(typeof(FundingDetailsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAll()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var fundingDetailsModel = _fundingDetailsService.GetAllFundingDetails();

                if (fundingDetailsModel != null)
                {
                    return Ok(fundingDetailsModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Post Method Funding Detail 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddFunding")]
        [ProducesResponseType(typeof(FundingDetailsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult AddFunding(FundingDetailsModel model)
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
                var fundingDetailsModel = _fundingDetailsService.AddFundingDetails(model, ref errorMessage);
                if (!string.IsNullOrEmpty(fundingDetailsModel))
                {
                    return Ok(fundingDetailsModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// Getall Funding Detail
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllFundingbyuserId/{userId}")]

        [ProducesResponseType(typeof(FundingDetailsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllByUserId(int userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var fundingDetailsModel = _fundingDetailsService.GetAllFundingbyuserId(userId);

                if (fundingDetailsModel != null)
                {
                    return Ok(fundingDetailsModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }


        /// <summary>
        /// GetFundingDetailsById Funding Detail
        /// </summary>
        /// <param name="FundingDetailsId"></param>
        /// <returns></returns>
        [HttpGet("GetFundingDetailsById/{FundingDetailsId}")]
        //[Authorize]
        [ProducesResponseType(typeof(FundingDetailsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long FundingDetailsId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var fundingDetailsModel = _fundingDetailsService.GetFundingDetailsById(FundingDetailsId, ref errorResponseModel);

                if (fundingDetailsModel != null)
                {
                    return Ok(fundingDetailsModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// Put Method Funding Detail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put(FundingDetailsModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var fundingDetailsModel = _fundingDetailsService.EditFundingDetails(model, ref errorMessage);
                if (!string.IsNullOrEmpty(fundingDetailsModel))
                {
                    return Ok(fundingDetailsModel);
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
