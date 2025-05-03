using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> AdicionarProduto(ProdutoDTO produtoDTO)
        {
            var produtoDTOIncluido = await _produtoService.AdicionarProduto(produtoDTO);
            if (produtoDTOIncluido == null)
            {
                return BadRequest("Ocorreu um erro ao incluir o produto!");
            }
            return Ok("Produto cadastrado com sucesso!");
        }

        [HttpPut("Alterar")]
        public async Task<ActionResult> AlterarProduto(ProdutoDTO produtoDTO)
        {
            var produtoDTOAlterado = await _produtoService.AlterarProduto(produtoDTO);
            if (produtoDTOAlterado == null)
            {
                return BadRequest("Ocorreu um erro ao alterar o produto!");
            }
            return Ok("Produto alterado com sucesso!");
        }

        [HttpDelete("Deletar")]
        public async Task<ActionResult> ExcluirProduto(int id)
        {

            var produtoDTOexcluido = await _produtoService.ExcluirProduto(id);
            if (produtoDTOexcluido == null)
            {
                return BadRequest("Ocorreu um erro ao excluir o produto!");
            }
            return Ok("Produto excluído com sucesso!");
        }

        [HttpGet("Listar/{id}")]
        public async Task<ActionResult> ListarPorID(int id)
        {
            var produtoDTO = await _produtoService.ListarPorId(id);
            if (produtoDTO == null)
            {
                return NotFound("Produto não encontrado!");
            }

            return Ok(produtoDTO);
        }

        [HttpGet("ListarTodos")]
        public async Task<ActionResult> ListarTodos()
        {
            var produtosDTO = await _produtoService.ListarTodosAsync();
            
            return Ok(produtosDTO);
        }
    }
}
