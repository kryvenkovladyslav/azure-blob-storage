namespace Abstract.Interfaces
{
    public interface IUnitOfWork<TEntity>
        where TEntity : class
    {
        public ISearcher<TEntity> Searcher { get; }

        public IRepository<TEntity> Repository { get; }
    }
}