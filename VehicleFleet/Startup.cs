using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Npgsql;
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
            services.AddControllersWithViews().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
            services.AddDbContext<ApplicationDbContext>(optBuilder =>
            {
                // var connection = new NpgsqlConnection(
                //     "Host=localhost;Database=vehicle_fleet;Username=postgres;Password=password"
                //     );
                var connectionStringBuilder = new NpgsqlConnectionStringBuilder(
                    Configuration.GetConnectionString("VehicleFleetDatabase")
                ) {
                    Password = Configuration["DbPassword"]
                };
                var connection = new NpgsqlConnection(connectionStringBuilder.ConnectionString);
                optBuilder.UseNpgsql(connection);
            });
            services.AddSingleton(_ => new ExpenseContext
            {
                DepreciationCoefficient = 0.003,
                InsuranceCoefficient = 0.004,
                MaintenanceCoefficient = 0.0025,
                FuelPriceRoublesPerLiter = 40.0
            });
            services.TryAddEnumerable(new[]
            {
                ServiceDescriptor.Scoped<IExpenseCalculator, FuelExpenseCalculator>(), 
                ServiceDescriptor.Scoped<IExpenseCalculator, DepreciationExpenseCalculator>(), 
                ServiceDescriptor.Scoped<IExpenseCalculator, MaintenanceExpenseCalculator>(), 
                ServiceDescriptor.Scoped<IExpenseCalculator, InsuranceExpenseCalculator>(), 
                ServiceDescriptor.Scoped<IExpenseCalculator, TaxExpenseCalculator>(), 
            });
            services.AddScoped<KilometrageCalculator>();
            services.AddScoped<VehicleBookCostCalculator>();
            services.AddSingleton(CreateMapper());
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
        private static IMapper CreateMapper()
        {
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
            return mapper;
        }
    }
}