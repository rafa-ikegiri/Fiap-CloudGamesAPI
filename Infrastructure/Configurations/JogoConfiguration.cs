using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class JogoConfiguration: IEntityTypeConfiguration<Jogo>
{
    public void Configure(EntityTypeBuilder<Jogo> builder)
    {
        builder.ToTable("Jogos");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("INT").UseIdentityColumn();
        builder.Property(u => u.DataCriacao).HasColumnType("DATETIME").IsRequired();
        builder.Property(u => u.Titulo).HasColumnType("VARCHAR(50)").IsRequired();
        builder.Property(u => u.Genero).HasColumnType("VARCHAR(50)").IsRequired();
        builder.Property(u => u.Plataforma).HasColumnType("VARCHAR(50)").IsRequired();
        builder.Property(u => u.DtLancamento).HasColumnType("DATETIME");
        builder.Property(u => u.Multiplayer).HasColumnType("bit").IsRequired();

    }
}