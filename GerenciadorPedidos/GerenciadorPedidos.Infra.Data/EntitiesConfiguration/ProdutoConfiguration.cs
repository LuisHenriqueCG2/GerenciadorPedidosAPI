using GerenciadorPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorPedidos.Infra.Data.EntitiesConfiguration;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(e => e.DataCadastro).HasDefaultValueSql("(getdate())").HasColumnType("datetime").IsRequired();
        builder.Property(e => e.Descricao).HasMaxLength(255).IsRequired();
        builder.Property(e => e.PrecoUnitario).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(e => e.Quantidade).IsRequired();
    }
}