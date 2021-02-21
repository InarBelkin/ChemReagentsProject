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
    class RecipeCrud : IServCrudAbstr<RecipeM, Solution_recipe>
    {
        public RecipeCrud(IDbRepos dbRepos) : base(dbRepos, dbRepos.Solution_Recipes) { }

        protected override string GetExString(int num)
        {
            switch (num)
            {
                case 0: return "При создании рецепта произошла ошибка, которую не удалось исправить.";
                case 1: return "Не удалось создать рецепт";
                case 2: return "При изменении рецепта произошла ошибка, которую не удалось исправить.";
                case 3: return "Не удалось изменить рецепт";
            }
            return "Что-то странное происходит";
        }
    }

    class ConcentrationCrud : IServCrudAbstr<ConcentrationM, Concentration>
    {
        public ConcentrationCrud(IDbRepos dbRepos) : base(dbRepos, dbRepos.Concentrations) { }

        protected override string GetExString(int num)
        {
            switch (num)
            {
                case 0: return "При создании концентрации произошла ошибка, которую не удалось исправить.";
                case 1: return "Не удалось создать концентрацию";
                case 2: return "При изменении концентрации произошла ошибка, которую не удалось исправить.";
                case 3: return "Не удалось изменить концентрацию";
            }
            return "Что-то странное происходит";
        }
        public override ObservableCollection<ConcentrationM> GetList(object filter)
        {
            if (filter is ConcentrationFilter f)
            {
                var sr = db.Solution_Recipes.GetItem(f.RecipeId);
                if(sr!=null)
                {
                    var rec = sr.Concentrations;
                    if (rec != null)
                    {
                        return new ObservableCollection<ConcentrationM>(rec.Select(i => new ConcentrationM(i)).ToList());
                    }
                    else return new ObservableCollection<ConcentrationM>();
                }
                else return new ObservableCollection<ConcentrationM>();

            }
            else return base.GetList(filter);
        }
    }

    class RecipeLineCrud : IServCrudAbstr<RecipeLineM, Solution_recipe_line>
    {
        public RecipeLineCrud(IDbRepos dbRepos) : base(dbRepos, dbRepos.Solution_Rezipe_Line) { }

        protected override string GetExString(int num)
        {
            switch (num)
            {
                case 0: return "При создании строки рецепта произошла ошибка, которую не удалось исправить.";
                case 1: return "Не удалось создать строку рецепта";
                case 2: return "При изменении строки рецепта произошла ошибка, которую не удалось исправить.";
                case 3: return "Не удалось изменить строку рецепта";
            }
            return "Что-то странное происходит";
        }
        public override ObservableCollection<RecipeLineM> GetList(object filter)
        {
            if (filter is RecipeLineFilter f)
            {
                var rec = db.Concentrations.GetItem(f.ConcentrationId).LineList;
                if (rec != null)
                {
                    return new ObservableCollection<RecipeLineM>(rec.Select(i => new RecipeLineM(i)).ToList());
                }
                else return new ObservableCollection<RecipeLineM>();
            }
            else return base.GetList(filter);
        }
        //public override Exception Create(RecipeLineM item)
        //{
        //    Concentration conc = db.Concentrations.GetItem(item.ConcentracionId);
        //    Solution_recipe rec = conc.Solution_Recipe;
        //    Exception ex = null;
        //    foreach (Concentration c in rec.Concentrations)
        //    {
        //        item.ConcentracionId = c.Id;
        //        ex = base.Create(item);
        //        if (ex != null) break;
        //    }
        //    return ex;
       
        //}
    }
}
