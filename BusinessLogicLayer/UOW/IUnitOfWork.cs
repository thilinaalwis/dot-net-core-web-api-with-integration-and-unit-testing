using BusinessLogicLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.UOW
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; set; }
        IUserRepository Users { get; set; }
        Task CompleteAsync();
        void Dispose();
    }
}
