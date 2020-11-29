using DAL.Tables;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL.Repository
{
    public class SuppliesRepSQL:IRepository<Supply>
    {
        private ChemContext db;

        public SuppliesRepSQL(ChemContext dbcontext)
        {
            db = dbcontext;
        }

        public List<Supply> GetList()
        {
            return db.Supplies.ToList();
        }

        public void Create(Supply item)
        {
            db.Supplies.Add(item);
        }

        public void Delete(int id)
        {
            Supply supl = db.Supplies.Find(id);
            if (supl != null)
            {
                db.Supplies.Remove(supl);
            }
        }

        public Supply GetItem(int id)
        {
            return db.Supplies.Find(id);
        }


        public void Update(Supply item)
        {
            //Supply s = db.Supplies.Find(item.Id);
            //s.Id = item.Id;
            //s.ReagentId = item.ReagentId;
            //s.SupplierId = item.SupplierId;
            //s.Date_Begin = item.Date_Begin;
            //s.Date_End = item.Date_End;
            //s.count = item.count;
            db.Entry(item).State = EntityState.Modified;
        }


    }
}
