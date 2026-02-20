using Ecom.Api.Middleware;
using Ecom.Core.Interfaces;
using Ecom.infrastructure;
using Ecom.infrastructure.Reposities;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace Ecom.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddMemoryCache();
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.infrastructureConfiguration(builder.Configuration);
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            ////////////////////////////////////////////////////////////
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:4200") 
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
            //////////////////////////////////////////////////////////
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var app = builder.Build();

           
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            ///policy for angular to access the api


            //midddleware for error handling
            app.UseMiddleware<ExceptionMiddleware>(); 
            app.UseStatusCodePagesWithReExecute("/errors/{0}");


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("CORSPolicy");
            app.MapControllers();

            app.Run();
        }
    }
}
