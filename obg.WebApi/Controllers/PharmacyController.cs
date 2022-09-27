﻿using Microsoft.AspNetCore.Mvc;
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

        // POST api/<PharmacyController>
        [HttpPost]
        public IActionResult PostPharmacy([FromBody] Pharmacy pharmacy)
        {
            try
            {
                pharmacyService.InsertPharmacy(pharmacy);
                return Ok(pharmacy.Name);
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
    }
}
