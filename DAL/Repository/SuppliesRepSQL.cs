using DAL.Tables;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Additional;

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
            var a = new List<Supply>();
            try
            {
                a = db.Supplies.ToList();
            }
            catch(Exception ex)
            {
                ExceptionSystemD.ConnectLostInv(new ConnectionExcetionD(ex));
            }
            return a;
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
            Supply a = null;
            try
            {
                a = db.Supplies.Find(id);
            }
            catch(Exception ex)
            {
                ExceptionSystemD.ConnectLostInv(new ConnectionExcetionD(ex));
            }

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
