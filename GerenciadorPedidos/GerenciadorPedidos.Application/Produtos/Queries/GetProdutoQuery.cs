using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.Domain.Enums;
using MediatR;

namespace GerenciadorPedidos.Application.Produtos.Queries;

public class GetProdutoQuery : IRequest<IEnumerable<ProdutoDTO>>
{ 
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    
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
