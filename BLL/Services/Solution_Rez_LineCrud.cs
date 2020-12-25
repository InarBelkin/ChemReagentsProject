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

namespace BLL.Services
{
    public class Solution_Rez_LineCrud : ICrudRepos<SolutRezLineM>
    {
        IDbRepos db;

        public Solution_Rez_LineCrud(IDbRepos dbRepos)
        {
            db = dbRepos;
        }
        public void Create(SolutRezLineM item)
        {
            Solution_recipe_line l = item.getDal();
            db.Solution_Rezipe_Line.Create(l);
            Save();
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

        public void Update(SolutRezLineM item)
        {
            Solution_recipe_line l = db.Solution_Rezipe_Line.GetItem(item.Id);
            item.updDal(l);
            db.Solution_Rezipe_Line.Update(l);
            Save();
        }

        int Save()
        {
            return db.Save();
        }
    }
}
