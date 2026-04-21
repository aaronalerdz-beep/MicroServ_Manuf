using System.Text.Json;
using System.Text.Json.Serialization;
using CQRS.Core.Events;
using Cycle.Common.Events;

namespace Post.Query.Infrastructure.Converters
{
    public class EventJsonConverter : JsonConverter<BaseEvent>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(BaseEvent));
        }

        public override BaseEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!JsonDocument.TryParseValue(ref reader, out var doc))
            {
                throw new JsonException($"Failed to parse {nameof(JsonDocument)}!");
            }

            // Producer uses System.Text.Json default (camelCase), so the discriminator is "type", not "Type".
            if (!TryGetTypeDiscriminator(doc.RootElement, out var typeDiscriminator) || typeDiscriminator is null)
            {
                throw new JsonException("Could not detect the Type discriminator property!");
            }
            var json = doc.RootElement.GetRawText();

            return typeDiscriminator switch
            {
                nameof(CycleCreatedEvent) => JsonSerializer.Deserialize<CycleCreatedEvent>(json, options),
                nameof(MachineConfEvent) => JsonSerializer.Deserialize<MachineConfEvent>(json, options),

                _ => throw new JsonException($"{typeDiscriminator} is not supported yet!")
            };
        }

        private static bool TryGetTypeDiscriminator(JsonElement root, out string? value)
        {
            foreach (var prop in root.EnumerateObject())
            {
                if (prop.Name.Equals("Type", StringComparison.OrdinalIgnoreCase))
                {
                    value = prop.Value.GetString();
                    return true;
                }
            }

            value = null;
            return false;
        }

        public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}