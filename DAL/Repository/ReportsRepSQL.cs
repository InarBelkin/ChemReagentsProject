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
            // Reagent d = db.Reagents.Find(reagId);
            Reagent r = db.Reagents.Find(reagId);
            List<Supply> a= null;
            if (r!=null)
            {
               a = r.Supplies;
            }
         

            return a ?? new List<Supply>();
        }
    }
}
