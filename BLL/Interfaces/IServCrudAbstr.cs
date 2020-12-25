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

        public IServCrudAbstr(IDbRepos repos, IRepository<DM> repIn)
        {
            db = repos;
            dbIn = repIn;
        }

        public virtual void Create(M item)
        {
            DM cl = item.getDal();
            dbIn.Create(cl);
            Save();
        }

        public virtual void Delete(int id)
        {
            dbIn.Delete(id);
            Save();
        }

        public abstract M GetItem(int id);


        public abstract ObservableCollection<M> GetList();


        public virtual void Update(M item)
        {
            DM s = dbIn.GetItem(item.Id);
            item.updDal(s);
            dbIn.Update(s);
            Save();
        }

        protected virtual int Save()
        {
            return db.Save();
        }
    }
}
