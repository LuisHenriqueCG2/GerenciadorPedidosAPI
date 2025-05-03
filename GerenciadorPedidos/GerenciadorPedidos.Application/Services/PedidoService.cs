using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Application.Interfaces;
using GerenciadorPedidos.Domain.Entities;
using GerenciadorPedidos.Domain.Enums;
using GerenciadorPedidos.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorPedidos.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public PedidoService(IPedidoRepository repository, IProdutoRepository produtoRepository, IMapper mapper)
        {
            _repository = repository;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<Pedido> AdicionarPedido(string descricaoPedido)
        {
            var pedido = new Pedido
            {
                DescricaoPedido = descricaoPedido,
                StatusPedido = StatusPedido.Aberto,
                DataAbertura = DateTime.Now,
                DataFechamento = null,
                DataCancelamento = null,
                DataFaturamento = null,
            };

            await _repository.AdicionarPedido(pedido);

            return pedido;
        }


        public async Task<PedidoDTO> AdicionarProdutoAoPedido(int pedidoId, int produtoId, int quantidade)
        {
            var pedido = await _repository.ListarPedidoPorID(pedidoId);
            if (pedido == null) throw new Exception("Pedido não encontrado");

            var produto = await _produtoRepository.ListarProdutoPorID(produtoId);
            if (produto == null) throw new Exception("Produto não encontrado");

            pedido.AdicionarProduto(produto, quantidade); 
            await _repository.AlterarPedido(pedidoId, pedido);

            var pedidoDTO = new PedidoDTO
            {
                Id = pedido.Id,
                DescricaoPedido = pedido.DescricaoPedido,
                StatusPedido = pedido.StatusPedido,
                DataAbertura = pedido.DataAbertura
            };

            return pedidoDTO;
        }

        public async Task<PedidoDTO> ExcluirPedido(int pedidoId)
        {
            var pedido = await _repository.ListarPedidoPorID(pedidoId);
            if (pedido == null) throw new Exception("Pedido não encontrado");

            await _repository.ExcluirPedido(pedidoId);

            var pedidoDTO = new PedidoDTO
            {
                Id = pedido.Id,
                DescricaoPedido = pedido.DescricaoPedido,
                StatusPedido = pedido.StatusPedido,
                DataAbertura = pedido.DataAbertura,
                DataFechamento = pedido.DataFechamento,
                DataCancelamento = pedido.DataCancelamento,
                DataFaturamento = pedido.DataFaturamento
            };

            return pedidoDTO;
        }

        public async Task<PedidoDTO> FaturarPedido(int pedidoId)
        {
            var pedido = await _repository.ListarPedidoPorID(pedidoId);
            if (pedido == null) throw new Exception("Pedido não encontrado");

            pedido.StatusPedido = StatusPedido.Faturado;
            pedido.DataFaturamento = DateTime.Now;

            await _repository.FaturarPedido(pedidoId);

            return new PedidoDTO
            {
                Id = pedido.Id,
                DescricaoPedido = pedido.DescricaoPedido,
                StatusPedido = pedido.StatusPedido,
                DataAbertura = pedido.DataAbertura,
                DataFechamento = pedido.DataFechamento,
                DataCancelamento = pedido.DataCancelamento,
                DataFaturamento = pedido.DataFaturamento
            };

        }


        //Método para retornar um pedido pelo ID;
        public async Task<PedidoDTO> ListarPedidoID(int id)
        {
            var pedido = await _repository.ListarPedidoPorID(id);
            return _mapper.Map<PedidoDTO>(pedido);
        }

        public async Task<IEnumerable<PedidoDTO>> ListarTodosAsync(StatusPedido? StatusPedido, int PageNumber, int PageSize)
        {
            var pedidos = await _repository.ListarTodos(StatusPedido, PageNumber, PageSize);
            return _mapper.Map<IEnumerable<PedidoDTO>>(pedidos);
        }

        public async Task<PedidoDTO> RemoverProdutoDoPedido(int pedidoId, int produtoId)
        {
            var pedido = await _repository.ListarPedidoPorID(pedidoId);
            if (pedido == null) throw new Exception("Pedido não encontrado");

            var produto = await _produtoRepository.ListarProdutoPorID(produtoId);
            if (produto == null) throw new Exception("Produto não encontrado");

            pedido.RemoverProduto(produto);
            await _repository.AlterarPedido(pedidoId, pedido);

            var pedidoDTO = new PedidoDTO
            {
                Id = pedido.Id,
                DescricaoPedido = pedido.DescricaoPedido,
                StatusPedido = pedido.StatusPedido,
                DataAbertura = pedido.DataAbertura,
                DataFechamento = pedido.DataFechamento,
                DataCancelamento = pedido.DataCancelamento,
                DataFaturamento = pedido.DataFaturamento
            };

            return pedidoDTO;
        }

        async Task<PedidoDTO> IPedidoService.FecharPedido(int pedidoId)
        {
            var pedido = await _repository.ListarPedidoPorID(pedidoId);
            if (pedido == null) throw new Exception("Pedido não encontrado");
       
            pedido.StatusPedido = StatusPedido.Fechado;
            pedido.DataFechamento = DateTime.Now;

            await _repository.FecharPedido(pedidoId);

            return new PedidoDTO
            {
                Id = pedido.Id,
                DescricaoPedido = pedido.DescricaoPedido,
                StatusPedido = pedido.StatusPedido,
                DataAbertura = pedido.DataAbertura,
                DataFechamento = pedido.DataFechamento,
                DataCancelamento = pedido.DataCancelamento,
                DataFaturamento = pedido.DataFaturamento
            };
        }
    }
}
