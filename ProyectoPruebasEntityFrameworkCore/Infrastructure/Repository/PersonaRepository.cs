using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoPruebasEntityFrameworkCore.Infrastructure.IRepository;
using ProyectoPruebasEntityFrameworkCore.Models;
using ProyectoPruebasEntityFrameworkCore.Models.Dtos;

namespace ProyectoPruebasEntityFrameworkCore.Infrastructure.Repository
{
    public class PersonaRepository : IPersonaRepository
    {

        private  readonly AplicationDbContext context;

        public PersonaRepository(AplicationDbContext context)
        {
            this.context = context;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var personaToDelete = await context.Persona.FindAsync(id);
            if (personaToDelete != null)
            {
                personaToDelete.FechaBaja = DateTime.UtcNow;
                await context.SaveChangesAsync();
                return true; 
            }

            return false; 
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            try
            {
                var personas = await context.Persona
                    .Include(p => p.Provincia)
                    .Where(p => p.FechaBaja == null)
                    .ToListAsync();
                return personas;
            }
            catch (Exception ex)
            {               
                throw new Exception(ex.Message);
            }
        }

        public async Task<Persona> GetByIdAsync(int id)
        {
            return await context.Persona.Include(p => p.Provincia).FirstOrDefaultAsync(p => p.Id == id && p.FechaBaja == null);
        }

        public async Task<Persona> GetByDniAndFechaBajaNullAsync(string  dni)
        {
            try
            {
                return await context.Persona.Where(p => p.Dni == dni && p.FechaBaja == null).FirstOrDefaultAsync();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }


        public async Task<Persona> CreateAsync(Persona persona)
        {
            try
            {

                var personaExiste= context.Persona.Where(p => p.Dni == persona.Dni && p.FechaBaja == null).FirstOrDefault();

                if (personaExiste != null) {
                    throw new Exception("La persona existe");
                }

                persona.FechaAlta = DateTime.UtcNow;
                persona.FechaBaja = null;
                persona.Provincia = null;

                context.Persona.Add(persona);
                await context.SaveChangesAsync();
                return persona;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la persona.", ex);
            }
        }


         public async Task<Persona> UpdateAsync(Persona persona)
        {
            try
            {
                var personaExistente = await context.Persona.FindAsync(persona.Id);
                if (personaExistente == null)
                {
                    throw new Exception("Persona no encontrada");
                }

                personaExistente.Nombre = persona.Nombre;
                personaExistente.Apellido = persona.Apellido;
                personaExistente.ProvinciaId = persona.ProvinciaId;
                personaExistente.Dni = persona.Dni;
                personaExistente.Telefono = persona.Telefono;
                personaExistente.FechaModificacion = DateTime.UtcNow; 

                context.Persona.Update(personaExistente);
                await context.SaveChangesAsync();

                return personaExistente; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la persona.", ex);
            }
        }


        public async Task<IEnumerable<Persona>> SearchAsync(string apellido)
        {
            try
            {
                var personas = await context.Persona
                    .Include(p => p.Provincia)
                    .Where(p => p.FechaBaja == null &&  p.Apellido.Contains(apellido))
                    .ToListAsync();
                return personas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
        public async Task<bool> DeleteMultiple(string ids)
        {
            try
            {
                // Dividir el string de IDs en una lista de strings y luego convertirlos a enteros
                List<int> idList = ids.Split(',').Select(int.Parse).ToList();

                var personasToDelete = await context.Persona
                    .Where(p => idList.Contains(p.Id))
                    .ToListAsync();

                personasToDelete.ForEach(p =>
                {
                    p.FechaBaja = DateTime.Now;
                });

                context.Persona.UpdateRange(personasToDelete);
                int rowsAffected = await context.SaveChangesAsync();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
