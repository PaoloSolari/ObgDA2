using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
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

        [HttpGet("{medicineName}")]
        public IActionResult GetMedicineByMedicineName([FromRoute] string medicineName)
        {
            try
            {
                return StatusCode(200, medicineService.GetMedicinesByName(medicineName));
            }
            catch (NotFoundException)
            {
                return StatusCode(404, "No existe el medicamento.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno.");
            }
        }

        [HttpPost]
        public IActionResult PostMedicine([FromBody] Medicine medicine)
        {
            try
            {
                return StatusCode(200, "Código del medicamento: " + medicineService.InsertMedicine(medicine));
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

        [HttpDelete("{id}")]
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
