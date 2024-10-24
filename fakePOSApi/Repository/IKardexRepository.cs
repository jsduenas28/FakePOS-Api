namespace fakePOSApi.Repository
{
    public interface IKardexRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetByID(int id);
        Task Add(TEntity entity);
        Task Save();
    }
}
