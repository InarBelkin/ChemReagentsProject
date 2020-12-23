using DAL.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IReportRepos
    {
        List<Supply> SupplyByReag(int reagId);
        List<Solution_recipe_line> GetReciepeLine(int ConcentrId);
        List<Concentration> ConcentrbyRecipe(int RecipeId);
    }
}
