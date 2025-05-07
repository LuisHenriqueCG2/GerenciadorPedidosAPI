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
        var novoPedido = new Pedido
        {
            DescricaoPedido = descricaoPedido,
            StatusPedidoEnum = StatusPedidoEnum.Aberto,
            DataAbertura = DateTime.Now,
            DataFechamento = null,
            DataCancelamento = null,
            DataFaturamento = null,
        };

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

        var pedidoDTO = new PedidoDto
        {
            Id = pedido.Id,
            DescricaoPedido = pedido.DescricaoPedido,
            StatusPedidoEnum = pedido.StatusPedidoEnum,
            DataAbertura = pedido.DataAbertura,
            Produtos = pedido.ItensPedido.Select(ip => new ProdutoDto
            {
                Id = ip.Produto.Id,
                Descricao = ip.Produto.Descricao,
                PrecoUnitario = ip.Produto.PrecoUnitario,
                Quantidade = ip.Quantidade
            }).ToList()
        };

        return pedidoDTO;
    }

    public async Task<PedidoDto> CancelarPedido(int pedidoId)
    {
        var pedido = await repository.ListarPedidoPorID(pedidoId);
        if (pedido == null) throw new NotFoundException("Pedido não encontrado");

        if (pedido.StatusPedidoEnum == StatusPedidoEnum.Faturado || pedido.StatusPedidoEnum == StatusPedidoEnum.Cancelado)
        {
            throw new Exception("Não é possível cancelar um pediudo Faturado ou Cancelado!");
        }

        pedido.StatusPedidoEnum = StatusPedidoEnum.Cancelado;
        pedido.DataCancelamento = DateTime.Now;

        await repository.CancelarPedido(pedidoId);

        var pedidoDTO = new PedidoDto
        {
            Id = pedido.Id,
            DescricaoPedido = pedido.DescricaoPedido,
            StatusPedidoEnum = pedido.StatusPedidoEnum,
            DataAbertura = pedido.DataAbertura,
            DataFaturamento = pedido.DataFaturamento,
            DataFechamento = pedido.DataFechamento,
            DataCancelamento = pedido.DataCancelamento,
            Produtos = pedido.ItensPedido.Select(ip => new ProdutoDto
            {
                Id = ip.Produto.Id,
                Descricao = ip.Produto.Descricao,
                PrecoUnitario = ip.Produto.PrecoUnitario,
                Quantidade = ip.Quantidade
            }).ToList()
        };

        return pedidoDTO;
    }

    public async Task<PedidoDto> ExcluirPedido(int pedidoId)
    {
        var pedido = await repository.ListarPedidoPorID(pedidoId);
        if (pedido == null) throw new NotFoundException("Pedido não encontrado");

        await repository.ExcluirPedido(pedidoId);

        var pedidoDTO = new PedidoDto
        {
            Id = pedido.Id,
            DescricaoPedido = pedido.DescricaoPedido,
            StatusPedidoEnum = pedido.StatusPedidoEnum,
            DataAbertura = pedido.DataAbertura,
            DataFaturamento = pedido.DataFaturamento,
            DataFechamento = pedido.DataFechamento,
            DataCancelamento = pedido.DataCancelamento,
            Produtos = pedido.ItensPedido.Select(ip => new ProdutoDto
            {
                Id = ip.Produto.Id,
                Descricao = ip.Produto.Descricao,
                PrecoUnitario = ip.Produto.PrecoUnitario,
                Quantidade = ip.Quantidade
            }).ToList()
        };

        return pedidoDTO;
    }

    public async Task<PedidoDto> FaturarPedido(int pedidoId)
    {
        var pedido = await repository.ListarPedidoPorID(pedidoId);
        if (pedido == null) throw new NotFoundException("Pedido não encontrado");

        if (pedido.StatusPedidoEnum == StatusPedidoEnum.Aberto)
        {
            throw new Exception("O pedido está aberto. Feche primeiro antes de faturar!");
        }

        if (pedido.StatusPedidoEnum == StatusPedidoEnum.Cancelado)
        {
            throw new Exception("O pedido está cancelado. Não é possível faturar!");
        }

        if (pedido.StatusPedidoEnum == StatusPedidoEnum.Faturado)
        {
            throw new Exception("O pedido já está faturado!");
        }

        pedido.StatusPedidoEnum = StatusPedidoEnum.Faturado;
        pedido.DataFaturamento = DateTime.Now;

        await repository.FaturarPedido(pedidoId);

        var pedidoDTO = new PedidoDto
        {
            Id = pedido.Id,
            DescricaoPedido = pedido.DescricaoPedido,
            StatusPedidoEnum = pedido.StatusPedidoEnum,
            DataAbertura = pedido.DataAbertura,
            DataFaturamento = pedido.DataFaturamento,
            DataFechamento = pedido.DataFechamento,
            DataCancelamento = pedido.DataCancelamento,
            Produtos = pedido.ItensPedido.Select(ip => new ProdutoDto
            {
                Id = ip.Produto.Id,
                Descricao = ip.Produto.Descricao,
                PrecoUnitario = ip.Produto.PrecoUnitario,
                Quantidade = ip.Quantidade
            }).ToList()
        };

        return pedidoDTO;
    }

    public async Task<PedidoDto> ListarPedidoID(int id)
    {
        var pedido = await repository.ListarPedidoPorID(id);
        return mapper.Map<PedidoDto>(pedido);
    }

    public async Task<IEnumerable<PedidoDto>> ListarTodosAsync(StatusPedidoEnum? StatusPedido, int PageNumber,
        int PageSize)
    {
        var pedidos = await repository.ListarTodos(StatusPedido, PageNumber, PageSize);

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

        var pedidoDTO = new PedidoDto
        {
            Id = pedido.Id,
            DescricaoPedido = pedido.DescricaoPedido,
            StatusPedidoEnum = pedido.StatusPedidoEnum,
            DataAbertura = pedido.DataAbertura,
            Produtos = pedido.ItensPedido.Select(ip => new ProdutoDto
            {
                Id = ip.Produto.Id,
                Descricao = ip.Produto.Descricao,
                PrecoUnitario = ip.Produto.PrecoUnitario,
                Quantidade = ip.Quantidade
            }).ToList()
        };

        return pedidoDTO;
    }

    public async Task<PedidoDto> FecharPedido(int pedidoId)
    {
        var pedido = await repository.ListarPedidoPorID(pedidoId);
        if (pedido == null) throw new NotFoundException("Pedido não encontrado");

        if (pedido.StatusPedidoEnum != StatusPedidoEnum.Aberto)
        {
            throw new Exception("O pedido precisa estar aberto para ser fechado!");
        }

        pedido.StatusPedidoEnum = StatusPedidoEnum.Fechado;
        pedido.DataFechamento = DateTime.Now;

        await repository.FecharPedido(pedidoId);

        var pedidoDTO = new PedidoDto
        {
            Id = pedido.Id,
            DescricaoPedido = pedido.DescricaoPedido,
            StatusPedidoEnum = pedido.StatusPedidoEnum,
            DataAbertura = pedido.DataAbertura,
            DataFaturamento = pedido.DataFaturamento,
            DataFechamento = pedido.DataFechamento,
            DataCancelamento = pedido.DataCancelamento,
            Produtos = pedido.ItensPedido.Select(ip => new ProdutoDto
            {
                Id = ip.Produto.Id,
                Descricao = ip.Produto.Descricao,
                PrecoUnitario = ip.Produto.PrecoUnitario,
                Quantidade = ip.Quantidade
            }).ToList()
        };

        return pedidoDTO;
    }
}