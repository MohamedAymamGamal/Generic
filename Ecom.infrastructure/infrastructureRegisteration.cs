using Ecom.Core.Entities.Identity;
using Ecom.Core.Interfaces;
using Ecom.Core.Service;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Reposities;
using Ecom.infrastructure.Reposities.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;


namespace Ecom.infrastructure
{
    public static class infrastructureRegisteration
    {
        public static  IServiceCollection infrastructureConfiguration(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            //apply Redis
            services.AddSingleton<IConnectionMultiplexer>(x =>
            {
                var configurationOptions = ConfigurationOptions.Parse(configuration.GetConnectionString("redis"));
                return ConnectionMultiplexer.Connect(configurationOptions);
            });

            //apply unit of work pattern 

            //repo
            services.AddScoped<IAuth, AuthRepository>();
            services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));

            //services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGenrateToken, GenrateToken>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<IImageMangamentService, ImageMangamentService>();

          
            //apply DbContext
            services.AddSingleton<IConfiguration>(configuration);

            services.AddSingleton<IFileProvider>( 
                new PhysicalFileProvider(
                 Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")
                    )
                  );
        ;
            // Register Identity for the application's AppUser so UserManager<AppUser>
            // and SignInManager<AppUser> are available in DI.
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()   
                .AddDefaultTokenProviders();

            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options => { 
                options.Cookie.Name = "token";
                options.Events.OnRedirectToLogin = context =>
                {
                    context .Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidAudience = configuration["Token:Audience"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Secret"]))
                };
                options.Events = new JwtBearerEvents()
                {
                   OnMessageReceived = context =>
                   {
                          var accessToken = context.Request.Query["token"];
                          if (!string.IsNullOrEmpty(accessToken))
                          {
                            context.Token = accessToken;
                          }
                          return Task.CompletedTask;
                   }
                };
            });

            return services;

        }
    }
}

