using Dashboard.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Dashboard.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductsDetials> productsDetials { get; set; }
        public DbSet<Damegedproducts> damegedproducts { get; set; }


    }
}