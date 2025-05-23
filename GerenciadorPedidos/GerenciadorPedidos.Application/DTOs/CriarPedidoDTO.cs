﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorPedidos.Application.Dtos
{
    public class CriarPedidoDto
    {
        public CriarPedidoDto() {} 

        public CriarPedidoDto(string descricaoPedido)
        {
            DescricaoPedido = descricaoPedido;
        }

        [MaxLength(255, ErrorMessage = "A Descrição do pedido não pode ultrapassar 255 caracteres!")]
        [Required(ErrorMessage = "O campo descrição é obrigatório!")]
        public string DescricaoPedido { get; set; }
    }


}
