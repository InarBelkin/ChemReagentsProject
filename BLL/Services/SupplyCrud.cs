using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Additional;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Tables;

namespace BLL.Services
{
    public class SupplyCrud //: ICrudRepos<SupplyM>
    {
        IDbRepos db;

        public SupplyCrud(IDbRepos dbRepos)
        {
            db = dbRepos;
        }

        public Exception Create(SupplyM s)
        {
            Supply nsup = s.getDal();
            //db.Supplies.Create(new Supply()
            //{
            //    Id = s.Id,
            //    ReagentId = s.ReagentId,
            //    SupplierId = s.SupplierId,
            //    Date_Begin = s.Date_Begin,
            //    Date_End = s.Date_End,
            //    State = (byte)s.State,
            //    count = s.Count
            //}); ;
            db.Supplies.Create(nsup);
            var ex1 = Save();
            //var list = db.Supplies.GetList();
            //var it = db.Supplies.GetItem(nsup.Id);

            if (ex1 != null)
            {
                db.Supplies.Delete(nsup.Id);
                var ex2 = Save();
               // var it2 = db.Supplies.GetItem(nsup.Id);
                if (ex2 != null) return new AddEditExeption("При создании поставки произошла ошибка, которую не удалось исправить.", ex2);
                else return new AddEditExeption("Не удалось создать поставку", ex1);
            }
            else return null;



        }

        public void Delete(int id)
        {
            db.Supplies.Delete(id);
            Save();
        }

        public SupplyM GetItem(int id)
        {
            SupplyM s = new SupplyM(db.Supplies.GetItem(id));
            //if (s.State == SupplStates.Active && DateTime.Now > s.Date_End)   //B
            //{
            //    s.State = SupplStates.ToWriteOff;
            //    Update(s);
            //}

            return s;

        }

        public ObservableCollection<SupplyM> GetList()
        {
            ObservableCollection<SupplyM> ret = new ObservableCollection<SupplyM>();
            foreach (Supply sold in db.Supplies.GetList())
            {
                SupplyM s = new SupplyM(sold);
                //if (s.State == SupplStates.Active && DateTime.Now > s.Date_End)
                //{
                //    s.State = SupplStates.ToWriteOff;
                //    Update(s);
                //}
                ret.Add(s);
            }

            return ret;
            //return db.Supplies.GetList().Select(i => new SupplyM(i)).ToList();
        }

        public Exception Update(SupplyM item)
        {

            Supply sup = db.Supplies.GetItem(item.Id);
            SupplyM backup = new SupplyM(sup);

            sup.ReagentId = item.ReagentId;
            sup.SupplierId = item.SupplierId;
            sup.Date_Begin = item.Date_Begin;
            sup.Date_End = item.Date_End;
            sup.State = (byte)item.State;
            sup.count = item.Count;
            db.Supplies.Update(sup);


            var ex1 = Save();
            if (ex1 != null)
            {
                backup.updDal(sup);
                db.Supplies.Update(sup);
                var ex2 = Save();
                if(ex2!=null) return new AddEditExeption("При изменении поставки произошла ошибка, которую не удалось исправить.", ex2);
                else return new AddEditExeption("Не удалось изменить поставку", ex1);
            }


            return null;

        }

        public Exception Save()
        {
          
            return db.Save();
       
        }
    }
}
