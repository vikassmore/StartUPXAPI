
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using StartUpX.Model;
using StartUpX.Common;

namespace StartUpX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseAPIController : Controller
    {
        /// <summary>
        /// This will create an error response as per status
        /// </summary>
        /// <param name="model"></param>
        /// <param name="customErrorMessage"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
       public  IActionResult ReturnErrorResponse(ErrorResponseModel model, string customErrorMessage = null)
        {
            if (model != null)
            {
                var errorMessage = string.IsNullOrEmpty(customErrorMessage) ? model.Message : customErrorMessage;
                switch (model.StatusCode)
                {
                    case HttpStatusCode.BadGateway:
                        return BadRequest(errorMessage);
                    case HttpStatusCode.ServiceUnavailable:
                        return StatusCode(StatusCodes.Status503ServiceUnavailable, GlobalConstants.Status503Message);
                    default:
                        return BadRequest(errorMessage);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
    }
}
