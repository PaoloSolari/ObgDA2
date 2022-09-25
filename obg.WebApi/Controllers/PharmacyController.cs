using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace obg.WebApi.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyService pharmacyService;
        public PharmacyController(IPharmacyService pharmacyService)
        {
            this.pharmacyService = pharmacyService;
        }
        // GET: <PharmacyController>
        [HttpGet]
        public IActionResult GetPharmacies()
        {
            try
            {
                return Ok(pharmacyService.GetPharmacies());
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo salió mal.");
            }

        }

        // GET <PharmacyController>/5
        [HttpGet("{id}")]
        public IActionResult GetPharmacyById([FromRoute] int id)
        {
            try
            {
                return Ok(pharmacyService.GetPharmacyById(id));
            }
            catch (PharmacyException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo salió mal.");
            }
        }

        // POST api/<PharmacyController>
        [HttpPost]
        public IActionResult PostPharmacy([FromBody] Pharmacy pharmacy)
        {
            try
            {
                return Ok(pharmacyService.InsertPharmacy(pharmacy));
            }
            catch (PharmacyException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo salió mal.");
            }
        }

        // PUT api/<PharmacyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PharmacyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
