using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using WM.Infrastructure.Filters;

namespace WM.Api.Manager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
       private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers();
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ApiAuthorizeFilter));
                //options.Filters.Add(typeof(ActionExecuteFilter));
            });
            services.AddSwaggerSetup();

            #region ◊¢≤· »’÷æ
            services.AddLogging(t => t.AddNLog());
            #endregion

            #region ◊¢≤· ª∫¥Ê
            services.AddMemoryCache();
            #endregion
            #region ◊¢≤· Token—È÷§
            services.AddJwtAuthSetup(Configuration);
            #endregion
            //øÁ”Ú…Ë÷√
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
            //≈‰÷√HttpContext
            Infrastructure.Utilities.HttpContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
