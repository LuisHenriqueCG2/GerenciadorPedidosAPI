using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorPedidos.Application.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
