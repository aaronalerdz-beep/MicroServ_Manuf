using CQRS.Core.Events;
using Cycle.Common.Events;
using MongoDB.Bson.Serialization;

public static class MongoEventClassMaps
{
    private static bool _registered;

    public static void Register()
    {
        if (_registered) return;

        if (!BsonClassMap.IsClassMapRegistered(typeof(BaseEvent)))
        {
            BsonClassMap.RegisterClassMap<BaseEvent>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
                cm.SetIgnoreExtraElements(true);
            });
        }

        RegisterEvent<CycleCreatedEvent>(nameof(CycleCreatedEvent));
        RegisterEvent<MachineConfEvent>(nameof(MachineConfEvent));

        _registered = true;
    }

    private static void RegisterEvent<TEvent>(string discriminator) where TEvent : BaseEvent
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(TEvent))) return;

        BsonClassMap.RegisterClassMap<TEvent>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator(discriminator);
            cm.SetIgnoreExtraElements(true);
        });
    }
}
