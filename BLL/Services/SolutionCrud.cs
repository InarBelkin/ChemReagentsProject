using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    class SolutionCrud : ICrudRepos<SolutionM>
    {
        IDbRepos db;

        public SolutionCrud(IDbRepos dbRepos)
        {
            db = dbRepos;
        }

        public void Create(SolutionM item)
        {
            Solution s = item.getDal();
            db.Solutions.Create(s);
            Save();
        }

        public void Delete(int id)
        {
            db.Solutions.Delete(id);
            Save();
        }

        public SolutionM GetItem(int id)
        {
            return new SolutionM(db.Solutions.GetItem(id));
        }

        public ObservableCollection<SolutionM> GetList()
        {
            ObservableCollection<SolutionM> ret = new ObservableCollection<SolutionM>();
            foreach(Solution s in db.Solutions.GetList())
            {
                ret.Add(new SolutionM(s));
            }
            return ret;
        }

        public void Update(SolutionM item)
        {
            Solution s = db.Solutions.GetItem(item.Id);
            item.updDal(s);
            db.Solutions.Update(s);
            Save();
        }

        private int Save()
        {
            return db.Save();
        }
    }
}
