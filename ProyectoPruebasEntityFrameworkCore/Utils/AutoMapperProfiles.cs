using AutoMapper;
using ProyectoPruebasEntityFrameworkCore.Models;
using ProyectoPruebasEntityFrameworkCore.Models.Dtos;

namespace ProyectoPruebasEntityFrameworkCore.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ProvinciaDto, Provincia>().ReverseMap();
            CreateMap<Persona, PersonaDto>()
            .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.Provincia))
            .ReverseMap();             
        }
    }
}
