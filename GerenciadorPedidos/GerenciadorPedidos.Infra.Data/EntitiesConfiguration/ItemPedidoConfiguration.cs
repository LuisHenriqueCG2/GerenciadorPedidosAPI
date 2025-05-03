using GerenciadorPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorPedidos.Infra.Data.EntitiesConfiguration
{

    public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.HasKey(ip => new { ip.PedidoId, ip.ProdutoId });

            builder.HasOne(ip => ip.Pedido)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(ip => ip.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ip => ip.Produto)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(ip => ip.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ip => ip.Quantidade).IsRequired();
            builder.Property(ip => ip.ValorTotal)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }


    //public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
    //{
    //    public void Configure(EntityTypeBuilder<ItemPedido> builder)
    //    {
    //        builder.HasKey(ip => new { ip.PedidoId, ip.ProdutoId });

    //        builder.HasOne(ip => ip.Pedido)
    //               .WithMany(p => p.ItensPedido)
    //               .HasForeignKey(ip => ip.PedidoId)
    //               .OnDelete(DeleteBehavior.Cascade);


    //        builder.HasOne(ip => ip.Produto)
    //               .WithMany(p => p.ItensPedido) 
    //               .HasForeignKey(ip => ip.ProdutoId)
    //               .OnDelete(DeleteBehavior.Restrict);

    //        builder.Property(ip => ip.Quantidade)
    //               .IsRequired();

    //        builder.Property(ip => ip.ValorTotal)
    //               .HasColumnType("decimal(18,2)")
    //               .IsRequired();
    //    }
    //}
}
