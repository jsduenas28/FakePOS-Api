using fakePOSApi.DTOs;
using fakePOSApi.Models;
using fakePOSApi.Repository;

namespace fakePOSApi.Services
{
    public class KardexService : IKardexService<KardexListDto>
    {
        private IKardexRepository<Kardex> _kardexRepository;
        private IProductoRepository<Producto> _productoRepository;
        public List<string> Message { get; private set; } = new List<string>();

        public KardexService(IKardexRepository<Kardex> kardexRepository, IProductoRepository<Producto> productoRepository)
        {
            _kardexRepository = kardexRepository;
            _productoRepository = productoRepository;
        }

        public async Task<KardexListDto> GetByID(int id)
        {
            var kardex = await _kardexRepository.GetByID(id);
            var producto = await _productoRepository.GetByID(id);
            int auxTotalEntradas = 0;
            int auxTotalSalidas = 0;

            foreach(var k in kardex)
            {
                auxTotalEntradas += k.Entrada;
                auxTotalSalidas += k.Salida;
            }

            return new KardexListDto
            {
                IDProducto = producto.IDProducto,
                CodProducto = producto.CodProducto,
                TotalEntradas = auxTotalEntradas,
                TotalSalidas = auxTotalSalidas,
                StockActual = producto.Stock,
                Kardex = kardex.Select(k => new KardexDto
                {
                    IDKardex = k.IDKardex,
                    IDProducto = k.IDProducto,
                    NumDocumento = k.NumDocumento,
                    TipoMovimiento = k.TipoMovimiento,
                    Entrada = k.Entrada,
                    Salida = k.Salida,
                    Fecha = k.Fecha,
                    IDUser = k.IDUser
                }).ToList()
            };
        }

        public async Task<KardexListDto> GetByCodProducto(string CodProducto)
        {
            var producto = await _productoRepository.GetByCodProducto(CodProducto);
            var kardex = await _kardexRepository.GetByID(producto.IDProducto);
            int auxTotalEntradas = 0;
            int auxTotalSalidas = 0;

            foreach (var k in kardex)
            {
                auxTotalEntradas += k.Entrada;
                auxTotalSalidas += k.Salida;
            }

            return new KardexListDto
            {
                IDProducto = producto.IDProducto,
                CodProducto = producto.CodProducto,
                TotalEntradas = auxTotalEntradas,
                TotalSalidas = auxTotalSalidas,
                StockActual = producto.Stock,
                Kardex = kardex.Select(k => new KardexDto
                {
                    IDKardex = k.IDKardex,
                    IDProducto = k.IDProducto,
                    NumDocumento = k.NumDocumento,
                    TipoMovimiento = k.TipoMovimiento,
                    Entrada = k.Entrada,
                    Salida = k.Salida,
                    Fecha = k.Fecha,
                    IDUser = k.IDUser
                }).ToList()
            };
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

        public async Task<bool> Validate(string CodProducto)
        {
            var producto = await _productoRepository.GetByCodProducto(CodProducto);
            if (producto == null)
            {
                Message.Add("El CodProducto: " + CodProducto + " no pertenece a ningun Producto existente.");
                return false;
            }

            return true;
        }
    }
}
