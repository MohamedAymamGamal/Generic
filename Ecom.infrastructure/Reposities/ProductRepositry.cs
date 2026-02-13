using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Reposities
{
    public class ProductRepositry : GenericRepositry<Product>, IProductRepoistry
    {
        public ProductRepositry(ApplicationDbContext context) : base(context)
        {
        }
    }
}
