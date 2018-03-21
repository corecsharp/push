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
                            settings.JsonSerializeLongAsString = true;
                            settings.JsonCapitalizationStyle = Sherlock.Framework.Web.CapitalizationStyle.CamelCase;
                        });

                    });
                    builder.AddDapperDataFeature();
                },
                scope =>
                {
                    scope.LoggerFactory.AddConsole(LogLevel.Error);
                    //scope.LoggerFactory.AddFile("d:\\start_log.txt");
                });
            services.AddAutoMapper();
            services.AddOptions().Configure<RedisConnectOptions>(Configuration.GetSection(nameof(RedisConnectOptions)));
            services.AddOptions().Configure<RedisCacheKeyOptions>(Configuration.GetSection(nameof(RedisCacheKeyOptions)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(LogLevel.Warning);
            app.StartSherlockWebApplication();
            //启动一个新的线程，来处理接收的数据
            IMsgService msgService = SherlockEngine.Current.GetService<IMsgService>();
            new Thread(new ThreadStart(msgService.HandleMsgToTreadPool)) { IsBackground = true }.Start();
            //初始化推送供应商
            PushSenderConfig.InitSender();
        }
    }
}
