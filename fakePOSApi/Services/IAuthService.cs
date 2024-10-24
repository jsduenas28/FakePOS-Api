namespace fakePOSApi.Services
{
    public interface IAuthService<TRDto, TLDto, TEntity>
    {
        public List<string> Message { get; }
        Task<bool> Register(TRDto dto);
        Task<string> Login(TLDto dto);
        Task<bool> Validate(TLDto dto);
        string GenerateToken(TEntity entity);
    }
}
