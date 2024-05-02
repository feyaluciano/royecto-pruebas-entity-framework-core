using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProyectoPruebasEntityFrameworkCore.Domain.Services;
using ProyectoPruebasEntityFrameworkCore.Helpers;
using ProyectoPruebasEntityFrameworkCore.Models;
using ProyectoPruebasEntityFrameworkCore.Models.Dtos;
using ProyectoPruebasEntityFrameworkCore.Utils;

namespace ProyectoPruebasEntityFrameworkCore.Controllers
{

    [Produces("application/json")]
    [Route("v1/[controller]/")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaService _personaService;
        private readonly IValidator<PersonaDto> _validator;
        private readonly ILogger<PersonasController> _logger;

        public PersonasController(IPersonaService personaService, IValidator<PersonaDto> validator, ILogger<PersonasController> logger)
        {
            _personaService = personaService;
            _validator = validator;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el listado de personas
        /// </summary>
        /// <remarks>        
        /// </remarks>
        [HttpGet("GetPersonas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            BaseResponse<IEnumerable<PersonaDto>> baseResponse = default;
            try
            {                
                var personasDto = await _personaService.GetAllAsync();
                baseResponse = new BaseResponse<IEnumerable<PersonaDto>>();
                baseResponse.Message = MessageManager.GetPersonasObtenidasCorrectamente();
                baseResponse.Result = personasDto;
                baseResponse.IsSuccess = true;
                baseResponse.StatusCode= (System.Net.HttpStatusCode)StatusCodes.Status200OK;
                return Ok(baseResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageManager.GetErrorObtenerPersonas());
                baseResponse = new BaseResponse<IEnumerable<PersonaDto>>().WithNotFound(MessageManager.GetErrorObtenerPersonas());
                baseResponse.IsSuccess = false;
                baseResponse.StatusCode = (System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError;
                return StatusCode(500, baseResponse);                
            }
        }


        /// <summary>
        /// Elimina una persona
        /// </summary>
        /// <remarks>
        /// Ingrese un id
        /// </remarks>
        [HttpDelete("DeletePersona")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _personaService.GetByIdAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            await _personaService.DeleteAsync(id);
            return NoContent();
        }


        /// <summary>
        /// Elimina un conjunto de personas
        /// </summary>
        /// <remarks>
        /// Ingrese un listado de ids separados por coma (,), por ejemplo "ids": "17,18"
        /// </remarks>
        [HttpPatch("DeleteMultiple")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMultiple([FromBody] DeleteMultipleRequest deleteMultipleRequest) 
        {
            bool elimino = await _personaService.DeleteMultiple(deleteMultipleRequest.Ids);
            BaseResponse<bool> baseResponse = default;
            if (elimino) {
                baseResponse = new BaseResponse<bool>().WithResultOk(true);
                baseResponse.Message = MessageManager.GetPersonasEliminadasCorrectamente();
                baseResponse.IsSuccess = true;
                baseResponse.StatusCode = (System.Net.HttpStatusCode)StatusCodes.Status200OK;            
            }
            return Ok(baseResponse);
        }

        /// <summary>
        /// Obtiene una persona según su id.
        /// </summary>
        /// <remarks>
        /// Ingrese el id de la persona
        /// </remarks>
        [HttpGet("GetPersonaById/{id}")]       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPersonaById(int id)
        {
            BaseResponse<PersonaDto> baseResponse = default;
            try
            {                
                var personaDto = await _personaService.GetByIdAsync(id);

                if (personaDto == null)
                {
                    baseResponse = new BaseResponse<PersonaDto>().WithNotFound(MessageManager.GetNoExistePersona());                    
                    baseResponse.Result = null;
                    baseResponse.IsSuccess = false;
                    baseResponse.StatusCode = (System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError;
                    return NotFound(baseResponse);
                }
                baseResponse = new BaseResponse<PersonaDto>().WithResultOk(personaDto);
                baseResponse.Message = MessageManager.GetPersonaObtenidaCorrectamente();
                baseResponse.Result = personaDto;
                baseResponse.IsSuccess = true;
                baseResponse.StatusCode = (System.Net.HttpStatusCode)StatusCodes.Status200OK;                
                return Ok(baseResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageManager.GetErrorAlBuscarPersonas());
                baseResponse = new BaseResponse<PersonaDto>().WithNotFound(MessageManager.GetOcurrioUnError());                               
                baseResponse.IsSuccess = false;
                baseResponse.StatusCode = (System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError;
                return StatusCode(500, baseResponse);
            }
        }


        /// <summary>
        /// Crea una persona.
        /// </summary>
        /// <remarks>
        /// Ingrese los datos de la persona a crear
        /// </remarks>
        [HttpPost("CreatePersona")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePersona([FromBody] PersonaDto personaDto)
        {
            BaseResponse<PersonaDto> baseResponse = default;
            try
            {                
                var validationResult = await _validator.ValidateAsync(personaDto);
                if (!validationResult.IsValid)
                {
                    baseResponse = new BaseResponse<PersonaDto>().WithBadRequest(MessageManager.GetDatosRecibidosIncorrectos());
                    baseResponse.ErrorMessages=validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                    return BadRequest(baseResponse);
                }

                var personaCreadaDto = await _personaService.CreateAsync(personaDto);
                baseResponse = new BaseResponse<PersonaDto>().WithResultOk(personaCreadaDto);
                baseResponse.Message = MessageManager.GetPersonaCreadaCorrectamente();                
                baseResponse.IsSuccess = true;
                baseResponse.StatusCode = (System.Net.HttpStatusCode)StatusCodes.Status201Created;
                return Ok(baseResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageManager.GetErrorAlCrearLaPersona());
                baseResponse = new BaseResponse<PersonaDto>().WithNotFound(MessageManager.GetErrorAlCrearLaPersona());               
                baseResponse.IsSuccess = false;
                baseResponse.StatusCode = (System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError;
                return StatusCode(500, baseResponse);
            }
        }



        /// <summary>
        /// Actualiza una persona.
        /// </summary>
        /// <remarks>
        /// Ingrese los datos de la persona a actualizar
        /// </remarks>
        [HttpPut("UpdatePersona")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePersona([FromBody] PersonaDto personaDto)
        {
            BaseResponse<PersonaDto> baseResponse = default;
            try
            {
                var validationResult = await _validator.ValidateAsync(personaDto);
                if (!validationResult.IsValid)
                {
                    baseResponse = new BaseResponse<PersonaDto>().WithBadRequest(MessageManager.GetDatosRecibidosIncorrectos());
                    baseResponse.ErrorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                    return BadRequest(baseResponse);
                }

                var personaActualizadaDto = await _personaService.UpdateAsync(personaDto);                
                baseResponse = new BaseResponse<PersonaDto>().WithResultOk(personaActualizadaDto);
                baseResponse.Message = MessageManager.GetPersonaActualizadaCorrectamente();
                baseResponse.IsSuccess = true;
                baseResponse.StatusCode = (System.Net.HttpStatusCode)StatusCodes.Status201Created;
                return Ok(baseResponse);                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageManager.GetErrorAlActualizarLaPersona());
                baseResponse = new BaseResponse<PersonaDto>().WithNotFound(MessageManager.GetErrorAlActualizarLaPersona());
                baseResponse.IsSuccess = false;
                baseResponse.StatusCode = (System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError;
                return StatusCode(500, baseResponse);                
            }
        }



        /// <summary>
        /// Obtiene una lista de todas las personas que cumplan con el apellido indicado.
        /// </summary>
        /// <remarks>
        /// Ingrese un apellido a buscar.
        /// </remarks>
        [HttpGet("SearchPersonas", Name = "SearchPersonas")]
        public async Task<IActionResult> Search(string apellidoPersona)
        {
            List<PersonaDto> profesionales;
            BaseResponse<IEnumerable<PersonaDto>> baseResponse = default;
            try
            {
                var personas = await _personaService.SearchAsync(apellidoPersona);                
                baseResponse = new BaseResponse<IEnumerable<PersonaDto>>();
                baseResponse.Message = MessageManager.GetPersonasObtenidasCorrectamente();
                baseResponse.Result = personas;
                baseResponse.IsSuccess = true;
                baseResponse.StatusCode = (System.Net.HttpStatusCode)StatusCodes.Status200OK;
                return Ok(baseResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageManager.GetErrorAlBuscarPersonas());
                baseResponse = new BaseResponse<IEnumerable<PersonaDto>>().WithNotFound(MessageManager.GetErrorAlBuscarPersonas());
                baseResponse.IsSuccess = false;
                baseResponse.StatusCode = (System.Net.HttpStatusCode)StatusCodes.Status500InternalServerError;
                return StatusCode(500, baseResponse);                
            }

           
        }



    }
}
