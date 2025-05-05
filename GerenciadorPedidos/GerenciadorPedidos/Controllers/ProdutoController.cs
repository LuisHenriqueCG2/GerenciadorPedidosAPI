using System.ComponentModel.DataAnnotations;
using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GerenciadorPedidos.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpPost("Cadastrar")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(
            Summary = "Cadastra um novo produto",
            Description = "Recebe os dados do produto e retorna o produto criado com seu ID.")]
        public async Task<ActionResult> AdicionarProduto([FromBody] ProdutoDTO produtoDTO)
        {
            var produtoDTOIncluido = await _produtoService.AdicionarProduto(produtoDTO);
            if (produtoDTOIncluido == null)
            {
                return BadRequest("Ocorreu um erro ao incluir o produto!");
            }
            return Ok(produtoDTOIncluido);
        }

        [HttpPut("Alterar")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Summary = "Altera um produto existente")]
        public async Task<ActionResult> AlterarProduto([FromBody] ProdutoDTO produtoDTO)
        {
            var produtoDTOAlterado = await _produtoService.AlterarProduto(produtoDTO);
            if (produtoDTOAlterado == null)
            {
                return BadRequest("Ocorreu um erro ao alterar o produto!");
            }
            return Ok("Produto alterado com sucesso!");
        }

        [HttpDelete("Deletar")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Summary = "Remove um produto pelo ID")]
        public async Task<ActionResult> ExcluirProduto(
            [SwaggerParameter(Required = true)]
            [FromQuery] int Id)
        {

            var produtoDTOexcluido = await _produtoService.ExcluirProduto(Id);
            if (produtoDTOexcluido == null)
            {
                return BadRequest("Ocorreu um erro ao excluir o produto!");
            }
            return Ok("Produto excluído com sucesso!");
        }
        
        [HttpGet("ConsultarId")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Summary = "Obtém um produto pelo ID")]
        public async Task<ActionResult> ListarPorID(
            [SwaggerParameter(Required = true)]
            [FromQuery, Required] int Id)
        {
            var produtoDTO = await _produtoService.ListarPorId(Id);
            if (produtoDTO == null)
            {
                return NotFound("Produto não encontrado!");
            }

            return Ok(produtoDTO);
        }
        
        [HttpGet("ListarTodos")]
        [ProducesResponseType(typeof(PedidoDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerOperation(Summary = "Obtém uma lista dos produtos cadastrados")]
        public async Task<ActionResult> ListarTodos()
        {
            var produtosDTO = await _produtoService.ListarTodosAsync();
            
            return Ok(produtosDTO);
        }
    }
}
