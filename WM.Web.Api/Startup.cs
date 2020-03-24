using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WM.Infrastructure.Config;
using WM.Infrastructure.DI;
using WM.Web.Api.Configurations;

namespace WM.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerSetup();


            #region ×¢²á TokenÑéÖ¤
            services.AddJwtAuthSetup(Configuration);
            //services.Configure<TokenManagement>(Configuration.GetSection("tokenManagement"));
            //var token = Configuration.GetSection("tokenManagement").Get<TokenManagement>();
            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
            //        ValidIssuer = token.Issuer,
            //        ValidAudience = token.Audience,
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //});
            #endregion

            #region ×¢²á Repositor
            services.RegisterAssembly("X.IRespository", "X.Respository");
            #endregion

            #region ×¢²á Service
            services.RegisterAssembly("WM.Service.App", ServiceLifetime.Scoped);
            #endregion
            #region ×¢²á Service
            services.RegisterAssembly("WM.Service.Domain", ServiceLifetime.Scoped);
            #endregion
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();
            //tokenÑéÖ¤
            app.UseAuthentication();

            app.UseAuthorization();
            // ¿çÓòÅäÖÃ
            //app.UseCors(c =>
            //{
            //    c.AllowAnyOrigin();
            //    c.AllowAnyHeader();
            //    c.AllowAnyMethod();
            //    c.AllowCredentials();
            //});
            app.UseSwaggerSetup();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
