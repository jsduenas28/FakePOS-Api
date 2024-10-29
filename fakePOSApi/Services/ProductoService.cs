using fakePOSApi.DTOs;
using fakePOSApi.Models;
using fakePOSApi.Repository;

namespace fakePOSApi.Services
{
    public class ProductoService : IService<ProductoDto, ProductoInsertDto, ProductoUpdateDto>
    {
        private IProductoRepository<Producto> _productoRepository;
        private IRepository<Categoria> _categoriaRepository;
        public List<string> Message { get; private set; } = new List<string>();

        public ProductoService(IProductoRepository<Producto> productoRepository, 
                                [FromKeyedServices("categoriaRepository")] IRepository<Categoria> categoriaRepository)
        {
            _productoRepository = productoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<ProductoDto>> Get()
        {
            var productos = await _productoRepository.GetProductosConCategoria();
            return productos;
        }

        public async Task<ProductoDto> GetByID(int id)
        {
            var producto = await _productoRepository.GetProductoConCategoriaByID(id);
            return producto;
        }

        public async Task<IEnumerable<ProductoDto>> Search(string search)
        {
            var produtos = await _productoRepository.GetProductosConCategoriaByCodProducto(search);
            return produtos;
        }

        public async Task<ProductoDto> Add(ProductoInsertDto dto)
        {
            var producto = new Producto()
            {
                CodProducto = dto.CodProducto,
                Descripcion = dto.Descripcion,
                Stock = 0,
                Precio = dto.Precio,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                IDCategoria = dto.IDCategoria
            };

            await _productoRepository.Add(producto);
            await _productoRepository.Save();

            var categoria = await _categoriaRepository.GetByID(producto.IDCategoria);

            return new ProductoDto
            {
                IDProducto = producto.IDProducto,
                CodProducto = producto.CodProducto,
                Descripcion = producto.Descripcion,
                Stock = producto.Stock,
                Precio = producto.Precio,
                CreateAt = producto.CreateAt,
                UpdateAt = producto.UpdateAt,
                Categoria = new CategoriaDto
                {
                    IDCategoria = categoria.IDCategoria,
                    CodCategoria = categoria.CodCategoria,
                    Descripcion = categoria.Descripcion,
                    CreateAt = categoria.CreateAt,
                    UpdateAt = categoria.UpdateAt
                }
            };
        }

        public async Task<ProductoDto> Update(int id, ProductoUpdateDto dto)
        {
            var producto = await _productoRepository.GetByID(id);

            producto.Descripcion = dto.Descripcion;
            producto.Precio = dto.Precio;
            producto.UpdateAt = DateTime.Now;

            _productoRepository.Update(producto);
            await _productoRepository.Save();

            var categoria = await _categoriaRepository.GetByID(producto.IDCategoria);

            return new ProductoDto
            {
                IDProducto = producto.IDProducto,
                CodProducto = producto.CodProducto,
                Descripcion = producto.Descripcion,
                Stock = producto.Stock,
                Precio = producto.Precio,
                CreateAt = producto.CreateAt,
                UpdateAt = producto.UpdateAt,
                Categoria = new CategoriaDto
                {
                    IDCategoria = categoria.IDCategoria,
                    CodCategoria = categoria.CodCategoria,
                    Descripcion = categoria.Descripcion,
                    CreateAt = categoria.CreateAt,
                    UpdateAt = categoria.UpdateAt
                }
            };
        }

        public async Task<ProductoDto> Delete(int id)
        {
            var producto = await _productoRepository.GetByID(id);

            var categoria = await _categoriaRepository.GetByID(producto.IDCategoria);

            var productoDto = new ProductoDto
            {
                IDProducto = producto.IDProducto,
                CodProducto = producto.CodProducto,
                Descripcion = producto.Descripcion,
                Stock = producto.Stock,
                Precio = producto.Precio,
                CreateAt = producto.CreateAt,
                UpdateAt = producto.UpdateAt,
                Categoria = new CategoriaDto
                {
                    IDCategoria = categoria.IDCategoria,
                    CodCategoria = categoria.CodCategoria,
                    Descripcion = categoria.Descripcion,
                    CreateAt = categoria.CreateAt,
                    UpdateAt = categoria.UpdateAt
                }
            };

            _productoRepository.Delete(producto);
            await _productoRepository.Save();

            return productoDto;
        }

        public async Task<bool> Validate(int id)
        {
            var producto = await _productoRepository.GetByID(id);

            if(producto == null)
            {
                Message.Add("El IDProducto no existe.");
                return false;
            }

            return true;
        }

        public async Task<bool> Validate(ProductoInsertDto dto)
        {
            var categoria = await _categoriaRepository.GetByID(dto.IDCategoria);

            if(categoria == null)
            {
                Message.Add("El IDCategoria no existe.");
                return false;
            }

            return true;
        }

        public async Task<bool> Validate(ProductoUpdateDto dto)
        {
            var categoria = await _categoriaRepository.GetByID(dto.IDCategoria);

            if (categoria == null)
            {
                Message.Add("El IDCategoria no existe.");
                return false;
            }

            return true;
        }
    }
}
