using $ext_safeprojectname$.Core.Entities.UsuarioAggregate.Events;
using $ext_safeprojectname$.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Core.Entities.UsuarioAggregate.Handler
{
    public class UsuarioAddedHandler : INotificationHandler<UsuarioAddedEvent>
    {
        private readonly IMessageService _messageService;

        public UsuarioAddedHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public Task Handle(UsuarioAddedEvent notification, CancellationToken cancellationToken)
        {
            _messageService.PublishMessage(notification.Usuario);
            return Task.CompletedTask;
        }
    }
}
