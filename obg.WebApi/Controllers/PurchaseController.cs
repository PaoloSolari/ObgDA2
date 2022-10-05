using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
using obg.BusinessLogic.Logics;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace obg.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost]
        public IActionResult PostPurchase([FromBody] Purchase purchase, [FromHeader] string buyerEmail)
        {
            try
            {
                purchase.BuyerEmail = buyerEmail;
                return StatusCode(200, "Compra " + _purchaseService.InsertPurchase(purchase) + " exitosa.");
            }
            catch (PurchaseException exception)
            {
                return StatusCode(400, exception.Message);
            }
            catch (NotFoundException)
            {
                return StatusCode(404, "No existe un medicamento a comprar.");
            }
            //catch (Exception)
            //{
            //    return StatusCode(500, "Error interno.");
            //}
        }
    }
}
