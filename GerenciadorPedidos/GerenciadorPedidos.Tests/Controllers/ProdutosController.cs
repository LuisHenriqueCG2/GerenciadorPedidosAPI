using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace GerenciadorPedidos.Tests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _produtoService.ListarTodosAsync();           
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostProdutoAsync([FromBody] ProdutoDTO produtoDTO)
        {
            var produtoAdicionado = await _produtoService.AdicionarProduto(produtoDTO);
            return Ok(produtoAdicionado);  
            
        }
    }
}
