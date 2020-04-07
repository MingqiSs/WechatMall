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
using NLog.Extensions.Logging;
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
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerSetup();

            #region ×¢²á ÈÕÖ¾
            services.AddLogging(t => t.AddNLog());
            #endregion

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });
            #region ×¢²á »º´æ
            services.AddMemoryCache();
            //services.AddRedisCaching(options =>
            //{
            //    options.ConnectionString = AppSetting.GetConfig("RedisConfig:ConnectionString");
            //    options.DefaultKey = AppSetting.GetConfig("RedisConfig:DefaultKey");
            //    options.DdIndex = AppSetting.GetConfigInt32("RedisConfig:DdIndex");
            //});
            #endregion
            #region ×¢²á TokenÑéÖ¤
            services.AddJwtAuthSetup(Configuration);          
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
            app.UseCors(MyAllowSpecificOrigins);
            app.UseSwaggerSetup();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
