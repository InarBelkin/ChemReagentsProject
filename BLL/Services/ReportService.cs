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

            //List<Reagent> reag = db.Reagents.GetList();
            //Console.WriteLine();
            List<Supply> s = db.Reports.SupplyByReag(reagId);
           
            foreach (Supply r in s)
            {
                ret.Add(new SupplyM(r));
            }
            return ret;
        }
    }
}
