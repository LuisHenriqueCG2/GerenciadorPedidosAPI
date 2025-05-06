using System.ComponentModel.DataAnnotations;
using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.Application.Produtos.Commands;
using GerenciadorPedidos.Application.Produtos.Queries;
using GerenciadorPedidos.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GerenciadorPedidos.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProdutoController(IProdutoService produtoService, ISender sender) : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        [HttpPost("Cadastrar")]
        [SwaggerOperation(
            Summary = "Cadastra um novo produto",
            Description = "Recebe os dados do produto e retorna o produto criado com seu ID.")]
        public async Task<ActionResult> PostTaskAsync([FromBody] PostProdutoCommand query)
        {
            var resultado = await sender.Send(query); 
            return Ok(resultado);
        }
        
        [HttpPut("Alterar")]
        [SwaggerOperation(Summary = "Altera um produto existente")]
        public async Task<ActionResult> PutTaskAsync([FromBody] PutProdutoCommand query)
        {
            var result = await sender.Send(query);
            return Ok(result);
        }

        [HttpDelete("Deletar")]
        [SwaggerOperation(Summary = "Remove um produto pelo ID")]
        public async Task<ActionResult> DeleteTaskAsync([FromQuery] DeleteProdutoCommand command)
        {
            await sender.Send(command);
            return Ok(new { message = "Produto deletado com sucesso!"});
        }

        [HttpGet("ConsultarId")]
        [SwaggerOperation(Summary = "Obtém um produto pelo ID")]
        public async Task<ActionResult> GetIdTaskAsync(
            [FromQuery] GetProdutoIdQuery query)
        {
            return Ok(await sender.Send(query));
        }

        [HttpGet("ListarTodos")]
        [SwaggerOperation(Summary = "Obtém uma lista dos produtos cadastrados")]
        public async Task<ActionResult> GetTaskAsync([FromQuery] GetProdutoQuery query)
        {
            return Ok(await sender.Send(query));
        }

    }
}
