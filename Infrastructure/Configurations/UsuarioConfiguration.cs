using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{

    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuario");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("INT").UseIdentityColumn();
        builder.Property(u => u.DataCriacao).HasColumnType("DATETIME").IsRequired();
        builder.Property(u => u.Nome).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(u => u.Email).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(u => u.Senha).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(u => u.IsAdmin).HasColumnType("bit").IsRequired();
    }
}
