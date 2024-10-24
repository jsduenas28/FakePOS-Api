namespace fakePOSApi.Repository
{
    public interface IAuthRepository<TEntity>
    {
        Task Register(TEntity entity);
        Task<TEntity> Login(string CodUser);
        Task<bool> UserExist(string CodUser);
        Task Save();
    }
}
