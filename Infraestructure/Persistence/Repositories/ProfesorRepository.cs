using Domain.Entities;
using Domain.Repositories;

namespace Infraestructure.Persistence.Repositories
{
    public class ProfesorRepository : EFCoreRepository<Profesor>, IProfesorRepository
    {
        public ProfesorRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
