using BusinessLogicLayer.IRepositories;
using BusinessLogicLayer.Tools;
using BusinessModels;
using DataAccessLayer;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(EcomContext context) : base(context)
        {
        }

        /// <summary>
        /// Get next product code according to the last added product code
        /// </summary>
        /// <returns></returns>
        public string GetNextProductCode()
        {
            var lastProduct = dbSet.OrderBy(x => x.ProductId).LastOrDefault();
            if (lastProduct == null) return "P0001";
            else
            {
                string lastcodeno = (Convert.ToInt32(lastProduct.ProductCode.Substring(1)) + 1).ToString("D4");
                return  "P" + lastcodeno;
            }
        }

        /// <summary>
        /// Validate product details before saving
        /// </summary>
        /// <param name="product"></param>
        /// <param name="validateImage"></param>
        /// <returns></returns>
        public List<string> ValidateProduct(Product product,bool validateImage = false)
        {
            List<string> errors = new List<string>();

            if (Cleaners.CleanString(product.Name) == "")
                errors.Add("product name is required");

            if (Cleaners.CleanString(product.Category) == "")
                errors.Add("product category is required");

            if (validateImage)
            {
                if (product.file == null)
                    errors.Add("product image is required");
            }

            if (Cleaners.CleanString(product.Name) == "")
                errors.Add("product name is required");

            if (Cleaners.CleanInt(product.MinimumQty) == 0)
                errors.Add("product quantity is required");

            return errors;
        }
    }
}
