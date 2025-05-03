using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;
using GerenciadorPedidos.Application.DTOs;
using GerenciadorPedidos.Domain.Entities;

namespace GerenciadorPedidos.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
       public DomainToDTOMappingProfile() {
            CreateMap<ProdutoDTO, Produto>().ReverseMap();
            CreateMap<PedidoDTO, Pedido>().ReverseMap();
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Pedido, PedidoDTO>()
                .ForMember(dest => dest.Produtos, opt => opt.MapFrom(src => src.ItensPedido.Select(ip => ip.Produto)));
            CreateMap<Produto, ProdutoDTO>();
            CreateMap<ItemPedido, ProdutoDTO>()
                .ConstructUsing(ip => new ProdutoDTO
                {
                    Id = ip.Produto.Id,
                    Descricao = ip.Produto.Descricao,
                    PrecoUnitario = ip.Produto.PrecoUnitario
                });

            CreateMap<Pedido, PedidoDTO>()
                .ForMember(dest => dest.Produtos, opt => opt.MapFrom(src => src.ItensPedido.Select(ip => ip.Produto)));


        }
    }
}
