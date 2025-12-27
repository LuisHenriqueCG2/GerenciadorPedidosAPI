using AutoMapper;
using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.Domain.Entities;
using GerenciadorPedidos.Domain.Enums;
using GerenciadorPedidos.Domain.Interfaces;
using GerenciadorPedidos.Domain.Validations;

namespace GerenciadorPedidos.Application.Services;

public class PedidoService(
    IPedidoRepository repository,
    IProdutoRepository produtoRepository,
    IMapper mapper)
    : IPedidoService
{
    public async Task<PedidoDto> AdicionarPedido(string descricaoPedido)
    {
        var novoPedido = new Pedido(descricaoPedido);

        await repository.AdicionarPedido(novoPedido);
        return mapper.Map<PedidoDto>(novoPedido);
    }

    public async Task<PedidoDto> AdicionarProdutoAoPedido(int pedidoId, int produtoId, int quantidade)
    {
        var pedido = await repository.ListarPedidoPorID(pedidoId);
        if (pedido == null) throw new NotFoundException("Pedido não encontrado");

        var produto = await produtoRepository.ListarProdutoPorID(produtoId);
        if (produto == null) throw new NotFoundException("Produto não encontrado");

        pedido.AdicionarProduto(produto, quantidade);

        await repository.AlterarPedido(pedidoId, pedido);
        return mapper.Map<PedidoDto>(pedido);
    }

    public async Task<PedidoDto> CancelarPedido(int pedidoId)
    {
        var pedido = await repository.ListarPedidoPorID(pedidoId);
        if (pedido == null) throw new NotFoundException("Pedido não encontrado");

        pedido.CancelarPedido(); // Regra de negócio na entidade
        await repository.CancelarPedido(pedidoId);

        return mapper.Map<PedidoDto>(pedido);
    }

    public async Task<PedidoDto> ExcluirPedido(int pedidoId)
    {
        var pedido = await repository.ListarPedidoPorID(pedidoId);
        if (pedido == null) throw new NotFoundException("Pedido não encontrado");

        await repository.ExcluirPedido(pedidoId);
        return mapper.Map<PedidoDto>(pedido);
    }

    public async Task<PedidoDto> FaturarPedido(int pedidoId)
    {
        var pedido = await repository.ListarPedidoPorID(pedidoId);
        if (pedido == null) throw new NotFoundException("Pedido não encontrado");

        if (pedido.StatusPedido == StatusPedidoEnum.Aberto)
        {
            throw new Exception("O pedido está aberto. Feche primeiro antes de faturar!");
        }

        if (pedido.StatusPedido == StatusPedidoEnum.Cancelado)
        {
            throw new Exception("O pedido está cancelado. Não é possível faturar!");
        }

        if (pedido.StatusPedido == StatusPedidoEnum.Faturado)
        {
            throw new Exception("O pedido já está faturado!");
        }

        pedido.StatusPedido = StatusPedidoEnum.Faturado;
        pedido.DataFaturamento = DateTime.Now;

        await repository.FaturarPedido(pedidoId);

        var pedidoDTO = new PedidoDto
        {
            Id = pedido.Id,
            DescricaoPedido = pedido.DescricaoPedido,
            StatusPedido = pedido.StatusPedido,
            DataAbertura = pedido.DataAbertura,
            DataFaturamento = pedido.DataFaturamento,
            DataFechamento = pedido.DataFechamento,
            DataCancelamento = pedido.DataCancelamento,
            Itens = pedido.ItensPedido
                .Select(ip => new ItemPedidoDto
                {
                    ProdutoId = ip.Produto.Id,
                    ProdutoDescricao = ip.Produto.Descricao,
                    Quantidade = ip.Quantidade,
                    ValorTotal = ip.ValorTotal
                })
                .ToList()
        };

        return pedidoDTO;
    }

    public async Task<PedidoDto> ListarPedidoID(int id)
    {
        var pedido = await repository.ListarPedidoPorID(id);
        return mapper.Map<PedidoDto>(pedido);
    }

    public async Task<IEnumerable<PedidoDto>> ListarTodosAsync(StatusPedidoEnum? statusPedido, int page, int pageSize)
    {
        var pedidos = await repository.ListarTodos(statusPedido, page, pageSize);
        return mapper.Map<IEnumerable<PedidoDto>>(pedidos);
    }

    public async Task<PedidoDto> RemoverProdutoDoPedido(int pedidoId, int produtoId)
    {
        var pedido = await repository.ListarPedidoPorID(pedidoId);
        if (pedido == null) throw new NotFoundException("Pedido não encontrado");

        var produto = await produtoRepository.ListarProdutoPorID(produtoId);
        if (produto == null) throw new NotFoundException("Produto não encontrado");

        pedido.RemoverProduto(produto);
        await repository.AlterarPedido(pedidoId, pedido);

        return mapper.Map<PedidoDto>(pedido);
    }

    public async Task<PedidoDto> FecharPedido(int pedidoId)
    {
        var pedido = await repository.ListarPedidoPorID(pedidoId);
        if (pedido == null) throw new NotFoundException("Pedido não encontrado");

        pedido.FecharPedido();
        await repository.FecharPedido(pedidoId);

        return mapper.Map<PedidoDto>(pedido);
    }
}
