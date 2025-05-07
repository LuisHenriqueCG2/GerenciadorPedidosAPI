using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Interfaces;
using MediatR;

namespace GerenciadorPedidos.Application.Pedidos.Commands;

public class PostPedidoCommand : IRequest<PedidoDto>
{
    public required string Descricao { get; set; }
}

public class PostPedidoCommandHandler : IRequestHandler<PostPedidoCommand, PedidoDto>
{
    private readonly IPedidoService _pedidoService;

    public PostPedidoCommandHandler(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    public async Task<PedidoDto> Handle(PostPedidoCommand request, CancellationToken cancellationToken)
    {
        var pedidoCriado = await _pedidoService.AdicionarPedido(request.Descricao);
    
        if (pedidoCriado == null)
        {
            throw new Exception("Ocorreu um problema ao criar o pedido!");
        }
        return pedidoCriado;
    }

}
