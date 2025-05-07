using AutoMapper;
using GerenciadorPedidos.Application.Dtos;
using GerenciadorPedidos.Domain.Entities;

namespace GerenciadorPedidos.Application.Mappings;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<ProdutoDto, Produto>().ReverseMap();
        CreateMap<PedidoDto, Pedido>().ReverseMap();
        CreateMap<Produto, ProdutoDto>().ReverseMap();
        CreateMap<Pedido, PedidoDto>()
            .ForMember(dest => dest.Produtos, opt => opt.MapFrom(src => src.ItensPedido.Select(ip => ip.Produto)));
        CreateMap<Produto, ProdutoDto>();
        CreateMap<ItemPedido, ProdutoDto>()
            .ConstructUsing(ip => new ProdutoDto
            {
                Id = ip.Produto.Id,
                Descricao = ip.Produto.Descricao,
                DataCadastro = ip.Produto.DataCadastro,
                PrecoUnitario = ip.Produto.PrecoUnitario,
                Quantidade = ip.Quantidade
            });
        CreateMap<Pedido, PedidoDto>()
            .ForMember(dest => dest.Produtos, opt => opt.MapFrom(src => src.ItensPedido.Select(ip => ip.Produto)));
    }
}