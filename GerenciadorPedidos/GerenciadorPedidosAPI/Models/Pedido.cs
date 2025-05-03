using System;
using System.Collections.Generic;

namespace GerenciadorPedidosAPI.Models;

public class Pedido
{
    public int ID { get; set; }

    public string Descricao { get; set; }

    public DateTime DataEmissao { get; set; }

    public DateTime? DataFechamento { get; set; }

    public DateTime? DataCancelamento { get; set; }

    public DateTime? DataFaturamento { get; set; }

    public string StatusPedido { get; set; }

    public virtual ICollection<ProdutosPedido> ProdutosPedidos { get; set; } = new List<ProdutosPedido>();
}
