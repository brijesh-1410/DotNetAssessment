using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApp.Common;
using BackendApp.Services;
using BackendApp.Models;
using AutoMapper;
using BackendApp.Handler;

namespace BackendApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private string AllowSpecificOrigins = "AllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowSpecificOrigins,
                                  policy =>
                                  {
                                      //policy.WithOrigins("http://localhost:4200");
                                      policy.WithOrigins(Configuration.GetValue<string>("Dev:UIAppUrl"))
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();

                                  });
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackendApp", Version = "v1" });
            });

            services.AddScoped<IApiCaller,ApiCaller>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddAutoMapper(typeof(Program));
            services.AddControllersWithViews(config => config.Filters.Add(typeof(CustomExceptionFilter)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackendApp v1"));
            }

            //app.UseCors();
            app.UseCors(AllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
