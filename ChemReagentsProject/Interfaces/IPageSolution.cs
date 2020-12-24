using BLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagentsProject.Interfaces
{
    interface IPageSolution
    {
        void SetRecipes(ObservableCollection<SolutionRezipeM> recipes);
        void SetConcentrations(ObservableCollection<ConcentrationM> concentrations);
    }
}
