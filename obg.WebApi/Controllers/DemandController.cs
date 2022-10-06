using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
using obg.BusinessLogic.Logics;
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
    public class DemandController : ControllerBase
    {
        private readonly IDemandService _demandService;
        public DemandController(IDemandService demandService)
        {
            _demandService = demandService;
        }

        [ServiceFilter(typeof(OwnerAuthorizationAttributeFilter))]
        [HttpGet]
        public IActionResult GetDemands([FromHeader] string token)
        {
            return StatusCode(200, _demandService.GetDemands(token));
        }

        [ServiceFilter(typeof(OwnerAuthorizationAttributeFilter))]
        [HttpPut("{idDemand}")]
        public IActionResult PutDemand([FromRoute] string idDemand, [FromBody] Demand demand)
        {
            return StatusCode(200, "Modificación de la solicitud: " + _demandService.UpdateDemand(idDemand, demand) + " exitosa.");
        }

        [ServiceFilter(typeof(EmployeeAuthorizationAttributeFilter))]
        [HttpPost]
        public IActionResult PostDemand([FromBody] Demand demand, [FromHeader] string token)
        {
            return StatusCode(200, "Solicitud " + _demandService.InsertDemand(demand, token) + " exitosa.");
        }

    }
}
