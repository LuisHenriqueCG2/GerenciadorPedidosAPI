using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using MediatR;

namespace GerenciadorPedidos.Application.Pedidos.Commands;

public class PostPedidoCommand : IRequest<PedidoDTO>
{
    public required string Descricao { get; set; }
}

public class PostPedidoCommandHandler : IRequestHandler<PostPedidoCommand, PedidoDTO>
{
    private readonly IPedidoService _pedidoService;

    public PostPedidoCommandHandler(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    public async Task<PedidoDTO> Handle(PostPedidoCommand request, CancellationToken cancellationToken)
    {
        var pedidoCriado = await _pedidoService.AdicionarPedido(request.Descricao);
    
        if (pedidoCriado == null)
        {
            throw new Exception("Ocorreu um problema ao criar o pedido!");
        }
        return pedidoCriado;
    }

}
