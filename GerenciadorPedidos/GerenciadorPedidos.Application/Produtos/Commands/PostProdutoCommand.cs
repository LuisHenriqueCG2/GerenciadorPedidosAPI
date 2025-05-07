using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Interfaces;
using MediatR;

namespace GerenciadorPedidos.Application.Produtos.Commands;

public class PostProdutoCommand : IRequest<ProdutoDto>, IRequest<int>
{
    public required string Descricao { get;  set; }
    public required int Quantidade { get;  set; }
    public required decimal PrecoUnitario { get;  set; }
    
    public required DateTime DataCadastro { get;  set; }
}

public class PostProdutoCommandHandler : IRequestHandler<PostProdutoCommand, ProdutoDto>
{
    private readonly IProdutoService _produtoService;

    public PostProdutoCommandHandler(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    public async Task<ProdutoDto> Handle(PostProdutoCommand request, CancellationToken cancellationToken)
    {
        var novoProduto = new ProdutoDto
        {
            Descricao = request.Descricao,
            DataCadastro = DateTime.Now,
            PrecoUnitario = request.PrecoUnitario,
            Quantidade = request.Quantidade
        };

        var produtoCadastrado = await _produtoService.AdicionarProduto(novoProduto);
        return produtoCadastrado;
    }
}
