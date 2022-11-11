﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface;
using obg.BusinessLogic.Logics;
using obg.Domain.Entities;
using obg.Exceptions;
using obg.WebApi.Filters;
using System;

namespace obg.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExceptionFilter]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet("{token}")]
        public IActionResult GetSession([FromRoute] string token)
        {
            //return StatusCode(200, _sessionService.GetSessionByToken(token));
            
            return Ok(_sessionService.GetSessionByToken(token));
        }

        [HttpPost]
        public IActionResult PostSession([FromBody] Session session, [FromHeader] string password)
        {
            return StatusCode(200, "Token: " + _sessionService.InsertSession(session, password));
        }

    }
}
