using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface.Interfaces;
using obg.BusinessLogic.Logics;
using obg.Domain.Entities;
using obg.Exceptions;
using obg.WebApi.Filters;
using System;
using System.Collections;
using System.Collections.Generic;

namespace obg.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExceptionFilter]
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
            purchase.BuyerEmail = buyerEmail;
            return StatusCode(200, "Compra " + _purchaseService.InsertPurchase(purchase) + " exitosa.");
        }

    }
}
