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
    public class InvestmentOpportunityDetailsController : BaseAPIController
    {
        IInvestmentOpportunityDetailsService _investmentOpportunityDetailsService;
        public InvestmentOpportunityDetailsController(IInvestmentOpportunityDetailsService investmentOpportunityDetailsService)
        {
            _investmentOpportunityDetailsService = investmentOpportunityDetailsService;
        }
        /// <summary>
        /// Add Investment Opportunity details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("InvestmentOpportunity")]
        public IActionResult InvestmentOpportunity(InvestmnetopportunityModel model)
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
                var Model = _investmentOpportunityDetailsService.AddInvestmnetOpportunity(model, ref errorMessage);
                if (!string.IsNullOrEmpty(Model))
                {
                    return Ok(Model);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Get investment opportunity by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetInvestmentOpportunityById/{userId}")]
        //[Authorize]
        [ProducesResponseType(typeof(InvestmentOpportunityDetailsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var opportunityModel = _investmentOpportunityDetailsService.GetInvestmentOpportunityById(userId, ref errorResponseModel);

                if (opportunityModel != null)
                {
                    return Ok(opportunityModel);
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
