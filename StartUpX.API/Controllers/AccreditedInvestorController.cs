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
    public class AccreditedInvestorController : BaseAPIController
    {
        IAccreditedInvestor _accreditedinvestorService;
        public AccreditedInvestorController(IAccreditedInvestor accreditedService)
        {
            _accreditedinvestorService = accreditedService;
        }


        [HttpGet("GetAllAccreditdInvestor")]

        [ProducesResponseType(typeof(AccreditedInvestorModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAll()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var AccreditdInvestorModel = _accreditedinvestorService.GetAllAccreditdInvestor();

                if (AccreditdInvestorModel != null)
                {
                    return Ok(AccreditdInvestorModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        [HttpGet("GetAccreditdInvestorById/{accreditedId}")]
        //[Authorize]
        [ProducesResponseType(typeof(AccreditedInvestorModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long accreditedId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var AccreditdInvestorModel = _accreditedinvestorService.GetAccreditedInvestorById(accreditedId, ref errorResponseModel);

                if (AccreditdInvestorModel != null)
                {
                    return Ok(AccreditdInvestorModel);
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
        public IActionResult Post(AccreditedInvestorModel model)
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
                var AccreditdInvestorModel = _accreditedinvestorService.AddAccreditedInvestor(model, ref errorMessage);
                if (!string.IsNullOrEmpty(AccreditdInvestorModel))
                {
                    return Ok(AccreditdInvestorModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        [HttpPost("Edit")]
        public IActionResult Put(AccreditedInvestorModel model)
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
                var AccreditdInvestorModel = _accreditedinvestorService.EditAccreditedInvestor(model, ref errorMessage);
                if (!string.IsNullOrEmpty(AccreditdInvestorModel))
                {
                    return Ok(AccreditdInvestorModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Delete Sector Data
        /// </summary>
        /// <param name="accreditedId"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int accreditedId)
        {
            try
            {
                var errorMessage = new ErrorResponseModel();
                var AccreditdInvestorModel = _accreditedinvestorService.DeleteAccreditedInvestor(accreditedId, errorMessage);
                if (!string.IsNullOrEmpty(AccreditdInvestorModel))
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

