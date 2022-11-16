using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
using obg.BusinessLogic.Logics;
using obg.Domain.Entities;
using obg.WebApi.Filters;
using System.Collections.Generic;

namespace obg.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExceptionFilter]
    [ServiceFilter(typeof(EmployeeAuthorizationAttributeFilter))]
    public class ExporterController : ControllerBase
    {
        private readonly IExporterService _exporterService;
        public ExporterController(IExporterService exporterService)
        {
            _exporterService = exporterService;
        }

        [HttpGet]
        public IActionResult GetExporters()
        {
            return StatusCode(200, _exporterService.GetExporters());
        }

        [HttpGet("parameters")]
        public IActionResult GetParameters([FromHeader] string typeOfExporter)
        {
            return StatusCode(200, _exporterService.GetParameters(typeOfExporter));
        }

        [HttpPost]
        public IActionResult ExportMedicine([FromHeader] string typeOfExporter, [FromHeader] string token, [FromBody] Dictionary<string,string> parametersMap)
        {
            return StatusCode(200, _exporterService.ExportMedicine(typeOfExporter, token, parametersMap));
        }
    }
}
