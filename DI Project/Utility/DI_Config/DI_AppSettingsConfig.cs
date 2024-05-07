using Microsoft.Identity.Client;
using Microsoft.Extensions.DependencyInjection;
using DI_Project.Utility.AppSettingsClasses;
namespace DI_Project.Utility.DI_Config
{
    public static class DI_AppSettingsConfig
    {
        public static IServiceCollection AddAppSettingsConfig(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<WazeForecastSettings>(config.GetSection("WazeForecast"));
            services.Configure<StripeSettings>(config.GetSection("Stripe"));
            services.Configure<SendGridSettings>(config.GetSection("SendGrid"));
            services.Configure<TwilioSettings>(config.GetSection("Twilio"));
            return services;
        }
    }
}
