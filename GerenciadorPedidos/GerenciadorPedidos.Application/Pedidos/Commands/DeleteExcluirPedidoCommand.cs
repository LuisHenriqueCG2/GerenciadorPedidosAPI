using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using MediatR;

namespace GerenciadorPedidos.Application.Pedidos.Commands;

     public class DeleteExcluirPedidoCommand : IRequest<PedidoDTO>
    {
        public required int IdPedido { get; set; }
    }

    public class DeleteExcluirPedidoCommandHandler(IPedidoService pedidoService) :
        IRequestHandler<DeleteExcluirPedidoCommand, PedidoDTO>
    {
        public async Task<PedidoDTO> Handle(
            DeleteExcluirPedidoCommand request,
            CancellationToken cancellationToken)
        {
            var pedido = await pedidoService.ExcluirPedido(request.IdPedido);
            return pedido;
        }
    }
    
