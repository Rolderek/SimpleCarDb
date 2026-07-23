using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimpleCarDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCarDb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Brand> Brands => Set<Models.Brand>();
        public DbSet<Models.Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //dummy data :)
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "BMW" },
                new Brand { Id = 2, Name = "Toyota" },
                new Brand { Id = 3, Name = "Ford" },
                new Brand { Id = 4, Name = "Audi" },
                new Brand { Id = 5, Name = "Mercedes-Benz" }
            );

            
            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, Name = "320d", Type = "Dízel", BrandId = 1 },
                new Car { Id = 2, Name = "M3", Type = "Benzin", BrandId = 1 },
                new Car { Id = 3, Name = "Corolla", Type = "Teljes Hibrid (HEV)", BrandId = 2 },
                new Car { Id = 4, Name = "RAV4", Type = "Plug-in Hibrid (PHEV)", BrandId = 2 },
                new Car { Id = 5, Name = "Mustang", Type = "V8 Benzin", BrandId = 3 },
                new Car { Id = 6, Name = "F-150", Type = "Benzin", BrandId = 3 },
                new Car { Id = 7, Name = "A4", Type = "Dízel", BrandId = 4 },
                new Car { Id = 8, Name = "RS6", Type = "Benzin", BrandId = 4 },
                new Car { Id = 9, Name = "EQE", Type = "Elektromos (BEV)", BrandId = 5 },
                new Car { Id = 10, Name = "C220", Type = "Mild Hibrid (MHEV)", BrandId = 5 }
            );
        }
    }
}
