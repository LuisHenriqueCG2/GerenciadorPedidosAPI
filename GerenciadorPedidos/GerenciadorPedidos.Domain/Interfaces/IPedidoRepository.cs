using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorPedidos.Domain.Entities;
using GerenciadorPedidos.Domain.Enums;

namespace GerenciadorPedidos.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task AdicionarPedido(Pedido pedido);
        Task<Pedido> AlterarPedido(int id, Pedido pedidoAtualizado);
        Task<Pedido> FecharPedido(int id);
        Task<Pedido> FaturarPedido(int id);
        Task<Pedido> ExcluirPedido(int id);
        Task<Pedido> ListarPedidoPorID(int id);
        Task<IEnumerable<Pedido>> ListarTodos(StatusPedido? status, int pageNumber, int pageSize);
        Task<bool> SaveAllAsync();
    }
}

