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
    public class StateController : BaseAPIController
    {
        IStateService _stateService;
        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }
        [HttpGet("GetStateById/{CountryId}")]
        //[Authorize]
        [ProducesResponseType(typeof(StateModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long CountryId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var stateModel = _stateService.GetStateById(CountryId, ref errorResponseModel);

                if (stateModel != null)
                {
                    return Ok(stateModel);
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
