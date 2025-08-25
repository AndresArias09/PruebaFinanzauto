using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        public Task<Usuario> GetUsuarioByUserName(string userName);
    }
}
