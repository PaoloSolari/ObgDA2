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
    //[ServiceFilter(typeof(AdministratorAuthorizationAttributeFilter))]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyService _pharmacyService;
        public PharmacyController(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
        }

        [HttpGet]
        public IActionResult GetPharmacies([FromHeader] string token)
        {
            //return StatusCode(200, _sessionService.GetSessionByToken(token));

            return StatusCode(200, _pharmacyService.GetPharmacies());
        }

        [HttpGet("{name}")]
        public IActionResult GetPharmacyByName([FromHeader] string token, [FromRoute] string name)
        {
            //return StatusCode(200, _sessionService.GetSessionByToken(token));

            return StatusCode(200, _pharmacyService.GetPharmacyByName(name));
        }

        [HttpPost]
        public IActionResult PostPharmacy([FromBody] Pharmacy pharmacy, [FromHeader] string token)
        {
            return StatusCode(200, "Nombre de la farmacia ingresada: " + _pharmacyService.InsertPharmacy(pharmacy, token));
        }

    }
}
