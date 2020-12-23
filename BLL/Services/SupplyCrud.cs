using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Tables;

namespace BLL.Services
{
    public class SupplyCrud : ICrudRepos<SupplyM>
    {
        IDbRepos db;

        public SupplyCrud(IDbRepos dbRepos)
        {
            db = dbRepos;
        }

        public void Create(SupplyM s)
        {
            db.Supplies.Create(new Supply()
            {
                Id = s.Id,
                ReagentId = s.ReagentId,
                SupplierId = s.SupplierId,
                Date_Begin = s.Date_Begin,
                Date_End = s.Date_End,
                State = (byte)s.State,
                count = s.Count
            });;
            Save();
        }

        public void Delete(int id)
        {
            db.Supplies.Delete(id);
            Save();
        }

        public SupplyM GetItem(int id)
        {
            SupplyM s = new SupplyM(db.Supplies.GetItem(id));
            if (s.State == SupplStates.Active && DateTime.Now > s.Date_End)
            {
                s.State = SupplStates.ToWriteOff;
                Update(s);
            }

            return s;

        }

        public ObservableCollection<SupplyM> GetList()
        {
            ObservableCollection<SupplyM> ret = new ObservableCollection<SupplyM>();
            foreach (Supply sold in db.Supplies.GetList())
            {
                SupplyM s = new SupplyM(sold);
                if (s.State == SupplStates.Active && DateTime.Now > s.Date_End)
                {
                    s.State = SupplStates.ToWriteOff;
                    Update(s);
                }
                ret.Add(s);
            }

            return ret;
            //return db.Supplies.GetList().Select(i => new SupplyM(i)).ToList();
        }

        public void Update(SupplyM item)
        {
            Supply sup = db.Supplies.GetItem(item.Id);
            sup.ReagentId = item.ReagentId;
            sup.SupplierId = item.SupplierId;
            sup.Date_Begin = item.Date_Begin;
            sup.Date_End = item.Date_End;
            sup.State = (byte)item.State;
            sup.count = item.Count;
            db.Supplies.Update(sup);
            Save();

        }

        public bool Save()
        {
            return db.Save() > 0;
        }
    }
}
