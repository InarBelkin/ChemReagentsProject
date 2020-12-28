using BLL.Additional;
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

        public Exception Create(SupplierM item)
        {
            Supplier s = new Supplier()
            {
                Id = item.Id,
                Name = item.Name
            };

            db.Suppliers.Create(s);
            var ex1 = Save();
            if (ex1 != null)
            {
                db.Suppliers.Delete(s.Id);
                var ex2 = Save();
                if (ex2 != null) return new AddEditExeption("При создании поставщика произошла ошибка, которую не удалось исправить.", ex2);
                else return new AddEditExeption("Не удалось создать поставщика", ex1);
            }
            else return null;
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

        public Exception Update(SupplierM item)
        {
            Supplier s = db.Suppliers.GetItem(item.Id);
            SupplierM back = new SupplierM(s);
            s.Id = item.Id;
            s.Name = item.Name;
            db.Suppliers.Update(s);
            var ex1 = Save();
            if (ex1 != null)
            {
                back.updDal(s);
                db.Suppliers.Update(s);
                var ex2 = Save();
                if (ex2 != null) return new AddEditExeption("При изменении поставщика произошла ошибка, которую не удалось исправить.", ex2);
                else return new AddEditExeption("Не удалось изменить поставщика", ex1);
            }


            return null;
        }

        public Exception Save()
        {

            return db.Save();

        }
    }
}

