using BLL.Models;
using BLL.Services.BigServices;
using BLL.Services.FIlters;
using DAL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using BLL.Interfaces;

namespace BLL.Services
{
    public class ReagentCrud : IServCrudAbstr<ReagentM, Reagent>
    {
       
        public ReagentCrud(IDbRepos dbRepos) : base(dbRepos, dbRepos.Reagents)
        {
           
        }

        protected override string GetExString(int num)
        {
            switch (num)
            {
                case 0: return "При создании реактива произошла ошибка, которую не удалось исправить.";
                case 1: return "Не удалось создать реактив";
                case 2: return "При изменении реактива произошла ошибка, которую не удалось исправить.";
                case 3: return "Не удалось изменить реактив";
            }

            return "Что-то странное происходит";
        }

        public override ObservableCollection<ReagentM> GetList(object filter)
        {
            if (filter is ReagentFilter f)
            {
                return new ObservableCollection<ReagentM>(db.Reagents.GetList()
                    .Where(i => i.IsAccounted == f.IsAccounting).Select(i => new ReagentM(i)).ToList());
            }

            return base.GetList(filter);
        }
    }

    class SupplyCrud : IServCrudAbstr<SupplyM, Supply>
    {
        public SupplyCrud(IDbRepos dbRepos) : base(dbRepos, dbRepos.Supplies)
        {
        }

        protected override string GetExString(int num)
        {
            switch (num)
            {
                case 0: return "При создании поставки произошла ошибка, которую не удалось исправить.";
                case 1: return "Не удалось создать поставку";
                case 2: return "При изменении поставки произошла ошибка, которую не удалось исп равить.";
                case 3: return "Не удалось изменить поставку";
            }

            return "Что-то странное происходит";
        }

        public override ObservableCollection<SupplyM> GetList(object filter)
        {
            if (filter is SupplyFilter f && f.ReagentId > 0)
            {
                var reag = db.Reagents.GetItem(f.ReagentId);
                if (reag.Supplies != null)
                {
                    //var slist = reag.Supplies.Where(i => i.Date_Expiration > f.DateFrom).Select(i => new SupplyM(i)).ToList();
                    var slist = reag.Supplies.Select(i => new SupplyM(i)).ToList();
                    slist.Sort(new SupplyComparer());
                    return new ObservableCollection<SupplyM>(slist);
                }
                else return new ObservableCollection<SupplyM>();
            }
            else if (filter is SupplyforSolutFilter f2 && f2.ReagentId > 0)
            {
                var reag = db.Reagents.GetItem(f2.ReagentId);
                if (reag.Supplies != null)
                {
                    var slist = reag.Supplies
                        .Where(i => f2.DateNow >= i.Date_StartUse && f2.DateNow <= i.Date_UnWrite.AddDays(1))
                        .Select(i => new SupplyM(i)).ToList();
                    if (!reag.IsAccounted)
                    {
                        foreach (var s in slist)
                        {
                            s.ShortName = "Не учитывается";
                        }
                    }

                    slist.Sort(new SupplyComparer());
                    return new ObservableCollection<SupplyM>(slist);
                }
                else return new ObservableCollection<SupplyM>();
            }

            return base.GetList(filter);
        }
    }

    class SupplierCrud : IServCrudAbstr<SupplierM, Supplier>
    {
        public SupplierCrud(IDbRepos dbRepos) : base(dbRepos, dbRepos.Suppliers)
        {
        }

        protected override string GetExString(int num)
        {
            switch (num)
            {
                case 0: return "При создании поставщика произошла ошибка, которую не удалось исправить.";
                case 1: return "Не удалось создать поставщика";
                case 2: return "При изменении поставщика произошла ошибка, которую не удалось исправить.";
                case 3: return "Не удалось изменить поставщика";
            }

            return "Что-то странное происходит";
        }
    }

    public class SupplConsumpCrud : IServCrudAbstr<SupplyStingM, Supply_consumption>
    {
        public IJsonDb jsonDb;
        public  SupplConsumpCrud(IDbRepos dbRepos,  IJsonDb jsonDb = null) : base(dbRepos, dbRepos.Consumptions)
        {
            this.jsonDb = jsonDb;
        }

        protected override string GetExString(int num)
        {
            switch (num)
            {
                case 0: return "При создании взятия поствки произошла ошибка, которую не удалось исправить.";
                case 1: return "Не удалось создать взятие поставки";
                case 2: return "При изменении взятия поставки произошла ошибка, которую не удалось исправить.";
                case 3: return "Не удалось изменить взятие поставки";
            }

            return "Что-то странное происходит";
        }

        public override ObservableCollection<SupplyStingM> GetList(object filter)
        {
            if (filter is SupplyStringsFilter f)
            {
                ObservableCollection<SupplyStingM> ret = new ObservableCollection<SupplyStingM>();
                Supply s = db.Supplies.GetItem(f.SupplyId);
                if (s.Solution_Lines != null)
                {
                    foreach (var sl in s.Solution_Lines)
                    {
                        var a = new SupplyStingM(sl);
                        if (sl.Solution.RecipeId == null)
                        {
                            a.Name = sl.Solution.RecipeName + " " + sl.Solution.ConcentrName;
                        }
                        else
                        {
                            var recep = db.Solution_Recipes.GetItem((int)sl.Solution.RecipeId);
                            a.Name = recep.Name;
                            if (sl.Solution.ConcentrationId == null)
                            {
                                a.Name += " " + sl.Solution.ConcentrName;
                            }
                            else
                            {
                                var conc = db.Concentrations.GetItem((int)sl.Solution.ConcentrationId);
                                a.Name += " " + conc.Name;
                            }
                        }

                        if (a.DateBegin <= f.DateTo)
                            ret.Add(a);
                    }
                }

                if (s.Consumptions != null)
                {
                    foreach (var c in s.Consumptions)
                    {
                        var b = new SupplyStingM(c);
                        if (b.DateBegin <= f.DateTo)
                            ret.Add(b);
                    }
                }

                return ret;
            }

            return base.GetList(filter);
        }

        public bool CheckReagent()
        {
            Reagent r = jsonDb.GetReagent();
            //1
            if (!r.isWater && r.isAlwaysWater || r.Name.Length < 3 //2,3,4
                                              || Regex.IsMatch(r.Formula, "^[А-Яа-я]+$")) //5
                return false; //6
            if (r.IsAccounted) //7
            {
                if (r.Location.Length < 3) //9
                    return false; //10

                for(int i =0;i<r.Supplies.Count;i++) //11
                {
                    if (r.Supplies[i].Count <= 0) //12
                        return false; //13
                }
            }
            return true; //8
        }
    }
}