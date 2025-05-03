using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorPedidos.Domain.Entities;
using GerenciadorPedidos.Domain.Interfaces;
using GerenciadorPedidos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorPedidos.Infra.Data.Repositories
{
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
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return null;

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto> ListarProdutoPorID(int id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Produto>> ListarTodos()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
