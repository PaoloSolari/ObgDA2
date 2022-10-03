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
    [Route("[controller]")]
    [ApiController]
    [ExceptionFilter]
    [ServiceFilter(typeof(AdministratorAuthorizationAttributeFilter))]
    public class InvitationController : ControllerBase
    {

        private readonly IInvitationService invitationService;
        public InvitationController(IInvitationService invitationService)
        {
            this.invitationService = invitationService;
        }

        [HttpPost]
        public IActionResult PostInvitation([FromBody] Invitation invitation, [FromHeader] string pharmacyName)
        {
            //try
            //{
                return Ok(invitationService.InsertInvitation(invitation, pharmacyName));
                //return StatusCode(200, "Código de invitación: " + invitationService.InsertInvitation(invitation, pharmacyName));
            //}
            //catch (InvitationException exception)
            //{
            //    return StatusCode(400, exception.Message);
            //}
            //catch(NotFoundException exception)
            //{
            //    return StatusCode(404, exception.Message);
            //}
            //catch (Exception)
            //{
            //    return StatusCode(500, "Error interno.");
            //}
        }

    }
}
