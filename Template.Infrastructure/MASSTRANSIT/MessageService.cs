using Ardalis.GuardClauses;
using $ext_safeprojectname$.Core.Entities.UsuarioAggregate;
using $ext_safeprojectname$.Core.Interfaces;
using $ext_safeprojectname$.SharedKernel.Util;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Infrastructure.MassTransit
{
    public class MessageService : IMessageService
    {
        private readonly IBus _bus;

        public MessageService(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishMessage(Usuario usuario)
        {
            Guard.Against.Null(usuario);
            var uri = new Uri($"{AmbienteUtil.GetValue("CONNECTION_STRING_RABBIT_MQ")}/usuario");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(usuario);
        }
    }
}
