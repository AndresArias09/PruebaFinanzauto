using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories
{
    public class ProfesorRepository : EFCoreRepository<Profesor>, IProfesorRepository
    {
        public ProfesorRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Profesor> GetProfesorByNumDoc(string numDoc, CancellationToken cancellationToken)
        {
            return await _dbContext.Profesores.AsNoTracking()
                .FirstOrDefaultAsync(_ => _.DocumentId.Equals(numDoc), cancellationToken);
        }
    }
}
