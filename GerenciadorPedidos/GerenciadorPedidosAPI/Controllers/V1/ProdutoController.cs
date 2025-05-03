using AutoMapper;
using GerenciadorPedidosAPI.Dtos;
using GerenciadorPedidosAPI.Infra.Repositories;
using GerenciadorPedidosAPI.Interfaces;
using GerenciadorPedidosAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorPedidosAPI.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;   
        }

        [HttpGet("Listar-Todos")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos() {

            var produto = await _produtoRepository.ListarTodos();
            var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produto);

            return Ok(produtosDTO);
        }

        [HttpGet("Listar-Produto/{id}")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutosById(int id)
        {
            var produto = await _produtoRepository.ListarProdutoPorID(id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado!");
            }

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDTO);
        }

        [HttpPost("Adicionar-Produto")]
        public async Task<ActionResult> CadastrarProduto(ProdutoDTO produtoDTO)
        {
            var produto = _mapper.Map<Produto>(produtoDTO);

            _produtoRepository.AdicionarProduto(produto);

            if (await _produtoRepository.SaveAllAsync())
            {
                return Ok("Produto cadastrado com sucesso!");
            }

            return BadRequest("Ocorreu um erro ao cadastrar o produto!");
            
        }

        [HttpPut("Alterar-Produto")]
        public async Task<ActionResult> AlterarProduto(ProdutoDTO produtoDTO)
        {
            if(produtoDTO.ID == 0)
            {
                return BadRequest("Não foi possível alterar o produto. Informe o ID!");
            }

            var produtoExiste = await _produtoRepository.ListarProdutoPorID(produtoDTO.ID);

            if(produtoExiste == null)
            {
                return NotFound("Produto não encontrado!");
            }

            var produto = _mapper.Map<Produto>(produtoDTO);

            _produtoRepository.AlterarProduto(produto);

            if (await _produtoRepository.SaveAllAsync())
            {
                return Ok("Produto alterado com sucesso!");
            }

            return BadRequest("Ocorreu um erro ao alterar o produto!");
        }

        [HttpDelete("Excluir-Produto/{id}")]
        public async Task<ActionResult> DeletarProduto(int id)
        {
            var produto = await _produtoRepository.ListarProdutoPorID(id);

            if(produto == null)
            {
                return NotFound("Produto não encontrado!");
            }

            _produtoRepository.RemoverProduto(produto);

            if (await _produtoRepository.SaveAllAsync())
            {
                return Ok("Produto excluido com sucesso!");
            }

            return BadRequest("Ocorreu um erro ao excluir o produto!");
        }
    }
}
