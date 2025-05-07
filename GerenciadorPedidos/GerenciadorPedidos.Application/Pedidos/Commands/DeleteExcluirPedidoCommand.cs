using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Interfaces;
using MediatR;

namespace GerenciadorPedidos.Application.Pedidos.Commands;

     public class DeleteExcluirPedidoCommand : IRequest<PedidoDto>
    {
        public required int IdPedido { get; set; }
    }

    public class DeleteExcluirPedidoCommandHandler(IPedidoService pedidoService) :
        IRequestHandler<DeleteExcluirPedidoCommand, PedidoDto>
    {
        public async Task<PedidoDto> Handle(
            DeleteExcluirPedidoCommand request,
            CancellationToken cancellationToken)
        {
            var pedido = await pedidoService.ExcluirPedido(request.IdPedido);
            return pedido;
        }
    }
    
