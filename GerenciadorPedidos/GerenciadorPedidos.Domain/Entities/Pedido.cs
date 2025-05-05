using System;
using System.Collections.Generic;
using System.Linq;
using GerenciadorPedidos.Domain.Enums;

namespace GerenciadorPedidos.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; private set; }
        public string DescricaoPedido { get; set; }
        public StatusPedido StatusPedido { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public DateTime? DataFaturamento { get; set; }
        public List<ItemPedido> ItensPedido { get; private set; } = new();

        public Pedido()
        {
            StatusPedido = StatusPedido.Aberto;
            DataAbertura = DateTime.UtcNow;
            DataFechamento = null;
            DataCancelamento = null;
            DataFaturamento = null;
        }

        public ItemPedido AdicionarProduto(Produto produto, int quantidade)
        {
            if (StatusPedido != StatusPedido.Aberto)
                throw new InvalidOperationException("Não é possível adicionar produtos a um pedido fechado, cancelado ou faturado.");

            var item = new ItemPedido(Id, produto.Id, quantidade, produto.PrecoUnitario * quantidade);
            ItensPedido.Add(item);
            return item;
        }

        public void RemoverProduto(Produto produto)
        {
            if (StatusPedido != StatusPedido.Aberto)
                throw new InvalidOperationException("Não é possível remover produtos de um pedido fechado, cancelado ou faturado.");

            var item = ItensPedido.FirstOrDefault(i => i.ProdutoId == produto.Id);
            if (item != null)
            {
                ItensPedido.Remove(item);
            }
        }

        public void FecharPedido()
        {
            if (StatusPedido != StatusPedido.Aberto)
                throw new InvalidOperationException("O pedido já está fechado ou cancelado.");

            if (ItensPedido == null || !ItensPedido.Any())
                throw new InvalidOperationException("Não é possível fechar um pedido sem produtos.");

            StatusPedido = StatusPedido.Fechado;
            DataFechamento = DateTime.UtcNow;
        }

        public void CancelarPedido()
        {
            if (StatusPedido == StatusPedido.Fechado)
                throw new InvalidOperationException("Não é possível cancelar um pedido já fechado.");

            StatusPedido = StatusPedido.Cancelado;
            DataCancelamento = DateTime.UtcNow;
        }
    }
}
