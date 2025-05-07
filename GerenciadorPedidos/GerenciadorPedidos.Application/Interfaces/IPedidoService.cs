using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Domain.Entities;
using GerenciadorPedidos.Domain.Enums;

namespace GerenciadorPedidos.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<PedidoDto> AdicionarPedido(string descricaoPedido);
        Task<PedidoDto> AdicionarProdutoAoPedido(int pedidoId, int produtoId, int quantidade);
        Task<PedidoDto> FecharPedido(int pedidoId);
        Task<PedidoDto> FaturarPedido(int pedidoId);
        Task<PedidoDto> CancelarPedido(int pedidoId);
        Task<PedidoDto> ExcluirPedido(int pedidoId);
        Task<PedidoDto> RemoverProdutoDoPedido(int pedidoId, int produtoId);
        Task<PedidoDto> ListarPedidoID(int id);
        Task<IEnumerable<PedidoDto>> ListarTodosAsync(StatusPedidoEnum? status, int pageNumber, int pageSize);
    }


}
