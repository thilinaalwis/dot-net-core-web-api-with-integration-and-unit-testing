using BusinessLogicLayer.IRepositories;
using BusinessLogicLayer.Repositories;
using DataAccessLayer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.UOW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EcomContext _context;

        public IProductRepository Products { get;  set; }
        public IUserRepository Users { get;  set; }

        public UnitOfWork(EcomContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            Users = new UserRepository(_context);
        }

        /// <summary>
        /// Comitting all the changes to the database
        /// </summary>
        /// <returns></returns>
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Disposing the connection to the database
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
