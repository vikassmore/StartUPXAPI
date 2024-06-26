using Microsoft.AspNetCore.Mvc;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;

namespace StartUpX.API.Controllers
{
    /// <summary>
    /// Api Startup Detail
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BankdetailController : BaseAPIController
    {
        IBankDetailService _bankDetailService;
        public BankdetailController(IBankDetailService bankDetailService)
        {
            _bankDetailService = bankDetailService;
        }
        /// <summary>
        /// Post Method Startup Detail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult Post(BankAccountDetailModel model)
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
                var bankDetailModel = _bankDetailService.AddBankDetail(model, ref errorMessage);
                if (!string.IsNullOrEmpty(bankDetailModel))
                {
                    return Ok(bankDetailModel);
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

        [ProducesResponseType(typeof(BankAccountDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAll()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var bankDetailModel = _bankDetailService.GetAllBankDetail();

                if (bankDetailModel != null)
                {
                    return Ok(bankDetailModel);
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
        [HttpGet("GetBankDetailById/{StartupId}")]
        //[Authorize]
        [ProducesResponseType(typeof(BankAccountDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long StartupId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var bankDetailModel = _bankDetailService.GetBankDetailById(StartupId, ref errorResponseModel);

                if (bankDetailModel != null)
                {
                    return Ok(bankDetailModel);
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
        [HttpGet("GetBankDetailByuserId/{userId}")]
        //[Authorize]
        [ProducesResponseType(typeof(BankAccountDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetByUserId(long userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var bankDetailModel = _bankDetailService.GetBankDetailByuserId(userId, ref errorResponseModel);

                if (bankDetailModel != null)
                {
                    return Ok(bankDetailModel);
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
