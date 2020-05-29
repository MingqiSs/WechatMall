using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using WM.Api.Manager.Configurations;
using WM.Api.Manager.Filter;
using WM.Infrastructure.DI;
using WM.Infrastructure.Extensions.AutofacManager;
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

            #region ×¢²á ÈÕÖ¾
            services.AddLogging(t => t.AddNLog());
            #endregion

            #region ×¢²á »º´æ
            services.AddMemoryCache();
            #endregion
            #region ×¢²á TokenÑéÖ¤
            services.AddJwtAuthSetup(Configuration);
            #endregion
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

            app.UseAuthorization();
            //tokenÑéÖ¤
            app.UseAuthentication();
            
            app.UseCheckTokenAuthentication();

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
