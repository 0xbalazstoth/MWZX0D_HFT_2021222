using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MWZX0D_HFT_2021222.Logic.Classes;
using MWZX0D_HFT_2021222.Logic.Exceptions;
using MWZX0D_HFT_2021222.Logic.Interfaces;
using MWZX0D_HFT_2021222.Models;
using MWZX0D_HFT_2021222.Repository.Database;
using MWZX0D_HFT_2021222.Repository.Interfaces;
using MWZX0D_HFT_2021222.Repository.ModelRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWZX0D_HFT_2021222.Endpoint
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FormulaDbContext>();

            services.AddTransient<IRepository<Driver>, DriverRepository>();
            services.AddTransient<IRepository<Team>, TeamRepository>();
            services.AddTransient<IRepository<EngineManufacturer>, EngineManufacturerRepository>();

            services.AddTransient<IDriverLogic, DriverLogic>();
            services.AddTransient<ITeamLogic, TeamLogic>();
            services.AddTransient<IEngineManufacturerLogic, EngineManufacturerLogic>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;

                if (exception is BaseException) // Custom exceptions
                {
                    var baseException = exception as BaseException;
                    var response = new { Error = baseException.Msg };
                    await context.Response.WriteAsJsonAsync(response);
                }
                else
                {
                    var response = new { Msg = exception.Message };
                    await context.Response.WriteAsJsonAsync(response);
                }
            }));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
