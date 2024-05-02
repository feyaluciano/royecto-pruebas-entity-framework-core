using ProyectoPruebasEntityFrameworkCore.Models;
using ProyectoPruebasEntityFrameworkCore.Models.Dtos;

namespace ProyectoPruebasEntityFrameworkCore.Infrastructure.IRepository
{
    public interface IProvinciaRepository
    {       
        Task<List<Provincia>> GetAllAsync();        
    }
}
