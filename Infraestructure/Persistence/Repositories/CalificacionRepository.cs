using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories
{
    public class CalificacionRepository : EFCoreRepository<Calificacion>, ICalificacionRepository
    {
        public CalificacionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Calificacion>> GetCalificacionesByEstudianteYCurso(long idEstudiante, long idCurso, CancellationToken cancellationToken)
        {
            return await _dbContext.Calificaciones.AsNoTracking()
                .Where(_ => _.CursoEstudiante.IdEstudiante == idEstudiante && _.CursoEstudiante.IdCurso == idCurso)
                .ToListAsync(cancellationToken);
        }
    }
}
