using BLL.Additional;
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
    public class SolutionCrud : ICrudRepos<SolutionM>
    {
        IDbRepos db;

        public SolutionCrud(IDbRepos dbRepos)
        {
            db = dbRepos;
        }

        public Exception Create(SolutionM item)
        {
            Solution s = item.getDal();
            db.Solutions.Create(s);
            var ex1 = Save();
            if (ex1 != null)
            {
                db.Solutions.Delete(item.Id);
                var ex2 = Save();
                if (ex2 != null) return new AddEditExeption("При создании раствора произошла ошибка, которую не удалось исправить.", ex2);
                else return new AddEditExeption("Не удалось создать раствор", ex1);
            }
            else return null;
        }

        public void Delete(int id)
        {
            db.Solutions.Delete(id);
            Save();
        }

        public SolutionM GetItem(int id)
        {
            var s = new SolutionM(db.Solutions.GetItem(id));
            if(s.ConcentrationId!=null)
            {
                Concentration c = db.Concentrations.GetItem((int)s.ConcentrationId);
                Solution_recipe sr = db.Solution_Recipes.GetItem(c.SolutionRecipeId);
                s.SolutionRecipeId = sr.Id;
                s.ConcentrName = c.Name;
                s.RecipeName = sr.Name;
            }


            return s;
        }

     
        public ObservableCollection<SolutionM> GetList()
        {
            ObservableCollection<SolutionM> ret = new ObservableCollection<SolutionM>();
            foreach (Solution ss in db.Solutions.GetList())
            {
                var s = new SolutionM(ss);
                if (s.ConcentrationId != null)
                {
                    Concentration c = db.Concentrations.GetItem((int)s.ConcentrationId);
                    Solution_recipe sr = db.Solution_Recipes.GetItem(c.SolutionRecipeId);
                    s.SolutionRecipeId = sr.Id;
                    s.ConcentrName = c.Name;
                    s.RecipeName = sr.Name;
                }
                ret.Add(s);
            }
            return ret;
        }



        public Exception Update(SolutionM item)
        {
            
            Solution s = db.Solutions.GetItem(item.Id);
            SolutionM back = new SolutionM(s);
            item.updDal(s);
            db.Solutions.Update(s);
            var ex1 = Save();
            if (ex1 != null)
            {
                back.updDal(s);
                db.Solutions.Update(s);
                var ex2 = Save();
                if (ex2 != null) return new AddEditExeption("При изменении раствора произошла ошибка, которую не удалось исправить.", ex2);
                else return new AddEditExeption("Не удалось изменить раствор", ex1);
            }
            return null;

        }

        public Exception Save()
        {

            return db.Save();

        }
    }
}
