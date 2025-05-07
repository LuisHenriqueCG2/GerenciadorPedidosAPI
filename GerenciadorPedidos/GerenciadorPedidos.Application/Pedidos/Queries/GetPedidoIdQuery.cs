using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.Domain.Enums;
using MediatR;

namespace GerenciadorPedidos.Application.Pedidos.Queries;
public class GetPedidoIdQuery : IRequest<PedidoDto>
{
    public required int IdPedido { get; set; }
}

public class GetPedidoIdQueryHandler(IPedidoService pedidoService) :
    IRequestHandler<GetPedidoIdQuery, PedidoDto>
{
    public async Task<PedidoDto> Handle(
        GetPedidoIdQuery request,
        CancellationToken cancellationToken)
    {
        var pedido = await pedidoService.ListarPedidoID(request.IdPedido);
        return pedido;
    }
}