using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using obg.WebApi.Filters;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace obg.WebApi.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(AdministratorAuthorizationAttributeFilter))]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyService pharmacyService;
        public PharmacyController(IPharmacyService pharmacyService)
        {
            this.pharmacyService = pharmacyService;
        }

        [HttpPost]
        public IActionResult PostPharmacy([FromBody] Pharmacy pharmacy)
        {
            try
            {
                return StatusCode(200, "Nombre de la farmacia ingresada: " + pharmacyService.InsertPharmacy(pharmacy));
            }
            catch (PharmacyException exception)
            {
                return StatusCode(400, exception.Message);
            }
            //catch (Exception)
            //{
            //    return StatusCode(500, "Error interno.");
            //}
        }
    }
}
