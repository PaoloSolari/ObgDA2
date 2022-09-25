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
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService ownerService;
        public OwnerController(IOwnerService ownerService)
        {
            this.ownerService = ownerService;
        }
        // GET: <OwnerController>
        [HttpGet]
        public IActionResult GetOwners()
        {
            try
            {
                return Ok(ownerService.GetOwners());
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
        public IActionResult PostOwner([FromBody] Owner owner)
        {
            try
            {
                return Ok(ownerService.InsertOwner(owner));
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
