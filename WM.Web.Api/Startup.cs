using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Resilience.http;
using WM.Infrastructure.Config;
using WM.Infrastructure.DI;
using WM.Infrastructure.Extensions.AutofacManager;
using WM.Web.Api.Configurations;
using WM.Web.Api.Extensions;

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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #region ע��Resilience.http
            services.AddHttpClient<IHttpClinet, ResilienceHttpClient>();
            services.AddSingleton(typeof(ResilienceClinetFactory), sp =>
            {
                var logger = sp.GetRequiredService<ILogger<ResilienceHttpClient>>();
                var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                var retyCount = 3;
                var exceptionCount = 3;
                return new ResilienceClinetFactory(logger, httpContextAccessor, new HttpClient(), retyCount, exceptionCount);
            });
            services.AddSingleton<IHttpClinet>(sp =>
            {
                return sp.GetRequiredService<ResilienceClinetFactory>().GetResilienceHttpClient();

            });
            #endregion
            services.AddControllers();

            services.AddSwaggerSetup();

            #region ע�� ��־
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
            #region ע�� ����
            services.AddMemoryCache();
            //services.AddRedisCaching(options =>
            //{
            //    options.ConnectionString = AppSetting.GetConfig("RedisConfig:ConnectionString");
            //    options.DefaultKey = AppSetting.GetConfig("RedisConfig:DefaultKey");
            //    options.DdIndex = AppSetting.GetConfigInt32("RedisConfig:DdIndex");
            //});
            #endregion
            #region ע�� Token��֤
            services.AddJwtAuthSetup(Configuration);          
            #endregion

            //#region ע�� Repositor
            //services.RegisterAssembly("X.IRespository", "X.Respository");
            //#endregion

            //#region ע�� Service
            //services.RegisterAssembly("WM.Service.App", ServiceLifetime.Scoped);
            //#endregion

            //#region ע�� Service
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
            //token��֤
            app.UseAuthentication();

            app.UseAuthorization();
            // ��������
            app.UseCors(MyAllowSpecificOrigins);

            app.UseSwaggerSetup();

            //����HttpContext
            Infrastructure.Utilities.HttpContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            //����ServiceProvider
            //ServiceProviderAccessor.SetServiceProvider(app.ApplicationServices);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
      
    }
  
}
