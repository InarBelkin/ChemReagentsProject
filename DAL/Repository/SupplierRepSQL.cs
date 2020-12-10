using DAL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class SupplierRepSQL: IReposAbstract<Supplier>
    {
        public SupplierRepSQL(ChemContext dbcontext) : base(dbcontext.Suppliers, dbcontext)
        {
            db = dbcontext;
        }
    }
}
