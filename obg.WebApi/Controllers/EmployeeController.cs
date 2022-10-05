using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
using obg.BusinessLogic.Logics;
using obg.Domain.Entities;
using obg.Exceptions;
using obg.WebApi.Filters;
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
        [ServiceFilter(typeof(AdministratorAuthorizationAttributeFilter))]

        [HttpPost]
        public IActionResult PostEmployee([FromBody] Employee employee)
        {
            try
            {
                
                return StatusCode(200, "Usuario " + employeeService.InsertEmployee(employee) + " identificado. Ingrese mail, contraseña y dirección.");
            }
            catch (UserException exception)
            {
                return StatusCode(400, exception.Message);
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
                return StatusCode(200, "Registro del usuario " + employeeService.UpdateEmployee(employee) + " exitoso.");
            }
            catch (UserException exception)
            {
                return StatusCode(400, exception.Message);
            }
            catch (NotFoundException)
            {
                return StatusCode(404, "No existe el usuario");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erorr interno.");
            }
        }
    }
}
