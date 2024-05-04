using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppApi.Models;

namespace AppApi.Services
{
    public class NewSalesViewService
    {
        private readonly AppDbContext _context;

        public NewSalesViewService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NewSalesView>> GetSalesReport(int OrderID = 0,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string productName = "",
            string productCategory = "")
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
                return salesReport;
            }
            catch (Exception ex)
            {
                // Aquí podrías manejar la excepción de forma adecuada, loguearla, etc.
                throw ex;
            }
        }
    }
}
