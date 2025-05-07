using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace GerenciadorPedidos.Tests.Controllers;

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
        var result = await _produtoService.ListarTodosAsync(1, 10);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostProdutoAsync([FromBody] ProdutoDto produtoDTO)
    {
        var produtoAdicionado = await _produtoService.AdicionarProduto(produtoDTO);
        return Ok(produtoAdicionado);
    }
}