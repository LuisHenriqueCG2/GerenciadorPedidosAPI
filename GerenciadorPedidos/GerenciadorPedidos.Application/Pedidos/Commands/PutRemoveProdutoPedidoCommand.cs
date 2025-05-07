using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Interfaces;
using MediatR;

namespace GerenciadorPedidos.Application.Pedidos.Commands;

public class PutRemoveProdutoCommand : IRequest<PedidoDto>, IRequest<int>
{
    public required int IdPedido { get; set; }
    public required int IdProduto { get; set; }
}

public class PutRemoveProdutoCommandHandler : IRequestHandler<PutRemoveProdutoCommand, PedidoDto>
{
    private readonly IPedidoService _pedidoService;

    public PutRemoveProdutoCommandHandler(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    public async Task<PedidoDto> Handle(PutRemoveProdutoCommand request, CancellationToken cancellationToken)
    { 
        var produtoAlterado = await _pedidoService.RemoverProdutoDoPedido(request.IdPedido, request.IdProduto);
        return produtoAlterado;
    }
}