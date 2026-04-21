namespace CQRS.Core.Consumers
{
    public interface IEventConsumer
    {
        void Consumer(params string[] topics);
    }
}