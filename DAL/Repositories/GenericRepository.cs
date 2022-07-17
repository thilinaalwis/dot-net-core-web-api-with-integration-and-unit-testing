using DataAccessLayer;
using DataAccessLayer.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected EcomContext _context;
        protected DbSet<T> dbSet;

        public GenericRepository(
            EcomContext context
        )
        {
            _context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }

        }

        public virtual async Task<T> GetById(int id)
        {
            try
            {
                return await dbSet.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public virtual async Task<bool> Add(T entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
 
        }

        public virtual async Task<bool> Delete(int id)
        {
            try
            {
                T entity = await GetById(id);
                dbSet.Remove(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public virtual bool Update(T entity)
        {
            try
            {
                dbSet.Update(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }

}
