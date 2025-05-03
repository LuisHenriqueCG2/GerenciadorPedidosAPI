using System;
using System.Collections.Generic;

namespace GerenciadorPedidosAPI.Models;

public class Produto
{
    public int ID { get; set; }

    public string Descricao { get; set; }

    public int Quantidade { get; set; }

    public decimal Valor { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual ICollection<ProdutosPedido> ProdutosPedidos { get; set; } = new List<ProdutosPedido>();
}
