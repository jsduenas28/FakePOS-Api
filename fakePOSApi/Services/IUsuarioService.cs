namespace fakePOSApi.Services
{
    public interface IUsuarioService<TDto, TUDto, TCPDTO>
    {
        public List<string> Message { get; }
        Task<IEnumerable<TDto>> Get();
        Task<IEnumerable<TDto>> Search(string CodUser);
        Task<TDto> GetByID(int id);
        Task<TDto> Update(int id, TUDto dto);
        Task ChangeIsAdmin(int id);
        Task ChangeIsActive(int id);
        Task ChangePassword(int id, TCPDTO dto);
        Task<bool> Validate(int id);
        Task<bool> Validate(int id, TCPDTO dto);
    }
}
