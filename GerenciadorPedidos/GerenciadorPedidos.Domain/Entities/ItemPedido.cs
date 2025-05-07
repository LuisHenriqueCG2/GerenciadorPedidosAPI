namespace GerenciadorPedidos.Domain.Entities;

public class ItemPedido
{
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; }

    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }

    public int Quantidade { get; set; }
    public decimal ValorTotal { get; set; }

    public ItemPedido(int pedidoId, int produtoId, int quantidade, decimal valorTotal)
    {
        PedidoId = pedidoId;
        ProdutoId = produtoId;
        Quantidade = quantidade;
        ValorTotal = valorTotal;
    }

    public void CalcularValorTotal()
    {
        ValorTotal = Produto.PrecoUnitario * Quantidade;
    }
}