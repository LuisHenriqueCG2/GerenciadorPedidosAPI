using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorPedidos.Domain.Enums;

namespace GerenciadorPedidos.Application.DTOs
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        [MaxLength(255, ErrorMessage = "A Descrição do pedido não pode ultrapassar de 255 caracteres!")]
        [Required(ErrorMessage = "O campo descrição é obrigatório!")]
        public string DescricaoPedido { get; set; }
        public StatusPedido StatusPedido { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public DateTime? DataFaturamento { get; set; }
        public List<ProdutoDTO> Produtos { get; set; }
    }
}
