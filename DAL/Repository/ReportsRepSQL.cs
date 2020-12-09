using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Tables;

namespace DAL.Repository
{
    public class ReportsRepSQL : IReportRepos
    {
        private ChemContext db;

        public ReportsRepSQL(ChemContext dbcontext) 
        {
            db = dbcontext;
        }
        public List<Supply> SupplyByReag(int reagId)
        {
            List < Supply > a = db.Reagents.Find(reagId).Supplies;
            return a;
        }
    }
}
