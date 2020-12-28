using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Tables;
using DAL.Interfaces;
using System.Collections.ObjectModel;
using BLL.Additional;

namespace BLL.Services
{
    public class Solution_Rez_LineCrud : ICrudRepos<SolutRezLineM>
    {
        IDbRepos db;

        public Solution_Rez_LineCrud(IDbRepos dbRepos)
        {
            db = dbRepos;
        }
        public Exception Create(SolutRezLineM item)
        {
            Solution_recipe_line l = item.getDal();
            db.Solution_Rezipe_Line.Create(l);
            var ex1 = Save();
            if (ex1 != null)
            {
                db.Solution_Rezipe_Line.Delete(l.Id);
                var ex2 = Save();
                if (ex2 != null) return new AddEditExeption("При создании строки рецепта произошла ошибка, которую не удалось исправить.", ex2);
                else return new AddEditExeption("Не удалось создать строку рецепта", ex1);
            }
            else return null;
        }

        public void Delete(int id)
        {
            db.Solution_Rezipe_Line.Delete(id);
            Save();
        }

        public SolutRezLineM GetItem(int id)
        {
            var a = new SolutRezLineM(db.Solution_Rezipe_Line.GetItem(id));
            a.PropertyChanged += SRL_PropertyChanged;
            return a;
        }

        public ObservableCollection<SolutRezLineM> GetList()
        {
            ObservableCollection<SolutRezLineM> ret = new ObservableCollection<SolutRezLineM>();
            foreach (Solution_recipe_line s in db.Solution_Rezipe_Line.GetList())
            {
                SolutRezLineM sM = new SolutRezLineM(s);
                sM.PropertyChanged += SRL_PropertyChanged;
                ret.Add(sM);
            }
            return ret;
        }

        private void SRL_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ReagentId")
            {
                if (sender is SolutRezLineM s)
                {
                    Reagent r = db.Reagents.GetItem(s.ReagentId);
                    if(r!=null)
                    {
                        s.Units = r.units;
                    }
                }
            }
        }

        public Exception Update(SolutRezLineM item)
        {
            Solution_recipe_line l = db.Solution_Rezipe_Line.GetItem(item.Id);
            SolutRezLineM back = new SolutRezLineM(l);
            item.updDal(l);
            db.Solution_Rezipe_Line.Update(l);
            var ex1 = Save();
            if (ex1 != null)
            {
                back.updDal(l);
                db.Solution_Rezipe_Line.Update(l);
                var ex2 = Save();
                if (ex2 != null) return new AddEditExeption("При изменении строки рецепта произошла ошибка, которую не удалось исправить.", ex2);
                else return new AddEditExeption("Не удалось изменить строку рецепта", ex1);
            }
            return null;
        }

        public Exception Save()
        {

            return db.Save();

        }
    }
}
