using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Entities.Identity
{
    public class AppUser:IdentityUser
    {
        public string DispalyName { get; set; }

        public short? OtpCode { get; set; }
        public DateTime? OtpExpiry { get; set; }
        public Address? Address { get; set; }


    }
}
