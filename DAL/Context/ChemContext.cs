using System.Linq;
using DAL.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class ChemContext : DbContext
    {
        public ChemContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Reagent> Reagents { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t=>t.GetProperties())
                .Where(p=>p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetPrecision(18);
                property.SetScale(4);
            }
        }
    }
    
    
    
}