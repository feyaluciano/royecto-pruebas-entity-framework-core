using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ProyectoPruebasEntityFrameworkCore.Controllers;
using ProyectoPruebasEntityFrameworkCore.Domain.Services;
using ProyectoPruebasEntityFrameworkCore.Models.Dtos;
using ProyectoPruebasEntityFrameworkCore.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPruebasEntityFrameworkCore.unittests.Controllers
{
    public class PersonasControllerUnitTest
    {
        [Test]
        public async Task GetAll_ReturnsOk()
        {
            // Arrange
            var mockService = new Mock<IPersonaService>();
            var mockLogger = new Mock<ILogger<PersonasController>>();
            mockService.Setup(service => service.GetAllAsync()).ReturnsAsync(new List<PersonaDto>());
            var controller = new PersonasController(mockService.Object, null, mockLogger.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Test]
        public async Task GetPersonaById_PersonaNoExiste_ReturnsNotFound()
        {
            // Arrange
            var mockService = new Mock<IPersonaService>();
            var mockLogger = new Mock<ILogger<PersonasController>>();
            mockService.Setup(service => service.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((PersonaDto)null);
            var controller = new PersonasController(mockService.Object, null, mockLogger.Object);

            // Act
            var result = await controller.GetPersonaById(1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }


        [Test]
        public async Task UpdatePersona_WithValidData_ReturnsOk()
        {
            // Arrange
            var mockService = new Mock<IPersonaService>();
            var mockLogger = new Mock<ILogger<PersonasController>>();

            var personaDto = new PersonaDto
            {
                // Proporciona datos válidos para la actualización
                Id = 1,
                Nombre = "Nombre actualizado",
                Apellido = "Apellido actualizado"
                // Completa con otros campos según la definición de PersonaDto
            };

            var baseResponse = new BaseResponse<PersonaDto>
            {
                IsSuccess = true,
                Message = "Persona actualizada correctamente.",
                Result = personaDto,
                StatusCode = System.Net.HttpStatusCode.OK
            };

            mockService.Setup(service => service.UpdateAsync(It.IsAny<PersonaDto>()))
                       .ReturnsAsync(personaDto);

            var controller = new PersonasController(mockService.Object, null, mockLogger.Object);

            // Act
            var result = await controller.UpdatePersona(personaDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

            // Verifica que el resultado retornado tenga los datos correctos
            var response = okResult.Value as BaseResponse<PersonaDto>;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(baseResponse.Message, response.Message);
            Assert.AreEqual(baseResponse.Result.Id, response.Result.Id);
            // Verifica otros campos según la definición de PersonaDto
        }


        [Test]
        public async Task UpdatePersona_WithException_ReturnsInternalServerError()
        {
            // Arrange
            var mockService = new Mock<IPersonaService>();
            var mockLogger = new Mock<ILogger<PersonasController>>();

            var personaDto = new PersonaDto
            {
                // Proporciona datos válidos para la actualización
                Id = 1,
                Nombre = "a",
                Apellido = "Apellido actualizado"
                // Completa con otros campos según la definición de PersonaDto
            };

            mockService.Setup(service => service.UpdateAsync(It.IsAny<PersonaDto>()))
                       .ThrowsAsync(new Exception("Error al actualizar la persona"));

            var controller = new PersonasController(mockService.Object, null, mockLogger.Object);

            // Act
            var result = await controller.UpdatePersona(personaDto);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

            // Verifica que el resultado retornado tenga los datos correctos
            var response = objectResult.Value as BaseResponse<PersonaDto>;
            Assert.IsNotNull(response);
            Assert.IsFalse(response.IsSuccess);
            Assert.AreEqual("Error al actualizar la persona", response.Message);

        }
    }
}



 
