using BLL.Additional;
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

        public Exception Create(ReagentM item)
        {
            db.Reagents.Create(new Reagent()
            {
                Id = item.Id,
                Name = item.Name,
                units = item.Units,
                CostUnit = item.CostUnit
                
            });
            Save();
            var ex1 = Save();
            if(ex1!=null)
            {
                db.Reagents.Delete(item.Id);
                var ex2 = Save();
                if (ex2 != null) return new AddEditExeption("При создании реактива произошла ошибка, которую не удалось исправить.", ex2);
                else return new AddEditExeption("Не удалось создать реактив", ex1);
            }
            return null;
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

        public Exception Update(ReagentM item)
        {
            Reagent r = db.Reagents.GetItem(item.Id);
            ReagentM back = new ReagentM(r);
            r.Id = item.Id;
            r.Name = item.Name;
            r.units = item.Units;
            r.CostUnit = item.CostUnit;

            var ex1 = Save();
            if (ex1 != null)
            {
                back.updDal(r);
                db.Reagents.Update(r);
                var ex2 = Save();
                if (ex2 != null) return new AddEditExeption("При изменении реактива произошла ошибка, которую не удалось исправить.", ex2);
                else return new AddEditExeption("Не удалось изменить реактив", ex1);
            }
            return null;
          
        }

        public Exception Save()
        {
            return db.Save();
        }
    }
}
