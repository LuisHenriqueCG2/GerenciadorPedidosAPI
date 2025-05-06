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
using GerenciadorPedidos.Domain.Validations;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<PedidoDTO> AdicionarPedido(string descricaoPedido)
        {
            var novoPedido = new Pedido
            {
                DescricaoPedido = descricaoPedido,
                StatusPedido = StatusPedido.Aberto,
                DataAbertura = DateTime.Now,
                DataFechamento = null,
                DataCancelamento = null,
                DataFaturamento = null,
            };

            await _repository.AdicionarPedido(novoPedido);

            return _mapper.Map<PedidoDTO>(novoPedido); ;
        }


        public async Task<PedidoDTO> AdicionarProdutoAoPedido(int pedidoId, int produtoId, int quantidade)
        {
            var pedido = await _repository.ListarPedidoPorID(pedidoId);
            if (pedido == null) throw new NotFoundException("Pedido não encontrado");

            var produto = await _produtoRepository.ListarProdutoPorID(produtoId);
            if (produto == null) throw new NotFoundException("Produto não encontrado");

            pedido.AdicionarProduto(produto, quantidade);

            await _repository.AlterarPedido(pedidoId, pedido);

            var pedidoDTO = new PedidoDTO
            {
                Id = pedido.Id,
                DescricaoPedido = pedido.DescricaoPedido,
                StatusPedido = pedido.StatusPedido,
                DataAbertura = pedido.DataAbertura,
                Produtos = pedido.ItensPedido.Select(ip => new ProdutoDTO
                {
                    Id = ip.Produto.Id,
                    Descricao = ip.Produto.Descricao,
                    PrecoUnitario = ip.Produto.PrecoUnitario,
                    Quantidade = ip.Quantidade
                }).ToList()
            };

            return pedidoDTO;
        }

        public async Task<PedidoDTO> CancelarPedido(int pedidoId)
        {
            var pedido = await _repository.ListarPedidoPorID(pedidoId);
            if (pedido == null) throw new NotFoundException("Pedido não encontrado");

            if (pedido.StatusPedido == StatusPedido.Faturado || pedido.StatusPedido == StatusPedido.Cancelado)
            {
                throw new Exception("Não é possível cancelar um pediudo Faturado ou Cancelado!");
            }

            pedido.StatusPedido = StatusPedido.Cancelado;
            pedido.DataCancelamento = DateTime.Now;

            await _repository.CancelarPedido(pedidoId);

            var pedidoDTO = new PedidoDTO
            {
                Id = pedido.Id,
                DescricaoPedido = pedido.DescricaoPedido,
                StatusPedido = pedido.StatusPedido,
                DataAbertura = pedido.DataAbertura,
                DataFaturamento = pedido.DataFaturamento,
                DataFechamento = pedido.DataFechamento,
                DataCancelamento = pedido.DataCancelamento,
                Produtos = pedido.ItensPedido.Select(ip => new ProdutoDTO
                {
                    Id = ip.Produto.Id,
                    Descricao = ip.Produto.Descricao,
                    PrecoUnitario = ip.Produto.PrecoUnitario,
                    Quantidade = ip.Quantidade 
                }).ToList()
            };

            return pedidoDTO;
        }

        public async Task<PedidoDTO> ExcluirPedido(int pedidoId)
        {
            var pedido = await _repository.ListarPedidoPorID(pedidoId);
            if (pedido == null) throw new NotFoundException("Pedido não encontrado");

            await _repository.ExcluirPedido(pedidoId);

            var pedidoDTO = new PedidoDTO
            {
                Id = pedido.Id,
                DescricaoPedido = pedido.DescricaoPedido,
                StatusPedido = pedido.StatusPedido,
                DataAbertura = pedido.DataAbertura,
                DataFaturamento = pedido.DataFaturamento,
                DataFechamento = pedido.DataFechamento,
                DataCancelamento = pedido.DataCancelamento,
                Produtos = pedido.ItensPedido.Select(ip => new ProdutoDTO
                {
                    Id = ip.Produto.Id,
                    Descricao = ip.Produto.Descricao,
                    PrecoUnitario = ip.Produto.PrecoUnitario,
                    Quantidade = ip.Quantidade 
                }).ToList()
            };

            return pedidoDTO;
        }

        public async Task<PedidoDTO> FaturarPedido(int pedidoId)
        {
            var pedido = await _repository.ListarPedidoPorID(pedidoId);
            if (pedido == null) throw new NotFoundException("Pedido não encontrado");

            if (pedido.StatusPedido == StatusPedido.Aberto)
            {
                throw new Exception("O pedido está aberto. Feche primeiro antes de faturar!");
            }

            if (pedido.StatusPedido == StatusPedido.Cancelado)
            {
                throw new Exception("O pedido está cancelado. Não é possível faturar!");
            }

            if (pedido.StatusPedido == StatusPedido.Faturado)
            {
                throw new Exception("O pedido já está faturado!");
            }

            pedido.StatusPedido = StatusPedido.Faturado;
            pedido.DataFaturamento = DateTime.Now;

            await _repository.FaturarPedido(pedidoId);
            
            var pedidoDTO = new PedidoDTO
            {
                Id = pedido.Id,
                DescricaoPedido = pedido.DescricaoPedido,
                StatusPedido = pedido.StatusPedido,
                DataAbertura = pedido.DataAbertura,
                DataFaturamento = pedido.DataFaturamento,
                DataFechamento = pedido.DataFechamento,
                DataCancelamento = pedido.DataCancelamento,
                Produtos = pedido.ItensPedido.Select(ip => new ProdutoDTO
                {
                    Id = ip.Produto.Id,
                    Descricao = ip.Produto.Descricao,
                    PrecoUnitario = ip.Produto.PrecoUnitario,
                    Quantidade = ip.Quantidade 
                }).ToList()
            };

            return pedidoDTO;

        }
        
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
            if (pedido == null) throw new NotFoundException("Pedido não encontrado");

            var produto = await _produtoRepository.ListarProdutoPorID(produtoId);
            if (produto == null) throw new NotFoundException("Produto não encontrado");

            pedido.RemoverProduto(produto);
            await _repository.AlterarPedido(pedidoId, pedido);

            var pedidoDTO = new PedidoDTO
            {
                Id = pedido.Id,
                DescricaoPedido = pedido.DescricaoPedido,
                StatusPedido = pedido.StatusPedido,
                DataAbertura = pedido.DataAbertura,
                Produtos = pedido.ItensPedido.Select(ip => new ProdutoDTO
                {
                    Id = ip.Produto.Id,
                    Descricao = ip.Produto.Descricao,
                    PrecoUnitario = ip.Produto.PrecoUnitario,
                    Quantidade = ip.Quantidade 
                }).ToList()
            };
            
            return pedidoDTO;
        }
        
        public async Task<PedidoDTO> FecharPedido(int pedidoId)
        {
            var pedido = await _repository.ListarPedidoPorID(pedidoId);
            if (pedido == null) throw new NotFoundException("Pedido não encontrado");

            if (pedido.StatusPedido != StatusPedido.Aberto)
            {
                throw new Exception("O pedido precisa estar aberto para ser fechado!");
            }

            pedido.StatusPedido = StatusPedido.Fechado;
            pedido.DataFechamento = DateTime.Now;

            await _repository.FecharPedido(pedidoId);

            var pedidoDTO = new PedidoDTO
            {
                Id = pedido.Id,
                DescricaoPedido = pedido.DescricaoPedido,
                StatusPedido = pedido.StatusPedido,
                DataAbertura = pedido.DataAbertura,
                DataFaturamento = pedido.DataFaturamento,
                DataFechamento = pedido.DataFechamento,
                DataCancelamento = pedido.DataCancelamento,
                Produtos = pedido.ItensPedido.Select(ip => new ProdutoDTO
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
}
