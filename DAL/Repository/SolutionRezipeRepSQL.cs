using DAL.Interfaces;
using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class SolutionRezipeRepSQL: IReposAbstract<Solution_recipe>
    {
        public SolutionRezipeRepSQL(ChemContext dbcontext) : base(dbcontext.Solution_Recipes, dbcontext)
        {
           //db = dbcontext;
        }

    }

    public class SolutRezLineRepSQl : IReposAbstract<Solution_recipe_line>
    {
        public SolutRezLineRepSQl(ChemContext dbcontext) : base(dbcontext.Solution_Recipe_Lines, dbcontext)
        {
            //db = dbcontext;
        }
    }
}
