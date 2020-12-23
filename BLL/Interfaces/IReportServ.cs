using BLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IReportServ
    {
        ObservableCollection<SupplyM> SupplyByReag(int reagId);
        ObservableCollection<SolutRezLineM> GetRecipeLine(int ConcentrId);
        ObservableCollection<ConcentrationM> ConcentrbyRecipe(int RecipeId);
    }
}
