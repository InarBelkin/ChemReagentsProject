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
    public class ReportService : IReportServ
    {
        IDbRepos db;

        public ReportService(IDbRepos repos)
        {
            db = repos;
        }

        public ObservableCollection<SupplyM> SupplyByReag(int reagId)
        {
            ObservableCollection<SupplyM> ret = new ObservableCollection<SupplyM>();

            List<Supply> ss = db.Reports.SupplyByReag(reagId);

            bool ischange = false;
            foreach (Supply r in ss)
            {
                SupplyM s = new SupplyM(r);

     
                if ((s.State == SupplStates.SoonToWriteOff || s.State==SupplStates.Active) && DateTime.Now > s.Date_End)
                {
                    s.State = SupplStates.ToWriteOff;
                    r.State = (byte)SupplStates.ToWriteOff;
                    db.Supplies.Update(r);
                    ischange = true;
                }
                if(s.State == SupplStates.Active && DateTime.Now > s.Date_End.AddMonths(-1))
                {
                    s.State = SupplStates.SoonToWriteOff;
                    r.State = (byte)SupplStates.SoonToWriteOff;
                    db.Supplies.Update(r);
                    ischange = true;
                }

                if (ischange) db.Save();


                ret.Add(s);

            }
            return ret;
        }

        public ObservableCollection<SolutRezLineM> GetRecipeLine(int RecipeId)
        {
            ObservableCollection<SolutRezLineM> ret = new ObservableCollection<SolutRezLineM>();

            foreach (Solution_recipe_line s in db.Reports.GetReciepeLine(RecipeId))
            {
                SolutRezLineM sM = new SolutRezLineM(s);
                sM.PropertyChanged += SML_PropertyChanged;
                ret.Add(sM);
            }
            return ret;
        }

        private void SML_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ReagentId")
            {
                if (sender is SolutRezLineM s)
                {
                    Reagent r = db.Reagents.GetItem(s.ReagentId);
                    if (r != null)
                    {
                        s.Units = r.units;
                    }
                }
            }
        }
    }
}
