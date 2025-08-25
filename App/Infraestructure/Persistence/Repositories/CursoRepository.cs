using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories
{
    public class CursoRepository : EFCoreRepository<Curso>, ICursoRepository
    {
        public CursoRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<bool> DeleteById(long id, CancellationToken cancellationToken)
        {
            var curso = await _dbContext.Cursos.AsNoTracking()
                .Where(_ => _.Id == id)
                .Include(_ => _.Matriculas)
                    .ThenInclude(_ => _.Calificaciones)
                .FirstOrDefaultAsync(cancellationToken);

            if (curso is null)
            {
                return false;
            }

            _dbContext.CursoEstudiante.RemoveRange(curso.Matriculas);
            _dbContext.Calificaciones.RemoveRange(curso.Matriculas.SelectMany(_ => _.Calificaciones));
            _dbContext.Cursos.Remove(curso);

            int entities = await _dbContext.SaveChangesAsync();

            return entities > 0;
        }
    }
}
