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
    public class ConcentrationCrud: ICrudRepos<ConcentrationM>
    {
        IDbRepos db;

        public ConcentrationCrud(IDbRepos dbRepos)
        {
            db = dbRepos;
        }

        public Exception Create(ConcentrationM item)
        {
            Concentration c = item.getDal();
            db.Concentrations.Create(c);
            var ex1 = Save();
            if (ex1 != null)
            {
                db.Concentrations.Delete(c.Id);
                var ex2 = Save();
                if (ex2 != null) return new AddEditExeption("При создании поставки произошла ошибка, которую не удалось исправить.", ex2);
                else return new AddEditExeption("Не удалось создать поставку", ex1);
            }
            else return null;
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

        public Exception Update(ConcentrationM item)
        {
            Concentration c = db.Concentrations.GetItem(item.Id);
            ConcentrationM back = new ConcentrationM(c);
            item.updDal(c);
            db.Concentrations.Update(c);
            var ex1 = Save();
            if (ex1 != null)
            {
                back.updDal(c);
                db.Concentrations.Update(c);
                var ex2 = Save();
                if (ex2 != null) return new AddEditExeption("При изменении концентрации произошла ошибка, которую не удалось исправить.", ex2);
                else return new AddEditExeption("Не удалось изменить концентрацию", ex1);
            }


            return null;
        }

        public Exception Save()
        {

            return db.Save();

        }
    }
}
