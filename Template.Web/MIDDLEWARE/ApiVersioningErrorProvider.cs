using $ext_safeprojectname$.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Net;
using static $ext_safeprojectname$.Web.Middleware.ExceptionHandlerMiddleware;

namespace $ext_safeprojectname$.Web.Middleware
{
    public class ApiVersioningErrorProvider : DefaultErrorResponseProvider
    {
        public override IActionResult CreateResponse(ErrorResponseContext context)
        {
            var error = new ErrorDetails
            {
                StatusCode = (int)InternalErrorCode.VersaoApiNaoInformada,
                Message = "A versão do endpoint é obrigatório"
            };

            var response = new ObjectResult(error);
            response.StatusCode = (int)HttpStatusCode.BadRequest;

            return response;
        }
    }
}
