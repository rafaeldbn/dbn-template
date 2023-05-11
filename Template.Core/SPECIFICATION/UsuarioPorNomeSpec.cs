using Ardalis.Specification;
using $ext_safeprojectname$.Core.Entities.UsuarioAggregate;
using System.Linq;

namespace $ext_safeprojectname$.Core.Specification
{
    public class UsuarioPorNomeSpec : Specification<Usuario>, ISingleResultSpecification
    {
        public UsuarioPorNomeSpec(string username)
        {
            Query.Where(x => x.Username == username);
        }
    }
}
