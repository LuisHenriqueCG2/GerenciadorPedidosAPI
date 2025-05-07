using GerenciadorPedidos.Application.Dtos;

namespace GerenciadorPedidos.Tests.MockData.Produto.GetProdutosAsync;

public static class ProdutosGetDtoMockData
{
    public static List<ProdutoDto> GetProdutos()
    {
        var fixedDate = new DateTime(2025, 05, 05);

        return new List<ProdutoDto>
        {
            new ProdutoDto
            {
                Id = 1,
                Descricao = "DTO 1",
                Quantidade = 10,
                PrecoUnitario = 10,
                DataCadastro = fixedDate
            },
            new ProdutoDto
            {
                Id = 2,
                Descricao = "DTO 2",
                Quantidade = 20,
                PrecoUnitario = 20,
                DataCadastro = fixedDate
            }
        };
    }
}