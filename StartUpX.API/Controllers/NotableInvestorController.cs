using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;

namespace StartUpX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotableInvestorController : BaseAPIController
    {

        INotableInvestorService _notableinvestorService;
        public NotableInvestorController(INotableInvestorService notableinvestorService)
        {
            _notableinvestorService = notableinvestorService;
        }
       
        [HttpGet]

        [ProducesResponseType(typeof(NotableInvestorModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAll()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var notableinvestorModel = _notableinvestorService.GetAllNotableInvestor();

                if (notableinvestorModel != null)
                {
                    return Ok(notableinvestorModel);
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
        /// <param name="ServiceId"></param>
        /// <returns></returns>
        [HttpGet("GetNotableInvestorById/{NotableInvestorId}")]
        //[Authorize]
        [ProducesResponseType(typeof(NotableInvestorModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(int NotableInvestorId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var notableinvestorModel = _notableinvestorService.GetNotableInvestorById(NotableInvestorId, ref errorResponseModel);

                if (notableinvestorModel != null)
                {
                    return Ok(notableinvestorModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
       
        [HttpPost]
        public IActionResult Post(NotableInvestorModel model)
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
                var notableinvestorModel = _notableinvestorService.AddNotableInvestor(model, ref errorMessage);
                if (!string.IsNullOrEmpty(notableinvestorModel))
                {
                    return Ok(notableinvestorModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
       
        [HttpPut("Edit")]
        public IActionResult Put(NotableInvestorModel model)
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
                var notableinvestorModel = _notableinvestorService.EditNotableInvestor(model, ref errorMessage);
                if (!string.IsNullOrEmpty(notableinvestorModel))
                {
                    return Ok(notableinvestorModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
       
        [HttpDelete]
        public IActionResult Delete(int notableinvestorId)
        {
            var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            var LoggedUserId = Convert.ToInt32(userId);
            try
            {
                var errorMessage = new ErrorResponseModel();
                var notableinvestorModel = _notableinvestorService.DeleteNotableInvestor(notableinvestorId, LoggedUserId, errorMessage);
                if (!string.IsNullOrEmpty(notableinvestorModel))
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
