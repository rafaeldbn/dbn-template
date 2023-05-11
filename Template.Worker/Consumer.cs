using $ext_safeprojectname$.Core.DTO;
using $ext_safeprojectname$.Core.Entities.UsuarioAggregate;
using $ext_safeprojectname$.Core.Interfaces;
using MassTransit;

namespace $ext_safeprojectname$.Worker
{
    public class Consumer : IConsumer<Usuario>
    {
        private readonly INoSqlRepository<UsuarioAuditDto> _noSqlRepository;

        public Consumer(INoSqlRepository<UsuarioAuditDto> noSqlRepository)
        {
            _noSqlRepository = noSqlRepository;
        }

        public async Task Consume(ConsumeContext<Usuario> context)
        {
            var usuarioDto = new UsuarioAuditDto
            {
                Usuario = context.Message,
                DataCriacao = DateTime.Now
            };

            _noSqlRepository.InsertOne(usuarioDto);
        }
    }
}
