using System.Threading.Tasks;
using FluentAssertions;
using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.API.Controllers; // <- Verifique se esse é o namespace correto
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Tests.MockData.Produto.GetProdutosAsync;
using GerenciadorPedidos.Tests.MockData.Produtos.PostProdutosAsync;

namespace GerenciadorPedidos.Tests.Controllers
{
    public class TestProdutoController
    {
        //O método é decorado com um atributo como 'Fact', isso determina que o método deve ser executado pelo executor de teste.
        [Fact]

        //Testando o método GetAllAsync utilizado para retornar a lista de produtos.
        public async Task GetTodosProdutosAsync_ShouldReturn200Status()
        {
            // Arrange
            var produtoService = new Mock<IProdutoService>();
            produtoService.Setup(_ => _.ListarTodosAsync())
                          .ReturnsAsync(ProdutosGetDtoMockData.GetProdutos());

            //Aqui a variável 'sut' significa 'System Under Test' apenas uma convenção de nomenclatura recomendada.
            var sut = new ProdutosController(produtoService.Object);

            // Act
            var result = await sut.GetAllAsync();
            var okResult = result as OkObjectResult;

            //No código invocamos o método Action do controlador 'GetAllAsync()', e, como nosso
            //método retorna 'OkObjectResult' para o status 200, aqui fazemos a conversão explicita do resultado :
            //var result = await sut.GetAllAsync();

            // Assert
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            var produtos = okResult.Value as List<ProdutoDTO>;
            produtos.Should().BeEquivalentTo(ProdutosGetDtoMockData.GetProdutos());
        }

        //Testando o método CadastrarNovoProduto() do controlador. Utilizado para realizar o cadastro do produto.
        public async Task PostProdutoAsync_ShouldReturn200Status()
        {
            // Arrange
            var produtoMock = ProdutosPostDtoMockData.PostProduto(); 
            var produtoService = new Mock<IProdutoService>();

            // Mock para AdicionarProduto e retornar o produtoMock configurado
            produtoService.Setup(_ => _.AdicionarProduto(It.IsAny<ProdutoDTO>()))
                          .ReturnsAsync(produtoMock);

            var sut = new ProdutosController(produtoService.Object);

            // Act
            var result = await sut.PostProdutoAsync(new ProdutoDTO { Descricao = "Produto Adicionado", Quantidade = 10, PrecoUnitario = 10 });
            var okResult = result as OkObjectResult;

            // Assert
            okResult.Should().NotBeNull(); 
            okResult.StatusCode.Should().Be(200);  

            var produto = okResult.Value as ProdutoDTO;  
            produto.Should().BeEquivalentTo(produtoMock); 
        }

    }
}
