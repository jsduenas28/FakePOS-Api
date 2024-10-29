namespace fakePOSApi.Repository
{
    public interface IFacturaRepository<TDto, TEntity> : IRepository<TEntity>
    {
        Task<IEnumerable<TDto>> GetViewFactura();
        Task<TDto> GetViewFacturaByID(int id);
    }
}
