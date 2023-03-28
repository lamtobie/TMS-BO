using Microsoft.Extensions.DependencyInjection;

namespace Repositories
{
    public static class DateTimeProviderExtensions
    {
        public static IServiceCollection AddDateTimeProvider(this IServiceCollection services)
        {
            _ = services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            return services;
        }
    }
}
