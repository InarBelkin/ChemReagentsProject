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
    class ReagentCrud : IServCrudAbstr<ReagentM, Reagent>
    {
        public ReagentCrud(IDbRepos dbRepos) : base(dbRepos, dbRepos.Reagents) { }

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
    }

    class SupplyCrud : IServCrudAbstr<SupplyM, Supply>
    {
        public SupplyCrud(IDbRepos dbRepos) : base(dbRepos, dbRepos.Supplies) { }

        protected override string GetExString(int num)
        {
            switch (num)
            {
                case 0: return "При создании поставки произошла ошибка, которую не удалось исправить.";
                case 1: return "Не удалось создать поставку";
                case 2: return "При изменении поставки произошла ошибка, которую не удалось исправить.";
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
                    return new ObservableCollection<SupplyM>(reag.Supplies.Where(i => i.Date_End > f.DateFrom).Select(i => new SupplyM(i)).ToList());
                }
                else return new ObservableCollection<SupplyM>();
            }
            return base.GetList(filter);
        }
    }



}
