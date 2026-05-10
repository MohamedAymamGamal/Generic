using Ecom.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Interfaces
{
    public  interface IAuth
    {
        Task<string> RegisterAsync(RegisterDto registerDTO);

        Task<string>loginAsync(LoginDto loginDTO);


    }
}
