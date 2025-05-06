using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GerenciadorPedidos.Application.Produtos.Commands;

public class PutProdutoCommand : IRequest<ProdutoDTO>, IRequest<int>
{
    public  required int Id { get; set; }
    public required string Descricao { get;  set; }
    public required int Quantidade { get;  set; }
    public required decimal PrecoUnitario { get;  set; }
}

public class PutProdutoCommandHandler : IRequestHandler<PutProdutoCommand, ProdutoDTO>
{
    private readonly IProdutoService _produtoService;

    public PutProdutoCommandHandler(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    public async Task<ProdutoDTO> Handle(PutProdutoCommand request, CancellationToken cancellationToken)
    {
        var alterarProduto = new ProdutoDTO
        {
            Id = request.Id,
            Descricao = request.Descricao,
            DataCadastro = DateTime.Now,
            PrecoUnitario = request.PrecoUnitario,
            Quantidade = request.Quantidade
        };

        var produtoAlterado = await _produtoService.AlterarProduto(alterarProduto);
        return produtoAlterado;
    }
}