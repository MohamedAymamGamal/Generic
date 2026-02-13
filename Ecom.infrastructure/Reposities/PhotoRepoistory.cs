using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Reposities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ecom.infrastructure.Reposities
{
    public  class PhotoRepoistory : GenericRepositry<Photo>, IPhotoRepositry
    {
        public PhotoRepoistory(ApplicationDbContext context) : base(context)
        {
        }
    }
}
