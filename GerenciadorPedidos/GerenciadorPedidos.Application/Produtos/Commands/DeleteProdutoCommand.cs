using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Interfaces;
using MediatR;

namespace GerenciadorPedidos.Application.Produtos.Commands;

public class DeleteProdutoCommand : IRequest<ProdutoDto>
{
    public required int IdProduto { get; set; }
}

public class DeleteProdutoCommandHandler(IProdutoService produtoService) :
    IRequestHandler<DeleteProdutoCommand, ProdutoDto>
{
    public async Task<ProdutoDto> Handle(
        DeleteProdutoCommand request,
        CancellationToken cancellationToken)
    {
        var produto = await produtoService.ExcluirProduto(request.IdProduto);
        return produto;
    }
}