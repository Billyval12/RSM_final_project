using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppApi.Models;

namespace CONTROLLER_BASED_API_with_ASP.NET_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesReportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalesReportController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SalesReport
        [HttpGet("GetSalesReport")]
        public async Task<ActionResult<IEnumerable<SalesReportView>>> GetSalesReport(
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null,
            [FromQuery] int? customerID = null,
            [FromQuery] int? productID = null,
            [FromQuery] string productName = "",
            [FromQuery] string productCategory = "")
        {
            IQueryable<SalesReportView> query = _context.SalesReportView;

            // Filtrar por fecha de pedido si se proporciona el rango de fechas
            if (fromDate != null && toDate != null)
            {
                query = query.Where(s => s.OrderDate >= fromDate && s.OrderDate <= toDate);
            }

            // Filtrar por ID de cliente si se proporciona ese parámetro
            if (customerID != null)
            {
                query = query.Where(s => s.CustomerID == customerID);
            }

            // Filtrar por ID de producto si se proporciona ese parámetro
            if (productID != null)
            {
                query = query.Where(s => s.ProductID == productID);
            }

            // Filtrar por nombre de producto si se proporciona ese parámetro
            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(s => s.ProductName == productName);
            }

            // Filtrar por categoría de producto si se proporciona ese parámetro
            if (!string.IsNullOrEmpty(productCategory))
            {
                query = query.Where(s => s.ProductCategory == productCategory);
            }

            // Ejecutar la consulta y devolver los resultados
            var salesReport = await query.ToListAsync();
            return Ok(salesReport);
        }
    }
}
