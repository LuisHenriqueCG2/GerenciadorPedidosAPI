using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.Domain.Enums;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace GerenciadorPedidos.Application.Produtos.Queries;

public class GetProdutoQuery : IRequest<IEnumerable<ProdutoDTO>>
{
    [SwaggerParameter(Description = "Número da página.")]
    public int PageNumber { get; set; } = 1;

    [SwaggerParameter(Description = "Quantidade de itens por página.")]
    public int PageSize { get; set; } = 20;

}

public class GetProdutoQueryHandler(IProdutoService produtoService) :
    IRequestHandler<GetProdutoQuery, IEnumerable<ProdutoDTO>>
{
    public async Task<IEnumerable<ProdutoDTO>> Handle(GetProdutoQuery request, CancellationToken cancellationToken)
    {
        var pedidos = await produtoService.ListarTodosAsync(
            request.PageNumber, 
            request.PageSize);

        return pedidos;
    }
}
