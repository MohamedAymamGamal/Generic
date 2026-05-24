using Ecom.Core.DTO;
using Ecom.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Interfaces
{
    public  interface IAuth
    {
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto login);
        Task<bool> SendEmailForForgetPassword(ForgetPasswordDto forgetPasswordDto);
        Task<string> ResetPassword(RestPasswordDto restPasswordDto,string token);
        Task<bool> ActiveAccount(ActiveAccountDto accountDto);

        Task<string> verifyOpt(verifyOtpDto dto);

    }
}
