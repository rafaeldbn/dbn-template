using $ext_safeprojectname$.Core.Entities.UsuarioAggregate;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Core.Interfaces
{
    public interface IMessageService
    {
        Task PublishMessage(Usuario usuario);
    }
}
