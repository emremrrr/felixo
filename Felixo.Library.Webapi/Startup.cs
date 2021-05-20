using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Felixo.Library.Service.Command;
using Felixo.Library.Service.Query;
using Felixo.Library.Entities.Models;
using Felixo.Library.Entities.Models.Context;
using Felixo.Library.Data.UnitOfWork;
using Felixo.Library.Messaging;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using AutoMapper;
using Felixo.Library.Entities.DTO;
using log4net.Config;
using PostSharp.Patterns.Diagnostics;
using System.IO;
using PostSharp.Patterns.Diagnostics.Backends.Log4Net;
using log4net.Repository;

namespace Felixo.Library.Webapi
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
            services.AddAutoMapper(typeof(Entities.Mapper));
            services.AddDbContext<AppDataContext>();
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<AppDataContext>()
            .AddDefaultTokenProviders();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .WithOrigins("http://localhost:86")
                    //.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddSignalR();
            services.AddScoped<IUnit, Felixo.Library.Data.UnitOfWork.Unit>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IRequestHandler<GetRequestsQueryCommand, IEnumerable<RequestDTO>>, GetRequestsQueryCommandHandler>();
            services.AddTransient<IRequestHandler<GetBookQueryCommand, IEnumerable<BookDTO>>, GetBookQueryCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateRequestCommand, Request>, UpdateRequestCommandHandler<Notificationhub>>();

            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            var serviceClientSettings = serviceClientSettingsConfig.Get<RabbitMqConfiguration>();
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);
            services.AddSingleton<IRabbitMQClientSender, RabbitMQClientSender>();

            ILoggerRepository relay = Log4NetCollectingRepositorySelector.RedirectLoggingToPostSharp();

            // Configure the *relay* repository (instead of the default repository) with your final output appenders:
            XmlConfigurator.Configure(relay, new FileInfo("log4net.config"));

            // Use the relay repository to create a Log4NetLoggingBackend and set it as the default backend:
            LoggingServices.DefaultBackend = new Log4NetLoggingBackend(relay);
            if (serviceClientSettings.Enabled)
                services.AddHostedService<RabbitMQClientReceiver>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<Notificationhub>("/request");
            });
        }
    }
}
