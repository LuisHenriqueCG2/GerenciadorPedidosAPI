using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.Application.Pedidos.Queries;
using MediatR;

namespace GerenciadorPedidos.Application.Pedidos.Commands;

public class PostAddProdutoPedidoCommand : IRequest<PedidoDto>
{
    public required int IdPedido { get; set; }
    public required int IdProduto { get; set; }
    public required int Quantidade { get; set; }
}

public class PostAddProdutoPedidoCommandHandler(IPedidoService pedidoService) :
    IRequestHandler<PostAddProdutoPedidoCommand, PedidoDto>
{
    public async Task<PedidoDto> Handle(
        PostAddProdutoPedidoCommand request,
        CancellationToken cancellationToken)
    {
        var pedido = await pedidoService.AdicionarProdutoAoPedido(request.IdPedido, request.IdProduto, request.Quantidade);
        return pedido;
    }
}