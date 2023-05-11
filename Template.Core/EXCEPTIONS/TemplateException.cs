using $ext_safeprojectname$.Core.Enums;
using System;
using System.Dynamic;
using System.Collections.Generic;

namespace $ext_safeprojectname$.Core.Exceptions
{
    public class $ext_safeprojectname$Exception : Exception
    {
        private static readonly Dictionary<InternalErrorCode, string> _errorMessages = new()
        {
            { InternalErrorCode.NotAuthorized, "O usuário precisa estar logado para efetuar essa ação." },
            { InternalErrorCode.Forbidden, "Usuário não tem as permissões necessárias para efetuar esta ação." },
            { InternalErrorCode.NotFound, "Entidade não encontrada. Por favor, verifique." }
        };

        public InternalErrorCode DetailErrorCode { get; set; }
        public ExpandoObject ExtraData { get; set; }

        public $ext_safeprojectname$Exception(InternalErrorCode errorCode, string message) : base(message)
        {
            DetailErrorCode = errorCode;
        }

        public $ext_safeprojectname$Exception(InternalErrorCode errorCode, string message, ExpandoObject extraData) : this(errorCode, message)
        {
            ExtraData = extraData;
        }

        public $ext_safeprojectname$Exception(string message) : this(InternalErrorCode.BadRequest, message) { }

        public $ext_safeprojectname$Exception(InternalErrorCode error) : this(error, _errorMessages[error]) { }
    }
}
