using FluentValidation;
using ProyectoPruebasEntityFrameworkCore.Models.Dtos;

namespace ProyectoPruebasEntityFrameworkCore.Helpers.PersonaValidator
{
    public class PersonaValidator : AbstractValidator<PersonaDto>
    {
        public PersonaValidator()
        {            
            RuleFor(dto => dto.Nombre).NotEmpty().MaximumLength(50);
            RuleFor(dto => dto.Apellido).NotEmpty().MaximumLength(50);
            RuleFor(dto => dto.ProvinciaId).NotEmpty();
            RuleFor(dto => dto.Dni).NotEmpty().MaximumLength(8);
            RuleFor(dto => dto.Telefono).MaximumLength(50);            
            RuleFor(dto => dto.FechaModificacion).Must(BeValidDateOrNull);            
        }

        private bool BeValidDateOrNull(DateTime? date)
        {
            return date == null || date > DateTime.MinValue;
        }
    }
}
