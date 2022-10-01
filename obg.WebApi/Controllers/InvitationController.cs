using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
using obg.BusinessLogic.Logics;
using obg.Domain.Entities;
using obg.Exceptions;
using System;

namespace obg.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InvitationController : ControllerBase
    {

        private readonly IInvitationService invitationService;
        public InvitationController(IInvitationService invitationService)
        {
            this.invitationService = invitationService;
        }

        [HttpPost]
        public IActionResult PostInvitation([FromBody] Invitation invitation)
        {
            try
            {
                return StatusCode(200, "Código de invitación: " + invitationService.InsertInvitation(invitation));
            }
            catch (InvitationException exception)
            {
                return StatusCode(400, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno.");
            }
        }

    }
}
