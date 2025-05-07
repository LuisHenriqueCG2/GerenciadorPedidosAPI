using System.ComponentModel.DataAnnotations;
using GerenciadorPedidos.Domain.Enums;

namespace GerenciadorPedidos.Application.Dtos;

public class PedidoDto
{
    public int Id { get; set; }

    [MaxLength(255, ErrorMessage = "A Descrição do pedido não pode ultrapassar de 255 caracteres!")]
    [Required(ErrorMessage = "O campo descrição é obrigatório!")]
    public string DescricaoPedido { get; set; }

    public StatusPedidoEnum StatusPedidoEnum { get; set; }
    public DateTime DataAbertura { get; set; }
    public DateTime? DataFechamento { get; set; }
    public DateTime? DataCancelamento { get; set; }
    public DateTime? DataFaturamento { get; set; }
    public List<ProdutoDto> Produtos { get; set; }
}