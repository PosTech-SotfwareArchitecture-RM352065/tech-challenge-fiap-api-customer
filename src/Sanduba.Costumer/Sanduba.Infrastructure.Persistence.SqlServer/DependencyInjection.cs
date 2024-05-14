using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sanduba.Core.Application.Abstraction.Costumers;
using Sanduba.Infrastructure.Persistence.SqlServer.Costumers;

namespace Sanduba.Infrastructure.Persistence.SqlServer
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddSqlServerInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetValue<string>("SqlServerSettings:ConnectionString") ?? string.Empty;

            services.AddDbContext<InfrastructureDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableSensitiveDataLogging();
            });

            services.AddScoped<ICostumerPersistenceGateway, CostumerRepository>();

            return services;
        }
    }
}
