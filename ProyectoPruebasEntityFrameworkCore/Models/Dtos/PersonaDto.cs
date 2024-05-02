namespace ProyectoPruebasEntityFrameworkCore.Models.Dtos
{
    public class PersonaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int ProvinciaId { get; set; }
        public ProvinciaDto? Provincia { get; set; }
        public string Dni { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public DateTime? FechaBaja { get; set; } // Nuevo campo
    }
}
