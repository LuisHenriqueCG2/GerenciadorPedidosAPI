using GerenciadorPedidos.Domain.Entities;
using GerenciadorPedidos.Domain.Enums;

namespace GerenciadorPedidos.Application.Pedidos.Validators;

public class AddProdutoPedidoValidator
{
    public StatusPedidoEnum StatusPedido { get; set; }
    public int Id { get; private set; }
    public List<ItemPedido> ItensPedido { get; private set; } = new();
    
    public void AdicionarProduto(Produto produto, int quantidade)
    {
        if (StatusPedido != StatusPedidoEnum.Aberto)
            throw new InvalidOperationException(
                "Não é possível adicionar produtos a um pedido fechado, cancelado ou faturado.");

        var itemPedidoExistente = ItensPedido.FirstOrDefault(ip => ip.ProdutoId == produto.Id);
        if (itemPedidoExistente != null)
        {
            itemPedidoExistente.Quantidade = quantidade;
            itemPedidoExistente.ValorTotal = produto.PrecoUnitario * quantidade;
        }
        else
        {
            var item = new ItemPedido(Id, produto.Id, quantidade, produto.PrecoUnitario * quantidade);
            ItensPedido.Add(item);
        }
    }
}