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
        // GET: <PharmacyController>
        [HttpGet]
        public IActionResult GetMedicines()
        {
            try
            {
                return Ok(medicineService.GetMedicines());
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo salió mal.");
            }

        }

        // GET <PharmacyController>/5
        //[HttpGet("{code}")]
        //public IActionResult GetMedicineByCode([FromRoute] string code)
        //{
        //    try
        //    {
        //        return Ok(medicineService.GetMedicineByCode(code));
        //    }
        //    catch (MedicineException exception)
        //    {
        //        return NotFound(exception.Message);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "Algo salió mal.");
        //    }
        //}

        [HttpGet("{medicineName}")]
        public IActionResult GetMedicineByMedicineName([FromRoute] string medicineName)
        {
            try
            {
                return Ok(medicineService.GetMedicineByMedicineName(medicineName));
            }
            catch (MedicineException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo salió mal.");
            }
        }

        // POST api/<PharmacyController>
        [HttpPost]
        public IActionResult PostMedicine([FromBody] Medicine medicine)
        {
            try
            {
                return Ok(medicineService.InsertMedicine(medicine));
            }
            catch (MedicineException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo salió mal.");
            }
        }

        // PUT api/<PharmacyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PharmacyController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMedicine(string code)
        {
            try
            {
                medicineService.DeleteMedicine(code);
                return Ok();
            }
            catch (MedicineException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo salió mal.");
            }
        }
    }
}
