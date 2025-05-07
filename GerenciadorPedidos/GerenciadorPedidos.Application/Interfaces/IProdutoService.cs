using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Domain.Entities;
using GerenciadorPedidos.Domain.Enums;

namespace GerenciadorPedidos.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<ProdutoDto> AdicionarProduto(ProdutoDto produtoDTO);
        Task<ProdutoDto> AlterarProduto(ProdutoDto produtoDTO);
        Task<ProdutoDto> ExcluirProduto(int id);
        Task<ProdutoDto> ListarPorId(int id);
        Task<IEnumerable<ProdutoDto>> ListarTodosAsync(int pageNumber, int pageSize);
        
    }
}
