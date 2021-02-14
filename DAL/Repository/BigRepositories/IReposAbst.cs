using DAL.Additional;
using DAL.Tables;
using System;
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
            T ret = null;
            try
            {
                ret = db2.Find(id);
            }
            catch(Exception ex)
            {
                ExceptionSystemD.ConnectLostInv(new ConnectionExcetionD(ex));   //ну тут как бы не совсем так надо делать, есть ведь ещё исключение, когда не найден элемент
            }
            return ret;
        }

        public virtual List<T> GetList()
        {
            List<T> a = new List<T>();
            try
            {
                a = db2.ToList();
            }
            catch(Exception ex)
            {
                ExceptionSystemD.ConnectLostInv(new ConnectionExcetionD(ex));
            }
            return a;
        }
  
        public virtual void Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }
}
