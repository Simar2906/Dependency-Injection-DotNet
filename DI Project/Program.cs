using DI_Project.Data;
using DI_Project.MiddleWare;
using DI_Project.Models;
using DI_Project.Services;
using DI_Project.Utility.AppSettingsClasses;
using DI_Project.Utility.DI_Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DI_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddTransient<IMarketForecaster, MarketForecasterV2>();
            //builder.Services.AddSingleton<IMarketForecaster>(new MarketForecasterV2());
            //builder.Services.AddTransient < MarketForecasterV2>();
            //builder.Services.AddSingleton(new MarketForecasterV2());
            //builder.Services.AddTransient(typeof(MarketForecasterV2));
            //builder.Services.AddTransient(typeof(IMarketForecaster), typeof(MarketForecasterV2));
            builder.Services.TryAddTransient<IMarketForecaster, MarketForecaster>();
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddLifetimeServices();
            builder.Services.AddAppSettingsConfig(builder.Configuration);


            builder.Services.AddScoped<CreditApprovedHigh>();
            builder.Services.AddScoped<CreditApprovedLow>();

            builder.Services.AddScoped<Func<CreditApprovedEnum, ICreditApproved>>(ServiceProvider => range =>
            {
                switch (range)
                {
                    case CreditApprovedEnum.High: return ServiceProvider.GetService<CreditApprovedHigh>();
                    case CreditApprovedEnum.Low: return ServiceProvider.GetService<CreditApprovedLow>();
                    default: return ServiceProvider.GetService<CreditApprovedLow>();
                }
            });
            //builder.Services.AddScoped<IValidationChecker, AddressValidationChecker>();
            //builder.Services.AddScoped<IValidationChecker, CreditValidationChecker>();
            //builder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<IValidationChecker, AddressValidationChecker>());
            //builder.Services.TryAddEnumerable(ServiceDescriptor.Scoped<IValidationChecker, CreditValidationChecker>());

            builder.Services.TryAddEnumerable(new[]
            {
                ServiceDescriptor.Scoped<IValidationChecker, AddressValidationChecker>(),
                ServiceDescriptor.Scoped<IValidationChecker, CreditValidationChecker>()
            });
            builder.Services.AddScoped<ICreditValidator, CreditValidator>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<CustomMiddleware>();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
