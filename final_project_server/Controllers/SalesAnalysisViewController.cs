using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppApi.Models;
using CONTROLLER_BASED_API_with_ASP.NET_Core.Services;

namespace CONTROLLER_BASED_API_with_ASP.NET_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesAnalysisViewController : ControllerBase
    {
        private readonly SalesAnalysisService _salesAnalysisService;

        public SalesAnalysisViewController(SalesAnalysisService salesAnalysisService)
        {
            _salesAnalysisService = salesAnalysisService;
        }

        // GET: api/SalesAnalysisView
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleAnalysisView>>> GetSalesAnalysisView([FromQuery] string productName = "", [FromQuery] string categoryName = "")
        {
            var salesAnalysis = await _salesAnalysisService.GetSalesAnalysis(productName, categoryName);
            return Ok(salesAnalysis);
        }
    }
}



