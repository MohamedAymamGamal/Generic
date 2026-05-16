using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers.v1
{
    [Route("api/v1/account")]

    public class AccountController : BaseController
    {
        public AccountController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {

        }

        [HttpPost("register")]

        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var token = await work.Auth.RegisterAsync(registerDto);

            if (token == null || token.Contains(" ")) 
                return BadRequest(new { status = 400, message = "Registration failed", token });

            return Ok(new ResponseAPI(200, "Registration successful. Please check your email to confirm your account.",token));

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await work.Auth.LoginAsync(loginDto);
            if (result == null)
            {
                return BadRequest(new ResponseAPI(400, result));
            }

            Response.Cookies.Append("token", result, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Domain = "localhost",
                Expires = DateTime.Now.AddDays(1)
            });
            return Ok(new ResponseAPI(200, result));



        }
        [HttpPost("forget-password")]

        public async Task<IActionResult> ForgetPassword(string email)
        {
            var result = await work.Auth.SendEmailForForgetPassword(email);
            if (result is not true)
            {
                return BadRequest(new ResponseAPI(400, "Email not found"));
            }
            return Ok(new ResponseAPI(200, "Email sent successfully"));
        }

        public async Task<IActionResult> ResetPassword(RestPasswordDto restPasswordDto)
        {
            var result = await work.Auth.ResetPassword(restPasswordDto);
            if (result == null)
            {
                return BadRequest(new ResponseAPI(400, "Invalid token or email"));
            }
            return Ok(new ResponseAPI(200, result));
        }



        [HttpPost("active-account")]
        public async Task<ActionResult<ActiveAccountDto>> active(ActiveAccountDto accountDTO)
        {
            var result = await work.Auth.ActiveAccount(accountDTO);
            return result ? Ok(new ResponseAPI(200)) : BadRequest(new ResponseAPI(200));
        }

        [HttpGet("send-email-forget-password")]
        public async Task<IActionResult> forget(string email)
        {
            var result = await work.Auth.SendEmailForForgetPassword(email);
            return result ? Ok(new ResponseAPI(200)) : BadRequest(new ResponseAPI(200));
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("token", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Domain = "localhost",
            });
            return Ok(new ResponseAPI(200, "Logged out successfully"));
        }
    }
}
