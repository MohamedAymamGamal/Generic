using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Reposities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure
{
    public static class infrastructureRegisteration
    {
        public static  IServiceCollection infrastructureConfiguration(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
            //apply unit of work pattern 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //apply DbContext
            services.AddDbContext<ApplicationDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); 
            });
            return services;
        }
    }
}

