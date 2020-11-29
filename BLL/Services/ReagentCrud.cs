using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
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

        public List<ReagentM> GetList()
        {
            return db.Reagents.GetList().Select(i => new ReagentM(i)).ToList();
        }

        public void Update(ReagentM item)
        {
            Reagent r = db.Reagents.GetItem(item.Id);
            r.Id = item.Id;
            r.Name = item.Name;
            r.units = item.Units;
        }

        public bool Save()
        {
            return db.Save() > 0;
        }
    }
}
