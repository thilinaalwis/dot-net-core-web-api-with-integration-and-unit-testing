using BusinessModels;
using DataAccessLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        string GetNextProductCode();
        List<string> ValidateProduct(Product product, bool validateImage = false);
    }
}
