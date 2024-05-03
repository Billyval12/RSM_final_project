// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using AppApi.Models;

// namespace CONTROLLER_BASED_API_with_ASP.NET_Core.Services
// {
//     public class SalesAnalysisService
//     {
//         private readonly AppDbContext _context;

//         public SalesAnalysisService(AppDbContext context)
//         {
//             _context = context;
//         }

//         public async Task<List<SaleAnalysisView>> GetSalesAnalysis(string productName = "", string categoryName = "")
//         {
//             IQueryable<SaleAnalysisView> query = _context.SalesAnalysisView;

//             if (!string.IsNullOrEmpty(productName))
//             {
//                 query = query.Where(s => s.ProductName == productName);
//             }

//             if (!string.IsNullOrEmpty(categoryName))
//             {
//                 query = query.Where(s => s.ProductCategory == categoryName);
//             }

//             return await query.ToListAsync();
//         }
//     }
// }



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppApi.Models;

namespace CONTROLLER_BASED_API_with_ASP.NET_Core.Services
{
    public class SalesAnalysisService
    {
        // Simula una lista de datos ficticios en lugar de acceder a la base de datos
        private List<SaleAnalysisView> _fakeData = new List<SaleAnalysisView>
        {
            new SaleAnalysisView { Id = 1, ProductName = "HL Mountain Frame - Silver, 46", ProductCategory = "Components", TotalSales = 2167.784700m, PercentageOfTotalSalesInRegion = 0.01m, PercentageOfTotalCategorySalesInRegion = 0.12m },
            new SaleAnalysisView { Id = 2, ProductName = "HL Mountain Frame - Black, 42", ProductCategory = "Components", TotalSales = 2144.112900m, PercentageOfTotalSalesInRegion = 0.01m, PercentageOfTotalCategorySalesInRegion = 0.12m },
            new SaleAnalysisView { Id = 3, ProductName = "HL Mountain Frame - Black, 48", ProductCategory = "Components", TotalSales = 1619.520000m, PercentageOfTotalSalesInRegion = 0.01m, PercentageOfTotalCategorySalesInRegion = 0.09m },
            new SaleAnalysisView { Id = 4, ProductName = "HL Mountain Frame - Silver, 48", ProductCategory = "Components", TotalSales = 1637.400000m, PercentageOfTotalSalesInRegion = 0.01m, PercentageOfTotalCategorySalesInRegion = 0.09m },
            new SaleAnalysisView { Id = 5, ProductName = "HL Mountain Frame - Black, 42", ProductCategory = "Components", TotalSales = 1429.408600m, PercentageOfTotalSalesInRegion = 0.01m, PercentageOfTotalCategorySalesInRegion = 0.08m },
            new SaleAnalysisView { Id = 6, ProductName = "HL Mountain Frame - Silver, 46", ProductCategory = "Components", TotalSales = 1445.189800m, PercentageOfTotalSalesInRegion = 0.01m, PercentageOfTotalCategorySalesInRegion = 0.08m },
            new SaleAnalysisView { Id = 7, ProductName = "HL Mountain Frame - Silver, 38", ProductCategory = "Components", TotalSales = 1445.189800m, PercentageOfTotalSalesInRegion = 0.01m, PercentageOfTotalCategorySalesInRegion = 0.08m },
            new SaleAnalysisView { Id = 8, ProductName = "HL Mountain Frame - Black, 38", ProductCategory = "Components", TotalSales = 1429.408600m, PercentageOfTotalSalesInRegion = 0.01m, PercentageOfTotalCategorySalesInRegion = 0.08m },
            new SaleAnalysisView { Id = 9, ProductName = "HL Mountain Frame - Black, 48", ProductCategory = "Components", TotalSales = 809.760000m, PercentageOfTotalSalesInRegion = 0.01m, PercentageOfTotalCategorySalesInRegion = 0.05m },
            new SaleAnalysisView { Id = 10, ProductName = "HL Mountain Frame - Silver, 48", ProductCategory = "Components", TotalSales = 818.700000m, PercentageOfTotalSalesInRegion = 0.01m, PercentageOfTotalCategorySalesInRegion = 0.05m },
            new SaleAnalysisView { Id = 11, ProductName = "HL Mountain Frame - Black, 42", ProductCategory = "Components", TotalSales = 714.704300m, PercentageOfTotalSalesInRegion = 0.00m, PercentageOfTotalCategorySalesInRegion = 0.04m },
            new SaleAnalysisView { Id = 12, ProductName = "HL Mountain Frame - Black, 38", ProductCategory = "Components", TotalSales = 714.704300m, PercentageOfTotalSalesInRegion = 0.00m, PercentageOfTotalCategorySalesInRegion = 0.04m },
            new SaleAnalysisView { Id = 13, ProductName = "HL Mountain Frame - Black, 42", ProductCategory = "Components", TotalSales = 714.704300m, PercentageOfTotalSalesInRegion = 0.00m, PercentageOfTotalCategorySalesInRegion = 0.04m },
            new SaleAnalysisView { Id = 14, ProductName = "HL Mountain Frame - Silver, 38", ProductCategory = "Components", TotalSales = 722.594900m, PercentageOfTotalSalesInRegion = 0.00m, PercentageOfTotalCategorySalesInRegion = 0.04m },
            new SaleAnalysisView { Id = 15, ProductName = "HL Mountain Frame - Black, 38", ProductCategory = "Components", TotalSales = 714.704300m, PercentageOfTotalSalesInRegion = 0.00m, PercentageOfTotalCategorySalesInRegion = 0.04m },
            new SaleAnalysisView { Id = 16, ProductName = "HL Mountain Frame - Black, 38", ProductCategory = "Components", TotalSales = 714.704300m, PercentageOfTotalSalesInRegion = 0.00m, PercentageOfTotalCategorySalesInRegion = 0.04m },
            new SaleAnalysisView { Id = 17, ProductName = "ML Road Frame - Red, 48", ProductCategory = "Components", TotalSales = 713.796000m, PercentageOfTotalSalesInRegion = 0.00m, PercentageOfTotalCategorySalesInRegion = 0.04m },
            new SaleAnalysisView { Id = 18, ProductName = "ML Road Frame - Red, 48", ProductCategory = "Components", TotalSales = 356.898000m, PercentageOfTotalSalesInRegion = 0.00m, PercentageOfTotalCategorySalesInRegion = 0.02m },
            new SaleAnalysisView { Id = 19, ProductName = "LL Road Frame - Black, 52", ProductCategory = "Components", TotalSales = 178.580800m, PercentageOfTotalSalesInRegion = 0.00m, PercentageOfTotalCategorySalesInRegion = 0.01m },
            new SaleAnalysisView { Id = 20, ProductName = "LL Road Frame - Red, 60", ProductCategory = "Components", TotalSales = 183.938200m, PercentageOfTotalSalesInRegion = 0.00m, PercentageOfTotalCategorySalesInRegion = 0.01m }
        };
        public async Task<List<SaleAnalysisView>> GetSalesAnalysis(string productName = "", string categoryName = "")
        {
            // Filtrar los datos ficticios según los parámetros de consulta
            var filteredData = _fakeData;

            if (!string.IsNullOrEmpty(productName))
            {
                filteredData = filteredData.Where(s => s.ProductName == productName).ToList();
            }

            if (!string.IsNullOrEmpty(categoryName))
            {
                filteredData = filteredData.Where(s => s.ProductCategory == categoryName).ToList();
            }

            return filteredData;
        }
    }
}

