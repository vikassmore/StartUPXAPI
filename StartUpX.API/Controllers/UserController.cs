using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StartUpX.Business.Implementation;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System.Web.Http.ModelBinding;

namespace StartUpX.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseAPIController
    {
        IUserService _userService;
        private readonly IOptions<SMTPSettingsModel> _emailSettings;

        public UserController(IUserService userService, IOptions<SMTPSettingsModel> emailSettings)
        {
            _userService = userService;
            _emailSettings = emailSettings;
        }
        /// <summary>
        /// User get method
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAll()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var servicemodel = _userService.GetAllUser();

                if (servicemodel != null)
                {
                    return Ok(servicemodel);
                }
                return (IActionResult)errorResponseModel;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// User
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>

        [HttpGet("{UserId}")]
        //[Authorize]
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(long UserId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var userModel = _userService.GetUserById(UserId, ref errorResponseModel);

                if (userModel != null)
                {
                    return Ok(userModel);
                }
                return (IActionResult)(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Create an user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult Post([FromForm] UserModel model)
        {

            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var emailSettings = _emailSettings.Value;

                var userModel = _userService.AddUser(model, emailSettings, ref errorMessage);
                if (userModel != null)
                {
                    return Ok(userModel);
                }
                return BadRequest(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        [HttpPut("Edit")]
       // [Authorize]
        public IActionResult Put([FromForm] UserModel model)
        {

            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var userModel = _userService.EditUser(model, ref errorMessage);

                if (!string.IsNullOrEmpty(userModel))
                {
                    return Ok(userModel);
                }
                return (IActionResult)errorMessage;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        [HttpPost("ForgotPasswordOTP")]
        public IActionResult ForgotPasswordOTP(string email)
        {
            ErrorResponseModel errorResponseModel = new ErrorResponseModel();
            if (!ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            try
            {
                string message = _userService.ForgotPasswordOTP(email, ref errorResponseModel);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        [HttpPatch("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            try
            {
                string message = _userService.ForgotPassword(model);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        /// <summary>
        /// Create an user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ActiveUser")]
        public IActionResult ActiveUser(UserActivateModel model)
        {
            try
            {
                var errorMessage = new ErrorResponseModel();
                var productmanualserviceModel = _userService.ActivateUser(model, ref errorMessage);
                if (!string.IsNullOrEmpty(productmanualserviceModel))
                {
                    return Ok(productmanualserviceModel);
                }
                return BadRequest(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int UserId)
        {

            try
            {
                var errorMessage = new ErrorResponseModel();
                var startupModel = _userService.DeleteUser(UserId, ref errorMessage);
                if (!string.IsNullOrEmpty(startupModel))
                {
                    return Ok(startupModel);
                }
                return ((IActionResult)errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ChangePassword")]
        [ProducesResponseType(typeof(ChangePassword), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [Authorize]
        public IActionResult ChangePassword(ChangePassword model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var emailSettings = _emailSettings.Value;

                var userModel = _userService.ChangePassword(model, ref errorMessage);
                if (userModel != null)
                {
                    return Ok(userModel);
                }
                return BadRequest(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usermodel"></param>
        /// <returns></returns>
        [HttpPost("AddserviceProvider")]
        //[Authorize(Roles = "Admin")]
        public IActionResult AddserviceProvider(ServiceProviderUserModel usermodel)
        {

            if (usermodel == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
                usermodel.LoggedUserId = Convert.ToInt32(userId);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var emailSettings = _emailSettings.Value;

                var userModel = _userService.AddServiceProviderUser(usermodel, emailSettings, ref errorMessage);
                if (userModel != null)
                {
                    return Ok(userModel);
                }
                return BadRequest(errorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <returns></returns>
        [HttpGet("GetServiceUserById/userId")]
        [ProducesResponseType(typeof(ServiceProviderUserModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(int userId)
        
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var Model = _userService.GetServiceProviderUserById(userId, ref errorResponseModel);

                if (Model != null)
                {
                    return Ok(Model);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        [HttpGet("GetAllServiceProviderUser")]
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllServiceProviderUser()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var model = _userService.GetAllServiceProviderUser();

                if (model != null)
                {
                    return Ok(model);
                }
                return (IActionResult)errorResponseModel;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        [HttpGet("GetAllRole")]
        [ProducesResponseType(typeof(RoleModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllRole()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var model = _userService.GetAllRole();

                if (model != null)
                {
                    return Ok(model);
                }
                return (IActionResult)errorResponseModel;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
        /// <summary>
        /// Activate or block service user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("EditServiceStatus")]
        // [Authorize]
        public IActionResult EditServiceStatus(ServiceStatusUpdateModel model)
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
                var userModel = _userService.EditServiceStatus(model, ref errorMessage);

                if (!string.IsNullOrEmpty(userModel))
                {
                    return Ok(userModel);
                }
                return (IActionResult)errorMessage;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        [HttpPut("EditServiceUser")]
        // [Authorize]
        public IActionResult EditServiceUser(ServiceProviderUserModel serviceusermodel)
        {

            if (serviceusermodel == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var userModel = _userService.EditServiceUser(serviceusermodel, ref errorMessage);

                if (!string.IsNullOrEmpty(userModel))
                {
                    return Ok(userModel);
                }
                return (IActionResult)errorMessage;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }
    }
}
