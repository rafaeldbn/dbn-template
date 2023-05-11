using Ardalis.Specification;
using $ext_safeprojectname$.Core.Entities.UsuarioAggregate;
using System.Linq;

namespace $ext_safeprojectname$.Core.Specification
{
    public class UsuariosPaginadosSpec : Specification<Usuario>
    {
        public UsuariosPaginadosSpec(int page, int size)
        {
            Query.OrderBy(x => x.Username)
                .AplicarPaginacao(page, size);
        }
    }
}
