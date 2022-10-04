using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface;
using obg.BusinessLogic.Interface.Interfaces;
using obg.BusinessLogic.Logics;
using obg.Domain.Entities;
using obg.Exceptions;
using obg.WebApi.Filters;
using System;

namespace obg.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
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
                return StatusCode(500, "Error interno.");
            }
        }

        // POST api/<AdministratorController>
        [ServiceFilter(typeof(AdministratorAuthorizationAttributeFilter))]

        [HttpPost]
        public IActionResult PostAdministrator([FromBody] Administrator administrator)
        {
            try
            {
                return StatusCode(200, "Usuario " + administratorService.InsertAdministrator(administrator) + " identificado. Ingrese mail, contraseña y dirección.");
            }
            catch (UserException exception)
            {
                return StatusCode(400, exception.Message);
            }
            //catch (Exception)
            //{
            //    return StatusCode(500, "Error interno.");
            //}
        }

        [HttpPut("{name}")]
        public IActionResult PutAdministrator([FromRoute] string name, [FromBody] Administrator administrator)
        {
            try
            {
                return StatusCode(200, "Registro del usuario " + administratorService.UpdateAdministrator(administrator) + " exitoso.");
            }
            catch (UserException exception)
            {
                return StatusCode(400, exception.Message);
            }
            catch (NotFoundException)
            {
                return StatusCode(404, "No existe el usuario");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno");
            }
        }
    }
}
