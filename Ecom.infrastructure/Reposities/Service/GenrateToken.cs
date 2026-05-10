using Ecom.Core.Entities.Identity;
using Ecom.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Reposities.Service
{
    public class GenrateToken : IGenrateToken
    {
        public Task<string> GetAndCreateToken(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
