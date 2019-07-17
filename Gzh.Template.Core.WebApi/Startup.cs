using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Autofac.Extensions.DependencyInjection;
using Gzh.Template.Core.Application;
using Gzh.Template.Core.Repository;
using Gzh.Template.Core.Repository.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Gzh.Template.Core.Application.Jobs;
using Quartz;
using Quartz.Impl;

namespace Gzh.Template.Core.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //swagger
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new Info { Title = "Gzh.Template.Core.WebApi", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath);
            });

            //mysql
            string connectionString = Configuration.GetConnectionString("MysqlConnectionString");
            services.AddDbContext<MysqlContext>(options => options.UseMySQL(connectionString));

            //mongodb
            services.Configure<DBSettings>(
               options =>
               {
                   options.ConnectionString = Configuration.GetSection("MongoDb:ConnectionString").Value;
                   options.Database = Configuration.GetSection("MongoDb:Database").Value;
               });

            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//注册ISchedulerFactory的实例。

            //跨域
            services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<HelloJob>();      // 这里使用瞬时依赖注入
            services.AddTransient<HelloJobTest>();
            services.AddSingleton<QuartzStartup>();

            //Autofac
            return new AutofacServiceProvider(AutofacExt.InitAutofac(services));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var quartz = app.ApplicationServices.GetRequiredService<QuartzStartup>();
            lifetime.ApplicationStarted.Register(quartz.Start);
            lifetime.ApplicationStopped.Register(quartz.Stop);


            app.UseHttpsRedirection();
            //跨域 开发环境，生产环境应该指定Origin
            app.UseCors(Options => { Options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
            //swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseMvc();
        }
    }
}
