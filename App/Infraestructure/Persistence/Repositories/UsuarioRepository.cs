using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories
{
    public class UsuarioRepository : EFCoreRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Usuario> GetUsuarioByUserName(string userName)
        {
            return await _dbContext.Usuarios.AsNoTracking()
                .FirstOrDefaultAsync(_ => _.UserName.Equals(userName));
        }
    }
}
