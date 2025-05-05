using System.ComponentModel.DataAnnotations;
using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.Application.Services;
using GerenciadorPedidos.Domain.Entities;
using GerenciadorPedidos.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GerenciadorPedidos.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost("CriarPedido")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation
            (Summary = "Cria um novo pedido",
            Description = "Recebe a Descrição do pedido e retorna o pedido criado com informações adicionais.")]
        public async Task<ActionResult<PedidoDTO>> AdicionarPedido(
            [FromBody] CriarPedidoDTO dto)
        {
            var pedido = await _pedidoService.AdicionarPedido(dto.DescricaoPedido);

            var pedidoDTO = new PedidoDTO
            {
                Id = pedido.Id,
                DescricaoPedido = pedido.DescricaoPedido,
                StatusPedido = pedido.StatusPedido,
                DataAbertura = pedido.DataAbertura
            };

            return Ok(pedidoDTO);
        }

        [HttpPost("AdicionarProduto")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation
            (Summary = "Adiciona um produto ao pedido",
            Description = "Recebe o ID do pedido, ID do produto e a Quantidade para a inclusão no pedido e retorna o pedido " +
            "criado com informações adicionais.")]
        public async Task<IActionResult> AdicionarProdutoAoPedido(
        [SwaggerParameter(Required = true)]
        [FromQuery, Required] int PedidoID, 
        [FromQuery, Required] int ProdutoID, 
        [FromQuery, Required] int Quantidade)
        {
            try
            {
                await _pedidoService.AdicionarProdutoAoPedido(PedidoID, ProdutoID, Quantidade);
                var pedidoDTO = await _pedidoService.ListarPedidoID(PedidoID);
                if (pedidoDTO == null)
                {
                    return NotFound("Pedido não encontrado!");
                }
                return Ok(pedidoDTO);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("RemoverProduto")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Remove um produto do pedido",
            Description = "Recebe o ID do pedido, ID do produto e a Quantidade para a inclusão no pedido e retorna o " +
            "pedido criado com informações adicionais.")]
        public async Task<IActionResult> RemoverProdutoPedido(
        [SwaggerParameter(Required = true)]
        [FromQuery, Required] int PedidoID,
        [FromQuery, Required] int ProdutoID)
        {
            try
            {
                if (PedidoID == 0 || ProdutoID == 0)
                {
                    return BadRequest("Os parâmetros PedidoID e ProdutoID são obrigatórios.");
                }

                await _pedidoService.RemoverProdutoDoPedido(PedidoID, ProdutoID);
                var pedidoDTO = await _pedidoService.ListarPedidoID(PedidoID);

                return Ok("Produto removido com sucesso do pedido!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("FecharPedido")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Fechar pedido",
            Description = "Recebe o ID do Pedido para atualizar o Status para Fechado")]
        public async Task<IActionResult> FecharPedido(
            [SwaggerParameter(Required = true)] 
            [FromQuery, Required] int PedidoID)
        {
            try
            {
                await _pedidoService.FecharPedido(PedidoID);
                return Ok("Pedido fechado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("FaturarPedido")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Faturar pedido",
            Description = "Recebe o ID do Pedido para atualizar o Status para Faturado")]
        public async Task<IActionResult> FaturarPedido(
            [SwaggerParameter(Required = true)]
            [FromQuery, Required] int PedidoID)
        {
            try
            {
                await _pedidoService.FaturarPedido(PedidoID);
                return Ok("Pedido faturado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("ExcluirPedido")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Deletar pedido",
            Description = "Recebe o ID do Pedido para excluir o pedido da base de dados")]
        public async Task<IActionResult> ExcluirPedido(
            [SwaggerParameter(Required = true)]
            [FromQuery, Required] int PedidoID)
        {
            var resultado = await _pedidoService.ExcluirPedido(PedidoID);
            if (resultado == null)
            {
                return BadRequest("Ocorreu um erro ao excluir o pedido!");
            }
            return Ok("Pedido excluído com sucesso!");
        }

        [HttpGet("ConsultarPedidoId")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Listar Pedido pelo ID",
            Description = "Recebe o ID do Pedido para Obter um pedido específico")]
        public async Task<ActionResult> ObterPorId(
            [SwaggerParameter(Required = true)]
            [FromQuery, Required] int Id)
        {
            var pedidoDTO = await _pedidoService.ListarPedidoID(Id);
            if (pedidoDTO == null)
            {
                return NotFound("Pedido não encontrado!");
            }
            return Ok(pedidoDTO);
        }

        
        [HttpGet("ListarTodos")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Listar Todos os Pedidos",
            Description = "Retorna uma lista paginada de pedidos filtrados por status. Parâmetros opcionais: status, page (padrão 1), pageSize (padrão 10).")]
        public async Task<ActionResult> ListarTodos(
            [SwaggerParameter(Description = "(ex: 1 - Aberto; 2 - Fechado; 3 - Cancelado ou Excluído; 4 - Faturado;)", Required = false)]
            [FromQuery] StatusPedido? StatusPedido = null,
            [FromQuery] int PageNumber = 1,
            [FromQuery] int PageSize = 10)
        {
            var pedidosDTO = await _pedidoService.ListarTodosAsync(StatusPedido, PageNumber, PageSize);
            return Ok(pedidosDTO);
        }

    }
}
