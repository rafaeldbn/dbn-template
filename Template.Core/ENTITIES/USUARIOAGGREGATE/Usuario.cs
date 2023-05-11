using Ardalis.GuardClauses;
using $ext_safeprojectname$.SharedKernel;
using $ext_safeprojectname$.SharedKernel.Interfaces;
using System;

namespace $ext_safeprojectname$.Core.Entities.UsuarioAggregate
{
    public class Usuario : BaseEntity<Guid>, IAggregateRoot
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Usuario(string username, string password)
        {
            Username = Guard.Against.NullOrEmpty(username, nameof(username));
            Password = Guard.Against.NullOrEmpty(password, nameof(password));
        }
    }
}
