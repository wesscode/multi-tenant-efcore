using Microsoft.EntityFrameworkCore;
using Multitenant.API.Domain;
using Multitenant.API.Provider;

namespace Multitenant.API.Data
{
    public class ApplicationContext : DbContext
    {
        //public readonly TenantData _tenant;
        public DbSet<Person> People { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options  /*TenantData tenant */) : base(options)
        {
            //_tenant = tenant;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema(_tenant.TenantId); //Para quando utilizar o replace SCHEMA com FactoryCacheKey

            modelBuilder.Entity<Person>().HasData(
               new Person { Id = 1, Name = "Person 1", TenantId = "tenant-1" },
               new Person { Id = 2, Name = "Person 2", TenantId = "tenant-2" },
               new Person { Id = 3, Name = "Person 3", TenantId = "tenant-2" });

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Description = "Description 1", TenantId = "tenant-1" },
                new Product { Id = 2, Description = "Description 2", TenantId = "tenant-2" },
                new Product { Id = 3, Description = "Description 3", TenantId = "tenant-2" });

            //Estratégia 01
            //Filtro Global para filtrar por tenant.
            //modelBuilder.Entity<Person>().HasQueryFilter(x => x.TenantId == _tenant.TenantId);
            //modelBuilder.Entity<Product>().HasQueryFilter(x => x.TenantId == _tenant.TenantId);
        }
    }
}
