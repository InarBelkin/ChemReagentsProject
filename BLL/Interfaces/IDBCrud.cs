using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDBCrud
    {
        ICrudRepos<ReagentM> Reagents { get; }
        ICrudRepos<SupplyM> Supplies { get; }
        ICrudRepos<SupplierM> Suppliers { get; }
        ICrudRepos<RecipeM> Recipes { get; }
        ICrudRepos<ConcentrationM> Concentrations { get; }
        ICrudRepos<RecipeLineM> Recipe_Lines { get; }
        ICrudRepos<SolutionM> Solutions { get; }
        ICrudRepos<SolutionLineM> SolutionLines { get; }
        ICrudRepos<SupplyStingM> SupplConsump { get; }
    }
}
