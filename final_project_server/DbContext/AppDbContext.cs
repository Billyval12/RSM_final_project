using Microsoft.EntityFrameworkCore;
namespace AppApi.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<SaleAnalysisView> SalesAnalysisView { get; set; } = null!;
    public DbSet<SalesReportView> SalesReportView { get; set; } = null!;
    public DbSet<NewSalesView> NewSalesView { get; set; } = null!;
}