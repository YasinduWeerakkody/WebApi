using AppCrudeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace crudApp.Models
{
    public class BrandixContext : DbContext
    {

        public BrandixContext(DbContextOptions<BrandixContext> options) : base(options)
        {

        }

        public DbSet<Brandix> Brandixs { get; set; }


    }
}
