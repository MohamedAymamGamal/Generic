using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

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
            var register = await work.Auth.RegisterAsync(registerDto);

            if (register == null || register.Contains(" ")) 
                return BadRequest(new { status = 400, message = register });

            return Ok(new ResponseAPI(200, message: "the user has been registered successfully.", token: register));

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
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto forgetPasswordDto)
        {
            var result = await work.Auth.SendEmailForForgetPassword(forgetPasswordDto);
            if (result is not true)
            {
                return BadRequest(new ResponseAPI(400, "Email not found"));
            }
            return Ok(new ResponseAPI(200, "Email sent successfully"));
        }

        [HttpPost("rest-password")]
        public async Task<IActionResult> ResetPassword(RestPasswordDto restPasswordDto)
        {
            var result = await work.Auth.ResetPassword(restPasswordDto);
            if (result == null)
            {
                return BadRequest(new ResponseAPI(400, "Invalid token or email" )) ;
            }
            return Ok(new ResponseAPI(200, result));
        }



        [HttpPost("active-account")]
        public async Task<ActionResult<ActiveAccountDto>> active(ActiveAccountDto accountDTO)
        {
            var result = await work.Auth.ActiveAccount(accountDTO);

            return result
                 ? Ok(new ResponseAPI(200, message: "Account activated successfully."))
                 : BadRequest(new ResponseAPI(400, message: "Invalid or expired OTP. A new code has been sent to your email."));
         }


        //public async Task<IActionResult> forget(ForgetPasswordDto forgetPasswordDto)
        //{
        //    var result = await work.Auth.SendEmailForForgetPassword(forgetPasswordDto);
        //    return result ? Ok(new ResponseAPI(200)) : BadRequest(new ResponseAPI(200));
        //}


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("token", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
            });
            return Ok(new ResponseAPI(200, "Logged out successfully"));
        }
    }
}



