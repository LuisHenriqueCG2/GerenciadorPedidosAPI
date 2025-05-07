using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Interfaces;
using MediatR;

namespace GerenciadorPedidos.Application.Pedidos.Commands;

public class PutCancelarPedidoCommand : IRequest<PedidoDto>
{
    public required int IdPedido { get; set; }
}

public class PutCancelarPedidoCommandHandler(IPedidoService pedidoService) :
    IRequestHandler<PutCancelarPedidoCommand, PedidoDto>
{
    public async Task<PedidoDto> Handle(
        PutCancelarPedidoCommand request,
        CancellationToken cancellationToken)
    {
        var pedido = await pedidoService.CancelarPedido(request.IdPedido);
        return pedido;
    }
}