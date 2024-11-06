namespace Ordering.Domain.Abstractions;

public interface IAggregate<out T> : IAggregate, IEntity<T>
{ }

public interface IAggregate : IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get;}
    IDomainEvent[] ClearDomainEvents();
}