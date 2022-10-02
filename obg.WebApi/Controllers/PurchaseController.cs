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
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService purchaseService;
        public PurchaseController(IPurchaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }

        // POST api/<AdministratorController>
        [HttpPost]
        public IActionResult PostPurchase([FromBody] Purchase purchase)
        {
            try
            {
                return StatusCode(200, "Compra " + purchaseService.InsertPurchase(purchase) + " exitosa.");
            }
            catch (PurchaseException exception)
            {
                return StatusCode(400, exception.Message);
            }
            catch (NotFoundException)
            {
                return StatusCode(404, "No existe un medicamento a comprar.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno.");
            }
        }
    }
}
