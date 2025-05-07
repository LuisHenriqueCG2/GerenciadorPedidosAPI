using GerenciadorPedidos.Application.Dtos;

namespace GerenciadorPedidos.Tests.MockData.Produtos.PostProdutosAsync;

public static class ProdutosPostDtoMockData
{
    public static ProdutoDto PostProduto()
    {
        var fixedDate = new DateTime(2025, 05, 05);

        return new ProdutoDto
        {
            Id = 1,
            Descricao = "Produto Post 1",
            Quantidade = 10,
            PrecoUnitario = 10,
            DataCadastro = fixedDate
        };
    }
}