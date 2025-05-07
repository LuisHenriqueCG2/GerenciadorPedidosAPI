using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Interfaces;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace GerenciadorPedidos.Application.Produtos.Queries;

public class GetProdutoQuery : IRequest<IEnumerable<ProdutoDto>>
{
    [SwaggerParameter(Description = "Número da página.")]
    public int PageNumber { get; set; } = 1;

    [SwaggerParameter(Description = "Quantidade de itens por página.")]
    public int PageSize { get; set; } = 20;
}

public class GetProdutoQueryHandler(IProdutoService produtoService) :
    IRequestHandler<GetProdutoQuery, IEnumerable<ProdutoDto>>
{
    public async Task<IEnumerable<ProdutoDto>> Handle(GetProdutoQuery request, CancellationToken cancellationToken)
    {
        var pedidos = await produtoService.ListarTodosAsync(
            request.PageNumber,
            request.PageSize);

        return pedidos;
    }
}