using Microsoft.AspNetCore.Authorization;
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
    public class InvestmentDetailController : BaseAPIController
    {
        IInvestmentDetailService _investmentDetailServices;

        public InvestmentDetailController(IInvestmentDetailService investmentDetailServices)
        {
            _investmentDetailServices = investmentDetailServices;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult Post([FromForm] InvestmentDetailModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }        
            try
            {
                var errorMessage = new ErrorResponseModel();
                var investmentModel = _investmentDetailServices.AddInvestmentDetail(model, ref errorMessage);
                if (!string.IsNullOrEmpty(investmentModel))
                {
                    return Ok(investmentModel);
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

        [ProducesResponseType(typeof(InvestmentDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAll()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var investmentModel = _investmentDetailServices.GetAllInvestmentDetail();

                if (investmentModel != null)
                {
                    return Ok(investmentModel);
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
        /// <param name="InvestmentId"></param>
        /// <returns></returns>
        [HttpGet("GetInvestmentDetailById/{InvestmentId}")]
        //[Authorize]
        [ProducesResponseType(typeof(InvestmentDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long InvestmentId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var investmentModel = _investmentDetailServices.GetInvestmentDetailById(InvestmentId, ref errorResponseModel);

                if (investmentModel != null)
                {
                    return Ok(investmentModel);
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
        [HttpGet("GetInvestmentDetailByuserId/{userId}")]
        //[Authorize]
        [ProducesResponseType(typeof(InvestmentDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetByUserId(long userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var investmentModel = _investmentDetailServices.GetInvestmentDetailByuserId(userId, ref errorResponseModel);

                if (investmentModel != null)
                {
                    return Ok(investmentModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }


        /// <summary>
        /// Put method Startup Detail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Edit")]
        public IActionResult Put([FromForm]InvestmentDetailModel model)
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
                var investmentModel = _investmentDetailServices.EditInvestmentDetail(model, ref errorMessage);
                if (!string.IsNullOrEmpty(investmentModel))
                {
                    return Ok(investmentModel);
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
