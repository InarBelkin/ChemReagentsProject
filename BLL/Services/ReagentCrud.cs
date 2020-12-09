using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BLL.Services
{
    public class ReagentCrud : ICrudRepos<ReagentM>
    {
        IDbRepos db;

        public ReagentCrud(IDbRepos dbRepos)
        {
            db = dbRepos;
        }

        public void Create(ReagentM item)
        {
            db.Reagents.Create(new Reagent()
            {
                Id = item.Id,
                Name = item.Name,
                units = item.Units
            });
            Save();
            
        }

        public void Delete(int id)
        {
            db.Reagents.Delete(id);
            Save();
        }

        public ReagentM GetItem(int id)
        {
            return new ReagentM(db.Reagents.GetItem(id));
        }

        public ObservableCollection<ReagentM> GetList()
        {
            // List<Reagent> l = db.Reagents.GetList();
            ObservableCollection<ReagentM> ret = new ObservableCollection<ReagentM>();
            foreach(Reagent r in db.Reagents.GetList())
            {
                ret.Add(new ReagentM(r));
            }
            return ret;
            //return db.Reagents.GetList().Select(i => new ReagentM(i)).ToList();
        }

        public void Update(ReagentM item)
        {
            Reagent r = db.Reagents.GetItem(item.Id);
            r.Id = item.Id;
            r.Name = item.Name;
            r.units = item.Units;
            Save();
        }

        public bool Save()
        {
            return db.Save() > 0;
        }
    }
}
