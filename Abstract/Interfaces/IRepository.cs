namespace Abstract.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        public void Create(TEntity entity);

        public void Update(TEntity entity);

        public void Delete(TEntity entity);

        public void SaveChanges();
    }
}