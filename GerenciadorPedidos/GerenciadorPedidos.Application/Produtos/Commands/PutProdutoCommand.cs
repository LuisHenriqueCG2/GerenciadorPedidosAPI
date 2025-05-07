using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Interfaces;
using MediatR;

namespace GerenciadorPedidos.Application.Produtos.Commands;

public class PutProdutoCommand : IRequest<ProdutoDto>, IRequest<int>
{
    public required int Id { get; set; }
    public required string Descricao { get; set; }
    public required int Quantidade { get; set; }
    public required decimal PrecoUnitario { get; set; }
}

public class PutProdutoCommandHandler : IRequestHandler<PutProdutoCommand, ProdutoDto>
{
    private readonly IProdutoService _produtoService;

    public PutProdutoCommandHandler(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    public async Task<ProdutoDto> Handle(PutProdutoCommand request, CancellationToken cancellationToken)
    {
        var alterarProduto = new ProdutoDto
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