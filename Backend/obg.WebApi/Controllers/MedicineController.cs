using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using obg.WebApi.Filters;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace obg.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExceptionFilter]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService medicineService;
        public MedicineController(IMedicineService medicineService)
        {
            this.medicineService = medicineService;
        }

        //[ServiceFilter(typeof(EmployeeAuthorizationAttributeFilter))]
        [HttpGet]
        public IActionResult GetMedicines([FromQuery] string employeeName)
        {
            if (employeeName.Equals("allMedicines"))
            {
                return StatusCode(200, medicineService.GetAllMedicines());
            }
            return StatusCode(200, medicineService.GetMedicines(employeeName));
        }

        //[HttpGet("stock")]
        //public IActionResult GetMedicinesWithStock([FromQuery] string Name)
        //{
        //    return StatusCode(200, medicineService.GetMedicinesWithStock(Name));
        //}
        
        [HttpGet("stock")]
        public IActionResult GetMedicinesWithStock([FromQuery] string pharmacyName)
        {
            return StatusCode(200, medicineService.GetMedicinesWithStockFromPharmacy(pharmacyName));
        }

        [ServiceFilter(typeof(EmployeeAuthorizationAttributeFilter))]
        [HttpPost]
        public IActionResult PostMedicine([FromBody] Medicine medicine, [FromHeader] string token)
        {
            return StatusCode(200, "Código del medicamento: " + medicineService.InsertMedicine(medicine, token));
        }

        [ServiceFilter(typeof(EmployeeAuthorizationAttributeFilter))]
        [HttpDelete("{medicineCode}")]
        public IActionResult DeleteMedicine(string medicineCode)
        {
            medicineService.DeleteMedicine(medicineCode);
            return StatusCode(200, "Eliminación exitosa.");
        }

    }
}
