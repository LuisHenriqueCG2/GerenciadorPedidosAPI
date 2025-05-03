using System;
using System.Collections.Generic;

namespace GerenciadorPedidosAPI.Models;

public partial class ProdutosPedido
{
    public int IDPedido { get; set; }

    public int IDProduto { get; set; }

    public decimal ValorProduto { get; set; }

    public virtual Pedido IDPedidoNavigation { get; set; }

    public virtual Produto IDProdutoNavigation { get; set; }
}
