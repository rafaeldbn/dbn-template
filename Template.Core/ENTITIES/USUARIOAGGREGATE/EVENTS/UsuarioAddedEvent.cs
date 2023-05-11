using $ext_safeprojectname$.SharedKernel;

namespace $ext_safeprojectname$.Core.Entities.UsuarioAggregate.Events
{
    public class UsuarioAddedEvent : BaseDomainEvent
    {
        public UsuarioAddedEvent(Usuario usuario)
        {
            Usuario = usuario;
        }

        public Usuario Usuario { get; set; }

    }
}
