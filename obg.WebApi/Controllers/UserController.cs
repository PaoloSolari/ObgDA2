using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
using obg.BusinessLogic.Logics;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections.Generic;

namespace obg.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        public IActionResult PostUser([FromBody] User user)
        {
            try
            {
                return StatusCode(200, "Usuario " + _userService.InsertUser(user) + " identificado. Ingrese mail, contraseña y dirección.");
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

        
        [HttpPut]
        public IActionResult PutUser([FromBody] User user)
        {
            try
            {
                return StatusCode(200, "Registro del usuario " + _userService.UpdateUser(user) + " exitoso.");
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
                return StatusCode(500, "Error interno");
            }
        }

        public class ForecastBody
        {
            [FromBody]
            public string Name { get; set; }

            [FromBody]
            public string Email { get; set; }

            [FromBody]
            public string Password { get; set; }

            [FromBody]
            public string Address { get; set; }
        }

    }
}
