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
    public class ConcentrationCrud: ICrudRepos<ConcentrationM>
    {
        IDbRepos db;

        public ConcentrationCrud(IDbRepos dbRepos)
        {
            db = dbRepos;
        }

        public void Create(ConcentrationM item)
        {
            Concentration c = item.getDal();
            db.Concentrations.Create(c);
            Save();
        }

        public void Delete(int id)
        {
            db.Concentrations.Delete(id);
            Save();
        }

        public ConcentrationM GetItem(int id)
        {
            return new ConcentrationM(db.Concentrations.GetItem(id));
        }

        public ObservableCollection<ConcentrationM> GetList()
        {
            ObservableCollection<ConcentrationM> ret = new ObservableCollection<ConcentrationM>();
            foreach(Concentration c in db.Concentrations.GetList())
            {
                ret.Add(new ConcentrationM(c));
            }
            return ret;
        }

        public void Update(ConcentrationM item)
        {
            Concentration c = db.Concentrations.GetItem(item.Id);
            item.updDal(c);
            db.Concentrations.Update(c);
            Save();
        }

        int Save()
        {
            throw new NotImplementedException();
            //return db.Save();
        }
    }
}
