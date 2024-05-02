using AutoMapper;
using ProyectoPruebasEntityFrameworkCore.Infrastructure.IRepository;
using ProyectoPruebasEntityFrameworkCore.Models;
using ProyectoPruebasEntityFrameworkCore.Models.Dtos;

namespace ProyectoPruebasEntityFrameworkCore.Domain.Services.Service
{
    public class ProvinciaService : IProvinciaService
    {
        private readonly IProvinciaRepository _provinciaRepository;
        private readonly IMapper _mapper;

        public ProvinciaService(IProvinciaRepository provinciaRepository, IMapper mapper)
        {
            _provinciaRepository = provinciaRepository;
            _mapper = mapper;
        }


       
        public async Task<List<ProvinciaDto>> GetAllAsync()
        {
            try
            {
                var provincias = await _provinciaRepository.GetAllAsync();
                var provinciasDto = _mapper.Map<List<ProvinciaDto>>(provincias);
                return provinciasDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar las provincias.", ex);
            }
        }

       
    }
}
