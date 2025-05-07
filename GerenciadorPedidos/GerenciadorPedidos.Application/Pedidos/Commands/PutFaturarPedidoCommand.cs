using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Interfaces;
using MediatR;

namespace GerenciadorPedidos.Application.Pedidos.Commands;

public class PutFaturarPedidoCommand : IRequest<PedidoDto>
{
    public required int IdPedido { get; set; }
}

public class PutFaturarPedidoCommandHandler(IPedidoService pedidoService) :
    IRequestHandler<PutFaturarPedidoCommand, PedidoDto>
{
    public async Task<PedidoDto> Handle(
        PutFaturarPedidoCommand request,
        CancellationToken cancellationToken)
    {
        var pedido = await pedidoService.FaturarPedido(request.IdPedido);
        return pedido;
    }
}