using Domain.Entities;
using Domain.Repositories;

namespace Infraestructure.Persistence.Repositories
{
    public class CalificacionRepository : EFCoreRepository<Calificacion>, ICalificacionRepository
    {
        public CalificacionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
