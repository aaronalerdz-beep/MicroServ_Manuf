using CQRS.Core.Events;

namespace CQRS.Core.Domain;

public abstract class AggregateRoot
{
    protected Guid _id;
    public Guid Id;

    public int Version { get; set; } = -1;

    private readonly List<BaseEvent> _changes = new();

    public IEnumerable<BaseEvent> GetUncommittedChanges() => _changes;

    public void MarkChangesAsCommitted() => _changes.Clear();

    protected void RaiseEvent(BaseEvent @event) {
        ApplyChange(@event, true);
    }

    public void ReplayEvents(IEnumerable<BaseEvent> events) {
        foreach (var @event in events) {
            ApplyChange(@event, false);
        }
    }

    private void ApplyChange(BaseEvent @event, bool isNew) {
        var method = this.GetType().GetMethod("Apply", new Type[] { @event.GetType() });
        
        if (method == null) {
            throw new InvalidOperationException($"Method Apply not found in {this.GetType().Name} for event {@event.GetType().Name}");
        }

        method.Invoke(this, new object[] { @event });
        if (isNew) {
            _changes.Add(@event);
        }
    }
}