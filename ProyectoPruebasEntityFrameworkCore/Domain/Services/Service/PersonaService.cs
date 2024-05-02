using AutoMapper;
using ProyectoPruebasEntityFrameworkCore.Infrastructure.IRepository;
using ProyectoPruebasEntityFrameworkCore.Models;
using ProyectoPruebasEntityFrameworkCore.Models.Dtos;

namespace ProyectoPruebasEntityFrameworkCore.Domain.Services.Service
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IMapper _mapper;

        public PersonaService(IPersonaRepository personaRepository, IMapper mapper)
        {
            _personaRepository = personaRepository;
            _mapper = mapper;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            return await _personaRepository.DeleteAsync(id);
        }


        public async Task<bool> DeleteMultiple(string ids)
        {
            return await _personaRepository.DeleteMultiple(ids);
        }

        public async Task<IEnumerable<PersonaDto>> GetAllAsync()
        {
            try
            {
                var personas = await _personaRepository.GetAllAsync();
                var personasDto = _mapper.Map<IEnumerable<PersonaDto>>(personas);
                return personasDto;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<PersonaDto>();
            }
        }

        public async Task<IEnumerable<PersonaDto>> SearchAsync(string apellido)
        {
            try
            {
                var personas = await _personaRepository.SearchAsync(apellido);
                var personasDto = _mapper.Map<IEnumerable<PersonaDto>>(personas);
                return personasDto;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<PersonaDto>();
            }
        }

        public async Task<PersonaDto> GetByIdAsync(int id)
        {
            var personaDto = _mapper.Map<PersonaDto>(await _personaRepository.GetByIdAsync(id));
            return personaDto;
        }

        public async Task<PersonaDto> UpdateAsync(PersonaDto personaDto)
        {
            try
            {

                var persona = _mapper.Map<Persona>(personaDto);
                var personaActualizada = await _personaRepository.UpdateAsync(persona);
                PersonaDto personaActualizadaDto = _mapper.Map<PersonaDto>(personaActualizada);

                return personaActualizadaDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la persona.", ex);
            }
        }

        public async Task<PersonaDto> CreateAsync(PersonaDto personaDto)
        {
            try
            {

                var persona = _mapper.Map<Persona>(personaDto);
                var personaCreada = await _personaRepository.CreateAsync(persona);
                PersonaDto personaCreadaDto = _mapper.Map<PersonaDto>(personaCreada);
                return personaCreadaDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la persona.", ex);
            }
        }


        

      
    }
}
