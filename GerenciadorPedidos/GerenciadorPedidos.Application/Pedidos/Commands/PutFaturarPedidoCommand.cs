using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using MediatR;

namespace GerenciadorPedidos.Application.Pedidos.Commands;

public class PutFaturarPedidoCommand : IRequest<PedidoDTO>
{
    public required int IdPedido { get; set; }
}

public class PutFaturarPedidoCommandHandler(IPedidoService pedidoService) :
    IRequestHandler<PutFaturarPedidoCommand, PedidoDTO>
{
    public async Task<PedidoDTO> Handle(
        PutFaturarPedidoCommand request,
        CancellationToken cancellationToken)
    {
        var pedido = await pedidoService.FaturarPedido(request.IdPedido);
        return pedido;
    }
}