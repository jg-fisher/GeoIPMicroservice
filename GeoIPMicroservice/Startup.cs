using System.Collections.Generic;
using System.Threading.Tasks;
using GeoIPMicroservice.Services;
using GeoIPMicroservice.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using FluentValidation;
using FluentValidation.AspNetCore;
using GeoIPMicroservice.Configuration;
using GeoIPMicroservice.Configuration.Logging;
using GeoIPMicroservice.DataAccess;
using GeoIPMicroservice.Models;

namespace GeoIPMicroservice
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
                {
                    options.Filters.Add(new ModelStateFilter());
                })
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<Startup>();
                });

            services.AddTransient<GeoIpService>();
            services.AddTransient<GeoIpRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddSerilog();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
