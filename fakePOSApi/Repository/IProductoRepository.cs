using fakePOSApi.DTOs;
using fakePOSApi.Models;

namespace fakePOSApi.Repository
{
    public interface IProductoRepository<TEntity> : IRepository<TEntity>
    {
        Task<bool> ValidateStock(int idProducto, int cantidad);
        Task ChangeStock(int idProducto, int cantidad, bool isDecrement);
        Task<TEntity> GetByCodProducto(string CodProducto);
        Task<IEnumerable<ProductoDto>> GetProductosConCategoria();
        Task<IEnumerable<ProductoDto>> GetProductosConCategoriaByCodProducto(string CodProducto);
        Task<ProductoDto> GetProductoConCategoriaByID(int id);
    }
}
