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
                return StatusCode(500, "Error interno.");
            }


        }

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
                return StatusCode(500, "Error interno.");
            }
        }

        [HttpPut("{name}")]
        public IActionResult PutEmployee([FromRoute] string name, [FromBody] Employee employee)
        {
            try
            {
                employee.Name = name;
                return Ok(employeeService.UpdateEmployee(employee));
            }
            catch (UserException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (NotFoundException)
            {
                return NotFound("No existe el usuario");
            }
            catch (Exception )
            {
                return StatusCode(500, "Erorr interno.");
            }
        }
    }
}
