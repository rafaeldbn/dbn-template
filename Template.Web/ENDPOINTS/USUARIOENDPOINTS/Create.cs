using Ardalis.ApiEndpoints;
using $ext_safeprojectname$.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Web.Endpoints.UsuarioEndpoints
{
    public class Create : EndpointBaseAsync
        .WithRequest<CreateUsuarioRequest>
        .WithActionResult
    {
        private readonly IUsuarioService _usuarioService;

        public Create(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [MapToApiVersion("1.0")]
        [HttpPost("usuario")]
        [SwaggerOperation(
            Summary = "Cria um usuário",
            OperationId = "Usuario.Create",
            Tags = new[] { "UsuarioEndpoints" })
        ]
        public override async Task<ActionResult> HandleAsync([FromBody] CreateUsuarioRequest request, CancellationToken cancellationToken = default)
        {
            var usuarioAdicionado = await _usuarioService.CriarUsuario(request.Username, request.Password);
            if (usuarioAdicionado) return Ok();

            return BadRequest();
        }
    }
}
