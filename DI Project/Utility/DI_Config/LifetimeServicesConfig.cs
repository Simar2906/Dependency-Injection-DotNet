using DI_Project.Services.LifetimeExample;

namespace DI_Project.Utility.DI_Config
{
    public static class LifetimeServicesConfig
    {
        public static IServiceCollection AddLifetimeServices(this IServiceCollection services)
        {
            services.AddSingleton<SingletonService>();
            services.AddTransient<TransientService>();
            services.AddScoped<ScopedService>();
            return services;
        }
    }
}
