using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Databases
{
    public static class DbExtensions
    {
        public static IServiceCollection AddNpgsql(this IServiceCollection services, string connectionString, string migrationsAssembly = "")
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString, sql =>
            {
                if (!string.IsNullOrEmpty(migrationsAssembly))
                {
                    sql.MigrationsAssembly(migrationsAssembly);
                }
            }).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            return services;
        }
    }
}
