using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories
{
    public class CursoEstudianteRepository : EFCoreRepository<CursoEstudiante>, ICursoEstudianteRepository
    {
        public CursoEstudianteRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> EliminarMatriculacionEstudianteCurso(long idEstudiante, long idCurso, CancellationToken cancellationToken)
        {
            int entities = await _dbContext.CursoEstudiante.AsNoTracking()
                .Where(_ => _.IdEstudiante == idEstudiante && _.IdCurso == idCurso)
                .ExecuteDeleteAsync(cancellationToken);

            return entities > 0;
        }

        public async Task<CursoEstudiante> GetMatricula(long idEstudiante, long idCurso, CancellationToken cancellationToken)
        {
            return await _dbContext.CursoEstudiante.AsNoTracking()
                .Where(_ => _.IdEstudiante == idEstudiante && _.IdCurso == idCurso)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
