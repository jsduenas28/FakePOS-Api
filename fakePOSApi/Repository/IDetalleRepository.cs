namespace fakePOSApi.Repository
{
    public interface IDetalleRepository<TDto, TEntity> : IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllByID(int id);
        Task<IEnumerable<TDto>> GetDetalleWithProducto();
        Task<IEnumerable<TDto>> GetDetalleWithProductoByID(int id);
    }
}
