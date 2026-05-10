using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.DTO
{
    public record LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public record RegisterDto : LoginDto
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }

    }
}
