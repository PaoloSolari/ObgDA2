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

        [ServiceFilter(typeof(EmployeeAuthorizationAttributeFilter))]
        [HttpGet]
        public IActionResult GetPurchases([FromHeader] string token)
        {
            return StatusCode(200, _purchaseService.GetPurchases(token));
        }

        [ServiceFilter(typeof(EmployeeAuthorizationAttributeFilter))]
        [HttpPut("{idPurchase}")]
        public IActionResult PutPurchase([FromRoute] string idPurchase, [FromBody] Purchase purchase, [FromHeader] string token)
        {
            return StatusCode(200, _purchaseService.UpdatePurchase(idPurchase, purchase, token));
        }

        [HttpPost]
        public IActionResult PostPurchase([FromBody] Purchase purchase, [FromHeader] string buyerEmail)
        {
            purchase.BuyerEmail = buyerEmail;
            return StatusCode(200, "Compra " + _purchaseService.InsertPurchase(purchase) + " exitosa.");
        }

    }
}
