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
    public class Solution_RezipeCrud : ICrudRepos<SolutionRezipeM>
    {
        IDbRepos db;

        public Solution_RezipeCrud(IDbRepos dbRepos)
        {
            db = dbRepos;
        }

        public Exception Create(SolutionRezipeM item)
        {
            Solution_recipe s = item.getDal();
            db.Solution_Recipes.Create(s);
            var ex1 = Save();
            if (ex1 != null)
            {
                db.Solution_Recipes.Delete(s.Id);
                var ex2 = Save();
                if (ex2 != null) return new AddEditExeption("При создании рецепта ошибка, которую не удалось исправить.", ex2);
                else return new AddEditExeption("Не удалось создать рецепт", ex1);
            }
            else return null;
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

        public Exception Update(SolutionRezipeM item)
        {
            Solution_recipe s = db.Solution_Recipes.GetItem(item.Id);
            SolutionRezipeM back = new SolutionRezipeM(s);
            item.updDal(s);
            db.Solution_Recipes.Update(s);
            var ex1 = Save();
            if (ex1 != null)
            {
                back.updDal(s);
                db.Solution_Recipes.Update(s);
                var ex2 = Save();
                if (ex2 != null) return new AddEditExeption("При изменении рецепта произошла ошибка, которую не удалось исправить.", ex2);
                else return new AddEditExeption("Не удалось изменить рецепт", ex1);
            }
            return null;
        }

        Exception Save()
        {
            return db.Save();
        }
    }
}
