using $ext_safeprojectname$.SharedKernel.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;

namespace $ext_safeprojectname$.Infrastructure.Data
{
    // inherit from Ardalis.Specification type
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
