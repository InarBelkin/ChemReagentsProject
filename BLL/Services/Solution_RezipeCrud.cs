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
    class Solution_RezipeCrud : ICrudRepos<SolutionRezipeM>
    {
        IDbRepos db;

        public Solution_RezipeCrud(IDbRepos dbRepos)
        {
            db = dbRepos;
        }

        public void Create(SolutionRezipeM item)
        {
            Solution_recipe s = item.getDal();
            db.Solution_Recipes.Create(s);
            Save();
        }

        public void Delete(int id)
        {
            db.Solution_Recipes.Delete(id);
            Save();
        }

        public SolutionRezipeM GetItem(int id)
        {
            return new SolutionRezipeM(db.Solution_Recipes.GetItem(id));
        }

        public ObservableCollection<SolutionRezipeM> GetList()
        {
            ObservableCollection<SolutionRezipeM> ret = new ObservableCollection<SolutionRezipeM>();
            foreach(Solution_recipe s in db.Solution_Recipes.GetList())
            {
                ret.Add(new SolutionRezipeM(s));
            }
            return ret;
        }

        public void Update(SolutionRezipeM item)
        {
            Solution_recipe s = db.Solution_Recipes.GetItem(item.Id);
            item.updDal(s);
            db.Solution_Recipes.Update(s);
            Save();
        }

        int Save()
        {
            return db.Save();
        }
    }
}
