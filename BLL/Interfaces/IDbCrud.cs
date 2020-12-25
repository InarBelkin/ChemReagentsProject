using BLL.Models;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDbCrud
    {
        SupplyCrud Supplies { get; }
        ReagentCrud Reagents { get; }
        SupplierCrud Suppliers { get; }
        Solution_RezipeCrud SolutRecipes {get;}
        Solution_Rez_LineCrud SolutRecLines { get; }
        SolutionCrud Solutions { get; }

        SolutionLineCrud SolutLines { get; }
        ConcentrationCrud Concentrations { get; }
    }
}
