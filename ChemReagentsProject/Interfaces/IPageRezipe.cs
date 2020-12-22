using BLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemReagentsProject.Interfaces
{
    interface IPageRezipe
    {
        void SetItemSource(ObservableCollection<ReagentM> PageList);
    }
}
