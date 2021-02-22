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
        decimal GetRemains(int supplId, DateTime DateEnd, bool OnDate = false, int SolutionId = 0);
        void LoadAll();
        void DeleteSolutLines(int SolutId);
    }
}
