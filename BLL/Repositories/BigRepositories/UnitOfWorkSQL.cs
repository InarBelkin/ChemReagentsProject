using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Context;
using DAL.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BLL.Repositories.BigRepositories
{
    public class UnitOfWorkSQL : IUnitOfWork
    {
        private ChemContext db;

        public UnitOfWorkSQL(string connectName) //произошла смерть
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();

            string connectionString = config.GetConnectionString(connectName);

            var optionsBuilder = new DbContextOptionsBuilder<ChemContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();
            db = new ChemContext(options.Options);
            db.Database.EnsureCreated();
        }

        private ReagentRep reagents;

        public IRepository<Reagent> Reagents => reagents ??= new ReagentRep(db);

        public int Save()
        {
            return db.SaveChanges();
        }

        public int CountChanges()
        {
            int counter = 0;
            foreach (var entry in db.ChangeTracker.Entries())
                if (entry.State != EntityState.Unchanged
                    && entry.State != EntityState.Detached)
                    counter++;

            return counter;
        }
    }
}