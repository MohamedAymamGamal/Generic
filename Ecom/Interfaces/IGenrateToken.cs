using Ecom.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Interfaces
{
    public interface IGenrateToken
    {
        Task<string> GetAndCreateToken(AppUser user);
    }
}
