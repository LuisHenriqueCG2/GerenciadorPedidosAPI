using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Domain.Entities;
using GerenciadorPedidos.Domain.Enums;

namespace GerenciadorPedidos.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<Pedido> AdicionarPedido(string descricaoPedido);
        Task<PedidoDTO> AdicionarProdutoAoPedido(int pedidoId, int produtoId, int quantidade);
        Task<PedidoDTO> FecharPedido(int pedidoId);
        Task<PedidoDTO> FaturarPedido(int pedidoId);
        Task<PedidoDTO> ExcluirPedido(int pedidoId);
        Task<PedidoDTO> RemoverProdutoDoPedido(int pedidoId, int produtoId);
        Task<PedidoDTO> ListarPedidoID(int id);
        Task<IEnumerable<PedidoDTO>> ListarTodosAsync(StatusPedido? status, int pageNumber, int pageSize);
    }


}
