using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
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

        // GET <AdministratorController>/5
        //[HttpGet("{id}")]
        //public IActionResult GetAdministratorById([FromRoute] int id)
        //{
        //    try
        //    {
        //        return Ok(administratorService.GetAdministratorById(id));
        //    }
        //    catch (UserException exception)
        //    {
        //        return NotFound(exception.Message);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "Algo salió mal.");
        //    }
        //}

        // POST api/<AdministratorController>
        [HttpPost]
        public IActionResult PostAdministrator([FromBody] Administrator administrator)
        {
            try
            {
                return Ok(administratorService.InsertAdministrator(administrator));
            }
            catch (UserException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo salió mal.");
            }
        }
    }
}
