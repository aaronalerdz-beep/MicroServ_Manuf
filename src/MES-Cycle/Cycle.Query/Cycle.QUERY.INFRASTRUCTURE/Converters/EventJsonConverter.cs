using System.Text.Json;
using System.Text.Json.Serialization;
using CQRS.Core.Events;
using Cycle.Common.Events;

namespace Post.Query.Infrastructure.Converters
{
    public class EventJsonConverter : JsonConverter<BaseEvent>
    {
        public override BaseEvent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // 1. Clonamos las opciones para asegurar que incluyan el NumberHandling
            var customOptions = new JsonSerializerOptions(options)
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString | 
                                JsonNumberHandling.WriteAsString
            };

            // 2. Cargamos el elemento como un JsonDocument para leer el tipo
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                var typeDiscriminator = root.GetProperty("Type").GetString();
                var json = root.GetRawText();

                // 3. Usamos las opciones "customOptions" aquí
                return typeDiscriminator switch
                {
                    nameof(CycleCreatedEvent) => JsonSerializer.Deserialize<CycleCreatedEvent>(json, customOptions),
                    nameof(MachineConfEvent) => JsonSerializer.Deserialize<MachineConfEvent>(json, customOptions),
                    _ => throw new JsonException($"Tipo de evento desconocido: {typeDiscriminator}")
                };
            }
        }

        public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}