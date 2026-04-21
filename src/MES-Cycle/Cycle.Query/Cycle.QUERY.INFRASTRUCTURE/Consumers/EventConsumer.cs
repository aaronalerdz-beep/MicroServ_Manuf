using System.Text.Json;
using Confluent.Kafka;
using CQRS.Core.Consumers;
using CQRS.Core.Events;
using Cycle.QUERY.INFRASTRUCTURE.Handler;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Post.Query.Infrastructure.Converters;

namespace Post.Query.Infrastructure.Consumers
{
    public class EventConsumer : IEventConsumer
    {
        private readonly ConsumerConfig _config;
        private readonly IEventHandler _eventHandler;
        private readonly ILogger<EventConsumer> _logger;

        public EventConsumer(IOptions<ConsumerConfig> config, IEventHandler eventHandler, ILogger<EventConsumer> logger)
        {
            _config = config.Value;
            _eventHandler = eventHandler;
            _logger = logger;
        }

        public void Consumer(params string[] topics)
        {
            using var consumer = new ConsumerBuilder<string, string>(_config)
                        .SetKeyDeserializer(Deserializers.Utf8)
                        .SetValueDeserializer(Deserializers.Utf8)
                        .Build();

            consumer.Subscribe(topics);
            _logger.LogInformation("Kafka consumer subscribed to topics: {Topics}", string.Join(", ", topics));

            var jsonOptions = new JsonSerializerOptions
            {
                Converters = { new EventJsonConverter() },
                PropertyNameCaseInsensitive = true
            };

            while (true)
            {
                try
                {
                    var consumeResult = consumer.Consume();

                    if (consumeResult?.Message?.Value == null) continue;

                    var @event = JsonSerializer.Deserialize<BaseEvent>(consumeResult.Message.Value, jsonOptions);
                    if (@event == null)
                    {
                        _logger.LogWarning("Skipped Kafka message: deserialization returned null.");
                        continue;
                    }

                    var handlerMethod = _eventHandler.GetType().GetMethod("On", new Type[] { @event.GetType() });

                    if (handlerMethod == null)
                    {
                        throw new InvalidOperationException($"No handler On({@event.GetType().Name}).");
                    }

                    var invokeResult = handlerMethod.Invoke(_eventHandler, new object[] { @event });
                    if (invokeResult is Task task)
                    {
                        task.GetAwaiter().GetResult();
                    }

                    consumer.Commit(consumeResult);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing Kafka message; consumer will retry after delay.");
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                }
            }
        }

    }
}