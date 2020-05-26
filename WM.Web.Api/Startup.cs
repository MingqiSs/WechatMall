using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using WM.Infrastructure.Extensions.AutofacManager;
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
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers();

            services.AddSwaggerSetup();

            #region ◊¢≤· »’÷æ
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
            #region ◊¢≤· ª∫¥Ê
            services.AddMemoryCache();
            //services.AddRedisCaching(options =>
            //{
            //    options.ConnectionString = AppSetting.GetConfig("RedisConfig:ConnectionString");
            //    options.DefaultKey = AppSetting.GetConfig("RedisConfig:DefaultKey");
            //    options.DdIndex = AppSetting.GetConfigInt32("RedisConfig:DdIndex");
            //});
            #endregion
            #region ◊¢≤· Token—È÷§
            services.AddJwtAuthSetup(Configuration);          
            #endregion

            //#region ◊¢≤· Repositor
            //services.RegisterAssembly("X.IRespository", "X.Respository");
            //#endregion

            //#region ◊¢≤· Service
            //services.RegisterAssembly("WM.Service.App", ServiceLifetime.Scoped);
            //#endregion

            //#region ◊¢≤· Service
            //services.RegisterAssembly("WM.Service.Domain", ServiceLifetime.Scoped);
            //#endregion
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule();
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
            //token—È÷§
            app.UseAuthentication();

            app.UseAuthorization();
            // øÁ”Ú≈‰÷√
            app.UseCors(MyAllowSpecificOrigins);

            app.UseSwaggerSetup();

            WM.Infrastructure.ServiceProviderAccessor.SetServiceProvider(app.ApplicationServices);

            //≈‰÷√HttpContext
            // WM.Infrastructure.Utilities.HttpContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
      
    }
  
}
