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

namespace GerenciadorPedidos.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProdutoDTO> AdicionarProduto(ProdutoDTO produtoDTO)
        {
            produtoDTO.DataCadastro = DateTime.Now;
            var produto = _mapper.Map<Produto>(produtoDTO);
            var produtoIncluido = await _repository.AdicionarProduto(produto);
            return _mapper.Map<ProdutoDTO>(produtoIncluido);
        }

        public async Task<ProdutoDTO> AlterarProduto(ProdutoDTO produtoDTO)
        {
            var produtoExistente = await _repository.ListarProdutoPorID(produtoDTO.Id);
            if (produtoExistente == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }
                
            _mapper.Map(produtoDTO, produtoExistente);

            var produtoAlterado = await _repository.AlterarProduto(produtoExistente);
            return _mapper.Map<ProdutoDTO>(produtoAlterado);
        }



        public async Task<ProdutoDTO> ExcluirProduto(int id)
        {
            var produto = await _repository.ListarProdutoPorID(id);
            if (produto == null)
            {
                throw new Exception("Produto não foi encontrado!");
            }
            var produtoExcluido = await _repository.ExcluirProduto(id);
            return _mapper.Map<ProdutoDTO>(produtoExcluido);
        }



        public async Task<ProdutoDTO> ListarPorId(int id)
        {
            var produto = await _repository.ListarProdutoPorID(id);
            if (produto == null)
            {
                throw new Exception("Produto não foi encontrado!");
            }
            return _mapper.Map<ProdutoDTO>(produto);
        }

        public async Task<IEnumerable<ProdutoDTO>> ListarTodosAsync(int pageNumber, int pageSize)
        {
            var produtos = await _repository.ListarTodos(pageNumber, pageSize);
            
            return _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            
        }
    }
}
