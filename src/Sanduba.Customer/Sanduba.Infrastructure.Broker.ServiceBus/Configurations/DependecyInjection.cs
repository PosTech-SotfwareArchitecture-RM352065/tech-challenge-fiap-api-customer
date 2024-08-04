using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sanduba.Core.Application.Abstraction.Customers;
using Sanduba.Core.Application.Abstraction.Customers.Events;
using Sanduba.Infrastructure.Broker.ServiceBus.Configurations.Options;
using Sanduba.Infrastructure.Broker.ServiceBus.Customers;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Sanduba.Infrastructure.Broker.ServiceBus.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the necessary services with the DI framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The same service collection.</returns>
        public static IServiceCollection AddServiceBusInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var entryAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            services.AddMassTransit(options =>
            {
                options.UsingAzureServiceBus((context, config) =>
                {
                    config.Host(configuration["BrokerSettings:ConnectionString"]);
                    config.Message<InactivationRequestedEvent>(x =>
                    {
                        x.SetEntityName(configuration["BrokerSettings:TopicName"]);
                    });
                    config.DeployTopologyOnly = false;
                });

            });

            services.AddScoped<ICustomerNotification, CustomerNotificationBroker>();
            services.AddOptions().ConfigureOptions<ServiceBusConfigureOptions>();

            return services;
        }
    }
}