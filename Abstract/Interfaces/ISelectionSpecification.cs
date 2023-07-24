using System;
using System.Linq.Expressions;

namespace Abstract.Interfaces
{
    public interface ISelectionSpecification<TEntity, TKey> : ISpecification<TEntity>
    where TEntity : class
    {
        public Expression<Func<TEntity, TKey>> Select { get; }
    }
}