using Domain.Entities;
using Domain.Repositories;

namespace Infraestructure.Persistence.Repositories
{
    public class CursoEstudianteRepository : EFCoreRepository<CursoEstudiante>, ICursoEstudianteRepository
    {
        public CursoEstudianteRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
