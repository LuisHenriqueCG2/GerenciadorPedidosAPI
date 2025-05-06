using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.Domain.Enums;
using MediatR;

namespace GerenciadorPedidos.Application.Pedidos.Queries;
public class GetPedidoIdQuery : IRequest<PedidoDTO>
{
    public required int IdPedido { get; set; }
}

public class GetPedidoIdQueryHandler(IPedidoService pedidoService) :
    IRequestHandler<GetPedidoIdQuery, PedidoDTO>
{
    public async Task<PedidoDTO> Handle(
        GetPedidoIdQuery request,
        CancellationToken cancellationToken)
    {
        var pedido = await pedidoService.ListarPedidoID(request.IdPedido);
        return pedido;
    }
}