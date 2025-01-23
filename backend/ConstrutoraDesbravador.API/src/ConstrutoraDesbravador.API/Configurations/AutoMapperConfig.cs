using AutoMapper;
using ConstrutoraDesbravador.API.DTOs;
using ConstrutoraDesbravador.Business.Models;

namespace ConstrutoraDesbravador.API.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, Funcionario>()
               .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Name.First))
               .ForMember(dest => dest.Sobrenome, opt => opt.MapFrom(src => src.Name.Last))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<Funcionario, FuncionarioDTO>()
                .ReverseMap();
        }
    }
}
