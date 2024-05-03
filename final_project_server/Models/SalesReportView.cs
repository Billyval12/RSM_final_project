using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppApi.Models
{
    [Table("SalesReportView")]
    public class SalesReportView
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        [StringLength(255)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(255)]
        public string ProductCategory { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public int SalesPersonID { get; set; }

        [StringLength(100)]
        public string SalesPersonName { get; set; }

        [StringLength(255)]
        public string ShippingAddress { get; set; }

        [StringLength(100)]
        public string BillingAddress { get; set; }
    }
}
