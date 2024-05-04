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
    public class NewSalesViewController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NewSalesViewController(AppDbContext context)
        {
            _context = context;
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
                IQueryable<NewSalesView> query = _context.NewSalesView;


                // Filtrar por ID de pedido si se proporciona ese parámetro
                if (OrderID != 0)
                {
                    query = query.Where(s => s.OrderID == OrderID);
                }

                // Filtrar por fecha de pedido si se proporciona el rango de fechas
                if (fromDate != null && toDate != null)
                {
                    query = query.Where(s => s.OrderDate >= fromDate && s.OrderDate <= toDate);
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
