using System.Threading.Tasks;

namespace $ext_safeprojectname$.Core.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> CriarUsuario(string username, string password);
    }
}
