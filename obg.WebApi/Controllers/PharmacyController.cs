using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using obg.WebApi.Filters;
using System;
using System.Collections.Generic;

namespace obg.WebApi.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    [ExceptionFilter]
    [ServiceFilter(typeof(AdministratorAuthorizationAttributeFilter))]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyService _pharmacyService;
        public PharmacyController(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
        }

        [HttpPost]
        public IActionResult PostPharmacy([FromBody] Pharmacy pharmacy, [FromHeader] string token)
        {
            return StatusCode(200, "Nombre de la farmacia ingresada: " + _pharmacyService.InsertPharmacy(pharmacy, token));
        }

    }
}
