using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tables
{
    public class ChemContext:DbContext
    {
        public ChemContext():base ("DbConnection")
        { }
        
        public DbSet<Reagent> Reagents { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        
        public DbSet<Solution_recipe_line> Solution_Recipe_Lines { get; set; }
        public DbSet<Solution_line> Solution_Lines { get; set; }
        public DbSet<Solution_recipe> Solution_Recipes { get; set; }
        public DbSet<Solution> Solutions { get; set; }

    }
}
