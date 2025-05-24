using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Pedidos.Commands;
using GerenciadorPedidos.Application.Pedidos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GerenciadorPedidos.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PedidosController(ISender sender) : ControllerBase
{
    [HttpPost("Criar")]
    [SwaggerOperation
    (Summary = "Cria um novo pedido",
        Description = "Recebe a Descrição do pedido e retorna o pedido criado com informações adicionais.")]
    public async Task<ActionResult<PedidoDto>> PostTaskAsync([FromQuery] PostPedidoCommand command)
    {
        return Ok(await sender.Send(command));
    }

    [HttpPost("AdicionarProduto")]
    [SwaggerOperation
    (Summary = "Adiciona um produto ao pedido",
        Description =
            "Recebe o ID do pedido, ID do produto e a Quantidade para a inclusão no pedido e retorna o pedido " +
            "criado com informações adicionais.")]
    public async Task<IActionResult> PostProductTaskAsync([FromQuery] PostAddProdutoPedidoCommand query)
    {
        return Ok(await sender.Send(query));
    }

    [HttpPut("RemoverProduto")]
    [SwaggerOperation(
        Summary = "Remove um produto do pedido",
        Description = "Recebe o ID do pedido, ID do produto para a remoção do produto no pedido e retorna " +
                      "informações adicionais.")]
    public async Task<IActionResult> PutRemoveProdutoTaskAsync([FromQuery] PutRemoveProdutoCommand query)
    {
        return Ok(await sender.Send(query));
    }

    [HttpPut("Fechar")]
    [SwaggerOperation(
        Summary = "Fechar pedido",
        Description = "Recebe o ID do Pedido para atualizar o Status para Fechado")]
    public async Task<IActionResult> PutFecharPedidoTaskAsync([FromQuery] PutFecharPedidoCommand query)
    {
        return Ok(await sender.Send(query));
    }

    [HttpPut("Faturar")]
    [SwaggerOperation(
        Summary = "Faturar pedido",
        Description = "Recebe o ID do Pedido para atualizar o Status para Faturado")]
    public async Task<IActionResult> PutFaturarPedidoTaskAsync([FromQuery] PutFaturarPedidoCommand query)
    {
        return Ok(await sender.Send(query));
    }

    [HttpPut("Cancelar")]
    [SwaggerOperation(
        Summary = "Cancelar pedido",
        Description = "Recebe o ID do Pedido para atualizar o Status para Cancelado mantendo seu histórico")]
    public async Task<IActionResult> PutCancelarPedidoTaskAsync([FromQuery] PutCancelarPedidoCommand query)
    {
        return Ok(await sender.Send(query));
    }

    [HttpDelete("Deletar")]
    [SwaggerOperation(
        Summary = "Deletar pedido",
        Description = "Recebe o ID do Pedido para excluir o pedido da base de dados")]
    public async Task<IActionResult> DeleteProdutoTaskAsync([FromQuery] DeleteExcluirPedidoCommand query)
    {
        await sender.Send(query);
        return Ok(new { message = "Produto deletado com sucesso!" });
    }

    [HttpGet("ConsultarPorId")]
    [SwaggerOperation(
        Summary = "Listar Pedido pelo ID",
        Description = "Recebe o ID do Pedido para Obter um pedido específico")]
    public async Task<ActionResult> GetIdTaskAsync([FromQuery] GetPedidoIdQuery query)
    {
        return Ok(await sender.Send(query));
    }

    [HttpGet("ObterTodos")]
    [SwaggerOperation(
        Summary = "Listar Todos os Pedidos",
        Description = "Retorna uma lista paginada de pedidos filtrados por status. Parâmetros opcionais: " +
                      "status, pageNumber (padrão 1), pageSize (padrão 10).")]
    public async Task<ActionResult> GetTaskAsync([FromQuery] GetPedidoQuery query)
    {
        return Ok(await sender.Send(query));
    }
}