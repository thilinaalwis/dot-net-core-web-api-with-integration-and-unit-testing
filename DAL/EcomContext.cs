using BusinessModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccessLayer
{
    public class EcomContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public EcomContext(DbContextOptions<EcomContext> options)
            : base(options)
        {

        }
    }
}
