using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppApi.Models;

namespace CONTROLLER_BASED_API_with_ASP.NET_Core.Services
{
    public class SalesAnalysisService
    {
        private readonly AppDbContext _context;

        public SalesAnalysisService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SaleAnalysisView>> GetSalesAnalysis(string productName = "", string categoryName = "")
        {
            IQueryable<SaleAnalysisView> query = _context.SalesAnalysisView;

            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(s => s.ProductName == productName);
            }

            if (!string.IsNullOrEmpty(categoryName))
            {
                query = query.Where(s => s.ProductCategory == categoryName);
            }

            return await query.ToListAsync();
        }
    }
}
