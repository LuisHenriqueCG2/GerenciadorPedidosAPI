using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorPedidos.Application.DTOs
{
    public class RemoverProdutoResponseDTO
    {
        public RemoverProdutoResponseDTO(string mensagem, PedidoDTO pedido)
        {
            Mensagem = mensagem;
            Pedido = pedido;
        }

        public string Mensagem { get; set; }
        public PedidoDTO Pedido { get; set; }
    }

}

