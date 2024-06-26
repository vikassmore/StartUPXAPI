using Microsoft.AspNetCore.Mvc;
using StartUpX.Business.Implementation;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;

namespace StartUpX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FounderTypeController : BaseAPIController
    {
        IFounderTypeServicecs _founderTypeService;
            public FounderTypeController(IFounderTypeServicecs founderTypeService)
            {
               _founderTypeService = founderTypeService;
            }

        [HttpGet]
        [ProducesResponseType(typeof(FounderTypeModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAll()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var founderTypeModel = _founderTypeService.GetAllfounderType();

                if (founderTypeModel != null)
                {
                    return Ok(founderTypeModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        [HttpGet("GetFounderTypeByUserId/UserId")]
        //[Authorize]
        [ProducesResponseType(typeof(FounderTypeModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(int userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var founderTypeModel = _founderTypeService.GetFounderTypeByUserId(userId, ref errorResponseModel);

                if (founderTypeModel != null)
                {
                    return Ok(founderTypeModel);
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
