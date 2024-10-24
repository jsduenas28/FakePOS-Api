namespace fakePOSApi.Services
{
    public interface IService<TDto, TIDto, TUDto>
    {
        public List<string> Message { get; }
        Task<IEnumerable<TDto>> Get();
        Task<TDto> GetByID(int id);
        Task<IEnumerable<TDto>> Search(string search);
        Task<TDto> Add(TIDto dto);
        Task<TDto> Update(int id, TUDto dto);
        Task<TDto> Delete(int id);
        Task<bool> Validate(int id);
        Task<bool> Validate(TIDto dto);
        Task<bool> Validate(TUDto dto);

    }
}
