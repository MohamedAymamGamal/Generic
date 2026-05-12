using Ecom.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Interfaces
{
    public  interface IAuth
    {
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto login);
        Task<bool> SendEmailForForgetPassword(string email);
        Task<string> ResetPassword(RestPasswordDto restPasswordDto);
        Task<bool> ActiveAccount(ActiveAccountDto accountDto);  

    }
}
