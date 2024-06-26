using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.IdentityModel.Tokens;
using StartUpX.Business.Implementation;
using StartUpX.Business.Interface;
using StartUpX.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Http.ModelBinding;
using StartUpX.API.Controllers;
using UserResponse = StartUpX.Model.UserResponse;
using System.Net;

namespace StartUpX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseAPIController
    {
        private readonly IEmailSender _emailSender;

        IAuthService _authService;
        IConfiguration _configuration;
        ICreateOTPandSendMessage _createOTPandSendMessage;
        public LoginController(IAuthService authService, IConfiguration configuration, ICreateOTPandSendMessage createOTPandSendMessage)
        {
            _authService = authService;
            _configuration = configuration;
            _createOTPandSendMessage = createOTPandSendMessage;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(LoginModel model)
        {
            var authModel = new AuthModel();
            try
            {
                ErrorResponseModel errorResponseModel = null;
                if (!ModelState.IsValid)
                {
                    var errorMessage = string.Join(",", ModelState.Values.ToList());
                    return BadRequest(new { message = errorMessage });
                }
                if (!string.IsNullOrEmpty(model.Email))
                {
                    authModel = _authService.EmailLogin(model.Email, model.Password, ref errorResponseModel);


                    if (authModel.StatusCode == HttpStatusCode.OK)
                    {
                        var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, authModel.UserId.ToString()),
                        new Claim(ClaimTypes.Role, authModel.RoleName),
                    };

                        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                        var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddDays(1),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );
                        var userToken = new JwtSecurityTokenHandler().WriteToken(token);
                        var userModel = new UserResponse();
                        userModel.token = userToken;
                        userModel.FirstName = authModel.FirstName;
                        userModel.LastName = authModel.LastName;
                        userModel.Email = authModel.Email;
                        userModel.UserId = authModel.UserId;
                        userModel.RoleId = authModel.RoleId;
                        userModel.RoleName = authModel.RoleName;
                        userModel.StatusCode = authModel.StatusCode;
                        userModel.FounderName = authModel.FounderName;
                        return Ok(userModel);
                    }
                    else
                    {
                        return Ok(authModel);
                    }
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        // Social midia to login

        [AllowAnonymous]
        [HttpPost("AuthenticateByEmail/{email}")]
        public IActionResult AuthenticateByEmail(string email)
        {
            var authModel = new AuthModel();
            try
            {
                ErrorResponseModel errorResponseModel = null;
                if (!ModelState.IsValid)
                {
                    var errorMessage = string.Join(",", ModelState.Values.ToList());
                    return BadRequest(new { message = errorMessage });
                }
                if (!string.IsNullOrEmpty(email))
                {
                    authModel = _authService.LoginByEmail(email, ref errorResponseModel);


                    if (authModel != null)
                    {
                        var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, authModel.UserId.ToString()),
                        new Claim(ClaimTypes.Role, authModel.RoleName),
                    };

                        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                        var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddDays(1),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );
                        var userToken = new JwtSecurityTokenHandler().WriteToken(token);


                        var userModel = new UserResponse();

                        userModel.token = userToken;
                        //userModel.FirstName = authModel.FirstName;
                        //userModel.LastName = authModel.LastName;
                        userModel.Email = authModel.Email;
                        userModel.UserId = authModel.UserId;
                        userModel.RoleId = authModel.RoleId;
                        userModel.RoleName = authModel.RoleName;
                        userModel.FounderName = authModel.FounderName;
                        return Ok(userModel);
                    }
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //Otp verify
        [HttpPost("VerifyOtp")]
        public IActionResult VerifyOtp(string otp, string email)
        {
            var authModel = new AuthModel();
            ErrorResponseModel errorResponseModel = null;
            try
            {
                if (string.IsNullOrEmpty(otp))
                {
                    var errorMessage = string.Join(",", ModelState.Values.ToList());
                    return BadRequest(new { message = errorMessage });
                }
                var isuserVerify = _authService.OTPVerify(otp, email, ref errorResponseModel);



                if (isuserVerify != null)
                {
                    var OTPModel = new OTPModel();

                    OTPModel.UserId = isuserVerify.UserId;
                    return Ok(OTPModel);
                }

                return ReturnErrorResponse(errorResponseModel);
            }


            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
        //Otp verify
        [HttpPost("ReSentOtp")]
        public IActionResult ReSentOtp(string email)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    var errorMessage = string.Join(",", ModelState.Values.ToList());
                    return BadRequest(new { message = errorMessage });
                }
                var reSentOtp = _createOTPandSendMessage.CreateOTPandSendMessage(email, ref errorResponseModel);
                if (reSentOtp != null)
                {
                    return Ok(reSentOtp);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }



}
