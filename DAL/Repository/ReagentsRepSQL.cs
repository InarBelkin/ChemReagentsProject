using DAL.Interfaces;
using DAL.Tables;
using System;
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
            //ChemContext db2 = new ChemContext();

            //List<Supply> s =  db2.Supplies.ToList();
            //List<Reagent> d = db2.Reagents.ToList();

            // db.Supplies.Load();

            List<Reagent> a;
            try
            {
               a = db.Reagents.ToList();
            }
            catch
            {
                throw new Exception("asdf");
            }
            finally
            {

            }

            //Supply s = a[0].Supplies[0];
            //s.count = 999;
            //List<Supply> supl = db.Supplies.ToList();
            return a;
        }
        public override void Create(Reagent item)
        {
            db.Reagents.Add(item);
        }
        public override void Delete(int id)
        {
            Reagent sup = db2.Find(id);
            if (sup != null)
            {
                db2.Remove(sup);
            }
            base.Delete(id);

        }
    }
}
