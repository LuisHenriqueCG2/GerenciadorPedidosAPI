using System.ComponentModel.DataAnnotations;
using GerenciadorPedidosAPI.Models;

namespace GerenciadorPedidosAPI.Dtos
{
    public class ProdutoDTO
    {
        public int ID { get; set; }

        public string Descricao { get; set; }

        public int Quantidade { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}
