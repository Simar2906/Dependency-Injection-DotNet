using DI_Project.Data;
using DI_Project.Models;
using DI_Project.Services;
using DI_Project.Utility.AppSettingsClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DI_Project.Utility.DI_Config
{
    public static class ConfigureDIServices
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services)
        {
            services.AddTransient<IMarketForecaster, MarketForecasterV2>();
            //services.AddSingleton<IMarketForecaster>(new MarketForecasterV2());
            //services.AddTransient < MarketForecasterV2>();
            //services.AddSingleton(new MarketForecasterV2());
            //services.AddTransient(typeof(MarketForecasterV2));
            //services.AddTransient(typeof(IMarketForecaster), typeof(MarketForecasterV2));
            services.TryAddTransient<IMarketForecaster, MarketForecaster>();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.TryAddEnumerable(new[]
           {
                ServiceDescriptor.Scoped<IValidationChecker, AddressValidationChecker>(),
                ServiceDescriptor.Scoped<IValidationChecker, CreditValidationChecker>()
            });
            services.AddScoped<ICreditValidator, CreditValidator>();


            services.AddScoped<CreditApprovedHigh>();
            services.AddScoped<CreditApprovedLow>();

            services.AddScoped<Func<CreditApprovedEnum, ICreditApproved>>(ServiceProvider => range =>
            {
                switch (range)
                {
                    case CreditApprovedEnum.High: return ServiceProvider.GetService<CreditApprovedHigh>();
                    case CreditApprovedEnum.Low: return ServiceProvider.GetService<CreditApprovedLow>();
                    default: return ServiceProvider.GetService<CreditApprovedLow>();
                }
            });
            //services.AddScoped<IValidationChecker, AddressValidationChecker>();
            //services.AddScoped<IValidationChecker, CreditValidationChecker>();
            //services.TryAddEnumerable(ServiceDescriptor.Scoped<IValidationChecker, AddressValidationChecker>());
            //services.TryAddEnumerable(ServiceDescriptor.Scoped<IValidationChecker, CreditValidationChecker>());

            return services;
        }
    }
}
