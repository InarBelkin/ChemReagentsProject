using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tables
{
    public class ChemContext : DbContext
    {
        public ChemContext() : base("DbConnection")
        { }

        public DbSet<Reagent> Reagents { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Solution_recipe> Solution_Recipes { get; set; }
        public DbSet<Concentration> Concentration { get; set; }
        public DbSet<Solution_recipe_line> Solution_Recipe_Lines { get; set; }

        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Solution_line> Solution_Lines { get; set; }
        public DbSet<Supply_consumption> Consumptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supply>().Property(p => p.Density).HasPrecision(18, 4);
            modelBuilder.Entity<Supply_consumption>().Property(p => p.Count).HasPrecision(18, 4);
            modelBuilder.Entity<Supply>().Property(p => p.Count).HasPrecision(18, 4);
            modelBuilder.Entity<Solution_recipe_line>().Property(p => p.Count).HasPrecision(18, 4);
            modelBuilder.Entity<Solution_line>().Property(p => p.Count).HasPrecision(18, 4);
            modelBuilder.Entity<Concentration>().Property(p => p.Count).HasPrecision(18, 4);
        }
    }
}
