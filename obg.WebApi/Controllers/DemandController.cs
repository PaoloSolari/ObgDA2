using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class DemandController : ControllerBase
    {
        private readonly IDemandService demandService;
        public DemandController(IDemandService demandService)
        {
            this.demandService = demandService;
        }

        [ServiceFilter(typeof(OwnerAuthorizationAttributeFilter))]
        [HttpGet]
        public IActionResult GetDemands()
        {
            try
            {
                return StatusCode(200, demandService.GetDemands());
            }
            catch (NotFoundException)
            {
                return StatusCode(404, "No hay solicitudes de reposición de stock.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno.");
            }


        }

        [ServiceFilter(typeof(EmployeeAuthorizationAttributeFilter))]
        [HttpPost]
        public IActionResult PostDemand([FromBody] Demand demand, [FromHeader] string token)
        {
            try
            {
                return StatusCode(200, "Solicitud " + demandService.InsertDemand(demand, token) + " exitosa.");
            }
            catch (DemandException exception)
            {
                return StatusCode(400, exception.Message);
            }
            catch (NotFoundException)
            {
                return StatusCode(404, "Solicitud de medicamento inexistente.");
            }
            //catch (Exception)
            //{
            //    return StatusCode(500, "Error interno.");
            //}
        }

        [ServiceFilter(typeof(OwnerAuthorizationAttributeFilter))]
        [HttpPut("{id}")]
        public IActionResult PutDemand([FromRoute] string id, [FromBody] Demand demand)
        {
            try
            {
                return StatusCode(200, "Modificación de la solicitud: " + demandService.UpdateDemand(demand) + " exitosa.");
            }
            catch (DemandException exception)
            {
                return StatusCode(400, exception.Message);
            }
            catch (NotFoundException exception)
            {
                return StatusCode(404, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno.");
            }
        }
    }
}
