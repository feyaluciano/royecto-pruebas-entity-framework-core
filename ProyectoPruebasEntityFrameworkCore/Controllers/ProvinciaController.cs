using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProyectoPruebasEntityFrameworkCore.Domain.Services;
using ProyectoPruebasEntityFrameworkCore.Models.Dtos;
using ProyectoPruebasEntityFrameworkCore.Utils;

namespace ProyectoPruebasEntityFrameworkCore.Controllers
{

    [Produces("application/json")]
    [Route("v1/[controller]/")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly IProvinciaService _personaService;        

        public ProvinciaController(IProvinciaService personaService)
        {
            _personaService = personaService;            
        }

        [HttpGet("GetProvincias")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            try
            {                
                var personasDto = await _personaService.GetAllAsync();
                BaseResponse<IEnumerable<ProvinciaDto>> baseResponse = new BaseResponse<IEnumerable<ProvinciaDto>>();
                baseResponse.Message = "Provincias obtenidas correctamente";
                baseResponse.Result = personasDto;
                baseResponse.IsSuccess = true;
                baseResponse.StatusCode= (System.Net.HttpStatusCode)StatusCodes.Status200OK;
                return Ok(baseResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al obtener las personas.");
            }
        }

        
    }
}
