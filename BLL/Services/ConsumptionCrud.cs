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
    public class ConsumptionCrud : IServCrudAbstr<ConsumptionM, Supply_consumption>
    {
        public ConsumptionCrud(IDbRepos dbRepos) : base(dbRepos, dbRepos.Consumptions)
        {

        }

        public override ConsumptionM GetItem(int id)
        {
            var sc = new ConsumptionM(db.Consumptions.GetItem(id));
            return sc;
        }

        public override ObservableCollection<ConsumptionM> GetList()
        {
            ObservableCollection<ConsumptionM> ret = new ObservableCollection<ConsumptionM>();
            foreach (Supply_consumption s in db.Consumptions.GetList())
            {
                var sc = new ConsumptionM(s);
                ret.Add(sc);
            }
            return ret;
        }

        public override Exception Update(ConsumptionM item)
        {
            Supply_consumption sc = db.Consumptions.GetItem(item.Id);
            ConsumptionM back = new ConsumptionM(sc);
            item.updDal(sc);

            var ex1 = Save();
            if (ex1 != null)
            {
                back.updDal(sc);
                db.Consumptions.Update(sc);
                var ex2 = Save();
                if (ex2 != null) return new AddEditExeption("При изменении списания произошла ошибка, которую не удалось исправить.", ex2);
                else return new AddEditExeption("Не удалось изменить списание", ex1);
            }
            return null;
        }

        protected override string GetExString(int num)
        {
            switch (num)
            {
                case 0: return "При создании списания произошла ошибка, которую не удалось исправить.";
                case 1: return "Не удалось создать списание";
            }
            return ("Что-то странное происходит");
        }
    }
}
