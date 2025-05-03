using GerenciadorPedidosAPI.Interfaces;
using GerenciadorPedidosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorPedidosAPI.Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly GerenciadorPedidosContext _context;

        public ProdutoRepository(GerenciadorPedidosContext context)
        {
            _context = context;
        }

        public void AdicionarProduto(Produto produto) 
        {
            _context.Produtos.Add(produto);
        }

        public void AlterarProduto(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
        }

        public void RemoverProduto(Produto produto)
        {
            _context.Produtos.Remove(produto);
        }

        public async Task<Produto> ListarProdutoPorID(int Id)
        {
            return await _context.Produtos.Where(x => x.ID == Id).FirstOrDefaultAsync();
           
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
