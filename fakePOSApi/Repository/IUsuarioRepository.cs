namespace fakePOSApi.Repository
{
    public interface IUsuarioRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> Get();
        Task<IEnumerable<TEntity>> Search(string codUser);
        Task<TEntity> GetByID(int id);
        void Update(TEntity entity);
        Task Save();
    }
}
