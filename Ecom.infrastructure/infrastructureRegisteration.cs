using Ecom.Core.Interfaces;
using Ecom.infrastructure.Reposities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure
{
    public static class infrastructureRegisteration
    {
        public static  IServiceCollection iserviceConfiguration(this IServiceCollection services)
        {

            services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
            services.AddScoped<ICategoryRepositry, CategoryRepositry>();
            services.AddScoped<IProductRepoistry, ProductRepositry>();
            services.AddScoped<IPhotoRepositry, PhotoRepoistory>(); 

            return services;
        }
    }
}

