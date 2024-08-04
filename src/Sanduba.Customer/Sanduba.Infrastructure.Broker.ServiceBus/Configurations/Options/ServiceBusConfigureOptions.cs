using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;


namespace Sanduba.Infrastructure.Broker.ServiceBus.Configurations.Options
{
    [ExcludeFromCodeCoverage]
    public class ServiceBusConfigureOptions(IConfiguration configuration) : IConfigureOptions<ServiceBusOptions>
    {
        private readonly string _connectionStringKey = "BrokerSettings:ConnectionString";
        private readonly string _topicNameKey = "BrokerSettings:TopicName";
        private readonly IConfiguration _configuration = configuration;

        public void Configure(ServiceBusOptions options)
        {
            options.ConnectionString = _configuration.GetValue<string>(_connectionStringKey) ?? string.Empty;
            options.TopicName = _configuration.GetValue<string>(_topicNameKey) ?? string.Empty;
        }
    }
}
