using System.Diagnostics.CodeAnalysis;

namespace Sanduba.Infrastructure.Broker.ServiceBus.Configurations.Options
{
    [ExcludeFromCodeCoverage]
    public class ServiceBusOptions
    {
        public string ConnectionString = string.Empty;
        public string TopicName = string.Empty;
    }
}
