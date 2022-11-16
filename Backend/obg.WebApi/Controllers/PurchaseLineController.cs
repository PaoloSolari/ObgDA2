using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using obg.BusinessLogic.Interface;
using obg.BusinessLogic.Interface.Interfaces;
using obg.Domain.Entities;
using obg.WebApi.Filters;

namespace obg.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(EmployeeAuthorizationAttributeFilter))]
    public class PurchaseLineController : ControllerBase
    {
        private readonly IPurchaseLineService _purchaseLineService;
        public PurchaseLineController(IPurchaseLineService purchaseLineService)
        {
            _purchaseLineService = purchaseLineService;
        }

        [HttpGet]
        public IActionResult GetPurchasesLines([FromHeader] string token, [FromHeader] string idPurchase)
        {
            return StatusCode(200, _purchaseLineService.GetPurchasesLines(token, idPurchase));
        }

        [HttpPut]
        public IActionResult PutPurchaseLine([FromHeader] string idPurchaseLine, [FromBody] PurchaseLine purchaseLine)
        {
            return StatusCode(200, _purchaseLineService.UpdatePurchaseLine(idPurchaseLine, purchaseLine));
        }
    }
}
