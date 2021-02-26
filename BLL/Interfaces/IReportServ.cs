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
        ObservableCollection<SolutionLineM> GetSuppliesForRecipe(int solutId, int recipeId, int concId, DateTime Date, decimal Count);
        /// <summary>
        /// Возвращает остаток в объёме для жидкостей
        /// </summary>
        decimal GetRemains(int supplId, int SolutionId = 0);
        (decimal mas, decimal vol) GetRemainsSW(int suplid, DateTime dateEnd, decimal Count, decimal Density, bool OnDate = false);
        void LoadAll();
        void DeleteSolutLines(int SolutId);
    }
}
