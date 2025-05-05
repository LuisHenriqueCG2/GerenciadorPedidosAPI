using System;
using System.Collections.Generic;
using GerenciadorPedidos.Application.DTOs;

namespace GerenciadorPedidos.Tests.MockData.Produto.GetProdutosAsync
{
    public static class ProdutosGetDtoMockData
    {
        public static List<ProdutoDTO> GetProdutos()
        {
            var fixedDate = new DateTime(2025, 05, 05);

            return new List<ProdutoDTO>
            {
                new ProdutoDTO
                {
                    Id = 1,
                    Descricao = "DTO 1",
                    Quantidade = 10,
                    PrecoUnitario = 10,
                    DataCadastro = fixedDate
                },
                new ProdutoDTO
                {
                    Id = 2,
                    Descricao = "DTO 2",
                    Quantidade = 20,
                    PrecoUnitario = 20,
                    DataCadastro = fixedDate
                }
            };
        }
    }
}
