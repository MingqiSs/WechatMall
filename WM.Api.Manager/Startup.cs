using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WM.Api.Manager.Configurations;
using WM.Api.Manager.Filter;
using WM.Infrastructure.DI;

namespace WM.Api.Manager
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

            #region ע�� ��־
            //services.AddLogging(t => t.AddNLog());
            #endregion

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

            #region ע�� Repositor
            services.RegisterAssembly("X.IRespository", "X.Respository");
            #endregion

            #region ע�� Service
            services.RegisterAssembly("WM.Service.App", ServiceLifetime.Scoped);
            #endregion

            #region ע�� Service
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

            app.UseAuthorization();
            //token��֤
           // app.UseAuthentication();
            
            app.UseCheckTokenAuthentication();

            // ��������
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
