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
    public class SupplierCrud : ICrudRepos<SupplierM>
    {
        IDbRepos db;

        public SupplierCrud(IDbRepos dbRepos)
        {
            db = dbRepos;
        }

        public void Create(SupplierM item)
        {
            Supplier s = new Supplier()
            {
                Id = item.Id,
                Name = item.Name
            };

            db.Suppliers.Create(s);
            Save();
        }

        public void Delete(int id)
        {
            db.Suppliers.Delete(id);
            Save();
        }

        public SupplierM GetItem(int id)
        {
            return new SupplierM(db.Suppliers.GetItem(id));
        }

        public ObservableCollection<SupplierM> GetList()
        {
            ObservableCollection<SupplierM> ret = new ObservableCollection<SupplierM>();
            foreach (Supplier s in db.Suppliers.GetList())
            {
                ret.Add(new SupplierM(s));
            }
            return ret;
        }

        public void Update(SupplierM item)
        {
            Supplier s = db.Suppliers.GetItem(item.Id);
            s.Id = item.Id;
            s.Name = item.Name;
            db.Suppliers.Update(s);
            Save();
        }

        int Save()
        {
            throw new NotImplementedException();
            //return db.Save();
        }
    }
}
