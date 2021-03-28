using BLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagents.Pages.ReciepePage
{
    interface IPageRecipe
    {
        void SetReagents(ObservableCollection<ReagentM> reagents);
        event EventHandler<ReagentM> ChangeReagent;
        void SetPlusLocation(int Marg);
    }
}
