using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorPedidosAPI.Models;

public partial class GerenciadorPedidosContext : DbContext
{
    public GerenciadorPedidosContext()
    {
    }

    public GerenciadorPedidosContext(DbContextOptions<GerenciadorPedidosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    public virtual DbSet<ProdutosPedido> ProdutosPedidos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-5S419HC\\SQLEXPRESS;Initial Catalog=GerenciadorPedidos;Integrated Security=True;Encrypt=False;Trust Server Certificate=True;Command Timeout=300");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Pedidos__3214EC2788395CFB");

            entity.Property(e => e.DataCancelamento).HasColumnType("datetime");
            entity.Property(e => e.DataEmissao).HasColumnType("datetime");
            entity.Property(e => e.DataFaturamento).HasColumnType("datetime");
            entity.Property(e => e.DataFechamento).HasColumnType("datetime");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.StatusPedido).HasMaxLength(10);
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Produtos__3214EC27CF76008F");

            entity.Property(e => e.DataCadastro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ProdutosPedido>(entity =>
        {
            entity.HasKey(e => new { e.IDPedido, e.IDProduto }).HasName("PK__Produtos__04E925AE04E7687C");

            entity.Property(e => e.ValorProduto).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IDPedidoNavigation).WithMany(p => p.ProdutosPedidos)
                .HasForeignKey(d => d.IDPedido)
                .HasConstraintName("FK_ProdutosPedidos_Pedido");

            entity.HasOne(d => d.IDProdutoNavigation).WithMany(p => p.ProdutosPedidos)
                .HasForeignKey(d => d.IDProduto)
                .HasConstraintName("FK_ProdutosPedidos_Produto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
