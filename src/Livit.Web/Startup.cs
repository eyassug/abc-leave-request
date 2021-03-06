﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Funq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServiceStack;
using ServiceStack.Api.Swagger;
using Livit.Common.Google;
using ServiceStack.Validation;
using ServiceStack.OrmLite;
using ServiceStack.Data;
using Livit.Common.Repository;
using System.Data;
using Livit.Common.Models;
using ServiceStack.Caching;
using Livit.Web.Models;

namespace Livit.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseServiceStack(new AppHost());
            
        }
    }

    public class AppHost : AppHostBase
    {
        static string[] AdminEmails = new string[]
        {
            "eyassug@gmail.com",
            // Add your email address here
        };

        public AppHost() : base("abc-services", typeof(Startup).GetAssembly())
        {
        }

        public override void Configure(Container container)
        {
            var dbFactory = new OrmLiteConnectionFactory(@"Data Source=./abc.db;", SqliteDialect.Provider);
            container.Register<IGoogleCalendarApi>(new GoogleCalendarApi());
            container.Register<IDbConnectionFactory>(dbFactory);
            using (var db = dbFactory.Open())
            {
                if (db.CreateTableIfNotExists<LeaveRequest>())
                {
                    // TODO: Add seed data here
                }
                db.CreateTableIfNotExists<Token>();
            }
            container.Register<ILeaveRequestRepository>(c => new LeaveRequestRepository(c.Resolve<IDbConnectionFactory>().Open()));
            container.Register<ITokenRepository>(c => new TokenRepository(c.Resolve<IDbConnectionFactory>().Open()));

            Plugins.Add(new SessionFeature());
            container.Register<ICacheClient>(new MemoryCacheClient());
            Plugins.Add(new SwaggerFeature());
            Plugins.Add(new ValidationFeature());
            container.RegisterValidators(typeof(AppHost).GetAssembly());

            
            RegisterTypedRequestFilter<IAdminServiceModel>((req, res, dto) =>
            {
                var session = req.GetSessionBag();
                var email = (string)session["email"];
                if (!AdminEmails.Contains(email))
                    res.Write("You have to be an admin to access this service");
            });

            SetConfig(new HostConfig
            {
                DefaultContentType = MimeTypes.Json
            });
        }
    }
}
