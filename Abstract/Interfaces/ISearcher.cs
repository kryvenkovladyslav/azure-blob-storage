using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Abstract.Interfaces
{
    public interface ISearcher<TEntity>
        where TEntity : class
    {
        public Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken token = default);

        public Task<TKey> SelectFromAsync<TKey>(ISelectionSpecification<TEntity, TKey> specification, CancellationToken token = default);

        public Task<ICollection<TEntity>> GetAllAsync(CancellationToken token = default);

        public Task<ICollection<TEntity>> GetAllAsync(ISpecification<TEntity> specification, CancellationToken token = default);

        public Task<ICollection<TKey>> SelectFromCollectionAsync<TKey>(ISelectionSpecification<TEntity, TKey> specification, CancellationToken token = default);
    }
}