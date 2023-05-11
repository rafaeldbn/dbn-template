using Ardalis.ApiEndpoints;
using $ext_safeprojectname$.Core.Entities.UsuarioAggregate;
using $ext_safeprojectname$.Core.Specification;
using $ext_safeprojectname$.SharedKernel;
using $ext_safeprojectname$.SharedKernel.Interfaces;
using $ext_safeprojectname$.Web.Base;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.Web.Endpoints.UsuarioEndpoints
{
    public class List : EndpointBaseAsync
        .WithRequest<PagedRequest>
        .WithResult<PagedList<ListUsuarioResponse>>
    {
        private readonly IReadRepository<Usuario> _readRepository;

        public List(IReadRepository<Usuario> readRepository)
        {
            _readRepository = readRepository;
        }

        [MapToApiVersion("1.0")]
        [HttpGet("usuario")]
        [SwaggerOperation(
            Summary = "Retorna uma listagem paginada de usuário",
            OperationId = "Usuario.List",
            Tags = new[] { "UsuarioEndpoints" })
        ]
        public override async Task<PagedList<ListUsuarioResponse>> HandleAsync([FromQuery] PagedRequest request, CancellationToken cancellationToken = default)
        {
            var spec = new UsuariosPaginadosSpec(request.Page, request.Size);
            var total = await _readRepository.CountAsync();
            var usuarios = await _readRepository.ListAsync(spec);

            return new PagedList<ListUsuarioResponse>
            {
                Items = usuarios.Select(x => new ListUsuarioResponse
                {
                    Username = x.Username,
                    Password = x.Password
                }).ToList(),
                TotalItems = total
            };
        }
    }
}
