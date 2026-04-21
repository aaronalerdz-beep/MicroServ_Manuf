using CQRS.Core.Consumers;
using Cycle.Common.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Post.Query.Infrastructure.Consumers
{
    public class ConsumerHostedService : IHostedService
    {
        private readonly ILogger<ConsumerHostedService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ConsumerHostedService(ILogger<ConsumerHostedService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Event consumer service running.");

            // Scope must live for the whole consumer loop (matches Cycle.CMD.INFRASTRUCTURE.Stores.EventStore topic names).
            _ = Task.Run(
                () =>
                {
                    try
                    {
                        using var scope = _serviceProvider.CreateScope();
                        var eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();
                        eventConsumer.Consumer(nameof(MachineConfEvent), nameof(CycleCreatedEvent));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogCritical(ex, "Kafka consumer terminated unexpectedly.");
                    }
                },
                cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Event consumer service stopped.");

            return Task.CompletedTask;
        }
    }
}