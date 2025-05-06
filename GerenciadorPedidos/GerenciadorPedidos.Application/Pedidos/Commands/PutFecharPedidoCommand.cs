using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using MediatR;

namespace GerenciadorPedidos.Application.Pedidos.Commands;

public class PutFecharPedidoCommand : IRequest<PedidoDTO>
{
    public required int IdPedido { get; set; }
}

public class PutFecharPedidoCommandHandler(IPedidoService pedidoService) :
    IRequestHandler<PutFecharPedidoCommand, PedidoDTO>
{
    public async Task<PedidoDTO> Handle(
        PutFecharPedidoCommand request,
        CancellationToken cancellationToken)
    {
        var pedido = await pedidoService.FecharPedido(request.IdPedido);
        return pedido;
    }
}