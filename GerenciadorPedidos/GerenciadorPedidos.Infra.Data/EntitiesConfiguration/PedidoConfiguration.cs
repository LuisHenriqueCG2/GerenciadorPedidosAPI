using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GerenciadorPedidos.Infra.Data.EntitiesConfiguration
{
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
            builder.Property(e => e.StatusPedido).HasMaxLength(10).IsRequired();
        }
    }
}
