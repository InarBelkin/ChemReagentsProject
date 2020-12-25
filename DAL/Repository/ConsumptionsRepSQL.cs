using DAL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ConsumptionsRepSQL: IReposAbstract<Supply_consumption>
    {
        public ConsumptionsRepSQL(ChemContext dbcontext) : base(dbcontext.Consumptions, dbcontext)
        {
            //db = dbcontext;
        }
    }
}
