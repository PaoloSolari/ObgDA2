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
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService medicineService;
        public MedicineController(IMedicineService medicineService)
        {
            this.medicineService = medicineService;
        }

        [ServiceFilter(typeof(EmployeeAuthorizationAttributeFilter))]
        [HttpGet]
        public IActionResult GetMedicines()
        {
            try
            {
                return StatusCode(200, medicineService.GetMedicines());
            }
            catch (NotFoundException)
            {
                return StatusCode(404, "No hay medicamentos.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno.");
            }
        }


        [HttpGet("stock")]
        //[HttpGet("/medicine?medicineName")]
        //[HttpGet]
        public IActionResult GetMedicinesWithStock([FromQuery] string Name)
        {
            try
            {
                return StatusCode(200, medicineService.GetMedicinesWithStock(Name));
            }
            catch (NotFoundException)
            {
                return StatusCode(404, "No existe el medicamento en el sistema.");
            }
            catch (MedicineException exception)
            {
                return StatusCode(400, exception.Message);
            }
            //catch (Exception)
            //{
            //    return StatusCode(500, "Error interno.");
            //}
        }

        [ServiceFilter(typeof(EmployeeAuthorizationAttributeFilter))]
        [HttpPost]
        public IActionResult PostMedicine([FromBody] Medicine medicine, [FromHeader] string token)
        {
            try
            {
                return StatusCode(200, "Código del medicamento: " + medicineService.InsertMedicine(medicine, token));
            }
            catch (MedicineException exception)
            {
                return StatusCode(400, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno.");
            }
        }

        [ServiceFilter(typeof(EmployeeAuthorizationAttributeFilter))]
        [HttpDelete("{code}")]
        public IActionResult DeleteMedicine(string code)
        {
            try
            {
                medicineService.DeleteMedicine(code);
                return StatusCode(200, "Eliminación exitosa.");
            }
            catch (MedicineException exception)
            {
                return StatusCode(400, exception.Message);
            }
            catch (NotFoundException)
            {
                return StatusCode(404, "No se ha encontrado el medicamento.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno.");
            }
        }
    }
}
