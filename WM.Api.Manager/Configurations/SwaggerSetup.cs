using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WM.Api.Manager.Configurations
{
    public static class SwaggerSetup
    {
        /// <summary>
        /// swagger配置
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WeChatMall-Manager Project",
                    Description = "WeChatMall-Manager API Swagger surface",
                   // Contact = new OpenApiContact { Name = "Eduardo Pires", Email = "contato@eduardopires.net.br", Url = new Uri("http://www.eduardopires.net.br") },
                 //   License = new OpenApiLicense() { Name = "MIT", Url = new Uri("") }
                });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "WM.Api.Manager.xml");
                var xmlPath1 = Path.Combine(AppContext.BaseDirectory, "WM.Service.App.xml");
                s.IncludeXmlComments(xmlPath);
                s.IncludeXmlComments(xmlPath1);
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeChatMall-Manager Project");
            });
        }
    }
}
