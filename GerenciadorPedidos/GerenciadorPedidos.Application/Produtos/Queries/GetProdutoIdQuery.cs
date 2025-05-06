using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.Application.Pedidos.Queries;
using MediatR;

namespace GerenciadorPedidos.Application.Produtos.Queries;

public class GetProdutoIdQuery : IRequest<ProdutoDTO>
{
    public required int IdProduto { get; set; }
}

public class GetProdutoIdQueryHandler(IProdutoService produtoService) :
    IRequestHandler<GetProdutoIdQuery, ProdutoDTO>
{
    public async Task<ProdutoDTO> Handle(
        GetProdutoIdQuery request,
        CancellationToken cancellationToken)
    {
        var produto = await produtoService.ListarPorId(request.IdProduto);
        return produto;
    }
}