using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorPedidos.Domain.Entities;
using GerenciadorPedidos.Domain.Enums;

namespace GerenciadorPedidos.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto> AdicionarProduto(Produto produto);
        Task<Produto> AlterarProduto(Produto produto);
        Task<Produto> ExcluirProduto(int id);
        Task<Produto> ListarProdutoPorID(int id);
        Task<IEnumerable<Produto>> ListarTodos(int pageNumber, int pageSize);

        Task<bool> SaveAllAsync();
    }
}
