using DAL.Interfaces;
using DAL.Tables;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repository
{
    class ReagentsRepSQL : IReposAbstract<Reagent>
    {
        // private ChemContext db;

        public ReagentsRepSQL(ChemContext dbcontext) : base(dbcontext.Reagents, dbcontext)
        {
            db = dbcontext;
        }

        public override List<Reagent> GetList()
        {
            ChemContext db2 = new ChemContext();
            db2.Supplies.Load();
            List<Reagent> d = db2.Reagents.ToList();

            db.Supplies.Load();
            List<Reagent> a = db.Reagents.ToList();
            return a;
        }
    }
}
