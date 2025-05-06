using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.Application.Produtos.Queries;
using MediatR;

namespace GerenciadorPedidos.Application.Produtos.Commands;

public class DeleteProdutoCommand : IRequest<ProdutoDTO>
{
    public required int IdProduto { get; set; }
}

public class DeleteProdutoCommandHandler(IProdutoService produtoService) :
    IRequestHandler<DeleteProdutoCommand, ProdutoDTO>
{
    public async Task<ProdutoDTO> Handle(
        DeleteProdutoCommand request,
        CancellationToken cancellationToken)
    {
        var produto = await produtoService.ExcluirProduto(request.IdProduto);
        return produto;
    }
}