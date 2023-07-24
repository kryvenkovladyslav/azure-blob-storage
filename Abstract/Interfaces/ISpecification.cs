using System;
using System.Linq.Expressions;

namespace Abstract.Interfaces
{
    public interface ISpecification<TEntity>
        where TEntity : class
    {
        public Expression<Func<TEntity, bool>> Where { get; }
    }
}