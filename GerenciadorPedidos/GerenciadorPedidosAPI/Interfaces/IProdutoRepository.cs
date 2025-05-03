using GerenciadorPedidosAPI.Models;

namespace GerenciadorPedidosAPI.Interfaces
{
    public interface IProdutoRepository
    {
        void AdicionarProduto(Produto produto)
        {

        }

        void AlterarProduto(Produto produto)
        {

        }

        void RemoverProduto(Produto produto)
        {

        }

        Task<Produto> ListarProdutoPorID(int Id);

        Task<IEnumerable<Produto>> ListarTodos();

        Task<bool> SaveAllAsync();
    }
}
