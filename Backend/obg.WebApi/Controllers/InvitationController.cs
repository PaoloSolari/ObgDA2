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
        public IActionResult PostInvitation([FromBody] Invitation invitation, [FromHeader] string pharmacyName, string token)
        {
            return StatusCode(200, invitationService.InsertInvitation(invitation, pharmacyName, token));
        }

        [HttpGet]
        public IActionResult GetInvitations([FromHeader] string token)
        {
            return StatusCode(200, invitationService.GetInvitations(token));
        }

        public IActionResult PutInvitation([FromRoute] string idInvitation, [FromBody] Invitation invitation, [FromHeader] string token)
        {
            return StatusCode(200, "Modificación de la solicitud: " + invitationService.UpdateInvitation(idInvitation, invitation, token) + " exitosa.");
        }
    }
}
