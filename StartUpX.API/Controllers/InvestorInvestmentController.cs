using Microsoft.AspNetCore.Mvc;
using StartUpX.Business.Implementation;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;

namespace StartUpX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestorInvestmentController : BaseAPIController
    {
        IInvestorInvestmentService _investorInvestmentService;
        public InvestorInvestmentController(IInvestorInvestmentService investorInvestmentService)
        {
            _investorInvestmentService = investorInvestmentService;
        }

        /// <summary>
        /// Get the Invested details by User ID
        /// </summary>
        /// <param name="investorUserId"></param>
        /// <returns></returns>
        [HttpGet("GetAllInvestmentById/{investorUserId}")]
        //[Authorize]
        [ProducesResponseType(typeof(InvestorDetailModelList), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long investorUserId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var investorModel = _investorInvestmentService.GetAllInvestmentById(investorUserId, ref errorResponseModel);

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
        /// Get the Invested details set as WatchList
        /// </summary>
        /// <param name="onWatch"></param>
        /// <param name="investorUserId"></param>
        /// <returns></returns>
        [HttpGet("GetAllInvestmentOnWatch/{onWatch}/{investorUserId}")]
        //[Authorize]
        [ProducesResponseType(typeof(InvestorDetailModelList), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(bool onWatch, long investorUserId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var investorModel = _investorInvestmentService.GetAllInvestmentOnWatch(onWatch,investorUserId, ref errorResponseModel);

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
        /// Add Indicate Interest of Investor
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddIndicateInterest")]
        public IActionResult AddIndicateInterest(InvestorInvestmentModel model)
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
                var investorModel = _investorInvestmentService.AddIndicateInterest(model, ref errorMessage);
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
        /// Add Investor Investment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddInvestorInvestment")]
        public IActionResult AddInvestorInvestment(InvestorInvestmentModel model)
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
                var investorModel = _investorInvestmentService.AddInvestorInvestment(model, ref errorMessage);
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
        /// Add Comapny as On Watch
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddOnWatch")]
        public IActionResult AddOnWatch(InvestorInvestmentModel model)
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
                var investorModel = _investorInvestmentService.AddOnWatch(model, ref errorMessage);
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
        /// Add to Request Offering
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddRequestOffering")]
        public IActionResult AddRequestOffering(InvestorInvestmentModel model)
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
                var investorModel = _investorInvestmentService.AddRequestOffering(model, ref errorMessage);
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
        /// Delete from watch list
        /// </summary>
        /// <param name="investorInvestmentId"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int investorInvestmentId)
        {

            var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            var LoggedUserId = Convert.ToInt32(userId);

            try
            {
                var errorMessage = new ErrorResponseModel();
                var watchModel = _investorInvestmentService.DeleteFromWatchlist(investorInvestmentId, LoggedUserId, errorMessage);
                if (!string.IsNullOrEmpty(watchModel))
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
