using Ardalis.GuardClauses;
using $ext_safeprojectname$.Core.Entities.UsuarioAggregate;
using $ext_safeprojectname$.Core.Entities.UsuarioAggregate.Events;
using $ext_safeprojectname$.Core.Exceptions;
using $ext_safeprojectname$.Core.Interfaces;
using $ext_safeprojectname$.Core.Specification;
using $ext_safeprojectname$.SharedKernel.Extensions;
using $ext_safeprojectname$.SharedKernel.Interfaces;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Core.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IRepository<Usuario> _repository;

        public UsuarioService(IRepository<Usuario> repository)
        {
            _repository = repository;
        }

        public async Task<bool> CriarUsuario(string username, string password)
        {
            Guard.Against.NullOrEmpty(username, nameof(username));
            Guard.Against.NullOrEmpty(password, nameof(password));

            var usuarioPorNomeSpec = new UsuarioPorNomeSpec(username);
            var usuarioJaExiste = await _repository.AnyAsync(usuarioPorNomeSpec);
            if(usuarioJaExiste)
            {
                throw new $ext_safeprojectname$Exception(Enums.InternalErrorCode.UsuarioJaExiste, Enums.InternalErrorCode.UsuarioJaExiste.Description());
            }

            var usuario = new Usuario(username, password);
            usuario.Events.Add(new UsuarioAddedEvent(usuario));
            await _repository.AddAsync(usuario);

            return true;
        }
    }
}
