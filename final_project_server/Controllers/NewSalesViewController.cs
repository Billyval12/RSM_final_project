using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppApi.Models;
using AppApi.Services;

namespace CONTROLLER_BASED_API_with_ASP.NET_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewSalesViewController : ControllerBase
    {
        private readonly NewSalesViewService _service;

        public NewSalesViewController(NewSalesViewService service)
        {
            _service = service;
        }

        // GET: api/NewSalesView/GetSalesReport
        [HttpGet("GetSalesReport")]
        public async Task<ActionResult<IEnumerable<NewSalesView>>> GetSalesReport(
            [FromQuery] int OrderID = 0,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null,
            [FromQuery] string productName = "",
            [FromQuery] string productCategory = "")
        {
            try
            {
                var salesReport = await _service.GetSalesReport(OrderID, fromDate, toDate, productName, productCategory);
                return Ok(salesReport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
