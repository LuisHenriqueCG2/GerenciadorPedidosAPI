using AutoMapper;
using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.Domain.Entities;
using GerenciadorPedidos.Domain.Interfaces;

public class ProdutoService(IProdutoRepository repository, IMapper mapper) : IProdutoService
{
    public async Task<ProdutoDto> AdicionarProduto(ProdutoDto produtoDto)
    {
        produtoDto.DataCadastro = DateTime.Now;
        var produto = mapper.Map<Produto>(produtoDto);
        var produtoIncluido = await repository.AdicionarProduto(produto);
        return mapper.Map<ProdutoDto>(produtoIncluido);
    }

    public async Task<ProdutoDto> AlterarProduto(ProdutoDto produtoDto)
    {
        var produtoExistente = await repository.ListarProdutoPorID(produtoDto.Id);
        if (produtoExistente == null)
        {
            throw new KeyNotFoundException("Produto não encontrado.");
        }

        mapper.Map(produtoDto, produtoExistente);

        var produtoAlterado = await repository.AlterarProduto(produtoExistente);
        return mapper.Map<ProdutoDto>(produtoAlterado);
    }

    public async Task<ProdutoDto> ExcluirProduto(int id)
    {
        var produto = await repository.ListarProdutoPorID(id);
        if (produto == null)
        {
            throw new Exception("Produto não foi encontrado!");
        }

        var produtoExcluido = await repository.ExcluirProduto(id);
        return mapper.Map<ProdutoDto>(produtoExcluido);
    }

    public async Task<ProdutoDto> ListarPorId(int id)
    {
        var produto = await repository.ListarProdutoPorID(id);
        if (produto == null)
        {
            throw new Exception("Produto não foi encontrado!");
        }

        return mapper.Map<ProdutoDto>(produto);
    }

    public async Task<IEnumerable<ProdutoDto>> ListarTodosAsync(int pageNumber, int pageSize)
    {
        var produtos = await repository.ListarTodos(pageNumber, pageSize);

        return mapper.Map<IEnumerable<ProdutoDto>>(produtos);
    }
}