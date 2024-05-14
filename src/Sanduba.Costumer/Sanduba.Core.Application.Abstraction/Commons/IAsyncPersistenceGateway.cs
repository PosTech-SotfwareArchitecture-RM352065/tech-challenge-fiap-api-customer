using Sanduba.Core.Domain.Commons.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sanduba.Core.Application.Abstraction.Commons
{
    public interface IAsyncPersistenceGateway<TId, T> where T : Entity<TId>
    {
        Task SaveAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<T> RemoveAsync(TId id, CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    }
}
