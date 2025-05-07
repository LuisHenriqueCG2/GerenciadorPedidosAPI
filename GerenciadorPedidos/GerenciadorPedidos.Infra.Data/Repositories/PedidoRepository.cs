using GerenciadorPedidos.Domain.Entities;
using GerenciadorPedidos.Domain.Enums;
using GerenciadorPedidos.Domain.Interfaces;
using GerenciadorPedidos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorPedidos.Infra.Data.Repositories;

public class PedidoRepository(ApplicationDbContext context) : IPedidoRepository
{
    public async Task AdicionarPedido(Pedido pedido)
    {
        pedido.DataFaturamento = null;
        pedido.DataCancelamento = null;
        pedido.DataFechamento = null;
        await context.Pedidos.AddAsync(pedido);
        await context.SaveChangesAsync();
    }

    public async Task<Pedido> AlterarPedido(int id, Pedido pedidoAtualizado)
    {
        var pedidoExistente = await context.Pedidos
            .Include(p => p.ItensPedido)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedidoExistente == null)
            throw new Exception("Pedido não encontrado");

        pedidoExistente.DescricaoPedido = pedidoAtualizado.DescricaoPedido;
        pedidoExistente.StatusPedidoEnum = pedidoAtualizado.StatusPedidoEnum;

        var itensAtualizados = pedidoAtualizado.ItensPedido.ToList();
        var itensRemovidos = pedidoExistente.ItensPedido
            .Where(itemExistente => itensAtualizados.All(i => i.ProdutoId != itemExistente.ProdutoId))
            .ToList();

        foreach (var item in itensRemovidos)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        foreach (var itemAtualizado in itensAtualizados)
        {
            if (pedidoExistente.ItensPedido.All(i => i.ProdutoId != itemAtualizado.ProdutoId))
            {
                pedidoExistente.ItensPedido.Add(itemAtualizado);
            }
        }

        await context.SaveChangesAsync();
        return pedidoExistente;
    }

    public async Task<Pedido> CancelarPedido(int id)
    {
        var pedido = await context.Pedidos
            .Include(p => p.ItensPedido)
            .ThenInclude(ip => ip.Produto)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido == null) throw new Exception("Pedido não encontrado");

        pedido.StatusPedidoEnum = StatusPedidoEnum.Cancelado;
        pedido.DataCancelamento = DateTime.Now;

        await context.SaveChangesAsync();

        return pedido;
    }

    public async Task<Pedido> ExcluirPedido(int id)
    {
        var pedido = await context.Pedidos.FindAsync(id);
        if (pedido == null) return null;

        context.Pedidos.Remove(pedido);
        await context.SaveChangesAsync();
        return pedido;
    }

    public async Task<Pedido> FaturarPedido(int id)
    {
        var pedido = await context.Pedidos
            .Include(p => p.ItensPedido)
            .ThenInclude(ip => ip.Produto)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido == null) throw new Exception("Pedido não encontrado");

        if (!pedido.ItensPedido.Any())
        {
            throw new InvalidOperationException("O pedido precisa ter pelo menos um produto para ser faturado.");
        }

        pedido.StatusPedidoEnum = StatusPedidoEnum.Faturado;
        pedido.DataFaturamento = DateTime.Now;

        await context.SaveChangesAsync();

        return pedido;
    }

    public async Task<Pedido> FecharPedido(int pedidoId)
    {
        var pedido = await context.Pedidos
            .Include(p => p.ItensPedido)
            .ThenInclude(ip => ip.Produto)
            .FirstOrDefaultAsync(p => p.Id == pedidoId);

        if (pedido == null) throw new Exception("Pedido não encontrado");

        if (!pedido.ItensPedido.Any())
        {
            throw new InvalidOperationException("O pedido precisa ter pelo menos um produto para ser fechado.");
        }

        pedido.StatusPedidoEnum = StatusPedidoEnum.Fechado;
        pedido.DataFechamento = DateTime.Now;

        await context.SaveChangesAsync();

        return pedido;
    }

    public async Task<Pedido> ListarPedidoPorID(int id)
    {
        var pedido = await context.Pedidos
            .Include(p => p.ItensPedido)
            .ThenInclude(ip => ip.Produto)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pedido == null)
            throw new KeyNotFoundException($"Pedido com ID {id} não encontrado.");

        return pedido;
    }

    public async Task<IEnumerable<Pedido>> ListarTodos(StatusPedidoEnum? status, int pageNumber, int pageSize)
    {
        pageNumber = pageNumber < 1 ? 1 : pageNumber;
        pageSize = pageSize < 1 ? 10 : pageSize;

        var query = context.Pedidos
            .Include(p => p.ItensPedido)
            .ThenInclude(ip => ip.Produto)
            .AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(p => p.StatusPedidoEnum == status.Value);
        }

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}