using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StartUpX.Business.Implementation;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;
using System.Text.Json;

namespace StartUpX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InvestorDetailController : BaseAPIController
    {
        IInvestorDetailService _inveatorDetailServices;        

        public InvestorDetailController(IInvestorDetailService inveatorDetailServices)
        {
            _inveatorDetailServices = inveatorDetailServices;
        }
        /// <summary>
        /// Post Method Investor Detail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult Post([FromForm] IFormCollection formdata)
        {
            var a1 = formdata["investormodel"];
            InvestorDetailModel model = JsonSerializer.Deserialize<InvestorDetailModel>(a1);
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
                var investorModel = _inveatorDetailServices.AddInvestor(model,  ref errorMessage);
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
        /// Getall Method Inveator  Detail
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        [ProducesResponseType(typeof(InvestorDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAll()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var investorModel = _inveatorDetailServices.GetAllInvestor();

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
        /// Get Investor Detail
        /// </summary>
        /// <param name="InvestorId"></param>
        /// <returns></returns>
        [HttpGet("GetInvestorById/{UserId}")]
        //[Authorize]
        [ProducesResponseType(typeof(InvestorDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long UserId)
         {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var investorModel = _inveatorDetailServices.GetInvestorById(UserId, ref errorResponseModel);

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
        /// /
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetInestorByuserId/{userId}")]
        //[Authorize]
        [ProducesResponseType(typeof(InvestorDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetByUserId(long userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var investorModel = _inveatorDetailServices.GetInvestorByuserId(userId, ref errorResponseModel);

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


        ///// <summary>
        ///// Put method Startup Detail
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPut]
        //public IActionResult Put(InvestorDetailModel model)
        //{
        //    if (model == null || !ModelState.IsValid)
        //    {
        //        return BadRequest(GlobalConstants.InvalidRequest);
        //    }
        //    if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
        //    {
        //        var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
        //        model.LoggedUserId = Convert.ToInt32(userId);
        //    }
        //    try
        //    {
        //        var errorMessage = new ErrorResponseModel();
        //        var investorModel = _inveatorDetailServices.EditInvestor(model, ref errorMessage);
        //        if (!string.IsNullOrEmpty(investorModel))
        //        {
        //            return Ok(investorModel);
        //        }
        //        return ReturnErrorResponse(errorMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
        //    }
        //}
    }
}
