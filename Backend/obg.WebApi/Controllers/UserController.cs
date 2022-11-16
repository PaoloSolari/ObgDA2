using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
using obg.BusinessLogic.Logics;
using obg.Domain.Entities;
using obg.Exceptions;
using obg.WebApi.Filters;
using System;
using System.Collections.Generic;

namespace obg.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExceptionFilter]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userName}")]
        public IActionResult GetUser([FromRoute] string userName)
        {
            return StatusCode(200, _userService.GetUserByName(userName));
        }

        [HttpPost]
        public IActionResult PostUser([FromBody] User user)
        {
            return StatusCode(200, "Usuario " + _userService.InsertUser(user) + " identificado. Ingrese email, contraseña y dirección.");
        }
        
        [HttpPut("{userName}")]
        public IActionResult PutUser([FromBody] User user, [FromRoute] string userName)
        {
            return StatusCode(200, "Registro del usuario " + _userService.UpdateUser(user, userName) + " exitoso.");
        }

    }
}
