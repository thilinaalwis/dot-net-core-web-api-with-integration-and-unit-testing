using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessModels
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int MinimumQty { get; set; }
        public decimal DiscountRate { get; set; }

        [NotMapped]
        public IFormFile file { get; set; }


    }
}
