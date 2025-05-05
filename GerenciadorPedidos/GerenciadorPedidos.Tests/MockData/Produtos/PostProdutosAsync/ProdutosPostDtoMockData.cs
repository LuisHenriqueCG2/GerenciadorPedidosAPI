using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorPedidos.Application.DTOs;

namespace GerenciadorPedidos.Tests.MockData.Produtos.PostProdutosAsync
{
    public static class ProdutosPostDtoMockData
    {
        public static ProdutoDTO PostProduto()
        {
            var fixedDate = new DateTime(2025, 05, 05);

            return new ProdutoDTO
            {
                Id = 1,
                Descricao = "Produto Post 1",
                Quantidade = 10,
                PrecoUnitario = 10,
                DataCadastro = fixedDate
            };
        }

    }
}
