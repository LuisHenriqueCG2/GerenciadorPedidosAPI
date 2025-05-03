using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.Application.Services;
using GerenciadorPedidos.Domain.Entities;
using GerenciadorPedidos.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GerenciadorPedidos.API.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        ///<summary>
        ///Criação de um novo pedido.
        ///</summary>
        ///<remarks>
        ///Cria um novo pedido no sistema com a descrição informada.
        ///</remarks>
        ///<param name="dto">Objeto contendo a descrição do pedido.</param>
        ///<returns>Dados do pedido criado.</returns>
        ///<response code="200">Pedido cadastrado com sucesso.</response>
        ///<response code="400">Requisição inválida.</response>
        ///<response code="500">Erro interno no servidor.</response>
        [HttpPost("CriarPedido")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PedidoDTO>> AdicionarPedido([FromBody] CriarPedidoDTO dto)
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

        [HttpPost("AdicionarProduto{pedidoId}/produtos/{produtoId}/{quantidade}")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AdicionarProdutoAoPedido(int pedidoId, int produtoId, int quantidade)
        {
            try
            {
                await _pedidoService.AdicionarProdutoAoPedido(pedidoId, produtoId, quantidade);
                return Ok("Produto adicionado ao pedido com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("RemoverProduto{pedidoId}/produtos/{produtoId}")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RemoverProdutoPedido(int pedidoId, int produtoId)
        {
            try
            {
                await _pedidoService.RemoverProdutoDoPedido(pedidoId, produtoId);
                return Ok("Produto removido com sucesso do pedido.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("FecharPedido{pedidoId}")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> FecharPedido(int pedidoId)
        {
            try
            {
                await _pedidoService.FecharPedido(pedidoId);
                return Ok("Pedido fechado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("FaturarPedido{pedidoId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> FaturarPedido(int pedidoId)
        {
            try
            {
                await _pedidoService.FaturarPedido(pedidoId);
                return Ok("Pedido faturado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("ExcluirPedido{id}")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ExcluirPedido(int id)
        {
            var resultado = await _pedidoService.ExcluirPedido(id);
            if (resultado == null)
            {
                return BadRequest("Ocorreu um erro ao excluir o pedido!");
            }
            return Ok("Pedido excluído com sucesso!");
        }

        [HttpGet("ConsultarPedido{id}")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> ObterPorId(int id)
        {
            var pedidoDTO = await _pedidoService.ListarPedidoID(id);
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
        public async Task<ActionResult> ListarTodos(
            [FromQuery] StatusPedido? StatusPedido = null,
            [FromQuery] int PageNumber = 1,
            [FromQuery] int PageSize = 10)
        {
            var pedidosDTO = await _pedidoService.ListarTodosAsync(StatusPedido, PageNumber, PageSize);
            return Ok(pedidosDTO);
        }

    }
}
