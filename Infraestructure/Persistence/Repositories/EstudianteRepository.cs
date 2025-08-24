using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories
{
    public class EstudianteRepository : EFCoreRepository<Estudiante>, IEstudianteRepository
    {
        public EstudianteRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<bool> DeleteById(long id, CancellationToken cancellationToken)
        {
            var estudiante = await _dbContext.Estudiantes.AsNoTracking()
                .Where(_ => _.Id == id)
                .Include(_ => _.Matriculas)
                .FirstOrDefaultAsync(cancellationToken);

            if (estudiante is null)
            {
                return false;
            }

            _dbContext.CursoEstudiante.RemoveRange(estudiante.Matriculas);
            _dbContext.Estudiantes.Remove(estudiante);

            int entities = await _dbContext.SaveChangesAsync();

            return entities > 0;
        }
    }
}
