using $ext_safeprojectname$.Core.Entities.UsuarioAggregate;
using $ext_safeprojectname$.SharedKernel;
using System;

namespace $ext_safeprojectname$.Core.DTO
{
    [BsonCollection("usuarios_log")]
    public class UsuarioAuditDto : Document
    {
        public Usuario Usuario { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
