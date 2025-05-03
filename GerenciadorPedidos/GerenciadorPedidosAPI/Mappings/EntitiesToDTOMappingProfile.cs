using AutoMapper;
using GerenciadorPedidosAPI.Dtos;
using GerenciadorPedidosAPI.Models;

namespace GerenciadorPedidosAPI.Mappings
{
    public class EntitiesToDTOMappingProfile : Profile
    {
        public EntitiesToDTOMappingProfile() { 

            CreateMap<Produto, ProdutoDTO>().ReverseMap();

        }
    }
}
