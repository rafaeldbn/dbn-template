using Ardalis.Specification;

namespace $ext_safeprojectname$.SharedKernel.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}