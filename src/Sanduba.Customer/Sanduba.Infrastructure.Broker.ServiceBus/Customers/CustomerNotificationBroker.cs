using MassTransit;
using Microsoft.Extensions.Logging;
using Sanduba.Core.Application.Abstraction.Customers;
using Sanduba.Core.Application.Abstraction.Customers.Events;
using System.Diagnostics.CodeAnalysis;

namespace Sanduba.Infrastructure.Broker.ServiceBus.Customers
{
    [ExcludeFromCodeCoverage]
    public class CustomerNotificationBroker(
        ILogger<CustomerNotificationBroker> logger,
        IPublishEndpoint publishClient,
        ICustomerPersistenceGateway customerPersistence
        ) : IConsumer<InactivationRequestCompletedEvent>, ICustomerNotification
    {
        private readonly ILogger<CustomerNotificationBroker> _logger = logger;
        private readonly IPublishEndpoint _publishClient = publishClient;
        private readonly ICustomerPersistenceGateway _customerPersistence = customerPersistence;

        public Task Consume(ConsumeContext<InactivationRequestCompletedEvent> context)
        {
            try
            {
                _logger.LogInformation($"Customer request received id: {context.MessageId}");

                _customerPersistence.UpdateRequest(
                    context.Message.Id, 
                    context.Message.Status
                );
                
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error consuming customer request id: {context.MessageId}, exception: {ex.Message}");
                return Task.FromException(ex);
            }
        }

        public async Task InactivationRequested(InactivationRequestedEvent eventData)
        {
            await _publishClient.Publish<InactivationRequestedEvent>(eventData, CancellationToken.None);
        }
    }
}
