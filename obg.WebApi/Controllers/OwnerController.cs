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

        // POST api/<OwnerController>
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
                return StatusCode(500, "Error interno.");
            }
        }

        [HttpPut("{name}")]
        public IActionResult PutOwner([FromRoute] string name, [FromBody] Owner owner)
        {
            try
            {
                owner.Name = name;
                return Ok(ownerService.UpdateOwner(owner));
            }
            catch (UserException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (NotFoundException)
            {
                return NotFound("No existe el usuario.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
