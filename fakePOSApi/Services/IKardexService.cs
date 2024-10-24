namespace fakePOSApi.Services
{
    public interface IKardexService<TDto>
    {
        public List<string> Message { get; }
        Task<TDto> GetByID(int id);
        Task<TDto> GetByCodProducto(string CodProducto);
        Task<bool> Validate(int id);
        Task<bool> Validate(string CodProducto);
    }
}
