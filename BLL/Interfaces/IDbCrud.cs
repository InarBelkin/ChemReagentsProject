using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDbCrud
    {
        ICrudRepos<SupplyM> Supplies { get; }
        ICrudRepos<ReagentM> Reagents { get; }
        ICrudRepos<SupplierM> Suppliers { get; }
        ICrudRepos<SolutionRezipeM> SolutRecipes{get;}
        ICrudRepos<SolutRezLineM> SolutRecLines { get; }
        ICrudRepos<SolutionM> Solutions { get; }
        ICrudRepos<SolutionLineM> SolutLines { get; }
    }
}
