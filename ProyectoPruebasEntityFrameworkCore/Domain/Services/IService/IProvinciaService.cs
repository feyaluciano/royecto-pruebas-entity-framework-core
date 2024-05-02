using ProyectoPruebasEntityFrameworkCore.Models.Dtos;

namespace ProyectoPruebasEntityFrameworkCore.Domain.Services
{
    public interface IProvinciaService
    {        
        Task<List<ProvinciaDto>> GetAllAsync();        
    }
}
