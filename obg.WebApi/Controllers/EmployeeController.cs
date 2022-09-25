using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
using obg.BusinessLogic.Logics;
using obg.Domain.Entities;
using obg.Exceptions;
using System;

namespace obg.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        // GET: <EmployeeController>
        [HttpGet]
        public IActionResult GetEmployees()
        {
            try
            {
                return Ok(employeeService.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo salió mal.");
            }


        }

        // GET <AdministratorController>/5
        //[HttpGet("{id}")]
        //public IActionResult GetAdministratorById([FromRoute] int id)
        //{
        //    try
        //    {
        //        return Ok(administratorService.GetAdministratorById(id));
        //    }
        //    catch (UserException exception)
        //    {
        //        return NotFound(exception.Message);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "Algo salió mal.");
        //    }
        //}

        // POST api/<EmployeeController>
        [HttpPost]
        public IActionResult PostEmployee([FromBody] Employee employee)
        {
            try
            {
                return Ok(employeeService.InsertEmployee(employee));
            }
            catch (UserException exception)
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
