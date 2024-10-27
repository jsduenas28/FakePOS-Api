using fakePOSApi.DTOs;
using fakePOSApi.Models;
using fakePOSApi.Repository;

namespace fakePOSApi.Services
{
    public class CategoriaService : IService<CategoriaDto, CategoriaInsertDto, CategoriaUpdateDto>
    {
        private IRepository<Categoria> _repository;
        public List<string> Message { get; private set; } = new List<string>();

        public CategoriaService([FromKeyedServices("categoriaRepository")] IRepository<Categoria> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoriaDto>> Get()
        {
            var categorias = await _repository.Get();

            return categorias.Select(c => new CategoriaDto
            {
                IDCategoria = c.IDCategoria,
                CodCategoria = c.CodCategoria,
                Descripcion = c.Descripcion,
                CreateAt = c.CreateAt,
                UpdateAt = c.UpdateAt
            });
        }

        public async Task<CategoriaDto> GetByID(int id)
        {
            var categoria = await _repository.GetByID(id);

            return new CategoriaDto
            {
                IDCategoria = categoria.IDCategoria,
                CodCategoria = categoria.CodCategoria,
                Descripcion = categoria.Descripcion,
                CreateAt = categoria.CreateAt,
                UpdateAt = categoria.UpdateAt
            };
        }

        public async Task<IEnumerable<CategoriaDto>> Search(string search)
        {
            var categorias = await _repository.Search(search);

            return categorias.Select(c => new CategoriaDto
            {
                IDCategoria = c.IDCategoria,
                CodCategoria = c.CodCategoria,
                Descripcion = c.Descripcion,
                CreateAt = c.CreateAt,
                UpdateAt = c.UpdateAt
            });
        }

        public async Task<CategoriaDto> Add(CategoriaInsertDto dto)
        {
            var categoria = new Categoria()
            {
                CodCategoria = dto.CodCategoria,
                Descripcion = dto.Descripcion,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now
            };

            await _repository.Add(categoria);
            await _repository.Save();

            return new CategoriaDto
            {
                IDCategoria = categoria.IDCategoria,
                CodCategoria = categoria.CodCategoria,
                Descripcion = categoria.Descripcion,
                CreateAt = categoria.CreateAt,
                UpdateAt = categoria.UpdateAt
            };
        }

        public async Task<CategoriaDto> Update(int id, CategoriaUpdateDto dto)
        {
            var categoria = await _repository.GetByID(id);

            categoria.Descripcion = dto.Descripcion;
            categoria.UpdateAt = DateTime.Now;
            _repository.Update(categoria);
            await _repository.Save();

            return new CategoriaDto
            {
                IDCategoria = categoria.IDCategoria,
                CodCategoria = categoria.CodCategoria,
                Descripcion = categoria.Descripcion,
                CreateAt = categoria.CreateAt,
                UpdateAt = categoria.UpdateAt
            };
        }

        public async Task<CategoriaDto> Delete(int id)
        {
            var categoria = await _repository.GetByID(id);

            var categoriaDto =  new CategoriaDto
            {
                IDCategoria = categoria.IDCategoria,
                CodCategoria = categoria.CodCategoria,
                Descripcion = categoria.Descripcion,
                CreateAt = categoria.CreateAt,
                UpdateAt = categoria.UpdateAt
            };

            _repository.Delete(categoria);
            await _repository.Save();

            return categoriaDto;
        }


        public async Task<bool> Validate(int id)
        {
            var categoria = await _repository.GetByID(id);

            if(categoria == null)
            {
                Message.Add("El IDCategoria no existe.");
                return false;
            }

            return true;
        }

        public Task<bool> Validate(CategoriaInsertDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Validate(CategoriaUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
