using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using System;

namespace obg.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
}
public class MedicineController : ControllerBase, IMedicineService
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
    [HttpGet("{id}")]
    public IActionResult GetMedicineById([FromRoute] int id)
    {
        try
        {
            return Ok(medicineService.GetMedicineById(id));
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
    public void Delete(int id)
    {
    }
}
