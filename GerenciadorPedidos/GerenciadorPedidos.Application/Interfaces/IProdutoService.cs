using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Domain.Entities;

namespace GerenciadorPedidos.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<ProdutoDTO> AdicionarProduto(ProdutoDTO produtoDTO);
        Task<ProdutoDTO> AlterarProduto(ProdutoDTO produtoDTO);
        Task<ProdutoDTO> ExcluirProduto(int id);
        Task<ProdutoDTO> ListarPorId(int id);
        Task<IEnumerable<ProdutoDTO>> ListarTodosAsync();
        
    }
}
