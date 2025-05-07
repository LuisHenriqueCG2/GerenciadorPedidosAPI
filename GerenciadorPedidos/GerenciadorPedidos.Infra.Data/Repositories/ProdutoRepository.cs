using GerenciadorPedidos.Domain.Entities;
using GerenciadorPedidos.Domain.Interfaces;
using GerenciadorPedidos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorPedidos.Infra.Data.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ApplicationDbContext _context;

    public ProdutoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Produto> AdicionarProduto(Produto produto)
    {
        await _context.Produtos.AddAsync(produto);
        await _context.SaveChangesAsync();
        return produto;
    }

    public async Task<Produto> AlterarProduto(Produto produto)
    {
        _context.Entry(produto).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return produto;
    }

    public async Task<Produto> ExcluirProduto(int id)
    {
        var produto = await _context.Produtos
            .Include(p => p.ItensPedido)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (produto == null)
        {
            throw new KeyNotFoundException("Produto não encontrado.");
        }

        if (produto.ItensPedido.Any())
        {
            throw new InvalidOperationException("Não é possível excluir um produto que está em pedidos.");
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return produto;
    }

    public async Task<Produto> ListarProdutoPorID(int id)
    {
        return await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Produto>> ListarTodos(int pageNumber, int pageSize)
    {
        var query = _context.Produtos
            .Include(p => p.ItensPedido)
            .AsQueryable();

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}