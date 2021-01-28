using BLL.Additional;
using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.BigServices
{
    public abstract class IServCrudAbstr<M, DM> : ICrudRepos<M> where M : IModel<DM>, new() where DM : class
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

        public virtual M GetItem(int id)
        {
            M a = new M();
            a.setfromDal(dbIn.GetItem(id));
            return a;
        }

        public virtual ObservableCollection<M> GetList()
        {
            ObservableCollection<M> ret = new ObservableCollection<M>();
            foreach (DM d in dbIn.GetList())
            {
                M a = new M();
                a.setfromDal(d);
                ret.Add(a);
            }
            return ret;

        }


        public virtual Exception Update(M item)
        {
            DM d = dbIn.GetItem(item.Id);
            M back = new M();
            back.setfromDal(d);
            var ex1 = Save();
            if(ex1!=null)
            {
                back.updDal(d);
                dbIn.Update(d);
                var ex2 = Save();
                if (ex2 != null) return new AddEditExeption(GetExString(2), ex2);
                else return new AddEditExeption(GetExString(3), ex1);
            }
            return null;
        }

        protected virtual Exception Save()
        {
            return db.Save();
        }
    }
}