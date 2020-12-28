using BLL.Additional;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public abstract class IServCrudAbstr<M, DM> : ICrudRepos<M> where M : IModel<DM> where DM : class
    {
        protected IDbRepos db;
        protected IRepository<DM> dbIn;
        protected abstract string GetExString(int num);
      

        public IServCrudAbstr(IDbRepos repos, IRepository<DM> repIn)
        {
            db = repos;
            dbIn = repIn;
        }

        public virtual Exception Create(M item)
        {
            DM cl = item.getDal();
            dbIn.Create(cl);
            var ex1 = Save();
            if (ex1 != null)
            {
                dbIn.Delete(item.Id);
                var ex2 = Save();
                if (ex2 != null) return new AddEditExeption(GetExString(0), ex2);
                else return new AddEditExeption(GetExString(1), ex1);
            }
            else return null;
        }

        public virtual void Delete(int id)
        {
            dbIn.Delete(id);
            Save();
        }

        public abstract M GetItem(int id);


        public abstract ObservableCollection<M> GetList();


        public abstract Exception Update(M item);
        //{
        //    DM s = dbIn.GetItem(item.Id);
            
        //    item.updDal(s);
        //    dbIn.Update(s);
        //    Save();
        //}

        protected virtual Exception Save()
        {
            return db.Save();
           
        }
    }
}
