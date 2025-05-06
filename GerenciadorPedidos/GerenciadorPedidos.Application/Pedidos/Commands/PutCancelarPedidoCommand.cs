using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using MediatR;

namespace GerenciadorPedidos.Application.Pedidos.Commands;

public class PutCancelarPedidoCommand : IRequest<PedidoDTO>
{
    public required int IdPedido { get; set; }
}

public class PutCancelarPedidoCommandHandler(IPedidoService pedidoService) :
    IRequestHandler<PutCancelarPedidoCommand, PedidoDTO>
{
    public async Task<PedidoDTO> Handle(
        PutCancelarPedidoCommand request,
        CancellationToken cancellationToken)
    {
        var pedido = await pedidoService.CancelarPedido(request.IdPedido);
        return pedido;
    }
}