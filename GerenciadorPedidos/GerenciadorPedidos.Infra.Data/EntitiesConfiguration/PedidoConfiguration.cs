using GerenciadorPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorPedidos.Infra.Data.EntitiesConfiguration;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(e => e.DescricaoPedido).HasMaxLength(255).IsRequired();
        builder.Property(e => e.DataAbertura).HasDefaultValueSql("(getdate())").HasColumnType("datetime").IsRequired();
        builder.Property(e => e.DataFechamento).HasColumnType("datetime");
        builder.Property(e => e.DataCancelamento).HasColumnType("datetime");
        builder.Property(e => e.DataFaturamento).HasColumnType("datetime");
        builder.Property(e => e.StatusPedidoEnum).HasMaxLength(10).IsRequired();
    }
}