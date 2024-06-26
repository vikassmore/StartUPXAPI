using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StartUpX.Business.Implementation;
using StartUpX.Business.Interface;
using StartUpX.Common;
using StartUpX.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StartUpX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaLoginController : BaseAPIController
    {
        ISocialMediaLogin _socialmedialogin;
        public SocialMediaLoginController(ISocialMediaLogin socialmedialogin)
        {
            _socialmedialogin = socialmedialogin;
        }
        [HttpGet("GetSocialMedialUserByEmail/emailId")]
        //[Authorize]
        [ProducesResponseType(typeof(SocialMediaUserLoginModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(string emailId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {

                var socialmediamodel = _socialmedialogin.GetSocialMedialUserByEmail( emailId, ref errorResponseModel);

                if (socialmediamodel != null)
                {
                    return Ok(socialmediamodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, GlobalConstants.Status500Message);
            }
        }

        [HttpPost]
        public IActionResult Post(SocialMediaUserLoginModel socialmediaLogin)
        {
            if (socialmediaLogin == null || !ModelState.IsValid)
            {
                return BadRequest(GlobalConstants.InvalidRequest);
            }
            try
            {
                var errorMessage = new ErrorResponseModel();
                var socialModel = _socialmedialogin.AddSocialMediaUser(socialmediaLogin, ref errorMessage);
                if (socialModel != null)
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, socialModel.UserId.ToString()),
                        new Claim(ClaimTypes.Role, socialModel.RoleName),
                    };

                    var userModel = new UserResponse();

                    userModel.token = socialModel.Token;
                    userModel.Email = socialModel.Email;
                    userModel.UserId = socialModel.UserId;
                    userModel.RoleId = socialModel.RoleId;
                    userModel.RoleName = socialModel.RoleName;
                    userModel.FounderName = socialModel.FounderName;
                    return Ok(userModel);
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
