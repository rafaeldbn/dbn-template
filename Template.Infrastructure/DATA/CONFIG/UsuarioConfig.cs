using $ext_safeprojectname$.Core.Entities.UsuarioAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace $ext_safeprojectname$.Infrastructure.Data.Config
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(p => p.Username)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Password)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
