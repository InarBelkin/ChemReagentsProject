using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDbRepos
    {
        IRepository<Supply> Supplies { get; }
        IRepository<Reagent> Reagents { get; }
        IRepository<Supplier> Suppliers { get; }
        IRepository<Solution_recipe> Solution_Recipes { get; }
        IRepository<Solution_recipe_line> Solution_Rezipe_Line { get; }
        IRepository<Solution> Solutions { get; }
        IRepository<Solution_line> Solution_Lines { get; }
        IRepository<Concentration> Concentrations { get; }
        IReportRepos Reports { get; }
        int Save();
    }
}
