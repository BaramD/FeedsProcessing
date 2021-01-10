using System;
using System.IO;
using FeedsProcessing.Common;
using FeedsProcessing.Dal;
using FeedsProcessing.Logic;
using FeedsProcessing.Models;
using FeedsProcessing.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FeedsProcessing
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
            services.AddMemoryCache(options =>
            {
                options.ExpirationScanFrequency = TimeSpan.FromSeconds(10);
                options.SizeLimit = 10000;
            });

            RegisterServices(services);

            if (!Directory.Exists(Constants.BasePath))
                Directory.CreateDirectory(Constants.BasePath);

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            //UI
            services.AddSingleton<INotificationModelParser, NotificationModelParser>();
            services.AddSingleton<INotificationModelValidator, NotificationModelValidator>();
            services.AddSingleton<IRequestHandler, RequestHandler>();

            //Logic
            services.AddSingleton<INotificationManager, NotificationManager>();
            services.AddSingleton<IReportCalculator, ReportCalculator>();

            //Dal
            services.AddSingleton<INotificationDal, NotificationDal>();
            services.AddSingleton<INotificationReportDal, NotificationReportDal>();
            services.AddSingleton<IProcessingStateDal, ProcessingStateDal>();
            services.AddSingleton<IReportCalculationDal, ReportCalculationDal>();
        }

    }
}
