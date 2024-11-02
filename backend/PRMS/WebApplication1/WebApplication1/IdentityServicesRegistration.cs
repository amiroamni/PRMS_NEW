
   using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;
    using PRMS_BackendAPI.Identity.IdentityInterface;
    using PRMS_BackendAPI.Identity.Identitys;
using PRMS_BackendAPI.Identity.Infra_Identitiy;
using PRMS_BackendAPI.Identity.Infra_Identitiy.Models;
using PRMS_BackendAPI.Identity.services;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    namespace PRMS_BackendAPI
    {
        public static class IdentityServicesRegistration
        {
            public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
            {
                services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

                services.AddDbContext<PRMS_IdentityDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("LeaveManagementIdentityConnectionString"),
                    b => b.MigrationsAssembly(typeof(PRMS_IdentityDbContext).Assembly.FullName)));

                services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<PRMS_IdentityDbContext>().AddDefaultTokenProviders();

                services.AddTransient<IAuthService, AuthService>();
       

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                    .AddJwtBearer(o =>
                    {
                        o.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                            ValidIssuer = configuration["JwtSettings:Issuer"],
                            ValidAudience = configuration["JwtSettings:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                        };
                    });

                return services;
            }
        }
    }
