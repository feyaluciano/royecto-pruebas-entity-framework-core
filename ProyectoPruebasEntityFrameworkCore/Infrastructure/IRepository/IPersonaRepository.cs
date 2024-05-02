using ProyectoPruebasEntityFrameworkCore.Models;
using ProyectoPruebasEntityFrameworkCore.Models.Dtos;

namespace ProyectoPruebasEntityFrameworkCore.Infrastructure.IRepository
{
    public interface IPersonaRepository
    {
        Task<Persona> GetByIdAsync(int id);
        Task<IEnumerable<Persona>> GetAllAsync();
        Task<Persona> CreateAsync(Persona persona);
        Task<Persona> UpdateAsync(Persona persona);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Persona>> SearchAsync(string apellido);

        Task<bool> DeleteMultiple(string ids);
    }
}
