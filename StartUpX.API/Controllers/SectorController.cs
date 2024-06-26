using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;

namespace StartUpX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorController : BaseAPIController
    {
        ISectorService _sectorService;
        public SectorController(ISectorService sectorService)
        {
            _sectorService = sectorService;
        }
        /// <summary>
        /// Get sector Data
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        [ProducesResponseType(typeof(SectorModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAll()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var sectormodel = _sectorService.GetAllSectors();

                if (sectormodel != null)
                {
                    return Ok(sectormodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// GetSectorById sector data
        /// </summary>
        /// <param name="SectorId"></param>
        /// <returns></returns>
        [HttpGet("GetSectorById/{SectorId}")]
        //[Authorize]
        [ProducesResponseType(typeof(SectorModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long SectorId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var sectormodel = _sectorService.GetSectorById(SectorId, ref errorResponseModel);

                if (sectormodel != null)
                {
                    return Ok(sectormodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// Post  service Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(SectorModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var sectorModel = _sectorService.AddSectors(model, ref errorMessage);
                if (!string.IsNullOrEmpty(sectorModel))
                {
                    return Ok(sectorModel);
                }
                return ReturnErrorResponse(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// Edit Sector Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Edit")]
        public IActionResult Put(SectorModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var sectorModel = _sectorService.EditSector(model, ref errorMessage);
                if (!string.IsNullOrEmpty(sectorModel))
                {
                    return Ok(sectorModel);
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
        /// <param name="sectorId"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int sectorId)
        {
            var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            var LoggedUserId = Convert.ToInt32(userId);
            try
            {
                var errorMessage = new ErrorResponseModel();
                var sectorModel = _sectorService.DeleteSector(sectorId, LoggedUserId, errorMessage);
                if (!string.IsNullOrEmpty(sectorModel))
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
