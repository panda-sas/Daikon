
using CQRS.Core.Domain;

namespace CQRS.Core.Handlers
{
    public interface IEventSourcingHandler<TAggregate> where TAggregate : AggregateRoot
    {
        Task SaveAsync(AggregateRoot Root);
        Task<TAggregate> GetByAsyncId(Guid id);
    }
}