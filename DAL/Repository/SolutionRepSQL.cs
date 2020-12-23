using DAL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class SolutionRepSQL : IReposAbstract<Solution>
    {
        public SolutionRepSQL(ChemContext dbcontext) : base(dbcontext.Solutions, dbcontext)
        {
            //db = dbcontext;
        }
    }

    public class SolutionLineRepSQL : IReposAbstract<Solution_line>
    {
        public SolutionLineRepSQL(ChemContext dbcontext) : base(dbcontext.Solution_Lines, dbcontext)
        {
            //db = dbcontext;
        }
    }


}
