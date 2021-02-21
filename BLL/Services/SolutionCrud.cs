using BLL.Models;
using BLL.Services.BigServices;
using BLL.Services.FIlters;
using DAL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace BLL.Services
{
    class SolutionCrud : IServCrudAbstr<SolutionM, Solution>
    {
        public SolutionCrud(IDbRepos dbRepos) : base(dbRepos, dbRepos.Solutions) { }
        protected override string GetExString(int num)
        {
            switch (num)
            {
                case 0: return "При создании раствора произошла ошибка, которую не удалось исправить.";
                case 1: return "Не удалось создать раствор";
                case 2: return "При изменении раствора произошла ошибка, которую не удалось исправить.";
                case 3: return "Не удалось изменить раствор";
            }
            return "Что-то странное происходит";
        }
        public override ObservableCollection<SolutionM> GetList(object filter)
        {
            if(filter is SolutionFilter f)
            {
                var lret = db.Solutions.GetList().Where(i => i.Date_Begin >= f.DateStart && i.Date_Begin <= f.DateEnd).Select(i => new SolutionM(i)).ToList();
                lret.Sort(new SolutComparer());
                var ret = new ObservableCollection<SolutionM>(lret);
                foreach(var s in ret)
                {
                    if(s.RecipeId!=null)
                    {
                        s.RecipeName = db.Solution_Recipes.GetItem((int)s.RecipeId).Name;
                        if(s.ConcentrationId!=null)
                        {
                            s.ConcentrName = db.Concentrations.GetItem((int)s.ConcentrationId).Name;
                        }
                    }
                }
                return ret;
            }
            return base.GetList(filter);
        }
    }
    class SolutionLineCrud : IServCrudAbstr<SolutionLineM, Solution_line>
    {
        public SolutionLineCrud(IDbRepos dbRepos) : base(dbRepos, dbRepos.Solution_Lines) { }
        protected override string GetExString(int num)
        {
            switch (num)
            {
                case 0: return "При создании строки раствора произошла ошибка, которую не удалось исправить.";
                case 1: return "Не удалось создать строку раствора";
                case 2: return "При изменении строки раствора произошла ошибка, которую не удалось исправить.";
                case 3: return "Не удалось изменить строку раствора";
            }
            return "Что-то странное происходит";
        }
        public override ObservableCollection<SolutionLineM> GetList(object filter)
        {
            if (filter is SolutionLineFilter f)
            {
                var solut = db.Solutions.GetItem(f.SolutionId);
                if (solut.Solution_Lines != null)
                {
                    var ret = new ObservableCollection<SolutionLineM>(solut.Solution_Lines.Select(i => new SolutionLineM(i)).ToList());
                    foreach (var sl in ret)
                    {
                        Setitem(sl);
                    }
                    return ret;
                }
                else return new ObservableCollection<SolutionLineM>();

            }
            else return base.GetList(filter);
        }


        public override SolutionLineM GetItem(int id)
        {
            return base.GetItem(id);
        }
        private void Setitem(SolutionLineM s)
        {
            if (s.SupplyId == null)
                s.ReagentId = -1;
            else
            {
                Supply sup = db.Supplies.GetItem((int)s.SupplyId);
                s.ReagentId = sup.ReagentId;
                if (sup.Reagent.isWater)
                    s.Units = "мл.";
                else s.Units = "гр.";
                        
                        
            }
        }

    }
}
