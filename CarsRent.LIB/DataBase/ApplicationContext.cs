using CarsRent.LIB.Model;
using Microsoft.EntityFrameworkCore;

namespace CarsRent.LIB.DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Human> Humans { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Renter> Renters { get; set; }
        public DbSet<Owner> Owners { get; set; }

        public ApplicationContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=cars_rent;Trusted_Connection=True;");
        }
    }
}