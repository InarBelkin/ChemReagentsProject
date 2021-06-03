using DAL.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class ChemContext : DbContext
    {
        public ChemContext(  DbContextOptions options): base(options)
        {
            
        }
        
        public DbSet<Reagent> Reagents { get; set; }

    }
}