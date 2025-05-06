using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorPedidos.Domain.Entities;
using GerenciadorPedidos.Domain.Enums;
using GerenciadorPedidos.Domain.Interfaces;
using GerenciadorPedidos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorPedidos.Infra.Data.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AdicionarPedido(Pedido pedido)
        {
            pedido.DataFaturamento = null;
            pedido.DataCancelamento = null;
            pedido.DataFechamento = null;
            await _context.Pedidos.AddAsync(pedido);   
            await _context.SaveChangesAsync();       
        }

        public async Task<Pedido> AlterarPedido(int id, Pedido pedidoAtualizado)
        {
            var pedidoExistente = await _context.Pedidos
                .Include(p => p.ItensPedido)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedidoExistente == null)
                throw new Exception("Pedido não encontrado");

            pedidoExistente.DescricaoPedido = pedidoAtualizado.DescricaoPedido;
            pedidoExistente.StatusPedido = pedidoAtualizado.StatusPedido;

            var itensAtualizados = pedidoAtualizado.ItensPedido.ToList();
            var itensRemovidos = pedidoExistente.ItensPedido
                .Where(itemExistente => !itensAtualizados.Any(i => i.ProdutoId == itemExistente.ProdutoId))
                .ToList();

            foreach (var item in itensRemovidos)
            {
                _context.Entry(item).State = EntityState.Modified;
            }

            foreach (var itemAtualizado in itensAtualizados)
            {
                if (!pedidoExistente.ItensPedido.Any(i => i.ProdutoId == itemAtualizado.ProdutoId))
                {
                    pedidoExistente.ItensPedido.Add(itemAtualizado);
                }
            }

            await _context.SaveChangesAsync();
            return pedidoExistente;
        }

        public async Task<Pedido> CancelarPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.ItensPedido)
                .ThenInclude(ip => ip.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null) throw new Exception("Pedido não encontrado");

            pedido.StatusPedido = StatusPedido.Cancelado;
            pedido.DataCancelamento = DateTime.Now;

            await _context.SaveChangesAsync();

            return pedido;
        }

        public async Task<Pedido> ExcluirPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return null;

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido> FaturarPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.ItensPedido)
                .ThenInclude(ip => ip.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null) throw new Exception("Pedido não encontrado");

            if (!pedido.ItensPedido.Any())
            {
                throw new InvalidOperationException("O pedido precisa ter pelo menos um produto para ser faturado.");
            }

            pedido.StatusPedido = StatusPedido.Faturado;
            pedido.DataFaturamento = DateTime.Now;

            await _context.SaveChangesAsync();

            return pedido;
        }

        public async Task<Pedido> FecharPedido(int pedidoId)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.ItensPedido)
                .ThenInclude(ip => ip.Produto)
                .FirstOrDefaultAsync(p => p.Id == pedidoId);

            if (pedido == null) throw new Exception("Pedido não encontrado");

            if (!pedido.ItensPedido.Any())
            {
                throw new InvalidOperationException("O pedido precisa ter pelo menos um produto para ser fechado.");
            }

            pedido.StatusPedido = StatusPedido.Fechado;
            pedido.DataFechamento = DateTime.Now;

            await _context.SaveChangesAsync();

            return pedido;
        }

        public async Task<Pedido> ListarPedidoPorID(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.ItensPedido)
                .ThenInclude(ip => ip.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                throw new KeyNotFoundException($"Pedido com ID {id} não encontrado.");

            return pedido;
        }

        public async Task<IEnumerable<Pedido>> ListarTodos(StatusPedido? status, int pageNumber, int pageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            var query = _context.Pedidos
                .Include(p => p.ItensPedido)
                .ThenInclude(ip => ip.Produto)
                .AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(p => p.StatusPedido == status.Value);
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
