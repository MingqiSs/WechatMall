using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Infrastructure.Config;

namespace WM.Web.Api.Configurations
{
    public static class IdentitySetup
    {
        public static void AddJwtAuthSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            // JWT Setup
            var appSettingsSection = configuration.GetSection("TokenManagement");
            services.Configure<TokenManagement>(appSettingsSection);
            var tokenManagement = appSettingsSection.Get<TokenManagement>();
            var key = Encoding.ASCII.GetBytes(tokenManagement.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,//是否验证Issuer
                    ValidateAudience = false,//是否验证Audience
                    ValidateLifetime = false,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    IssuerSigningKey = new SymmetricSecurityKey(key),//拿到SecurityKey
                    ValidAudience = tokenManagement.Audience,
                    ValidIssuer = tokenManagement.Issuer

                };
            });
          
        }
    }
}
