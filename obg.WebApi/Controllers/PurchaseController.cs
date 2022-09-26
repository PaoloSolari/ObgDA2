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
        // GET: <PurchaseController>
        [HttpGet]
        public IActionResult GetPurchases()
        {
            try
            {
                return Ok(purchaseService.GetPurchases());
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo salió mal.");
            }


        }

        // GET <AdministratorController>/5
        //[HttpGet("{id}")]
        //public IActionResult GetAdministratorById([FromRoute] int id)
        //{
        //    try
        //    {
        //        return Ok(administratorService.GetAdministratorById(id));
        //    }
        //    catch (UserException exception)
        //    {
        //        return NotFound(exception.Message);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "Algo salió mal.");
        //    }
        //}

        // POST api/<AdministratorController>
        [HttpPost]
        public IActionResult PostPurchase([FromBody] Purchase purchase)
        {
            try
            {
                return Ok(purchaseService.InsertPurchase(purchase));
            }
            catch (UserException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo salió mal.");
            }
        }
    }
}
