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

        [HttpPost]
        public IActionResult exportMedicine([FromHeader] List<string> medicinesCodes, [FromHeader] string typeOfExporter, [FromHeader] string token, [FromHeader] string path)
        {
            return StatusCode(200, _exporterService.ExportMedicine(medicinesCodes, typeOfExporter, token, path));
        }
    }
}
