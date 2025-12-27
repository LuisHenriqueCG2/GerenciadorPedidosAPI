namespace GerenciadorPedidos.Application.Dtos;

public class ItemPedidoDto
{
    public int ProdutoId { get; set; }
    public string ProdutoDescricao { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorTotal { get; set; }
}
