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
        public override ObservableCollection<ReagentM> GetList(object filter)
        {
            if(filter is ReagentFilter f)
            {
                return new ObservableCollection<ReagentM>(db.Reagents.GetList().Where(i => i.IsAccounted == f.IsAccounting).Select(i=>new ReagentM(i)).ToList());
            }
            return base.GetList(filter);
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
                    return new ObservableCollection<SupplyM>(reag.Supplies.Where(i => i.Date_End > f.DateFrom).Select(i => new SupplyM(i)).ToList());
                }
                else return new ObservableCollection<SupplyM>();
            }
            return base.GetList(filter);
        }
    }

    class SupplierCrud: IServCrudAbstr<SupplierM,Supplier>
    {
        public SupplierCrud(IDbRepos dbRepos) : base(dbRepos, dbRepos.Suppliers) { }
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

}
