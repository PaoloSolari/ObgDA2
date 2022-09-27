using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
using obg.BusinessLogic.Logics;
using obg.Domain.Entities;
using obg.Exceptions;
using System;

namespace obg.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorService administratorService;
        public AdministratorController(IAdministratorService administratorService)
        {
            this.administratorService = administratorService;
        }
        // GET: <AdministratoryController>
        [HttpGet]
        public IActionResult GetAdministrators()
        {
            try
            {
                return Ok(administratorService.GetAdministrators());
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo salió mal.");
            }
        }

        // POST api/<AdministratorController>
        [HttpPost]
        public IActionResult PostAdministrator([FromBody] Administrator administrator)
        {
            try
            {
                administratorService.InsertAdministrator(administrator);
                return Ok("Administrador creado correctamente.");
            }
            catch (UserException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno.");
            }
        }

        [HttpPut("{name}")]
        public IActionResult PutAdministrator([FromRoute] string name, [FromBody] Administrator administrator)
        {
            try
            {
                administrator.Name = name;
                administratorService.UpdateAdministrator(administrator);
                return Ok("Usuario identificado. Ingrese email, contraseña y dirección.");
            }
            catch (UserException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (NotFoundException)
            {
                return NotFound("No existe el usuario");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
