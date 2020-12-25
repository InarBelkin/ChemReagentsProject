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
        public ConsumptionCrud(IDbRepos dbRepos):base(dbRepos, dbRepos.Consumptions)
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
            foreach(Supply_consumption s in db.Consumptions.GetList())
            {
                var sc = new ConsumptionM(s);
                ret.Add(sc);
            }
            return ret;
        }
    }
}
