namespace fakePOSApi.Repository
{
    public interface IDetalleRepository<TEntity> : IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllByID(int id);
    }
}
