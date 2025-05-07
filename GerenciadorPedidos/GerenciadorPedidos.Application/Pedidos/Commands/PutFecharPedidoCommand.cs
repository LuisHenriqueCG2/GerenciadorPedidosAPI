using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Interfaces;
using MediatR;

namespace GerenciadorPedidos.Application.Pedidos.Commands;

public class PutFecharPedidoCommand : IRequest<PedidoDto>
{
    public required int IdPedido { get; set; }
}

public class PutFecharPedidoCommandHandler(IPedidoService pedidoService) :
    IRequestHandler<PutFecharPedidoCommand, PedidoDto>
{
    public async Task<PedidoDto> Handle(
        PutFecharPedidoCommand request,
        CancellationToken cancellationToken)
    {
        var pedido = await pedidoService.FecharPedido(request.IdPedido);
        return pedido;
    }
}