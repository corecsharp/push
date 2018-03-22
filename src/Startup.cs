using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Sherlock.Framework;
using AutoMapper;
using Platform.Core.Options;
using Push.Api.Service;
using Sherlock.Framework.Environment;
using System.Threading;
using Push.Api.Config;
using Sherlock;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace Push
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            //TODO 注册中心连不上的时候要警告 
            .AddEnvironmentVariables();


            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSherlockFramework(this.Configuration,
                builder =>
                {

                    builder.AddWebFeature(web =>
                    {
                        web.AddFluentValidationForMvc();
                        web.ConfigureFeature(settings =>
                        {
                            settings.MvcFeatures = Sherlock.Framework.Web.MvcFeatures.Api;
                            settings.JsonCaseStyle = Sherlock.Framework.Web.JsonCaseStyle.CamelCase;
                        });

                    });
                    builder.AddDapperDataFeature();
                });
            services.AddAutoMapper();
            services.AddOptions().Configure<RedisConnectOptions>(Configuration.GetSection(nameof(RedisConnectOptions)));
            services.AddOptions().Configure<RedisCacheKeyOptions>(Configuration.GetSection(nameof(RedisCacheKeyOptions)));
            services.AddSwaggerGen(s=> {
                s.SwaggerDoc("v2",
                   new Info
                   {
                       Title = "Push API",
                       Version = "2.3.0",
                       Contact = new Contact { Name = "the developer", Email = "allen_wheecas@163.com" },
                       Description = "Push API"
                   });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Push.Api.xml");
                s.IncludeXmlComments(xmlPath);
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Push API V2");
            });
            loggerFactory.AddConsole(LogLevel.Information);
            loggerFactory.AddDebug(LogLevel.Error);
            app.StartSherlockWebApplication();
            //启动一个新的线程，来处理接收的数据
            IMsgService msgService = SherlockEngine.Current.GetService<IMsgService>();
            new Thread(new ThreadStart(msgService.HandleMsgToTreadPool)) { IsBackground = true }.Start();
            //初始化推送供应商
            PushSenderConfig.InitSender();
        }
    }
}
