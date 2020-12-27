using BLL.Models;
using BLL.Models.OtherModels;
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
        ObservableCollection<SupplyM> SupplyByReagOnlyActual(int reagid, DateTime NowDate);
        ObservableCollection<SolutRezLineM> GetRecipeLine(int ConcentrId);
        ObservableCollection<ConcentrationM> ConcentrbyRecipe(int RecipeId);
        (List<SupplyStringM>, float Summ) GetSupplyStrings(int SupplId, int SolutLineId=-1);
      //  (List<SupplyStringM>, float Summ) GetSupplyStringsWithoutSolutLine(int SupplId);
        List<MonthReportM> GetMonthReport(DateTime start, DateTime end);
        void AcceptRecipe(int SolutId, DateTime NowDate);
    }
}
