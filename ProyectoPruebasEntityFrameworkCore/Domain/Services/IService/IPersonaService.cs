using ProyectoPruebasEntityFrameworkCore.Models.Dtos;

namespace ProyectoPruebasEntityFrameworkCore.Domain.Services
{
    public interface IPersonaService
    {
        Task<PersonaDto> GetByIdAsync(int id);
        Task<IEnumerable<PersonaDto>> GetAllAsync();
        Task<PersonaDto> CreateAsync(PersonaDto personaDto);
        Task<PersonaDto> UpdateAsync(PersonaDto personaDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PersonaDto>> SearchAsync(string apellido);

        Task<bool> DeleteMultiple(string id);
    }
}
