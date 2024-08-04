using MassTransit;
using Microsoft.Extensions.Options;
using Sanduba.Core.Application.Abstraction.Customers;
using Sanduba.Core.Application.Abstraction.Customers.Events;
using Sanduba.Infrastructure.Broker.ServiceBus.Configurations.Options;
using System.Diagnostics.CodeAnalysis;

namespace Sanduba.Infrastructure.Broker.ServiceBus.Customers
{
    [ExcludeFromCodeCoverage]
    public class CustomerNotificationBroker(IPublishEndpoint publishClient, IOptions<ServiceBusOptions> options) : ICustomerNotification
    {
        private readonly IPublishEndpoint _publishClient = publishClient;

        public async Task InactivationRequested(InactivationRequestedEvent eventData)
        {
            await _publishClient.Publish<InactivationRequestedEvent>(eventData, CancellationToken.None);
        }
    }
}
