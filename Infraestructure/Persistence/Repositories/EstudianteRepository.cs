using Domain.Dto;
using Domain.Dto.Estudiantes;
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
                    .ThenInclude(_ => _.Calificaciones)
                .FirstOrDefaultAsync(cancellationToken);

            if (estudiante is null)
            {
                return false;
            }

            _dbContext.CursoEstudiante.RemoveRange(estudiante.Matriculas);
            _dbContext.Calificaciones.RemoveRange(estudiante.Matriculas.SelectMany(_ => _.Calificaciones));
            _dbContext.Estudiantes.Remove(estudiante);

            int entities = await _dbContext.SaveChangesAsync();

            return entities > 0;
        }

        public async Task<Estudiante> GetEstudianteByNumDoc(string numDoc, CancellationToken cancellationToken)
        {
            return await _dbContext.Estudiantes.AsNoTracking()
                .FirstOrDefaultAsync(_ => _.DocumentId.Equals(numDoc), cancellationToken);
        }

        public async Task<PaginatedCollection<EstudianteDto>> GetEstudiantesPaginado(int page, int pageSize, CancellationToken cancellationToken)
        {
            var collection = new PaginatedCollection<EstudianteDto>();

            collection.Items = await _dbContext.Estudiantes
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(_ =>
                    new EstudianteDto()
                    {
                        Id = _.Id,
                        Nombres = _.Names,
                        Apellidos = _.Surnames,
                        Correo = _.Email,
                        FechaIngreso = _.EntryDate,
                        NumeroDocumento = _.DocumentId
                    }
                )
                .ToListAsync(cancellationToken);

            collection.TotalCount = await _dbContext.Estudiantes.CountAsync();
            collection.TotalPages = (int)Math.Ceiling((double)collection.TotalCount / (double)pageSize);
            collection.CurrentPage = page;
            collection.PageSize = pageSize;

            collection.HasNextPage = collection.CurrentPage < collection.TotalPages;
            collection.HasPreviousPage = collection.CurrentPage != 1;

            return collection;
        }
    }
}
