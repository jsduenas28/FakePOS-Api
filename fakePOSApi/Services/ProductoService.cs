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
            var productos = await _productoRepository.Get();

            return productos.Select(p => new ProductoDto
            {
                IDProducto = p.IDProducto,
                CodProducto = p.CodProducto,
                Descripcion = p.Descripcion,
                Stock = p.Stock,
                Precio = p.Precio,
                IDCategoria = p.IDCategoria
            });
        }

        public async Task<ProductoDto> GetByID(int id)
        {
            var producto = await _productoRepository.GetByID(id);

            return new ProductoDto
            {
                IDProducto = producto.IDProducto,
                CodProducto = producto.CodProducto,
                Descripcion = producto.Descripcion,
                Stock = producto.Stock,
                Precio = producto.Precio,
                IDCategoria = producto.IDCategoria
            };
        }

        public async Task<IEnumerable<ProductoDto>> Search(string search)
        {
            var productos = await _productoRepository.Search(search);

            return productos.Select(p => new ProductoDto
            {
                IDProducto = p.IDProducto,
                CodProducto = p.CodProducto,
                Descripcion = p.Descripcion,
                Stock = p.Stock,
                Precio = p.Precio,
                IDCategoria = p.IDCategoria
            });
        }

        public async Task<ProductoDto> Add(ProductoInsertDto dto)
        {
            var producto = new Producto()
            {
                CodProducto = dto.CodProducto,
                Descripcion = dto.Descripcion,
                Stock = 0,
                Precio = dto.Precio,
                IDCategoria = dto.IDCategoria
            };

            await _productoRepository.Add(producto);
            await _productoRepository.Save();

            return new ProductoDto
            {
                IDProducto = producto.IDProducto,
                CodProducto = producto.CodProducto,
                Descripcion = producto.Descripcion,
                Stock = producto.Stock,
                Precio = producto.Precio,
                IDCategoria = producto.IDCategoria
            };
        }

        public async Task<ProductoDto> Update(int id, ProductoUpdateDto dto)
        {
            var producto = await _productoRepository.GetByID(id);

            producto.Descripcion = dto.Descripcion;
            producto.Precio = dto.Precio;

            _productoRepository.Update(producto);
            await _productoRepository.Save();

            return new ProductoDto
            {
                IDProducto = producto.IDProducto,
                CodProducto = producto.CodProducto,
                Descripcion = producto.Descripcion,
                Stock = producto.Stock,
                Precio = producto.Precio,
                IDCategoria = producto.IDCategoria
            };
        }

        public async Task<ProductoDto> Delete(int id)
        {
            var producto = await _productoRepository.GetByID(id);

            var productoDto =  new ProductoDto
            {
                IDProducto = producto.IDProducto,
                CodProducto = producto.CodProducto,
                Descripcion = producto.Descripcion,
                Stock = producto.Stock,
                Precio = producto.Precio,
                IDCategoria = producto.IDCategoria
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
