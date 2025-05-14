using System.ComponentModel;
using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.Domain.Enums;
using GerenciadorPedidos.Domain.Validations;
using MediatR;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http.HttpResults;
using Swashbuckle.AspNetCore.Annotations;

namespace GerenciadorPedidos.Application.Pedidos.Queries;

public class GetPedidoQuery : IRequest<IEnumerable<PedidoDto>>
{
    [SwaggerParameter(Description = "Legenda: 1 - Aberto; 2 - Fechado; 3 - Cancelado; 4 - Faturado")]
    public StatusPedidoEnum? StatusPedido { get; set; }

    [SwaggerParameter(Description = "Número da página.")]
    public int PageNumber { get; set; } = 1;

    [SwaggerParameter(Description = "Quantidade de itens por página.")]
    public int PageSize { get; set; } = 20;
}

public class GetPedidoQueryHandler(IPedidoService pedidoService) :
    IRequestHandler<GetPedidoQuery, IEnumerable<PedidoDto>>
{
    public async Task<IEnumerable<PedidoDto>> Handle(GetPedidoQuery request, CancellationToken cancellationToken)
    {
        var pedidos = await pedidoService.ListarTodosAsync(
            request.StatusPedido,
            request.PageNumber,
            request.PageSize);

        return pedidos;
    }
}

