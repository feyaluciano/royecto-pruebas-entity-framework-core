using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoPruebasEntityFrameworkCore.Infrastructure.IRepository;
using ProyectoPruebasEntityFrameworkCore.Models;
using ProyectoPruebasEntityFrameworkCore.Models.Dtos;

namespace ProyectoPruebasEntityFrameworkCore.Infrastructure.Repository
{
    public class ProvinciaRepository : IProvinciaRepository
    {

        private  readonly AplicationDbContext context;

        public ProvinciaRepository(AplicationDbContext context)
        {
            this.context = context;
        }
        
      

        public async Task<List<Provincia>> GetAllAsync()
        {
            try
            {
                var provincias = await context.Provincias.ToListAsync();
                return provincias;
            }
            catch (Exception ex)
            {               
                throw new Exception(ex.Message);
            }
        }
       
    }
}
