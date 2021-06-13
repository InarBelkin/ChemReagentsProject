using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using DAL.Context;
using DAL.Tables;
using Microsoft.EntityFrameworkCore;

namespace BLL.Repositories.BigRepositories
{
    public class AbstractRepository<T> : IRepository<T> where T : BaseDBModel
    {
        protected ChemContext db;
        protected DbSet<T> dbSet;

        public  AbstractRepository(ChemContext db, DbSet<T> dbSet)
        {
            this.db = db;
            this.dbSet = dbSet;
        }

        public virtual T GetItem(int id) => dbSet.Find(id);

        public virtual List<T> GetList()
        {
            
            var ret = dbSet.ToList();
            int c = db.ChangeTracker.Entries().Count();

            return ret;
        }

        public virtual void Create(T item)
        {
            var entity = dbSet.Add(item);
        }

        public virtual void Update(T item)
        {
            dbSet.Update(item);
        }

        public virtual bool Delete(int id)
        {
            T item = dbSet.Find(id);
            if (item != null)
            {
                dbSet.Remove(item);
                return true;
            }
            else return false;
        }
    }
}