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
        public IActionResult PostInvitation([FromBody] Invitation invitation, [FromHeader] string pharmacyName)
        {
            return StatusCode(200, invitationService.InsertInvitation(invitation, pharmacyName));
        }

        [HttpGet]
        public IActionResult GetInvitations()
        {
            return StatusCode(200, invitationService.GetInvitations());
        }
    }
}
