using GerenciadorPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorPedidos.Infra.Data.Context;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }

    public DbSet<ItemPedido> ItemPedido { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}