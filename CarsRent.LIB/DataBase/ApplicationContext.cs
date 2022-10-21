using CarsRent.LIB.Model;
using Microsoft.EntityFrameworkCore;

namespace CarsRent.LIB.DataBase
{
    public class ApplicationContext : DbContext
    {
        private static ApplicationContext _instance;
        
        public DbSet<Car> Cars { get; set; }
        public DbSet<Human> Humans { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Renter> Renters { get; set; }
        public DbSet<Owner> Owners { get; set; }

        public ApplicationContext() { }

        public static ApplicationContext Instance()
        {
            if (_instance == null)
            {
                _instance = new ApplicationContext();
                _instance.Database.EnsureCreated();
            }

            return _instance;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=cars_rent;Trusted_Connection=True;");
        }
    }
}