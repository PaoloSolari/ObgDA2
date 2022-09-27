using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
using obg.BusinessLogic.Logics;
using obg.Domain.Entities;
using obg.Exceptions;
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
        // GET: <DemandController>
        [HttpGet]
        public IActionResult GetDemands()
        {
            try
            {
                return Ok(demandService.GetDemands());
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo salió mal.");
            }


        }

        // POST api/<DemandController>
        [HttpPost]
        public IActionResult PostDemand([FromBody] Demand demand)
        {
            try
            {
                return Ok(demandService.InsertDemand(demand));
            }
            catch (DemandException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo salió mal.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutDemand([FromRoute] int id, [FromBody] Demand demand)
        {
            try
            {
                demand.IdDemand = id;
                return Ok(demandService.UpdateDemand(demand));
            }
            catch (DemandException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
