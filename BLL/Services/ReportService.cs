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
            foreach (Supply r in db.Reports.SupplyByReag(reagId))
            {
                ret.Add(new SupplyM(r));
            }
            return ret;
        }
    }
}
