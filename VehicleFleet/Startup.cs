using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using VehicleFleet.Database;
using VehicleFleet.DTO;
using VehicleFleet.Entities;
using VehicleFleet.Services;
using VehicleFleet.Services.FuelSpendingCalculator;

namespace VehicleFleet
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
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
            services.AddDbContext<ApplicationDbContext>();
            services.AddSingleton(_ => new ExpenseContext
            {
                DepreciationCoefficient = 0.05,
                InsuranceCoefficient = 0.1,
                MaintenanceCoefficient = 0.025,
                FuelPriceRoublesPerLiter = 40.0
            });
            services.TryAddEnumerable(new[]
            {
                ServiceDescriptor.Scoped<IExpenseCalculator, FuelExpenseCalculator>(), 
                ServiceDescriptor.Scoped<IExpenseCalculator, DepreciationExpenseCalculator>(), 
                ServiceDescriptor.Scoped<IExpenseCalculator, MaintenanceExpenseCalculator>(), 
                ServiceDescriptor.Scoped<IExpenseCalculator, InsuranceExpenseCalculator>(), 
            });
            services.AddScoped<VehicleBookCostCalculator>();
            var mapperCfg = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Driver, DriverDto>();
                cfg.CreateMap<DriverDto, Driver>();
                cfg.CreateMap<Shift, ShiftDto>();
                cfg.CreateMap<ShiftDto, Shift>();
                cfg.CreateMap<Vehicle, VehicleDto>();
                cfg.CreateMap<VehicleDto, Vehicle>();
            });
            var mapper = mapperCfg.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}