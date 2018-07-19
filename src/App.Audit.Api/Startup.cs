using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Audit.Infrastructure;
using App.Common;
using App.Common.Entity;
using App.Common.Filters;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace App.Audit.Api
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

            //Add in Global Exception Handler
            services.AddMvc(options => { options.Filters.Add(typeof(HttpGlobalExceptionFilter)); });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Audit API", Version = "v1" });
            });


            services.Configure<SettingConnectionString>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<SettingApplicationSetting>(Configuration.GetSection("ApplicationSettings"));
            services.AddOptions();

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<CommonModule>();
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<ApiModule>();
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Audit API V1");
            });

            app.UseMvc();
        }
    }
}
