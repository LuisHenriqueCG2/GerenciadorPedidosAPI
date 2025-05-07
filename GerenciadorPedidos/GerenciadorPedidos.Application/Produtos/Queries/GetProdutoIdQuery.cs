using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Interfaces;
using MediatR;

namespace GerenciadorPedidos.Application.Produtos.Queries;

public class GetProdutoIdQuery : IRequest<ProdutoDto>
{
    public required int IdProduto { get; set; }
}

public class GetProdutoIdQueryHandler(IProdutoService produtoService) :
    IRequestHandler<GetProdutoIdQuery, ProdutoDto>
{
    public async Task<ProdutoDto> Handle(
        GetProdutoIdQuery request,
        CancellationToken cancellationToken)
    {
        var produto = await produtoService.ListarPorId(request.IdProduto);
        return produto;
    }
}