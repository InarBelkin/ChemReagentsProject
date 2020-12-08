using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.Interfaces
{
    public abstract class IReposAbstract<T> : IRepository<T> where T : class
    {
        protected DbSet<T> db2;
        protected ChemContext db;

        protected IReposAbstract(DbSet<T> dbset, ChemContext chem)
        {
            db2 = dbset;
            db = chem;
        }

        public virtual void Create(T item)
        {
            db2.Add(item);
        }

        public virtual void Delete(int id)
        {
            T sup = db2.Find(id);
            if (sup != null)
            {
                db2.Remove(sup);
            }
        }

        public virtual T GetItem(int id)
        {
            return db2.Find(id);
        }

        public virtual List<T> GetList()
        {
            List <T> a = db2.ToList();
            return a;
        }

        /// <summary>
        /// Переопредели эту фигню!
        /// </summary>
        public virtual void Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }
}
